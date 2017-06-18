using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[RequireComponent(typeof(AudioSource))]
[System.Serializable]
public class HarmonyAudioSource : MonoBehaviour {

    [HideInInspector]
    public SoundManager soundManager;

	[HideInInspector]
	public AudioSource m_AudioSource;

	[HideInInspector]
	[SerializeField]
	public bool m_PlaylistMode = false;
	[HideInInspector]
	[SerializeField]
	public bool m_LoopPlaylist=false;
	[HideInInspector]
	[SerializeField]
	public bool m_RandomMode = false;

	public List<AudioClip> m_OffPrintList = new List<AudioClip>();

	public int m_CurrentClipNumber=0;

	[HideInInspector]
	public bool m_IsPlaying=false;

	[HideInInspector]
	public bool m_IsPause=false;

	[SerializeField]
	public List<AudioClip> m_PlayList = new List<AudioClip>();

	[HideInInspector]
	public float m_PlaybackTime;

	// Custom Inspector

	//[HideInInspector]
	public Texture m_LoopTexture;
	[HideInInspector]
	public Texture m_RandomTexture;
	[HideInInspector]
	public Texture m_PlayTexture;
	[HideInInspector]
	public Texture m_StopTexture;
	[HideInInspector]
	public Texture m_PauseTexture;
	[HideInInspector]
	public Texture m_PreviousTexure;
	[HideInInspector]
	public Texture m_NextTexture;

	[HideInInspector]
	public Texture m_LoopTextureSelected;
	[HideInInspector]
	public Texture m_RandomTextureSelected;
	[HideInInspector]
	public Texture m_PlayTextureSelected;
	[HideInInspector]
	public Texture m_StopTextureSelected;
	[HideInInspector]
	public Texture m_PauseTextureSelected;

	[HideInInspector]
	public Texture m_PlusTexture;
	[HideInInspector]
	public Texture m_MinusTexture;

	[HideInInspector]
	public Texture m_UpTexture;
	[HideInInspector]
	public Texture m_DownTexture;
	[HideInInspector]
	public Texture m_DeleteTexture;
	[HideInInspector]
	public Texture m_CleanTexture;

	public Color m_Active;

	// Use this for initialization
	void Start () 
	{
		m_AudioSource = GetComponent<AudioSource>();

		if (m_AudioSource.playOnAwake || m_AudioSource.isPlaying )
        {
			m_IsPlaying = true;
			m_AudioSource.Play();
			StartCoroutine(PlayingLoop());
		}
    }

    public void SetSoundManager(SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

	public void EnabledPlaylistMode( bool _PlaylistMode )
	{
		m_PlaylistMode = _PlaylistMode;
    }

	public void EnabledLoopPlaylist(bool _LoopPlaylist )
	{
		m_LoopPlaylist = _LoopPlaylist;
	}

	public void Play()
	{
		if ( m_AudioSource.isPlaying == false )
		{
			if ( m_RandomMode == true )
			{
				if ( m_OffPrintList.Count == 0 )
				{
					RandomizeTheOffSetList();
					PlayRandomMode();
				}
			}
			else
			{
				m_AudioSource.Play();
			}
		}

		m_IsPlaying = true;
		StartCoroutine(PlayingLoop());
		
	}

	public void Pause()
	{
		if ( m_AudioSource.isPlaying == true )
		{
			m_AudioSource.Pause();
			StopAllCoroutines();
			m_IsPlaying = false;
			m_IsPause = true;
		}
	}

	public void UnPause()
	{
		if ( m_AudioSource.isPlaying == false )
		{
			m_AudioSource.UnPause();
			m_IsPlaying = true;
			StartCoroutine(PlayingLoop());
			m_IsPause = false;
		}

	}

	public void Stop()
	{
		StopAllCoroutines();
		m_AudioSource.Stop();
		m_IsPlaying = false;
		m_IsPause = false;
	}

	public void NextClip()
	{
		if ( m_RandomMode == true )
		{
			if ( m_OffPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				m_IsPlaying = true;
			}
			PlayRandomMode();
		}
		else
		{
			if ( m_CurrentClipNumber + 1 > m_PlayList.Count - 1 )
			{
				m_CurrentClipNumber = 0;
			}
			else
			{
				m_CurrentClipNumber++;
			}

			if ( m_AudioSource.isPlaying )
			{
				m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
				m_AudioSource.Play();
			}
		}
	}

	public void PreviousClip()
	{
		if(m_RandomMode == true)
		{
			if ( m_OffPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				m_IsPlaying = true;
			}
			PlayRandomMode();
		}
		else
		{
			if ( m_CurrentClipNumber - 1 < 0 )
			{
				m_CurrentClipNumber = m_PlayList.Count-1;
			}
			else
			{
				m_CurrentClipNumber--;
			}

			if ( m_AudioSource.isPlaying )
			{
				m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
				m_AudioSource.Play();
			}
		}

	}

	public void SetClip(int _clipIndex)
	{

		if(m_PlayList[_clipIndex]==null)
		{
			Debug.LogError("NULL");
			return;
		}

		if ( m_RandomMode )
		{
			if ( m_OffPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				m_IsPlaying = true;
			}
			
		}
		else
		{
			

			if ( _clipIndex > -1 && _clipIndex < m_PlayList.Count )
			{
                m_CurrentClipNumber = _clipIndex;

				m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];

				if ( m_AudioSource.isPlaying )
				{
					m_AudioSource.Play();
					m_IsPlaying = true;
					m_IsPause = false;
				}
			}
		}
	}

