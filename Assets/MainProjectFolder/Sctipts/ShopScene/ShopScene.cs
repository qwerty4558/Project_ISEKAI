using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopScene : MonoBehaviour
{
    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _resultPanel;
    public WorkTable workTableSelector;
    private void Awake()
    {
        workTableSelector.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        workTableSelector.Tick();
        Inventory.Instance.ViewItem();
    }
}
