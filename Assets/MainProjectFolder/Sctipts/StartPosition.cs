using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public string startPosition;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        //if(startPosition == )
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
