using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private List<GameObject> tileprefabs;

    public void Visualize(Ingredient_Item result)
    {
        foreach(GameObject t in tileprefabs)
        {

        }

        tileprefabs = new List<GameObject>();

        for(int y = 0; y < result.puzzle.GetLength(0); y++)
        {
            for(int x = 0; x < result.puzzle.GetLength(1); x++)
            {
                GameObject newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector3(x * 100f,y*100f);
            }
        }
    }
}
