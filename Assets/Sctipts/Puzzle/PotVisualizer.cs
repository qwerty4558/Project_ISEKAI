using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Color positive;
    [SerializeField] private Color negative;

    private List<GameObject> tileprefabs;


    private void Awake()
    {
        tileprefabs = new List<GameObject>();
    }

    public void Visualize(Vector2 position, Ingredient_Item result, bool isPositive)
    {
        if (tileprefabs != null)
        {
            foreach (GameObject t in tileprefabs)
            {
                Destroy(t);
            }
        }

        tileprefabs = new List<GameObject>();

        GetComponent<RectTransform>().localPosition = position;

        for(int y = 0; y < result.puzzle.GetLength(0); y++)
        {
            for(int x = 0; x < result.puzzle.GetLength(1); x++)
            {
                if (result.puzzle[x, y] == PUZZLE_PIECE.PIECE || result.puzzle[x, y] == PUZZLE_PIECE.END)
                {
                    GameObject newTile = Instantiate(tilePrefab, transform);
                    newTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x - result.puzzle.GetLength(0)/2) * 100f, -(y - result.puzzle.GetLength(1) / 2) * 100f);
                    tileprefabs.Add(newTile);
                }
            }
        }

        foreach (GameObject t in tileprefabs)
        {
            if (isPositive)
            {
                t.GetComponent<Image>().color = positive;
            }
            else
            {
                t.GetComponent<Image>().color = negative;
            }
        }
    }
}
