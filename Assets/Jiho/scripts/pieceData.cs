using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceData : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get => index; set => index = value; }
}
