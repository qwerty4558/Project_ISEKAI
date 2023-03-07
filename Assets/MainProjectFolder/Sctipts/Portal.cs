using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("ForestMove"))
        {
            SceneManager.LoadScene("L_forest");
        }

     /*   else if (col.gameObject.tag.Equals("MineMove"))
        {

        }*/
    }

}
