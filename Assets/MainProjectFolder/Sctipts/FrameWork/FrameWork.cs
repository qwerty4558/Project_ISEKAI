using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class FrameWork : MonoBehaviour
{

    static private FrameWork _instance = null;

    static public FrameWork Instance => _instance;

    [System.NonSerialized] public UIManager uiManager = null;


    [Header("reference")]
    public AudioSource audioSource;
    public TextAsset[] templateRefs;
    public UIManager uiManagerPrefab;


    private void Awake()
    {
        _instance = this;

        DataTableManager.InitTable(templateRefs);

        uiManager = Instantiate(uiManagerPrefab);
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("Title", new LoadSceneParameters
            {
                loadSceneMode = LoadSceneMode.Additive,
                localPhysicsMode = LocalPhysicsMode.None
            });
        }
    }
    private void OnDestroy()
    {
        DataTableManager.ForceDestroy();
        _instance = null;
    }
}
