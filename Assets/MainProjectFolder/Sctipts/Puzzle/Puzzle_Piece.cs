using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Piece : MonoBehaviour
{
    public Point pos;
    public Image puzzle_Image;
    public PUZZLE_STATE state; 
    [HideInInspector] public Puzzle_Piece prev;
    [HideInInspector] public int distance;

    private void Start()
    {
        puzzle_Image.gameObject.SetActive(true);
    }

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
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void Load(Point p)
    {
        pos = p;
        Match();
    }
    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.y));
    }
}
