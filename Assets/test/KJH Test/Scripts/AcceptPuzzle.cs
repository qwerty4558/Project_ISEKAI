using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptPuzzle : MonoBehaviour
{
    [SerializeField] int index;

    private ActiveCore activeCore;
    public Result_Item table_Item;
 

    [SerializeField] bool isPuzzleActive = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        activeCore = FindObjectOfType<ActiveCore>();
    }

    public void OpenCore()
    {
        activeCore.OpenCore(this.gameObject, index);
    }
}
