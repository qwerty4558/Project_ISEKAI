using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour
{
    [SerializeField] GameObject explosionCol;
    [SerializeField] GameObject particleOBJ;
    [SerializeField] GameObject indicator;
    
    private const float alter_Deley = 1f;

    private void Start()
    {
        explosionCol.SetActive(false);
        indicator.SetActive(false);
        particleOBJ.SetActive(false);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ShowIndicator());
    }

    IEnumerator ShowIndicator()
    {
        yield return new WaitForSeconds(0.2f);
        indicator.SetActive(true);
        yield return new WaitForSeconds(alter_Deley);
        indicator.SetActive(false);

        particleOBJ.SetActive(true);
        explosionCol?.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
