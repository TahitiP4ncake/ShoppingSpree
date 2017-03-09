using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

[CustomEditor(typeof(HarmonyAudioSource))]
public class HarmonyAudioSourceEditor : Editor {

	HarmonyAudioSource myTarget;
	private float RandomLoopSize = 50;

	private int _timeSeconds;
	private int _timeMinutes;

	private int _timeMaxSeconds;
	private int _timeMaxMinutes;

	//private int PlaylistCount=1;

	Vector2 m_PlaylistScrollPos;

	Color m_Blue = new Color(0.294f,0.486f,0.819f);

    string PathProMode = "Assets/Harmony/HarmonyExtra/ProMode/";
    string PathFreeMode = "Assets/Harmony/HarmonyExtra/FreeMode/";
    string PathMode;

    string PathSelected = "Assets/Harmony/HarmonyExtra/Selected/";

    public override void OnInspectorGUI( )
	{
		// Style
		GUIStyle ClipName = new GUIStyle();
		ClipName.alignment = TextAnchor.MiddleCenter;
        if (HarmonyWindow.IsFreeMode==false)
        {
            ClipName.normal.textColor = Color.white;
        }
        else
        {
            ClipName.normal.textColor = Color.black;
        }

		// Style
		GUIStyle ClipSelected = new GUIStyle();
		ClipSelected.alignment = TextAnchor.MiddleLeft;
		ClipSelected.normal.textColor = m_Blue;

		// Style
		GUIStyle Playlist = new GUIStyle();
		Playlist.alignment = TextAnchor.MiddleCenter;
        if (!HarmonyWindow.IsFreeMode==false)
        {
            Playlist.normal.textColor = Color.white;
        }
        else
        {
            Playlist.normal.textColor = Color.black;
        }

        // Style
        GUIStyle PlaylistSelected = new GUIStyle();
		PlaylistSelected.alignment = TextAnchor.MiddleCenter;
		PlaylistSelected.normal.textColor = m_Blue;

		// The original script
		myTarget = (HarmonyAudioSource) target;

		// The audioSource
		myTarget.m_AudioSource = myTarget.GetComponent<AudioSource>();

		#region Textures

        // Check the free mode
        if(HarmonyWindow.IsFreeMode)
        {
            PathMode = PathFreeMode;
        }
        else
        {
            PathMode = PathProMode;
        }

		myTarget.m_LoopTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Loop.png", typeof(Texture)) );

		myTarget.m_RandomTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Random.png", typeof(Texture)) );

		myTarget.m_PlayTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Play.png", typeof(Texture)) );

		myTarget.m_StopTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Stop.png", typeof(Texture)) );

		myTarget.m_PreviousTexure = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Previous.png", typeof(Texture)) );

		myTarget.m_NextTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Next.png", typeof(Texture)) );

		myTarget.m_PauseTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Pause.png", typeof(Texture)) );

		myTarget.m_LoopTextureSelected = ( (Texture) AssetDatabase.LoadAssetAtPath(PathSelected + "LoopSelected.png", typeof(Texture)) );

		myTarget.m_RandomTextureSelected = ( (Texture) AssetDatabase.LoadAssetAtPath(PathSelected + "RandomSelected.png", typeof(Texture)) );

		myTarget.m_PlayTextureSelected = ( (Texture) AssetDatabase.LoadAssetAtPath(PathSelected + "PlaySelected.png", typeof(Texture)) );

		myTarget.m_StopTextureSelected = ( (Texture) AssetDatabase.LoadAssetAtPath(PathSelected + "StopSelected.png", typeof(Texture)) );

		myTarget.m_PauseTextureSelected = ( (Texture) AssetDatabase.LoadAssetAtPath(PathSelected + "PauseSelected.png", typeof(Texture)) );

		myTarget.m_PlusTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Plus.png", typeof(Texture)) );

		myTarget.m_MinusTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Minus.png", typeof(Texture)) );

		myTarget.m_UpTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Up.png", typeof(Texture)) );

		myTarget.m_DownTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Down.png", typeof(Texture)) );

		myTarget.m_DeleteTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Delete.png", typeof(Texture)) );

		myTarget.m_CleanTexture = ( (Texture) AssetDatabase.LoadAssetAtPath(PathMode + "Clean.png", typeof(Texture)) );

		#endregion

		#region Timers and clip name
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		//Time actual
		string _timeAudio="00:00:00";

		if ( myTarget.m_PlaybackTime >= 3600 )
		{
			TimeSpan _time = TimeSpan.FromSeconds(myTarget.m_PlaybackTime);
			_timeAudio = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
				_time.Hours,
				_time.Minutes,
				_time.Seconds,
				_time.Milliseconds);
		}
		else
		{
			TimeSpan _time = TimeSpan.FromSeconds(myTarget.m_PlaybackTime);
			_timeAudio = string.Format("{0:D2}:{1:D2}:{2:D2}",
				_time.Minutes,
				_time.Seconds,
				_time.Milliseconds);
		}

