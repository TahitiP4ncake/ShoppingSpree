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
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Audio;


public class HarmonyWindow : EditorWindow
{
    // Global Window
    static int tab;

    // Windows
    #region Windows

    [MenuItem("Harmony/General")]
    public static void ShowWindowGeneral()
    {
        tab = 0;
        HarmonyWindow window = (HarmonyWindow)EditorWindow.GetWindow(typeof(HarmonyWindow));
        window.Show();
    }

    [MenuItem("Harmony/Triggers")]
    public static void ShowWindowTriggers()
    {
        tab = 1;
        HarmonyWindow window = (HarmonyWindow)EditorWindow.GetWindow(typeof(HarmonyWindow));
        window.Show();
    }

    [MenuItem("Harmony/Options")]
    public static void ShowWindowOptions()
    {
        tab = 2;
        HarmonyWindow window = (HarmonyWindow)EditorWindow.GetWindow(typeof(HarmonyWindow));
        window.Show();
    }



    #endregion

    // General
    #region General

    // Stock here, all the directories in child from Sounds
    static private string[] AudioClipsDirectories;
    static private string[] AudioMixerDirectories;

    static private List<string> Categories = new List<string>();

    // The SoundManager prefab
    static public GameObject SoundManager;

    static public SoundManager SoundManagerScript;

    static private Harmony HarmonyScript;

    static private HarmonyAudioSource HarmonyAudioSourceScript;

    // The current AudioMixer
    static private AudioMixer AudioMixer;