	public void SetRandomClip()
	{
		if ( m_RandomMode )
		{
			if ( m_OffPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				m_IsPlaying = true;
			}
			else
			{
				m_CurrentClipNumber = Random.Range(0, m_OffPrintList.Count + 1);
				PlayRandomMode();
			}
		}
		else
		{
			m_CurrentClipNumber = Random.Range(0, m_PlayList.Count + 1);

			if ( m_AudioSource.isPlaying )
			{
				m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
				m_AudioSource.Play();
			}
		}
	}

	public void SetRandomMode( bool _enable)
	{
		m_RandomMode = _enable;

		if(m_RandomMode == true)
		{
			RandomizeTheOffSetList();
		}

    }

	private void PlayRandomMode()
	{
		m_CurrentClipNumber = Random.Range(0, m_OffPrintList.Count + 1);
		m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
		m_AudioSource.Play();

		m_OffPrintList.RemoveAt(m_CurrentClipNumber);
	}

	private void RandomizeTheOffSetList()
	{
		m_OffPrintList = new List<AudioClip>();

		foreach ( AudioClip _clip in m_PlayList )
		{
			m_OffPrintList.Add(_clip);
		}

		m_OffPrintList = m_OffPrintList.RandomList();
	}

	public List<AudioClip> GetPlaylist()
	{
		return m_PlayList;
	}

	public void AddClip(AudioClip _clip = null)
	{
		m_PlayList.Add(_clip);
	}

	public void SubClip( AudioClip _clip = null )
	{
		if ( _clip != null )
		{
			if ( m_PlayList.Contains(_clip) )
			{
				m_PlayList.Remove(_clip);
			}
		}
		else
		{
			if ( m_PlayList.Count > 1 )
			{
				m_PlayList.RemoveAt(m_PlayList.Count - 1);
			}
		}
	}

	public void DeleteClip( AudioClip _clip)
	{
		if ( m_PlayList.Count == 1 )
		{
			m_PlayList [0] = null;
		}
		else
		{
			if ( m_PlayList.Contains(_clip) )
			{
				m_PlayList.Remove(_clip);
			}
		}
	}

	public void ResetPlaylist()
	{
		m_PlayList.Clear();
		m_PlayList.Add(null);
	}

	public void SwapClip( AudioClip _clip1, AudioClip _clip2 )
	{
		if ( m_PlayList.Contains(_clip1) && m_PlayList.Contains(_clip2) )
		{

			int _position1 = m_PlayList.IndexOf(_clip1);
			int _position2= m_PlayList.IndexOf(_clip2);

			m_PlayList [_position1] = _clip2;

			m_PlayList [_position2] = _clip1;
		}
	}

	public void SwapUpClip(AudioClip _clip)
	{
		if(m_PlayList.Contains(_clip))
		{
			int _positionClip = m_PlayList.IndexOf(_clip);

			if(_positionClip>0)
			{
				SwapClip(_clip, m_PlayList [_positionClip - 1]);
			}

		}
	}