		EditorGUILayout.LabelField(_timeAudio,GUILayout.Width(80));

        // Name
        if (myTarget.m_AudioSource.clip != null)
        {
            EditorGUILayout.LabelField(myTarget.m_AudioSource.clip.name, ClipName);
        }
        else
        {
            if (myTarget.m_PlayList.Count > 0)
            {
                if (myTarget.m_PlayList[0]!=null)
                {
                    EditorGUILayout.LabelField(myTarget.m_PlayList[0].name, ClipName);
                }
                else
                {
                    EditorGUILayout.LabelField(myTarget.gameObject.name, ClipName);
                }
            }
            else
            {
                EditorGUILayout.LabelField(myTarget.gameObject.name, ClipName);
            }
            
        }

		//Time Max
		string _timeMaxAudio="00:00:00";

        if (myTarget.m_AudioSource.clip != null)
        {
            if (myTarget.m_AudioSource.clip.length >= 3600)
            {
                TimeSpan _time = TimeSpan.FromSeconds(myTarget.m_AudioSource.clip.length);
                _timeMaxAudio = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                    _time.Hours,
                    _time.Minutes,
                    _time.Seconds,
                    _time.Milliseconds);
            }
            else
            {
                TimeSpan _time = TimeSpan.FromSeconds(myTarget.m_AudioSource.clip.length);
                _timeMaxAudio = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    _time.Minutes,
                    _time.Seconds,
                    _time.Milliseconds);
            }
        }

		EditorGUILayout.LabelField(_timeMaxAudio,GUILayout.Width(80));

		EditorGUILayout.EndHorizontal();
		#endregion

		#region ProgressBar
		EditorGUILayout.BeginHorizontal();
        if (myTarget.m_AudioSource.clip != null)
        {
            float _max = 1000;
            float _actu = (_max * myTarget.m_PlaybackTime) / myTarget.m_AudioSource.clip.length;

            GUILayout.HorizontalSlider(_actu, 0, _max, GUILayout.Width(EditorGUIUtility.currentViewWidth - 35));
        }
		EditorGUILayout.EndHorizontal();
		#endregion ProgressBar

		#region Loop
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		if ( myTarget.m_LoopPlaylist == true )
		{

			if ( GUILayout.Button(myTarget.m_LoopTextureSelected, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.EnabledLoopPlaylist(false);
			}
		}
		else
		{
			if ( GUILayout.Button(myTarget.m_LoopTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.EnabledLoopPlaylist(true);
			}
		}
		#endregion

		#region LectureParameters

		// Previous
		if ( GUILayout.Button(myTarget.m_PreviousTexure, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.PreviousClip();
		}

		//Play / Pause
		if ( myTarget.m_IsPlaying )
		{
			if ( myTarget.m_IsPause == false )
			{
				if ( GUILayout.Button(myTarget.m_PauseTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Pause();
				}
			}
			else
			{
				if ( GUILayout.Button(myTarget.m_PlayTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Play();
				}
			}
		}
		else
		{
			if ( myTarget.m_IsPause == false )
			{
				if ( GUILayout.Button(myTarget.m_PlayTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Play();
				}
			}
			else
			{
				if ( GUILayout.Button(myTarget.m_PlayTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.UnPause();
				}
			}
		}


		// Stop
		if ( GUILayout.Button(myTarget.m_StopTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.Stop();
		}

		// Next
		if ( GUILayout.Button(myTarget.m_NextTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.NextClip();
		}
		#endregion

		#region Random
		if ( myTarget.m_RandomMode == true )
		{
			if ( GUILayout.Button(myTarget.m_RandomTextureSelected, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.SetRandomMode(false);
			}
		}
		else
		{
			if ( GUILayout.Button(myTarget.m_RandomTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.SetRandomMode(true);
			}
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndHorizontal();

		#endregion

		#region Playlist list Button

		EditorGUILayout.BeginHorizontal();
		if ( myTarget.m_PlaylistMode == true )
		{
			if ( GUILayout.Button("Playlist", PlaylistSelected, GUILayout.Width(60), GUILayout.Height(20)) )
			{
				myTarget.EnabledPlaylistMode(false);
			}
		}
		else
		{
			if ( GUILayout.Button("Playlist", Playlist, GUILayout.Width(60), GUILayout.Height(20)) )
			{
				myTarget.EnabledPlaylistMode(true);
			}
		}

		EditorGUILayout.Space();
        if ( GUILayout.Button(myTarget.m_PlusTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.AddClip();
		}
		if ( GUILayout.Button(myTarget.m_MinusTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.SubClip();
		}
		if ( GUILayout.Button(myTarget.m_CleanTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.CleanPlaylist();
		}
		if ( GUILayout.Button(myTarget.m_DeleteTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.ResetPlaylist();
		}


		EditorGUILayout.EndHorizontal();

		#endregion

		#region Clips

		EditorGUILayout.Space();

		// Scroll
		if ( myTarget.m_PlayList.Count > 10 )
		{
			m_PlaylistScrollPos = EditorGUILayout.BeginScrollView(m_PlaylistScrollPos, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(220));
		}
		else
		{
			m_PlaylistScrollPos = EditorGUILayout.BeginScrollView(m_PlaylistScrollPos, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(myTarget.m_PlayList.Count*22));
		}

		// Playlist clips
		int _size = (int)(Mathf.Log10(myTarget.m_PlayList.Count-1));

		for (int i=0; i<myTarget.m_PlayList.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(10+10*_size), GUILayout.Height(20));

			#region ButtonsBefore
			if ( myTarget.m_PlayList[i] == myTarget.m_AudioSource.clip)
			{
				if (myTarget.m_IsPlaying)
				{
					if ( GUILayout.Button(myTarget.m_PauseTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
					{
						myTarget.Pause();
					}
				}
				else
				{
					if ( myTarget.m_IsPause )
					{
						if ( GUILayout.Button(myTarget.m_PlayTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
						{
							myTarget.UnPause();
						}
					}
					else
					{
						if ( GUILayout.Button(myTarget.m_PlayTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
						{
							myTarget.Play();
						}
					}
				}
			}	   
			else
			{
				if ( GUILayout.Button(myTarget.m_PlayTexture, GUILayout.Width(20), GUILayout.Height(20)) )
				{
					myTarget.Stop();

					myTarget.SetClip(i);
					
					myTarget.Play();
					
				}
			}
			#endregion

			//The clip
			if(myTarget.m_PlayList[i] == myTarget.m_AudioSource.clip)
			{
				myTarget.m_PlayList [i] = (AudioClip) EditorGUILayout.ObjectField(myTarget.m_PlayList [i], typeof(AudioClip), true, GUILayout.Width(EditorGUIUtility.currentViewWidth-155), GUILayout.Height(20));
			}
			else
			{
				myTarget.m_PlayList [i] = (AudioClip) EditorGUILayout.ObjectField(myTarget.m_PlayList [i], typeof(AudioClip), true, GUILayout.Width(EditorGUIUtility.currentViewWidth-155), GUILayout.Height(20));
			}

			#region ButtonAfter
			if ( GUILayout.Button(myTarget.m_UpTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.SwapUpClip(myTarget.m_PlayList [i]);
            }

			if ( GUILayout.Button(myTarget.m_DownTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.SwapDownClip(myTarget.m_PlayList [i]);
			}

			if ( GUILayout.Button(myTarget.m_DeleteTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.DeleteClip(myTarget.m_PlayList[i]);
			}
			#endregion

			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();

		#endregion

		#region Effects
		EditorGUILayout.Space(); EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();

		// EditorGUILayout.LabelField("Effects",ClipName);

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		#endregion


		EditorUtility.SetDirty(myTarget);
    }


}

[CustomEditor(typeof(HarmonyEffectFadeIn))]
public class HarmonyEffectFadeInEditor : Editor
{

	HarmonyEffectFadeIn myTarget;

	AudioClip _clip=null;

	float _currentValuePourcentage;
	float _currentValueTime;

	public void Enable()
	{
		myTarget.SetCurve();
	}

	public override void OnInspectorGUI( )
	{
		GUIStyle Value = new GUIStyle();
		Value.alignment = TextAnchor.MiddleCenter;
        if (!HarmonyWindow.IsFreeMode)
        {
            Value.normal.textColor = Color.white;
        }
        else
        {
            Value.normal.textColor = Color.black;
        }

		myTarget = (HarmonyEffectFadeIn) target;

		myTarget.m_AudioSource = myTarget.GetComponent<AudioSource>();

		if ( myTarget.m_AudioSource.clip != null )
		{
			/*
			if ( _clip != myTarget.m_AudioSource.clip )
			{
				_clip = myTarget.m_AudioSource.clip;
				myTarget.SetCurve(_clip);
			}*/



			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("",GUILayout.Width(80));
			EditorGUILayout.LabelField("Time", Value, GUILayout.Width(80));
			EditorGUILayout.LabelField("Pourcentage", Value, GUILayout.Width(80));
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Min",Value, GUILayout.Width(40));
			EditorGUILayout.LabelField("00:00", Value, GUILayout.Width(40));
			EditorGUILayout.LabelField("0%", Value, GUILayout.Width(40));
			EditorGUILayout.EndVertical();

			/*
			string _timeAudio="00:00:00";
			TimeSpan _time = TimeSpan.FromSeconds(myTarget.m_AudioSource.time);
			_timeAudio = string.Format("{0:D2}:{1:D2}",
				_time.Minutes,
				_time.Seconds);
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Now", Value, GUILayout.Width(40));
			EditorGUILayout.LabelField(_timeAudio, Value, GUILayout.Width(40));
			EditorGUILayout.LabelField("0%", Value, GUILayout.Width(40));
			EditorGUILayout.EndVertical();
			*/

			string _timeMaxAudio="00:00:00";
			TimeSpan _timemax = TimeSpan.FromSeconds(myTarget.m_AudioSource.clip.length);
			_timeMaxAudio = string.Format("{0:D2}:{1:D2}",
				_timemax.Minutes,
				_timemax.Seconds);

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Max",Value, GUILayout.Width(60));
			EditorGUILayout.LabelField(_timeMaxAudio, Value, GUILayout.Width(60));
			EditorGUILayout.LabelField("100%", Value, GUILayout.Width(60));
			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			myTarget.m_Curve = EditorGUILayout.CurveField(myTarget.m_Curve);

		}

		


		EditorUtility.SetDirty(myTarget);
	}
}