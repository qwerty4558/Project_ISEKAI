using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