	public void SwapDownClip( AudioClip _clip )
	{
		if ( m_PlayList.Contains(_clip) )
		{
			int _positionClip = m_PlayList.IndexOf(_clip);

			if ( _positionClip < m_PlayList.Count-1 )
			{
				SwapClip(_clip, m_PlayList [_positionClip + 1]);
			}

		}
	}

	IEnumerator PlayingLoop()
	{
		while( m_IsPlaying == true)
		{
			if( m_PlaylistMode )
			{

				if ( !m_AudioSource.isPlaying )
				{

					if ( m_RandomMode == false )
					{

						if ( m_CurrentClipNumber + 1 > m_PlayList.Count - 1 )
						{
							if ( m_LoopPlaylist )
							{
								m_CurrentClipNumber = 0;
								m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
								m_AudioSource.Play();
							}
						}
						else
						{
							Debug.Log("a");
							m_CurrentClipNumber++;
							m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
							m_AudioSource.Play();
						}
					}
					else
					{
						if ( m_OffPrintList.Count <= 0 )
						{
							if ( m_LoopPlaylist )
							{
								RandomizeTheOffSetList();
								PlayRandomMode();
							}
						}
						else
						{
							m_CurrentClipNumber = Random.Range(0, m_OffPrintList.Count + 1);
							m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
							m_AudioSource.Play();

							m_OffPrintList.RemoveAt(m_CurrentClipNumber);
						}

					}
				}
			}
			else
			{
				if(!m_AudioSource.isPlaying)
				{ 
					m_IsPlaying = false;
				}
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	void Update()
	{
		if(m_AudioSource.isPlaying)
		{
			m_PlaybackTime = m_AudioSource.time;
        }
	}

	public void CleanPlaylist()
	{
		for(int i=0; i<m_PlayList.Count; i++)
		{
			if( m_PlayList[i]==null)
			{
				m_PlayList.RemoveAt(i);
				i = 0;
			}
		}

		if(m_PlayList.Count==0)
		{
			m_PlayList.Add(null);
        }

	}

	public void ClearPlaylist( )
	{
		if(m_PlayList.Count>0)
		{
			AudioClip _clip = m_PlayList[0];

			if(_clip == null)
			{
				_clip = m_AudioSource.clip;
			}

			m_PlayList.Clear();

			m_PlayList.Add(_clip);

			m_AudioSource.clip = _clip;

			if ( m_IsPlaying )
			{
				if(m_IsPause)
				{
					m_AudioSource.UnPause();
				}
				else
				{
					m_AudioSource.Play();
				}
				
			}
			else
			{
				Stop();

			}

			

			

		}
		
	}

    /*
	IEnumerator WaitForEndOfSong()
	{

		if(m_PlaylistMode == false)
		{
			
		}
		else
		{
			if ( !m_AudioSource.isPlaying )
			{
				if ( m_CurrentClipNumber + 1 > m_PlayList.Count - 1 )
				{
					if ( m_LoopPlaylist )
					{
						m_CurrentClipNumber = 0;
						m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
						m_AudioSource.Play();
					}
				}
				else
				{
					m_CurrentClipNumber++;
					m_AudioSource.clip = m_PlayList [m_CurrentClipNumber];
					m_AudioSource.Play();
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
	*/

    public void DestroyAfterPlaying()
    {
        StartCoroutine(BurnAfterPlaying());
    }
    
    IEnumerator BurnAfterPlaying()
    {
        while (m_AudioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        soundManager.Source.Remove(m_AudioSource);

        Destroy(gameObject);
    }

}


public static class RandomListExtension
{
	public static List<T> RandomList<T>( this List<T> _list )
	{
		List<int> _ListStraightOrder = new List<int>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			_ListStraightOrder.Add(i);
		}

		List<int> _ListRandomOrder = new List<int>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			int y = Random.Range(0, _ListStraightOrder.Count-1);
			_ListRandomOrder.Add(_ListStraightOrder [y]);

			_ListStraightOrder.RemoveAt(y);
		}

		List<T> _OldList = new List<T>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			_OldList.Add(_list [_ListRandomOrder [i]]);
		}

		_list = _OldList;

		return _list;
	}
}