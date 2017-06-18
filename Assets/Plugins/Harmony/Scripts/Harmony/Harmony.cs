/*	
	 _	  _				
	| |  | |                                       
	| |__| | __ _ _ __ _ __ ___   ___  _ __  _   _ 
	|  __  |/ _` | '__| '_ ` _ \ / _ \| '_ \| | | |
	| |  | | (_| | |  | | | | | | (_) | | | | |_| |
	|_|  |_|\__,_|_|  |_| |_| |_|\___/|_| |_|\__, | 
	                                          __/ |
	                                         |___/   



 */

/*! \mainpage ReadMe
*
* \section introduction Introduction
* Harmony is a unity plugin and tool usefull for managing the sounds aspects of a project.
* It was created for the project The Key.
*
*\n
* \section setup SetUp
* Unpack Harmony plugin on your Unity game. 
*\n
* You need to place the "Editor" content from the Harmony plugin into your "Editor" folder if you have already one.
*\n
* You need an “AudioMixers” and an “AudioClips” folder for running Harmony. If you don't have one, running Harmony for the first time will create them.
*
*\n
* \section feditor Features of Harmony in Editor
*
* \subsection actualize Actualize
* The Actualize function actualize the SoundManager gameobject of the Sounds folder with all the sounds of this folder. Alternatively, if there is no Sounds folder or SoundManager, it create them.
*
* \subsection clean Clean
* Clean all the clips or sources of the SoundManager.
*
* \subsection soundtrigger New Sound Trigger
* Create a new sound trigger in the scene. 
* You can define before creating it:
* - If the trigger will be destroyed after playing (can’t loop so). 
* - The type of Collider you want to use (Sphere, Box, Capsule of Mesh).
* - If the collider work only once, for the first correct thing who enter in.
*
* After, you can change directly on the component the size of the collider, all the parameters of the audio source (set the clip by example), and the variables of the Harmony Sound Trigger script.
* You can define the category of elements who can be triggered and the lists to fill it. These lists are public, so you can also access them by script.
*
*\n
* \section fscript Features of Harmony in script
*
* \subsection base Base
* The Base section is usefull for the basic and general functions of Harmony.
* Play, Pause, UnPause, Stop...
*
* \subsection attachreturn Attach and Return
* The Attach and Return functions exist in order to place some sources at some places.
* It can be useful to place some 3D sound sources. The return function bring them back to Harmony. 
*
* \subsection playlist Playlist
* All the sources can be use as playlist in order to change clip easily in runtime and play differents clips, one after the other. The Playlist can’t be fully empty, the playlist would automatically add a unique null clip. 
*
*\n
* \section bugs Bugs and Issues
* You can check all Bugs and Issues in the [Bug List](bug.html) 
*
*\n
* \section patchnote PatchNote
* v0.2 :
* No more overflow functions. No more automatic sources. No more name functions. All Harmony now work with AudioSource component.
*  
*  
*\n
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*! 
 *  \brief     The Harmony class hub of fonctionnalities
 *  \details   This class is use in code by users to manipulate Harmony fonctions
 *  \author    Baptiste Billet, proofreading by Clarisse Blondy
 *  \version   0.1
 *  \date      2016
 *  \pre       For the first use, you should use the actualize button of the Harmony window in Unity to setup
 *  \bug       
 *  \warning   Beta content here, use carrefully
 *  \copyright Baptiste Billet - 2016
*/
public class Harmony : MonoBehaviour
{
	/// @cond DEV
	//SoundManager
	public static SoundManager m_SoundManager;

	#region Delegate and static event

	// Delegates

	

	public delegate void DStringFloat( string _clipName, float _delay = 0 );

	public delegate void DFloat( float _delay = 0 );

	public delegate void DAudioSourceBoolFloat( AudioSource _source, bool _destroyAfterPlaying, float _delay = 0 );

	public delegate void DAudioSourceFloat( AudioSource _source, float _delay = 0 );

	public delegate void AudioSourceTransform( AudioSource _source, Transform _transform );

	public delegate void DAudioSource( AudioSource _source );

	public delegate void DAudioSourceBoolean( AudioSource _source, bool _enabled );

	public delegate void DAudioSourceIntFloat( AudioSource _source, int _clipIndex, float _delay );

	public delegate AudioSource DStringReturnAudioSource( string _name );

	public delegate List<AudioClip> DAudioSourceReturnListOfAudioClip( AudioSource _source );

	public delegate void DAudioSourceClip( AudioSource _source , AudioClip _clip);

