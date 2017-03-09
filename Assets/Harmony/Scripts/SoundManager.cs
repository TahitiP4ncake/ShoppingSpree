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
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	#region Members

	[Header("All Clips")]
	public List<AudioClip> ListClips =new List<AudioClip>();

	[Header("Sound Listeners")]
	public List<AudioSource> Source = new List<AudioSource>();

	[Header("All the AudioMixers")]
	public List<AudioMixer> AudioMixers = new List<AudioMixer>();

    public AudioMixer CurrentAudioMixer;

	#endregion


	// Use this for initialization
	void Awake( )
	{
		//CreateAudioSource();
		Harmony.SMPlay += SMPlay;
		Harmony.SMPause += SMPause;
		Harmony.SMUnPause += SMUnPause;
		Harmony.SMStop += SMStop;
		Harmony.SMStopAll += SMStopAll;
		Harmony.SMStopAllBut += SMStopAllBut;

        Harmony.SMSetSource += SMSetSource;

        Harmony.SMDestroySource += SMDestroySource;

        Harmony.SMAttachTo += SMAttachTo;
		Harmony.SMReturnToHarmony += SMReturnToHarmony;

		Harmony.SMEnabledPlaylist += SMEnabledPlaylist;
		Harmony.SMEnabledPlaylistLoop += SMEnabledPlaylistLoop;
		Harmony.SMEnabledPlaylistRandom += SMEnabledPlaylistRandom;
		Harmony.SMNextClip += SMNextClip;
		Harmony.SMPreviousClip += SMPreviousClip;
		Harmony.SMSetClipInPlaylist += SMSetClipInPlaylist;
		Harmony.SMGetPlaylist += SMGetPlaylist;

		Harmony.SMAddClip += SMAddClip;
		Harmony.SMSubClip += SMSubClip;
		Harmony.SMDeleteClip += SMDeleteClip;
		Harmony.SMResetPlaylist += SMResetPlaylist;
		Harmony.SMSwapClip += SMSwapClip;
		Harmony.SMSwapUpClip += SMSwapUpClip;
		Harmony.SMSwapDownClip += SMSwapDownClip;
		Harmony.SMCleanPlaylist += SMCleanPlaylist;
		Harmony.SMClearPlaylist += SMClearPlaylist;

}

	void OnDestroy( )
	{
		Harmony.SMPlay -= SMPlay;
		Harmony.SMStop -= SMStop;
		Harmony.SMPause -= SMPause;
		Harmony.SMUnPause -= SMUnPause;
		Harmony.SMStopAll -= SMStopAll;
		Harmony.SMStopAllBut -= SMStopAllBut;

        Harmony.SMSetSource -= SMSetSource;

        Harmony.SMDestroySource -= SMDestroySource;

        Harmony.SMAttachTo -= SMAttachTo;
		Harmony.SMReturnToHarmony -= SMReturnToHarmony;

		Harmony.SMEnabledPlaylist -= SMEnabledPlaylist;
		Harmony.SMEnabledPlaylistLoop -= SMEnabledPlaylistLoop;
		Harmony.SMEnabledPlaylistRandom -= SMEnabledPlaylistRandom;
		Harmony.SMNextClip -= SMNextClip;
		Harmony.SMPreviousClip -= SMPreviousClip;
		Harmony.SMSetClipInPlaylist -= SMSetClipInPlaylist;
		Harmony.SMGetPlaylist -= SMGetPlaylist;
		Harmony.SMAddClip -= SMAddClip;
		Harmony.SMSubClip -= SMSubClip;
		Harmony.SMDeleteClip -= SMDeleteClip;
		Harmony.SMResetPlaylist -= SMResetPlaylist;
		Harmony.SMSwapClip -= SMSwapClip;
		Harmony.SMSwapUpClip -= SMSwapUpClip;
		Harmony.SMSwapDownClip -= SMSwapDownClip;
		Harmony.SMCleanPlaylist -= SMCleanPlaylist;
		Harmony.SMClearPlaylist -= SMClearPlaylist;
	}

	private void CreateAudioSource()
	{
		Source.Clear();	

		foreach(AudioClip _clip in ListClips)
		{
			GameObject _go = new GameObject();
			AudioSource _source;
			_go.AddComponent<AudioSource>();

			_source = _go.GetComponent<AudioSource>();

			_source.playOnAwake = false;

			_source.loop = false;

			_source.clip = _clip;

			_go.name = _clip.name;

			_go.transform.parent = transform;

			Source.Add(_source);
		}
	}

    public AudioSource SMSetSource( string _name)
    {
        AudioClip _clip = IsClipExist(_name);

        if (_clip == null)
        {
            Debug.LogError("THE CLIP " + _name + " DOES'NT EXIST");
            return null;
        }

        GameObject _go = new GameObject();

        _go.transform.parent = transform;

        _go.name = _name;

        _go.AddComponent<AudioSource>();

        AudioSource _source = _go.GetComponent<AudioSource>();

        _go.GetComponent<AudioSource>().playOnAwake = false;

        _source.clip = _clip;

        _go.AddComponent<HarmonyAudioSource>();

        _go.GetComponent<HarmonyAudioSource>().m_PlayList.Add(_clip);

        _go.GetComponent<HarmonyAudioSource>().SetSoundManager(this);

        Source.Add(_source);

        return _source;
    }

	private void SMPlay(AudioSource _source, bool _destroyAfterPlaying=false, float _delay=0)
	{

		if ( _source == null )
		{
			Debug.LogError("THE SOURCE " + _source.name + " DOES'NT EXIST");
			return;
		}

		StartCoroutine(Play(_source, _destroyAfterPlaying, _delay));
	}

	IEnumerator Play(AudioSource _source, bool _destroyAfterPlaying, float _delay)
	{
		yield return new WaitForSeconds(_delay);
		_source.GetComponent<HarmonyAudioSource>().Play();

        if(_destroyAfterPlaying)
        {
            _source.GetComponent<HarmonyAudioSource>().DestroyAfterPlaying();
        }
	}

    private void SMPause(AudioSource _source, float _delay=0)
	{
		if ( _source == null )
		{
            Debug.LogError("THE SOURCE " + _source.name + " DOES'NT EXIST");
            return;
		}

		StartCoroutine(Pause(_source, _delay));
	}

	IEnumerator Pause( AudioSource _source, float _delay )
	{
		yield return new WaitForSeconds(_delay);
		_source.GetComponent<HarmonyAudioSource>().Pause();
	}

	private void SMUnPause( AudioSource _source, float _delay = 0 )
	{
		if ( _source == null )
		{
            Debug.LogError("THE SOURCE " + _source.name + " DOES'NT EXIST");
            return;
		}

		StartCoroutine(UnPause(_source, _delay));
	}

	IEnumerator UnPause( AudioSource _source, float _delay )
	{
		yield return new WaitForSeconds(_delay);
		_source.GetComponent<HarmonyAudioSource>().UnPause();
	}

	IEnumerator Stop(AudioSource _source, float _delay)
	{
		yield return new WaitForSeconds(_delay);
		_source.GetComponent<HarmonyAudioSource>().Stop();
	}

	private void SMStop( AudioSource _source, float _delay=0)
	{
		if ( _source == null )
		{
            Debug.LogError("THE SOURCE " + _source.name + " DOES'NT EXIST");
            return;
		}

		StartCoroutine(CoroutineStop(_source, _delay));

	}

	IEnumerator CoroutineStop(AudioSource _source, float _delay)
	{
		yield return new WaitForSeconds(_delay);
		_source.GetComponent<HarmonyAudioSource>().Stop();
	}

	private void SMStopAll(float _delay=0)
	{
		foreach(AudioSource _source in Source)
		{
			CoroutineStop(_source, _delay);
		}

	}

	private void SMStopAllBut(AudioSource _butSource, float _delay=0)
	{
		if ( _butSource == null )
		{
            Debug.LogError("THE SOURCE " + _butSource.name + " DOES'NT EXIST");
            return;
		}

		foreach ( AudioSource _source in Source )
		{
			if ( _butSource != _source )
			{
				StartCoroutine(CoroutineStop(_source, _delay));
			}
		}

	}

    private void SMDestroySource(AudioSource _source)
    {
        if(_source!=null)
        {
            Source.Remove(_source);

            Destroy(_source.gameObject);
        }
    }


	// Attach and Return

	private void SMAttachTo(AudioSource _source, Transform _tranform)
	{
		if(IsInAList(_source))
		{
			_source.gameObject.transform.parent = _tranform;
			_source.gameObject.transform.localPosition = Vector3.zero;
		}
		else
		{
			Debug.LogError("Can't attach this source "+ _source.name +"to another object from Harmony, the source isn't attach to Harmony");
		}
	}

	private void SMReturnToHarmony( AudioSource _source)
	{
		if ( _source==null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		if (_source.gameObject.transform.parent==transform)
		{
			Debug.LogError("The Source is already on Harmony");
			return;
		}

		_source.gameObject.transform.parent = transform;
		_source.gameObject.transform.localPosition = Vector3.zero;
		
	}

	// Playlist

	private void SMEnabledPlaylist(AudioSource _source, bool _enabled)
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		_source.GetComponent<HarmonyAudioSource>().EnabledPlaylistMode(_enabled);
	}

	private void SMEnabledPlaylistLoop( AudioSource _source, bool _enabled )
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		_source.GetComponent<HarmonyAudioSource>().EnabledLoopPlaylist(_enabled);
	}

	private void SMEnabledPlaylistRandom( AudioSource _source, bool _enabled )
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		_source.GetComponent<HarmonyAudioSource>().SetRandomMode(_enabled);
	}

	private void SMNextClip( AudioSource _source, float _delay = 0 )
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		StartCoroutine(NextOrPreviousClipDelayed(_source, _delay, true));
    }

	private void SMPreviousClip( AudioSource _source, float _delay = 0 )
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		StartCoroutine(NextOrPreviousClipDelayed(_source, _delay, false));
	}

	IEnumerator NextOrPreviousClipDelayed( AudioSource _source, float _delay, bool _add )
	{
		yield return new WaitForSeconds(_delay);

		if(_add)
		{
			_source.GetComponent<HarmonyAudioSource>().NextClip();
		}
		else
		{
			_source.GetComponent<HarmonyAudioSource>().PreviousClip();
		}
	}

	private void SMSetClipInPlaylist( AudioSource _source, int _clipIndex, float _delay = 0 )
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return;
		}

		StartCoroutine(SetClipDelayed(_source, _clipIndex, _delay));

	}

	IEnumerator SetClipDelayed( AudioSource _source, int _clipIndex, float _delay)
	{
		yield return new WaitForSeconds(_delay);

		_source.GetComponent<HarmonyAudioSource>().SetClip(_clipIndex);

	}

	public List<AudioClip> SMGetPlaylist(AudioSource _source)
	{
		if ( _source == null )
		{
			Debug.LogError("The Source is null");
			return null;
		}

		return _source.GetComponent<HarmonyAudioSource>().GetPlaylist();

	}

	public void SMAddClip( AudioSource _source, AudioClip _clip = null )
	{
		if ( _source != null )
		{
			_source.GetComponent<HarmonyAudioSource>().AddClip(_clip);
		}
		else
		{
			Debug.LogError("The Source is null");
		}

	}

	public void SMSubClip( AudioSource _source, AudioClip _clip = null )
	{
		if ( _source != null )
		{
			_source.GetComponent<HarmonyAudioSource>().SubClip(_clip);
		}
		else
		{
			Debug.LogError("The Source is null");
		}

	}

	public void SMDeleteClip( AudioSource _source, AudioClip _clip )
	{
		if ( _source != null && _clip != null )
		{
			_source.GetComponent<HarmonyAudioSource>().DeleteClip(_clip);
		}
		else
		{
			Debug.LogError("The Source or/and the clip is null");
		}
	}

	public void SMResetPlaylist( AudioSource _source )
	{
		if ( _source != null )
		{
			_source.GetComponent<HarmonyAudioSource>().ResetPlaylist();
		}
		else
		{
			Debug.LogError("The Source is null");
		}
	}

	public void SMSwapClip( AudioSource _source, AudioClip _clip1, AudioClip _clip2 )
	{
		if ( _source != null && _clip1 != null && _clip2 != null )
		{
			_source.GetComponent<HarmonyAudioSource>().SwapClip(_clip1, _clip2);
		}
		else
		{
			Debug.LogError("The Source or one of the clip (at least) is null");
		}
	}

	public void SMSwapUpClip( AudioSource _source, AudioClip _clip )

	{
		if ( _source != null && _clip != null )
		{
			_source.GetComponent<HarmonyAudioSource>().SwapUpClip(_clip);
		}
		else
		{
			Debug.LogError("The Source or the clip is null");
		}
	}

	public void SMSwapDownClip( AudioSource _source, AudioClip _clip )
	{
		if ( _source != null && _clip != null)
		{
			_source.GetComponent<HarmonyAudioSource>().SwapDownClip(_clip);
		}
		else
		{
			Debug.LogError("The Source is null");
		}
	}

	public void SMCleanPlaylist( AudioSource _source )
	{
		if ( _source != null )
		{
			_source.GetComponent<HarmonyAudioSource>().CleanPlaylist();
		}
		else
		{
			Debug.LogError("The Source is null");
		}
	}

	public void SMClearPlaylist( AudioSource _source )
	{
		if ( _source != null )
		{
			_source.GetComponent<HarmonyAudioSource>().ClearPlaylist();
		}
		else
		{
			Debug.LogError("The Source is null");
		}
	}



	// Verify function
	private AudioSource IsSourceExist (string _s)
	{
		foreach(AudioSource _source in Source )
		{
			if ( _source.name == _s)
			{
				return _source;
			}
		}
		return null;
	}

	private AudioClip IsClipExist(string _s)
	{
		foreach ( AudioClip _clip in ListClips )
		{
			if ( _clip.name == _s )
			{
				return _clip;
			}
		}
		return null;
	}


	private bool IsInAList(AudioSource _sourceToFind)
	{
		foreach ( AudioSource _source in Source )
		{
			if(_source == _sourceToFind )
			{
				return true;
			}
		}

		return false;
	}


}
