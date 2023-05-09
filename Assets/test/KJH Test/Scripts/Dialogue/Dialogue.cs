using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public string name;
    public string[] context;
    public string expression;
}

public enum FacialExpression
{
    nomal,
    smile,
    sad,
    qurious,
    suprise1, 
    suprise2,
    worried,
    furious
}