    // To verify that all the folders and files are in the right place
    static bool Verify()
    {
        // Verify if the directory AudioClips exist
        if (!Directory.Exists("Assets/AudioClips/"))
        {
            Debug.LogError("NO AudioClips DIRECTORY FOUND");
            HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }

        // Get all the directories of the AudioClips folder
        AudioClipsDirectories = Directory.GetDirectories("Assets/AudioClips/");

        // Verify if the directory AudioMixers exist
        if (!Directory.Exists("Assets/AudioMixers/"))
        {
            Debug.LogError("NO AUDIOMIXERS DIRECTORY FOUND");
            //HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }


        // Get all the categories as Directories names
        foreach (string _s in AudioClipsDirectories)
        {
            Categories.Add(_s.Remove(0, 18));
        }

        // Verify if the prefab SoundManager exist
        if (!File.Exists("Assets/AudioClips/SoundManager.prefab"))
        {
            Debug.LogError("NO SOUNDMANAGER FOUND");

            HarmonyEditorPopUpSoundManager.ShowPopUp();

            return false;
        }
        else
        {
            SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
        }

        SoundManagerScript = SoundManager.GetComponent<SoundManager>();

        if (SoundManagerScript == null)
        {
            SoundManager.AddComponent<SoundManager>();
        }

        HarmonyScript = SoundManager.GetComponent<Harmony>();

        if (HarmonyScript == null)
        {
            SoundManager.AddComponent<Harmony>();
        }

        // Verify if there are categories
        if (Categories.Count == 0)
        {
            Debug.LogError("NO CATEGORIES FOUND IN HARMONY");
            return false;
        }






        return true;
    }

    // The main process
    public static void ActualizeHarmonyProcess()
    {
        // Do all the verification
        if (Verify() == false)
        {
            return;
        }

        Assign();

    }

    // If all are in place, set all clips 
    static void Assign()
    {
        // For each folder
        foreach (string _category in AudioClipsDirectories)
        {
            // Get all the files of the folder
            string[] Files = Directory.GetFiles(_category);

            if (Files.Length > 0)
            {
                AddClipsToSoundManager(Files);
            }

        }
    }

    static void AddClipsToSoundManager(string[] Files)
    {
        // For each files 
        foreach (string _s in Files)
        {

            AudioClip _Clip = ((AudioClip)AssetDatabase.LoadAssetAtPath(_s, typeof(AudioClip)));

            if (_Clip != null)
            {

                // If the clip is not in the librairy yet
                if (!IsThisClipUnknowed(_Clip))
                {
                    //Add to list
                    SoundManagerScript.ListClips.Add(_Clip);

                    // If nomenclature, change the name
                    /*
					if ( HarmonyNomenclatures.GetUseNomenclature() )
					{
						_name = UseNomenclatureOnString(SoundManagerScript.ListClips[SoundManagerScript.ListClips.Count-1].name);

						
						SoundManagerScript.ListClips [SoundManagerScript.ListClips.Count-1].name = _name;

					}
					*/

                }
            }
        }
    }

    /*
	static string UseNomenclatureOnString(string _name)
	{
		// Stock  the name in another variable // Safety
		string _returnNamePrefix = _name;
		string _returnNameSuffix = _name;
		// Made a part string to check for prefix
		string part="";

		
		// Prefix
		foreach (string _prefix in HarmonyNomenclatures.GetPrefixList())
		{
			_returnNamePrefix = _name;
			//Get the prefix of the name
			part = _returnNamePrefix.Substring(0, _prefix.Length);

			if (part== _prefix)
			{
				_returnNamePrefix = _returnNamePrefix.Remove(0, _prefix.Length);
				break;
			}
		}

		Debug.Log(_returnNamePrefix);
		_returnNameSuffix = _returnNamePrefix;
		// Suffix
		foreach ( string _suffix in HarmonyNomenclatures.GetSuffixList() )
		{
			//Get the prefix of the name
			_returnNameSuffix = _returnNamePrefix;
			Debug.Log(_returnNameSuffix.Length - _suffix.Length + " ! " + _suffix.Length);

			/*
			for(int i=1; i< _suffix.Length; i++)
			{
				part = _returnName.Substring(_returnName.Length - _suffix.Length, _suffix.Length);
			}
			

			part = _returnNameSuffix.Substring(_returnNameSuffix.Length - _suffix.Length, _suffix.Length);

			//part = _returnName.Substring(start, _returnName.Length);

			if ( part == _suffix )
			{
				Debug.Log(part);
				_returnNameSuffix = _returnNameSuffix.Remove(_returnNameSuffix.Length - _suffix.Length, _suffix.Length);
				Debug.Log(_returnNameSuffix);
				break;
			}
		}
		Debug.Log(_returnNameSuffix);

		return _returnNameSuffix;
	}
	*/

    // Verify if this clip is unknowed
    static bool IsThisClipUnknowed(AudioClip _newClip)
    {
        foreach (AudioClip _Clip in SoundManagerScript.ListClips)
        {
            if (_newClip == _Clip)
            {
                foreach (AudioSource _source in SoundManagerScript.Source)
                {
                    if (_source.clip == _newClip)
                    {
                        _source.clip = _Clip;
                    }
                }
                return true;
            }
        }

        return false;

    }

    static public void CreateAudioSource(GameObject GO_SoundManger)
    {
        foreach (AudioClip _clip in SoundManagerScript.ListClips)
        {

            if (!IsSourceKnowed(_clip))
            {

                GameObject _go = new GameObject();
                AudioSource _source;
                _go.AddComponent<AudioSource>();
                _go.AddComponent<HarmonyAudioSource>();

                _source = _go.GetComponent<AudioSource>();

                _source.playOnAwake = false;

                _source.loop = false;

                _source.clip = _clip;

                _go.GetComponent<HarmonyAudioSource>().m_PlayList.Add(_clip);

                _go.name = _clip.name;

                _go.transform.parent = GO_SoundManger.transform;

                GO_SoundManger.GetComponent<SoundManager>().Source.Add(_source);
            }
        }
    }

    static public bool IsSourceKnowed(AudioClip _clip)
    {
        foreach (AudioSource _Source in SoundManagerScript.Source)
        {
            if (_Source.clip == _clip)
            {
                return true;
            }
        }

        return false;
    }

    void OnGUIGeneral()
    {
        if (GUILayout.Button("Actualize the Librairy"))
        {
            ActualizeHarmonyProcess();
        }

        if (GUILayout.Button("Clean Sources"))
        {
            CleanSources();
        }

        if (GUILayout.Button("Clean Clips"))
        {
            CleanClips();
        }
    }

    public static void CleanSources()
    {

        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().Source.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);

            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanSources();
            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanSources();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }
    }

    Object obj;

    //[MenuItem("Harmony/Clean/Clips")]
    public static void CleanClips()
    {
        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().ListClips.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);
            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanClips();

            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanClips();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }

    }

    #endregion

    // Triggers work
    #region Triggers

    public bool m_IsDestroyingAfterPlaying = false;

    public HarmonyCollisionCategory m_CollisionCategory;

    public List<string> m_Layers = new List<string>();

    public List<string> m_Tags = new List<string>();

    public List<string> m_Names = new List<string>();

    public bool m_IsWorkingOnce = false;

    private AudioSource m_AudioSource;

    public enum ColliderCategory
    {
        Sphere,
        Cube,
        Capsule,
        Mesh
    }

    private ColliderCategory m_ColliderCategory;

    private Collider m_Collider;

    private Mesh m_Mesh;

    // The SoundManager prefab
    public GameObject SoundTrigger;

    public HarmonySoundTrigger m_HarmonySoundTrigger;

    private void CreateHarmonyTrigger()
    {
        // Base
        SoundTrigger = new GameObject();

        SoundTrigger.name = "HarmonySoundTrigger";

        // Add Components
        SoundTrigger.AddComponent<HarmonySoundTrigger>();

        SoundTrigger.AddComponent<AudioSource>();

        SoundTrigger.AddComponent<HarmonyAudioSource>();

        switch (m_ColliderCategory)
        {
            case ColliderCategory.Capsule:
                SoundTrigger.AddComponent<CapsuleCollider>();
                break;

            case ColliderCategory.Cube:
                SoundTrigger.AddComponent<BoxCollider>();
                break;

            case ColliderCategory.Sphere:
                SoundTrigger.AddComponent<SphereCollider>();
                break;

            case ColliderCategory.Mesh:
                SoundTrigger.AddComponent<MeshCollider>();
                break;
        }

        // Get the Collider
        m_Collider = SoundTrigger.GetComponent<Collider>();

        m_Collider.isTrigger = true;

        // For Mesh Collider Only
        if (m_ColliderCategory == ColliderCategory.Mesh && m_Mesh != null)
        {
            m_Collider = SoundTrigger.GetComponent<MeshCollider>();
            SoundTrigger.GetComponent<MeshCollider>().sharedMesh = m_Mesh;
            SoundTrigger.GetComponent<MeshCollider>().convex = true;
            m_Collider.isTrigger = true;
        }

        // Get the Script
        m_HarmonySoundTrigger = SoundTrigger.GetComponent<HarmonySoundTrigger>();

        m_HarmonySoundTrigger.Initialize(m_IsDestroyingAfterPlaying, m_CollisionCategory, m_IsWorkingOnce, SoundTrigger.GetComponent<AudioSource>(), m_Collider, m_Mesh);
    }

    public void OnGUITriggers()
    {
        EditorGUILayout.LabelField("Create New Sound Trigger");

        EditorGUILayout.Space();

        m_IsDestroyingAfterPlaying = EditorGUILayout.Toggle("Destroy after playing ?", m_IsDestroyingAfterPlaying);

        EditorGUILayout.Space();

        m_ColliderCategory = (ColliderCategory)EditorGUILayout.EnumPopup("Type of Collider:", m_ColliderCategory);

        if (m_ColliderCategory == ColliderCategory.Mesh)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            m_Mesh = (Mesh)EditorGUILayout.ObjectField(m_Mesh, typeof(Mesh), true);
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();

        m_IsWorkingOnce = EditorGUILayout.Toggle("Collider work only once ?", m_IsWorkingOnce);

        EditorGUILayout.Space();

        Rect r = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(r, GUIContent.none))
        {
            CreateHarmonyTrigger();
        }
        GUILayout.Label("Validate");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }

    #endregion

    // Options
    #region Options

    public static bool IsFreeMode;

    public void OnGUIOptions()
    {
        
    }
    #endregion

    void Start()
    {
        IsFreeMode = !UnityEditorInternal.InternalEditorUtility.HasPro();
    }

    // GUI
    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, new string[] { "General", "Triggers", "Options" });

        switch (tab)
        {
            case 0:
                OnGUIGeneral();
                break;

            case 1:
                OnGUITriggers();
                break;

            case 2:
                OnGUIOptions();
                break;
        }
    }

}


