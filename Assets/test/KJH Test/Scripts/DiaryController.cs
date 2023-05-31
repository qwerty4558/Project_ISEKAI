using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BookCurlPro;
using UnityEngine.UI;

public class DiaryController : MonoBehaviour
{
    int bookType;
    int bookPage;
    int infoPage_Diary;
    int infoPage_Recipe;
    [SerializeField] private GameObject _diary;
    [SerializeField] private GameObject _recipe;
    [SerializeField] public GameObject[] diaryPage;
    [SerializeField] public GameObject[] recipePage;
    [SerializeField] public GameObject[] diaryPageInfo;
    [SerializeField] public GameObject[] recipePageInfo;
    [SerializeField] private AutoFlip diaryAuto;
    [SerializeField] private AutoFlip recipeAuto;
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        InitDiary();
    }

    public void InitDiary()
    {
        int diaryPage_Lenght = diaryPage.Length;
        int recipePage_Lenght = recipePage.Length;
        diaryPageInfo = new GameObject[diaryPage.Length];
        recipePageInfo = new GameObject[recipePage.Length];
        infoPage_Diary = diaryPageInfo.Length;
        infoPage_Recipe = recipePageInfo.Length;
        for (int i = 0; i < diaryPage_Lenght; ++i)
        {
            diaryPageInfo[i] = diaryPage[i].transform.GetChild(0).gameObject;
            diaryPageInfo[i].SetActive(false);
        }
        for (int i = 0; i < recipePage_Lenght; ++i)
        {
            recipePageInfo[i] = recipePage[i].transform.GetChild(0).gameObject;
            recipePageInfo[i].SetActive(false);
        }
    }

    public void GetBookType(int book)
    {
        bookType = book;
    }
    public void GetBookPage(int page)
    {
        bookPage = page;
    }
    public void UnLockPage()
    {
        StopAllCoroutines();
        StartCoroutine(OpenBook(bookType, bookPage));
        
    }
    IEnumerator OpenBook(int book, int _page)
    {
        _diary.SetActive(false);
        _recipe.SetActive(false);
        switch (book)
        {
            case 0:
                _diary.SetActive(true);
                if (_page >= 0 && _page < infoPage_Diary)
                {
                    diaryPageInfo[_page].SetActive(true);
                }
                else
                {
                    Debug.LogError("Invalid page index: " + _page);
                }
                GotoPage(book, _page);
                yield return new WaitForSeconds(0.7f);
                for (int i = 0; i < 3; ++i)
                {
                    diaryPageInfo[_page].GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    diaryPageInfo[_page].GetComponent<UnityEngine.UI.Outline>().enabled = false;
                    yield return new WaitForSeconds(0.3f);
                }
                break;
            case 1:
                _recipe.SetActive(true);
                if (_page >= 0 && _page < infoPage_Recipe)
                {
                    recipePageInfo[_page].SetActive(true);
                }
                else
                {
                    Debug.LogError("Invalid page index: " + _page);
                }
                GotoPage(book, _page);
                yield return new WaitForSeconds(0.7f);
                for (int i = 0; i < 3; ++i)
                {
                    recipePageInfo[_page].GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    recipePageInfo[_page].GetComponent<UnityEngine.UI.Outline>().enabled = false;
                    yield return new WaitForSeconds(0.3f);
                }
                break;
        }
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }


    public void GotoPage(int book, int _page)
    {
        switch (book)
        {
            case 0:
                int pageNum = _page;
                if (pageNum < 0) pageNum = 0;
                if (pageNum > diaryAuto.ControledBook.papers.Length * 2) pageNum = diaryAuto.ControledBook.papers.Length * 2 - 1;
                diaryAuto.enabled = true;
                diaryAuto.PageFlipTime = 0.2f;
                diaryAuto.TimeBetweenPages = 0;
                diaryAuto.StartFlipping((pageNum + 1) / 2);
                break;
            case 1:
                int pageNum2 = _page;
                if (pageNum2 < 0) pageNum2 = 0;
                if (pageNum2 > diaryAuto.ControledBook.papers.Length * 2) pageNum2 = diaryAuto.ControledBook.papers.Length * 2 - 1;
                diaryAuto.enabled = true;
                diaryAuto.PageFlipTime = 0.2f;
                diaryAuto.TimeBetweenPages = 0;
                diaryAuto.StartFlipping((pageNum2 + 1) / 2);
                break;
        }
    }


}


