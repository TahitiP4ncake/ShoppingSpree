using UnityEngine;
using System.Collections;

public class HarmonyEffectFadeIn : MonoBehaviour {

	[HideInInspector]
	public AudioSource m_AudioSource;

	public AnimationCurve m_Curve;

	
	public void SetCurve()
	{
		m_Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(100, 100));
		/*
		if ( m_AudioSource.clip != null )
		{
			m_Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(_clip.length, 100));
		}*/
	}

}
