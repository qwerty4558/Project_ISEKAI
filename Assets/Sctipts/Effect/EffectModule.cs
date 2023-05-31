using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectModule : MonoBehaviour
{
    [SerializeField] private Effects[] effectItem;
    public Effects[] effects { get => effectItem; }


}

[Serializable]
public class Effects
{
    public string name;
    public ParticleSystem[] particles;

}
