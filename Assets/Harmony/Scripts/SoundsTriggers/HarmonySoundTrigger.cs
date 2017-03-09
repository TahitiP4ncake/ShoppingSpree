/*	
	 _	  _				
	| |  | |                                       
	| |__| | __ _ _ __ _ __ ___   ___  _ __  _   _ 
	|  __  |/ _` | '__| '_ ` _ \ / _ \| '_ \| | | |
	| |  | | (_| | |  | | | | | | (_) | | | | |_| |
	|_|  |_|\__,_|_|  |_| |_| |_|\___/|_| |_|\__, | 
	                                          __/ |
	                                         |___/   v 0.1


	Present by
	Baptiste Billet

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HarmonyCollisionCategory
{
	All,
	Layers,
	Tags,
	Names,
	None
}

public class HarmonySoundTrigger : MonoBehaviour {

	[Header("Sound Parameters")]

	public bool m_IsDestroyingAfterPlaying;

	[Space(10)]
	[Header("Collision Parameters")]
	
	public HarmonyCollisionCategory m_CollisionCategory;

	public List<string> m_Layers;

	public List<string> m_Tags;

	public List<string> m_Names;

	public bool m_IsWorkingOnce;

	// Other
	private AudioSource m_AudioSource;

	private Collider m_Collider;

	private Mesh m_Mesh;

	// Use this for initialization
	public void Initialize( bool _IsDestroyingAfterPlaying, HarmonyCollisionCategory _CollisionCategory, bool _IsWorkingOnce, AudioSource _AudioSource, Collider _Collider, Mesh _Mesh =null)
	{
		m_IsDestroyingAfterPlaying = _IsDestroyingAfterPlaying;
		m_CollisionCategory = _CollisionCategory;
		m_IsWorkingOnce = _IsWorkingOnce;
		m_AudioSource = _AudioSource;
		m_Collider = _Collider;
		m_Mesh = _Mesh;

		m_AudioSource.playOnAwake = false;
    }

	void Start () 
	{
		m_AudioSource = GetComponent<AudioSource>();
		m_Collider = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider other)
	{
		switch(m_CollisionCategory)
		{
			case HarmonyCollisionCategory.Layers:
				foreach ( string _layer in m_Layers )
				{
					if ( _layer == ( LayerMask.LayerToName(other.gameObject.layer)))
					{
						PlaySound();
						break;
					}
				}
				break;

			case HarmonyCollisionCategory.Tags:
				foreach(string _tag in m_Tags)
				{
					if(_tag == other.tag)
					{
						PlaySound();
						break;
					}
				}
				break;

			case HarmonyCollisionCategory.Names:
				foreach ( string _name in m_Tags )
				{
					if ( _name == other.gameObject.name)
					{
						PlaySound();
						break;
					}
				}
				break;

			case HarmonyCollisionCategory.All:
				PlaySound();
				break;
		}

		if(m_CollisionCategory!=HarmonyCollisionCategory.None && m_IsWorkingOnce )
		{
			m_Collider.enabled = false;
        }


	}

	void PlaySound()
	{
		if(m_AudioSource!=null)
		{
			if(m_AudioSource.clip!=null)
			{
				m_AudioSource.Play();
				if( m_IsDestroyingAfterPlaying )
				{
					m_AudioSource.loop = false;
					StartCoroutine(BurnAfterPlaying());
				}
			}
			else
			{
				Debug.LogError("There is no clip on " + gameObject.name);
			}
		}
		else
		{
			Debug.LogError("There is no audiosource on " + gameObject.name);
		}
	}

	/// <summary>
	/// Destroy the gameobject when the sound end
	/// </summary>
	/// <returns></returns>
	IEnumerator BurnAfterPlaying()
	{
		while(m_AudioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}

}
