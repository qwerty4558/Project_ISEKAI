using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest01 : MonoBehaviour
{
    public GameObject disChest;

    public void DisChest()
    {
        StartCoroutine(Chest_delay());
    }
    IEnumerator Chest_delay()
    {
        yield return new WaitForSeconds(3f);
        StopAllCoroutines();
        Destroy(disChest);
    }
}
