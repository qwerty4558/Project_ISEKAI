using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGet : MonoBehaviour
{
    public Text ScriptTxt;
    int Ingredient = 0;
    public Image skipLight;
    
    
    void Start()
    {
        ScriptTxt.text = "0";
        
        skipLight.color = new Color(skipLight.color.r, skipLight.color.g, skipLight.color.b, 0);
    }
    public void PlyerIngredientGet()
    {
        if (Ingredient >= 180)
            return;
        Ingredient += 5;
        ScriptTxt.text = Ingredient.ToString();

        CheckCoast();
    }
    public void MonsterIngredientGet()
    {
        if (Ingredient >= 180)
            return;
        Ingredient += 10;
        ScriptTxt.text = Ingredient.ToString();

        CheckCoast();
    }
    public void BossIngredientGet()
    {
        if (Ingredient >= 180)
            return;
        
        Ingredient += 15;
        ScriptTxt.text = Ingredient.ToString();

        CheckCoast();
    } 
    void CheckCoast()
    {
        if(Ingredient >= 180)
        {
            skipLight.color = new Color(skipLight.color.r, skipLight.color.g, skipLight.color.b, 1);
        }
    }
    
}
