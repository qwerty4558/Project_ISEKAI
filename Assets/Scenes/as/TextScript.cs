using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text ScriptTxt;
    public int Number = 360;
    void Start()
    {
        ScriptTxt.text = "360";
    }
    void Update()
    {
        if (Number <= 0)
        {
            Number = 0; 
        }
        else
        {
            return;
        }
    }

    public void IngredientGet() 
    {
        if (Number == 0) return;
        Number -= 10;
        ScriptTxt.text = Number.ToString();  
    }
    public void MonsterGet()
    {
        if (Number == 0) return;
        Number -= 20;
        ScriptTxt.text = Number.ToString();
    }
    public void BossMonsterGet()
    {
        if (Number == 0) return;
        Number -= 30;
        ScriptTxt.text = Number.ToString();
    }
}