	public delegate void DAudioSourceClipClip( AudioSource _source, AudioClip _clip1, AudioClip _clip2 );

	// Base Sound


	public static event DAudioSourceBoolFloat SMPlay;

	public static event DAudioSourceFloat SMPause;

	public static event DAudioSourceFloat SMUnPause;

	public static event DAudioSourceFloat SMStop;

	public static event DAudioSourceFloat SMStopAllBut;

	public static event DFloat SMStopAll;

    public static event DStringReturnAudioSource SMSetSource;

    public static event DAudioSource SMDestroySource;

    // Attach and Return

    public static event AudioSourceTransform SMAttachTo;

	public static event DAudioSource SMReturnToHarmony;

	// Playlist

	public static event DAudioSourceBoolean SMEnabledPlaylist;

	public static event DAudioSourceBoolean SMEnabledPlaylistLoop;

	public static event DAudioSourceBoolean SMEnabledPlaylistRandom;

	public static event DAudioSourceFloat SMPreviousClip;

	public static event DAudioSourceFloat SMNextClip;

	public static event DAudioSourceIntFloat SMSetClipInPlaylist;

	public static event DAudioSourceReturnListOfAudioClip SMGetPlaylist;

	public static event DAudioSourceClip SMAddClip;

	public static event DAudioSourceClip SMSubClip;

	public static event DAudioSourceClip SMDeleteClip;

	public static event DAudioSource SMResetPlaylist;

	public static event DAudioSourceClipClip SMSwapClip;

	public static event DAudioSourceClip SMSwapUpClip;

	public static event DAudioSourceClip SMSwapDownClip;

	public static event DAudioSource SMCleanPlaylist;

    public static event DAudioSource SMClearPlaylist;



    /// @endcond
    #endregion

    // Base

    /// <summary>
	/// Play the source
	/// </summary>
	/// <param name="_source"> The AudioSource of the wanted clip</param>
    /// <param name="_destroyAfterPlaying"> If the source gameobject must destroy itself after playing </param>
    /// <param name="_delay"> Delay before proceed </param>
    public static void Play(AudioSource _source, bool _destroyAfterPlaying = false, float _delay=0)
	{
		if ( SMPlay != null )
		{
			SMPlay(_source, _destroyAfterPlaying, _delay);
		}

	}

    /// <summary>
    /// Pause the source
    /// </summary>
	/// <param name="_source"> The AudioSource of the wanted clip</param>
    /// <param name="_delay"> Delay before proceed </param>
    public static void Pause(AudioSource _source, float _delay = 0 )
	{
		if ( SMPause != null )
		{
			SMPause(_source, _delay);
		}

	}

    /// <summary>
    /// UnPause the source
    /// </summary>
	/// <param name="_source"> The AudioSource of the wanted clip</param>
    /// <param name="_delay"> Delay before proceed </param>
    public static void UnPause(AudioSource _source, float _delay = 0 )
	{
		if ( SMUnPause != null )
		{
			SMUnPause(_source, _delay);
		}

	}

    /// <summary>
    /// Stop playing the source
    /// </summary>
	/// <param name="_source"> The AudioSource of the wanted clip</param>
    /// <param name="_delay"> Delay before proceed </param>
    public static void Stop(AudioSource _source, float _delay = 0 )
	{ 
		if ( SMStop != null )
		{
            SMStop (_source, _delay);
		}

	}

    /// <summary>
    /// Stop all the clip which are playing from the source list
    /// </summary>
    /// <param name="_delay"> Delay before proceed </param>
    public static void StopAll(float _delay = 0 )
	{
		if(SMStopAll!=null)
		{
			SMStopAll(_delay);
		}
	}

    /// <summary>
    /// Stop all the clip which are playing, except the _source
    /// </summary>
	/// <param name="_source"> The AudioSource of the wanted clip</param>
    /// <param name="_delay"> Delay before proceed </param>
    public static void StopAllBut(AudioSource _source, float _delay = 0 )
	{
		if ( SMStopAllBut != null )
		{
			SMStopAllBut(_source, _delay);
		}
	}

    /// <summary>
    /// Set an AudioSource and return it
    /// </summary>
    /// <param name="_clipName">The clip name to research</param>
    /// <returns></returns>
    public static AudioSource SetSource ( string _clipName)
    {
        return m_SoundManager.SMSetSource(_clipName);
    }

    /// <summary>
    /// Destroy the Source
    /// </summary>
    /// <param name="_source"></param>
    public static void DestroySource(AudioSource _source)
    {
        if (SMDestroySource != null)
        {
            SMDestroySource(_source);
        }
    }


