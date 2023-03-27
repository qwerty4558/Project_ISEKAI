using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject content;
    public Point pos;
    [HideInInspector] public Tile prev;
    [HideInInspector] public int distance;

    public Vector3 center
    {
        get
        {
            return new Vector3(pos.x, pos.y, 0);
        }
    }

    private void Match()
    {
        transform.localPosition = new Vector3(pos.x, pos.y, 0);
        transform.localScale = Vector3.one;
    }

    public void Load(Point p)
    {
        pos = p;
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x,(int)v.y));
    }
}
