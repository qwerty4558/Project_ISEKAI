using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BookCurlPro;
using UnityEngine.UI;

public class DiaryController : MonoBehaviour
{

    [SerializeField] private GameObject _diary;
    [SerializeField] private GameObject _recipe;
    [SerializeField] public GameObject[] diaryPage;
    [SerializeField] public GameObject[] recipePage;
    [SerializeField] private AutoFlip diaryAuto;
    [SerializeField] private AutoFlip recipeAuto;
    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {
        int diaryPage_Lenght = diaryPage.Length;
        int recipePage_Lenght = recipePage.Length;
        for(int i = 0; i < diaryPage_Lenght; ++i)
        {
            diaryPage[i].SetActive(false);
        }
        for(int i = 0; i < recipePage_Lenght; ++i)
        {
            recipePage[i].SetActive(false);
        }
    }

    public void UnLockPage(string keyPair)
    {
        int booktype = int.Parse(keyPair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[0]);
        int toPage = int.Parse(keyPair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[1]);
        StopAllCoroutines();
        StartCoroutine(OpenBook(booktype, toPage));
    }
    IEnumerator OpenBook(int book,int _page)
    {
       
        _diary.SetActive(false);
        _recipe.SetActive(false);
        switch (book)
        {
            case 0:
                _diary.SetActive(true);
                diaryPage[_page].SetActive(true);
                GotoPage(book, _page);
                for (int i = 0; i < 3; ++i)
                {
                    diaryPage[_page].GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    diaryPage[_page].GetComponent<UnityEngine.UI.Outline>().enabled = false;
                    yield return new WaitForSeconds(0.3f);
                }
                
                break;
            case 1:
                _recipe.SetActive(true);
                recipePage[_page].SetActive(true);
                GotoPage(book, _page);
                for (int i = 0; i < 3; ++i)
                {
                    recipePage[_page].GetComponent<UnityEngine.UI.Outline>().enabled = true;
                    yield return new WaitForSeconds(0.3f);
                    recipePage [_page].GetComponent<UnityEngine.UI.Outline>().enabled = false;
                    yield return new WaitForSeconds(0.3f);
                }
                break;
        }
        yield return null;
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