    // Attach and Return

    /// <summary>
    /// Make _transform the parent of the gameobject of _source
    /// </summary>
    /// <param name="_source"> The AudioSource </param>
    /// <param name="_transform">The new parent</param>
    public static void AttachTo( AudioSource _source, Transform _transform)
	{
		if ( SMAttachTo != null )
		{
			SMAttachTo(_source, _transform);
		}
	}

	/// <summary>
	/// Make Harmony the new parent of the _source
	/// </summary>
	/// <param name="_source">The source</param>
	public static void ReturnToHarmony( AudioSource _source)
	{
		if ( SMReturnToHarmony != null )
		{
			SMReturnToHarmony(_source);
		}
	}

	// Playlist

	/// <summary>
	/// Enable or disable the PlayList mode of a _source
	/// </summary>
	/// <param name="_source">The source to change</param>
	/// <param name="_enabled">Enable the Playlist mode</param>
	public static void EnabledPlayList( AudioSource _source, bool _enabled)
	{
		if ( SMEnabledPlaylist != null )
		{
			SMEnabledPlaylist(_source, _enabled);
		}
	}

	/// <summary>
	/// Enable or disable the loop PlayList mode of a _source
	/// </summary>
	/// <param name="_source">The source to change</param>
	/// <param name="_enabled">Enable the Playlist Loop mode</param>
	public static void EnabledPlaylistLoop( AudioSource _source, bool _enabled )
	{
		if ( SMEnabledPlaylistLoop != null )
		{
			SMEnabledPlaylistLoop(_source, _enabled);
		}
	}

	/// <summary>
	/// Enable of disable the random mode of the playlist of _source
	/// </summary>
	/// <param name="_source">The source to change</param>
	/// <param name="_enabled">Enable or disable</param>
	public static void EnabledPlaylistRandom( AudioSource _source, bool _enabled )
	{
		if ( SMEnabledPlaylistRandom != null )
		{
			SMEnabledPlaylistRandom(_source, _enabled);
		}
	}

	/// <summary>
	/// Set the next clip in the playlist of _source
	/// </summary>
	/// <param name="_source">The source to set the next clip</param>
	/// <param name="_delay">The action can be delayed</param>
	public static void NextClip( AudioSource _source, float _delay=0)
	{
		if ( SMNextClip != null )
		{
			SMNextClip(_source, _delay);
		}
	}

	/// <summary>
	/// Set the previous clip in the playlist of _source
	/// </summary>
	/// <param name="_source">The source to set the previous clip</param>
	/// <param name="_delay">The action can be delayed</param>
	public static void PreviousClip( AudioSource _source, float _delay = 0 )
	{
		if ( SMPreviousClip != null )
		{
			SMPreviousClip(_source, _delay);
		}
	}

	/// <summary>
	/// Set the _clipIndex clip in the playlist of _source
	/// </summary>
	/// <param name="_source">The source to setclip</param>
	/// <param name="_clipIndex">The index of the clip to set</param>
	/// <param name="_delay">The action can be delayed</param>
	public static void SetClipInPlaylist( AudioSource _source, int _clipIndex, float _delay =0)
	{
		if ( SMSetClipInPlaylist != null )
		{
			SMSetClipInPlaylist(_source, _clipIndex, _delay);
		}
	}

	/// <summary>
	/// Get the Playlist as a list of AudioClip
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clipIndex">The index of the clip to set</param>
	/// <param name="_delay">The action can be delayed</param>
	public static List<AudioClip> GetPlaylist( AudioSource _source)
	{
		return m_SoundManager.SMGetPlaylist(_source);
	}

	/// <summary>
	/// Add the _clip to the Playlist (at the end of)
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip">The clip to add</param>
	public static void AddClip(AudioSource _source, AudioClip _clip = null)
	{
		if ( SMAddClip != null )
		{
			SMAddClip(_source, _clip);
		}
	}

	/// <summary>
	/// Remove the _clip from the Playlist
	/// \note Only the first one found
	/// \note If the _clip is null, remove the last one on the Playlist
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip">The clip to sub</param>
	public static void SubClip( AudioSource _source, AudioClip _clip = null )
	{
		if ( SMSubClip != null )
		{
			SMSubClip(_source, _clip);
		}
	}

	/// <summary>
	/// Remove the _clip from the Playlist
	/// \note Only the first one found
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip">The clip to delete</param>
	public static void DeleteClip( AudioSource _source, AudioClip _clip)
	{
		if ( SMDeleteClip != null )
		{
			SMDeleteClip(_source, _clip);
		}
	}