// Popup Work
public class HarmonyEditorPopupFolderSounds : EditorWindow
{
    public static void ShowPopUp()
    {
        HarmonyEditorPopupFolderSounds window = ScriptableObject.CreateInstance<HarmonyEditorPopupFolderSounds>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("There is no AudioClips folder in your project, do you want us to create one ?", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        if (GUILayout.Button("Agree!"))
        {
            string guid = AssetDatabase.CreateFolder("Assets", "AudioClips");
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

            HarmonyEditor.HarmonyProcess();

            this.Close();
        }

        if (GUILayout.Button("No"))
        {
            this.Close();
        }

    }
}

public class HarmonyEditorPopUpSoundManager : EditorWindow
{
    public static void ShowPopUp()
    {
        HarmonyEditorPopUpSoundManager window = ScriptableObject.CreateInstance<HarmonyEditorPopUpSoundManager>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("There is no SoundManager Prefab in your Assets/AudioClips folder, do you want us to create one ?", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        if (GUILayout.Button("Agree!"))
        {
            GameObject SoundManager = new GameObject();

            SoundManager.name = "SoundManager";

            SoundManager.AddComponent<Harmony>();
            SoundManager.AddComponent<SoundManager>();

            PrefabUtility.CreatePrefab("Assets/AudioClips/SoundManager.prefab", SoundManager);

            DestroyImmediate(SoundManager);

            HarmonyEditor.HarmonyProcess();

            this.Close();
        }

        if (GUILayout.Button("No"))
        {
            this.Close();
        }

    }
}


// To rework
public class HarmonyAudioMixerEditor : EditorWindow
{

