using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopScene : MonoBehaviour
{
    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _resultPanel;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Inventory.Instance.ViewItem();
    }
}
