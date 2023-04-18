using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IPlayerAction.IDamage
{
    public float max_HP = 5;
    private float testval = 5;

    public Image monster_HPImage;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(max_HP < testval)
        {
            testval -= Time.deltaTime; 
        }
        //monster_HPImage.fillAmount = testval / 5;

        if (max_HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
  
    public void Damage(float damage)
    {
        Debug.Log("Player Hit Monster");
        max_HP -= damage;        
    }
}