    //[MenuItem("Harmony/AudioMixer")]
    public static void ShowWindow()
    {
        HarmonyAudioMixerEditor window = (HarmonyAudioMixerEditor)EditorWindow.GetWindow(typeof(HarmonyAudioMixerEditor));
        window.Show();
    }

    void OnGUI()
    {

    }


}

/*
public class HarmonyNomenclatures : EditorWindow
{

	[MenuItem("Harmony/Nomenclatures")]
	public static void ShowWindow( )
	{
		HarmonyNomenclatures window = (HarmonyNomenclatures)EditorWindow.GetWindow(typeof(HarmonyNomenclatures));
		window.Show();
	}

	// Members
	[SerializeField]
	public static bool m_UseNomenclatures;

	[SerializeField]
	public static List<string> m_Prefix = new List<string>();
	[SerializeField]
	public static List<string> m_Suffix = new List<string>();

	Vector2 m_PosScrollList;

	void OnGUI( )
	{
		EditorGUILayout.LabelField("Create New Sound Trigger");

		EditorGUILayout.Space();

		m_UseNomenclatures = EditorGUILayout.Toggle("Use Nomenclature ?", m_UseNomenclatures);

		EditorGUILayout.Space();

		if ( m_UseNomenclatures )
		{
			EditorGUILayout.Space();

			#region Button
			// Add Buttons
			EditorGUILayout.BeginHorizontal();

				Rect pre = EditorGUILayout.BeginHorizontal ("Button");
				if ( GUI.Button(pre, GUIContent.none) )
				{
					AddPrefix(true,true);
				}
				GUILayout.Label("+ Prefix");
				EditorGUILayout.EndHorizontal();

				Rect suppre = EditorGUILayout.BeginHorizontal ("Button");
				if ( GUI.Button(suppre, GUIContent.none) )
				{
					AddPrefix(true, false);
				}
				GUILayout.Label("- Prefix");
				EditorGUILayout.EndHorizontal();

				Rect suf = EditorGUILayout.BeginHorizontal ("Button");
				if ( GUI.Button(suf, GUIContent.none) )
				{
					AddPrefix(false,true);
				}
				GUILayout.Label("+ Suffix");
				EditorGUILayout.EndHorizontal();

				Rect supsuf = EditorGUILayout.BeginHorizontal ("Button");
				if ( GUI.Button(supsuf, GUIContent.none) )
				{
					AddPrefix(false, false);
				}
				GUILayout.Label("- Suffix");
				EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndHorizontal();

			#endregion

			// List

			// Scroll

			#region Scroll
			int _maxCount;

			if(m_Prefix.Count>=m_Suffix.Count)
			{
				_maxCount = m_Prefix.Count;
            }
			else
			{
				_maxCount = m_Suffix.Count;
			}

			if ( _maxCount > 10 )
			{
				m_PosScrollList = EditorGUILayout.BeginScrollView(m_PosScrollList, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(220));
			}
			else
			{
				m_PosScrollList = EditorGUILayout.BeginScrollView(m_PosScrollList, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(_maxCount * 22));
			}

			#endregion

			EditorGUILayout.BeginHorizontal();

				EditorGUILayout.BeginVertical();
				if ( m_Prefix.Count > 0 )
				{
				for ( int i = 0; i < m_Prefix.Count; i++ )
					{
						if ( m_Prefix.Count > 0 )
						{
						m_Prefix [i] = EditorGUILayout.TextField("Prefix : ", m_Prefix [i]);
						}
					}
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical();
				if ( m_Suffix.Count > 0 )
				{
					for ( int i = 0; i < m_Suffix.Count; i++ )
					{
						if ( m_Suffix.Count > 0 )
						{
						m_Suffix [i] = EditorGUILayout.TextField("Suffix : ", m_Suffix [i]);
						}
					}
				}
				EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndScrollView();

			EditorGUILayout.Space();
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		Rect r = EditorGUILayout.BeginHorizontal ("Button");
		if ( GUI.Button(r, GUIContent.none) )
		{
			Validate();
		}
		GUILayout.Label("Validate");
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();
	}

	private void AddPrefix( bool _prefix, bool _add )
	{
		if ( _prefix )
		{
			if ( _add )
			{
				m_Prefix.Add("");
			}
			else
			{
				if ( m_Prefix.Count > 0 )
				{
					m_Prefix.RemoveAt(m_Prefix.Count-1);
				}
			}
		}
		else
		{
			if ( _add )
			{
				m_Suffix.Add("");
			}
			else
			{
				if ( m_Suffix.Count > 0 )
				{

					m_Suffix.RemoveAt(m_Prefix.Count-1);
				}
			}
		}
	}


	private void Validate()
	{
		EditorUtility.SetDirty(this);
	}

	public static bool GetUseNomenclature()
	{
		return m_UseNomenclatures;
    }

	public static List<string> GetPrefixList( )
	{
		return m_Prefix;
	}

	public static List<string> GetSuffixList( )
	{
		return m_Suffix;
	}


}
*/


//OLD
public class HarmonyEditorTrigger : EditorWindow
{