	/// <summary>
	/// Clear all the Playlist, then add a null clip
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	public static void ResetPlaylist( AudioSource _source)
	{
		if ( SMResetPlaylist != null )
		{
			SMResetPlaylist(_source);
		}
	}

	/// <summary>
	/// Swap the two clip in the playlist list
	/// \note If one of the clip is currently played in the playlist, it will continue playing
	/// \note The next clip to be played will be the next on the list from the ancient index of the current clip
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip1">The first clip to swap</param>
	/// <param name="_clip2">The second clip to swap</param>
	public static void SwapClip( AudioSource _source, AudioClip _clip1, AudioClip _clip2 )
	{
		if ( SMSwapClip != null )
		{
			SMSwapClip(_source, _clip1, _clip2);
		}
	}

	/// <summary>
	/// Swap the _clip with the clip above
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip">The clip to swap</param>
	public static void SwapUpClip( AudioSource _source, AudioClip _clip )

	{
		if ( SMSwapUpClip != null )
		{
			SMSwapUpClip(_source, _clip);
		}
	}

	/// <summary>
	/// Swap the _clip with the clip below
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	/// <param name="_clip">The clip to swap</param>
	public static void SwapDownClip( AudioSource _source, AudioClip _clip )
	{
		if ( SMSwapDownClip != null )
		{
			SMSwapDownClip(_source, _clip);
		}
	}

	/// <summary>
	/// Remove all the clip which are null from the playlist
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	public static void CleanPlaylist( AudioSource _source )
	{
		if ( SMCleanPlaylist != null )
		{
			SMCleanPlaylist(_source);
		}
	}

	/// <summary>
	/// Reset the playlist but keep the first clip from the playlist
	/// </summary>
	/// <param name="_source">The source of the playlist</param>
	public static void ClearPlaylist( AudioSource _source )
	{
		if ( SMClearPlaylist != null )
		{
			SMClearPlaylist(_source);
		}
	}

	void Configure()
	{
		// Base

		SMPlay += (AudioSource _source, bool _destroyAfterPlaying, float _delay ) => { };

		SMPause += (AudioSource _source, float _delay ) => { };

		SMUnPause += (AudioSource _source, float _delay ) => { };

		SMStop += (AudioSource _source, float _delay) => { };

		SMStopAll += ( float _delay ) => { };

		SMStopAllBut += (AudioSource _source, float _delay ) => { };

        // Attach and Return

        SMAttachTo += ( UnityEngine.AudioSource _source, Transform _transform ) => { };

		SMReturnToHarmony += ( UnityEngine.AudioSource _source ) => { };

		// Playlist

		SMEnabledPlaylist += ( UnityEngine.AudioSource _source, bool _enabled ) => { };

		SMEnabledPlaylistLoop += ( UnityEngine.AudioSource _source, bool _enabled ) => { };

		SMEnabledPlaylistRandom += ( UnityEngine.AudioSource _source, bool _enabled ) => { };

		SMNextClip += ( UnityEngine.AudioSource _source, float _delay ) => { };

		SMPreviousClip += ( UnityEngine.AudioSource _source, float _delay ) => { };

		SMSetClipInPlaylist += ( UnityEngine.AudioSource _source, int _clipIndex, float _delay ) => { };

		SMAddClip += ( AudioSource _source, AudioClip _clip ) => { };

		SMSubClip += ( AudioSource _source, AudioClip _clip ) => { };

		SMDeleteClip += ( AudioSource _source, AudioClip _clip ) => { };

		SMResetPlaylist += ( AudioSource _source) => { };

		SMSwapClip += ( AudioSource _source, AudioClip _clip1, AudioClip _clip2 ) => { };

		SMSwapUpClip += ( AudioSource _source, AudioClip _clip ) => { };

		SMSwapDownClip += ( AudioSource _source, AudioClip _clip ) => { };

		SMCleanPlaylist += ( AudioSource _source) => { };

		SMClearPlaylist += ( AudioSource _source) => { };

	}

	#region Singleton
	/// @cond DEV
	static private Harmony s_Instance;
	static public Harmony instance
	/// @endcond
	{
		get
		{
			return s_Instance;
		}
	}


	#endregion

	void Awake( )
	{
		
		if ( s_Instance == null )
			s_Instance = this;
		//DontDestroyOnLoad(this);
		m_SoundManager = GetComponent<SoundManager>();

		Configure();


		
	}
}
