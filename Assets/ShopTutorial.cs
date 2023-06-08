using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopTutorial : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject spaceImage;
    [SerializeField] float getDelay = 1f;
    [SerializeField] bool isNext;

    private int currentPageIndex = 0;

    private void Start()
    {
        spaceImage.SetActive(false);

        for (int i = 0; i < pages.Length; i++)
        {
            for (int j = 0; j < pages[i].transform.childCount; j++)
            {
                pages[i].transform.GetChild(j).gameObject.SetActive(false);
            }
            pages[i].SetActive(false);
        }
        isNext = false;
        StartCoroutine(IDoTutorial(0));
    }

    void OnDisable()
    {
        if (PlayerController.Instance != null)
            PlayerController.SetActivePlayer();
        spaceImage.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            for (int j = 0; j < pages[i].transform.childCount; j++)
            {
                pages[i].transform.GetChild(j).gameObject.SetActive(false);
            }
            pages[i].SetActive(false);
        }
        if (currentPageIndex != 0)
            currentPageIndex = 0;
        isNext = false;
        StartCoroutine(IDoTutorial(0));
    }
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.DeActivePlayer();
            }
            else return;
        }
        if (isNext)
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextPage();
            }
        }
    }

    private void NextPage()
    {
        if (currentPageIndex + 1 < pages.Length)
        {
            currentPageIndex++;
            pages[currentPageIndex - 1].SetActive(false);
            if (currentPageIndex < pages.Length)
            {
                StartCoroutine(IDoTutorial(currentPageIndex));
            }
        }
        else this.gameObject.SetActive(false);
    }

    IEnumerator IDoTutorial(int index)
    {
        spaceImage.SetActive(false);
        isNext = false;
        if (index >= 0 && index < pages.Length)
        {
            pages[index].SetActive(true);
            for (int i = 0; i < pages[index].transform.childCount; ++i)
            {
                pages[index].transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(getDelay);
            }
        }

        spaceImage.SetActive(true);
        isNext = true;
        yield return null;
    }
}