    public bool m_IsDestroyingAfterPlaying = false;

    public HarmonyCollisionCategory m_CollisionCategory;

    public List<string> m_Layers = new List<string>();

    public List<string> m_Tags = new List<string>();

    public List<string> m_Names = new List<string>();

    public bool m_IsWorkingOnce = false;

    private AudioSource m_AudioSource;

    public enum ColliderCategory
    {
        Sphere,
        Cube,
        Capsule,
        Mesh
    }

    private ColliderCategory m_ColliderCategory;

    private Collider m_Collider;

    private Mesh m_Mesh;

    //[MenuItem("Harmony/New Sound Trigger")]
    public static void ShowWindow()
    {
        HarmonyEditorTrigger window = (HarmonyEditorTrigger)EditorWindow.GetWindow(typeof(HarmonyEditorTrigger));
        window.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.LabelField("Create New Sound Trigger");

        EditorGUILayout.Space();

        m_IsDestroyingAfterPlaying = EditorGUILayout.Toggle("Destroy after playing ?", m_IsDestroyingAfterPlaying);

        EditorGUILayout.Space();

        m_ColliderCategory = (ColliderCategory)EditorGUILayout.EnumPopup("Type of Collider:", m_ColliderCategory);

        if (m_ColliderCategory == ColliderCategory.Mesh)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            m_Mesh = (Mesh)EditorGUILayout.ObjectField(m_Mesh, typeof(Mesh), true);
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();

        m_IsWorkingOnce = EditorGUILayout.Toggle("Collider work only once ?", m_IsWorkingOnce);

        EditorGUILayout.Space();

        Rect r = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(r, GUIContent.none))
        {
            CreateHarmonyTrigger();
        }
        GUILayout.Label("Validate");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }


