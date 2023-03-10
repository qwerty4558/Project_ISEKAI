using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countExit : MonoBehaviour
{
    [SerializeField]Customer_count custumer;
    
    void Awake()
    {
        custumer = GetComponent<Customer_count>();
        
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