    // The SoundManager prefab
    public GameObject SoundTrigger;

    public HarmonySoundTrigger m_HarmonySoundTrigger;


    // The current AudioMixer
    private AudioMixer AudioMixer;

    private void CreateHarmonyTrigger()
    {
        // Base
        SoundTrigger = new GameObject();

        SoundTrigger.name = "HarmonySoundTrigger";

        // Add Components
        SoundTrigger.AddComponent<HarmonySoundTrigger>();

        SoundTrigger.AddComponent<AudioSource>();

        SoundTrigger.AddComponent<HarmonyAudioSource>();

        switch (m_ColliderCategory)
        {
            case ColliderCategory.Capsule:
                SoundTrigger.AddComponent<CapsuleCollider>();
                break;

            case ColliderCategory.Cube:
                SoundTrigger.AddComponent<BoxCollider>();
                break;

            case ColliderCategory.Sphere:
                SoundTrigger.AddComponent<SphereCollider>();
                break;

            case ColliderCategory.Mesh:
                SoundTrigger.AddComponent<MeshCollider>();
                break;
        }

        // Get the Collider
        m_Collider = SoundTrigger.GetComponent<Collider>();

        m_Collider.isTrigger = true;

        // For Mesh Collider Only
        if (m_ColliderCategory == ColliderCategory.Mesh && m_Mesh != null)
        {
            m_Collider = SoundTrigger.GetComponent<MeshCollider>();
            SoundTrigger.GetComponent<MeshCollider>().sharedMesh = m_Mesh;
            SoundTrigger.GetComponent<MeshCollider>().convex = true;
            m_Collider.isTrigger = true;
        }

        // Get the Script
        m_HarmonySoundTrigger = SoundTrigger.GetComponent<HarmonySoundTrigger>();

        m_HarmonySoundTrigger.Initialize(m_IsDestroyingAfterPlaying, m_CollisionCategory, m_IsWorkingOnce, SoundTrigger.GetComponent<AudioSource>(), m_Collider, m_Mesh);
    }

}

public class HarmonyCleanSourcesFromSoundManager : EditorWindow
{
    //[MenuItem("Harmony/Clean/Sources")]
    public static void CleanSources()
    {

        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().Source.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);

            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanSources();
            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanSources();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }
    }

}

public class HarmonyCleanClipsFromSoundManager : EditorWindow
{
    Object obj;

    //[MenuItem("Harmony/Clean/Clips")]
    public static void CleanClips()
    {
        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().ListClips.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);
            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanClips();

            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanClips();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }

    }


}

public class HarmonyEditor : EditorWindow
{

    //[MenuItem("Harmony/Actualize")]
    public static void ShowWindow()
    {
        HarmonyProcess();

    }


    // Stock here, all the directories in child from Sounds
    static private string[] AudioClipsDirectories;
    static private string[] AudioMixerDirectories;

    static private List<string> Categories = new List<string>();

    // The SoundManager prefab
    static public GameObject SoundManager;

    static public SoundManager SoundManagerScript;

    static private Harmony HarmonyScript;

    static private HarmonyAudioSource HarmonyAudioSourceScript;

    // The current AudioMixer
    static private AudioMixer AudioMixer;

    // To verify that all the folders and files are in the right place
    static bool Verify()
    {
        // Verify if the directory AudioClips exist
        if (!Directory.Exists("Assets/AudioClips/"))
        {
            Debug.LogError("NO AudioClips DIRECTORY FOUND");
            HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }

        // Get all the directories of the AudioClips folder
        AudioClipsDirectories = Directory.GetDirectories("Assets/AudioClips/");

        // Verify if the directory AudioMixers exist
        if (!Directory.Exists("Assets/AudioMixers/"))
        {
            Debug.LogError("NO AUDIOMIXERS DIRECTORY FOUND");
            //HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }


        // Get all the categories as Directories names
        foreach (string _s in AudioClipsDirectories)
        {
            Categories.Add(_s.Remove(0, 18));
        }

        // Verify if the prefab SoundManager exist
        if (!File.Exists("Assets/AudioClips/SoundManager.prefab"))
        {
            Debug.LogError("NO SOUNDMANAGER FOUND");

            HarmonyEditorPopUpSoundManager.ShowPopUp();

            return false;
        }
        else
        {
            SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
        }

        SoundManagerScript = SoundManager.GetComponent<SoundManager>();

        if (SoundManagerScript == null)
        {
            SoundManager.AddComponent<SoundManager>();
        }

        HarmonyScript = SoundManager.GetComponent<Harmony>();

        if (HarmonyScript == null)
        {
            SoundManager.AddComponent<Harmony>();
        }

        // Verify if there are categories
        if (Categories.Count == 0)
        {
            Debug.LogError("NO CATEGORIES FOUND IN HARMONY");
            return false;
        }






        return true;
    }

    // The main process
    public static void HarmonyProcess()
    {
        // Do all the verification
        if (Verify() == false)
        {
            return;
        }

        Assign();

        GenerateSources();

    }

    // If all are in place, set all clips 
    static void Assign()
    {
        // For each folder
        foreach (string _category in AudioClipsDirectories)
        {
            // Get all the files of the folder
            string[] Files = Directory.GetFiles(_category);

            if (Files.Length > 0)
            {
                AddClipsToSoundManager(Files);
            }

        }
    }

    static void AddClipsToSoundManager(string[] Files)
    {

        // For each files 
        foreach (string _s in Files)
        {

            AudioClip _Clip = ((AudioClip)AssetDatabase.LoadAssetAtPath(_s, typeof(AudioClip)));

            if (_Clip != null)
            {

                // If the clip is not in the librairy yet
                if (!IsThisClipUnknowed(_Clip))
                {
                    //Add to list
                    SoundManagerScript.ListClips.Add(_Clip);

                    // If nomenclature, change the name
                    /*
					if ( HarmonyNomenclatures.GetUseNomenclature() )
					{
						_name = UseNomenclatureOnString(SoundManagerScript.ListClips[SoundManagerScript.ListClips.Count-1].name);

						
						SoundManagerScript.ListClips [SoundManagerScript.ListClips.Count-1].name = _name;

					}
					*/

                }
            }
        }
    }

    /*
	static string UseNomenclatureOnString(string _name)
	{
		// Stock  the name in another variable // Safety
		string _returnNamePrefix = _name;
		string _returnNameSuffix = _name;
		// Made a part string to check for prefix
		string part="";

		
		// Prefix
		foreach (string _prefix in HarmonyNomenclatures.GetPrefixList())
		{
			_returnNamePrefix = _name;
			//Get the prefix of the name
			part = _returnNamePrefix.Substring(0, _prefix.Length);

			if (part== _prefix)
			{
				_returnNamePrefix = _returnNamePrefix.Remove(0, _prefix.Length);
				break;
			}
		}

		Debug.Log(_returnNamePrefix);
		_returnNameSuffix = _returnNamePrefix;
		// Suffix
		foreach ( string _suffix in HarmonyNomenclatures.GetSuffixList() )
		{
			//Get the prefix of the name
			_returnNameSuffix = _returnNamePrefix;
			Debug.Log(_returnNameSuffix.Length - _suffix.Length + " ! " + _suffix.Length);

			/*
			for(int i=1; i< _suffix.Length; i++)
			{
				part = _returnName.Substring(_returnName.Length - _suffix.Length, _suffix.Length);
			}
			

			part = _returnNameSuffix.Substring(_returnNameSuffix.Length - _suffix.Length, _suffix.Length);

			//part = _returnName.Substring(start, _returnName.Length);

			if ( part == _suffix )
			{
				Debug.Log(part);
				_returnNameSuffix = _returnNameSuffix.Remove(_returnNameSuffix.Length - _suffix.Length, _suffix.Length);
				Debug.Log(_returnNameSuffix);
				break;
			}
		}
		Debug.Log(_returnNameSuffix);

		return _returnNameSuffix;
	}
	*/

    // Verify if this clip is unknowed
    static bool IsThisClipUnknowed(AudioClip _newClip)
    {
        foreach (AudioClip _Clip in SoundManagerScript.ListClips)
        {
            if (_newClip == _Clip)
            {
                foreach (AudioSource _source in SoundManagerScript.Source)
                {
                    if (_source.clip == _newClip)
                    {
                        _source.clip = _Clip;
                    }
                }
                return true;
            }
        }

        return false;

    }

    static void GenerateSources()
    {
        GameObject GO_SoundManager;
        GO_SoundManager = Instantiate(SoundManager);

        //SoundManager GO_SoundManagerScript = GO_SoundManager.GetComponent<SoundManager>();

        CreateAudioSource(GO_SoundManager);


        PrefabUtility.ReplacePrefab(GO_SoundManager, SoundManager, ReplacePrefabOptions.ConnectToPrefab);
        DestroyImmediate(GO_SoundManager);
    }

    static public void CreateAudioSource(GameObject GO_SoundManger)
    {
        foreach (AudioClip _clip in SoundManagerScript.ListClips)
        {

            if (!IsSourceKnowed(_clip))
            {

                GameObject _go = new GameObject();
                AudioSource _source;
                _go.AddComponent<AudioSource>();
                _go.AddComponent<HarmonyAudioSource>();

                _source = _go.GetComponent<AudioSource>();

                _source.playOnAwake = false;

                _source.loop = false;

                _source.clip = _clip;

                _go.GetComponent<HarmonyAudioSource>().m_PlayList.Add(_clip);

                _go.name = _clip.name;

                _go.transform.parent = GO_SoundManger.transform;

                GO_SoundManger.GetComponent<SoundManager>().Source.Add(_source);
            }
        }
    }

    static public bool IsSourceKnowed(AudioClip _clip)
    {
        foreach (AudioSource _Source in SoundManagerScript.Source)
        {
            if (_Source.clip == _clip)
            {
                return true;
            }
        }

        return false;
    }


}

