using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BookCurlPro;
using UnityEngine.UI;

public class DiaryController : MonoBehaviour
{
    [SerializeField] private int bookType;
    [SerializeField] private int bookPage;
    private int infoPage_Diary;
    private int infoPage_Recipe;
    [SerializeField] private GameObject _diary;
    [SerializeField] private GameObject _recipe;
    [SerializeField] public GameObject[] diaryPage;
    [SerializeField] public GameObject[] recipePage;
    [SerializeField] public GameObject[] diaryPageInfo;
    [SerializeField] public GameObject[] recipePageInfo;
    [SerializeField] private AutoFlip diaryAuto;
    [SerializeField] private AutoFlip recipeAuto;
    [SerializeField] UnityEngine.UI.Outline diaryOutline;
    public bool isDiaryPageActive = false;
    public bool isRecipePageActive = false;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        InitDiary();
    }
    void Start()
    {
        
    }

    private void OnDisable()
    {
        CursorManage.instance.HideMouse();
    }

    private void Update()
    {
        if(isDiaryPageActive)
        StartCoroutine(TabIconBlink());

        
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
            diaryPageInfo[i] = FindPageInfoWithTag(diaryPage[i], "PageInfo");
            diaryPageInfo[i].SetActive(false);
        }
        for (int i = 0; i < recipePage_Lenght; ++i)
        {
            recipePageInfo[i] = FindPageInfoWithTag(recipePage[i], "PageInfo");
            recipePageInfo[i].SetActive(false);
        }
    }

    private GameObject FindPageInfoWithTag(GameObject parent, string _tag)
    {
        for(int i = 0; i < parent.transform.childCount; ++i)
        {
            Transform child = parent.transform.GetChild(i);
            if(child.CompareTag(_tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void GetBookInfomation(string _bookInfo)
    {
        bookType = int.Parse(_bookInfo.Split(',')[0]);
        bookPage = int.Parse(_bookInfo.Split(",")[1]);

        UIManager.Instance.checkingDiary = false;

        if (bookType == 0)
        {
            isDiaryPageActive = true;
            
        }
        else if(bookType == 1)
        {
            isRecipePageActive = true;
        }
    }

    public void OffBlink()
    {
        if (isDiaryPageActive)
        {
            isDiaryPageActive = false;
            GotoPage(bookType, bookPage);
            StopCoroutine(TabIconBlink());
        }
    }

    public void UnLockPage()
    {
        if(isDiaryPageActive)
        {
            
            StartCoroutine(OpenBook(0, bookPage-1));
            
        }

        else if (isRecipePageActive)
        {
            
            StartCoroutine(OpenBook(1, bookPage-1));
        }
    }




    IEnumerator OpenBook(int book, int _page)
    {
        switch (book)
        {
            case 0:
                if (_page >= 0 && _page < infoPage_Recipe)
                {
                    diaryPageInfo[_page].SetActive(true);
                }
                else
                {
                    Debug.LogError("Invalid page index: " + _page);
                }
                yield return new WaitForSeconds(0.7f);
                break;
            case 1:
                isRecipePageActive = false;
                if (_page >= 0 && _page < infoPage_Diary)
                {
                    recipePageInfo[_page].SetActive(true);
                }
                else
                {
                    Debug.LogError("Invalid page index: " + _page);
                }
                if (!_recipe.activeSelf)
                {
                    _recipe.SetActive(true);
                    GotoPage(book, _page);
                }
                else GotoPage(book, _page);
                
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
    }

    IEnumerator TabIconBlink()
    {
        while (isDiaryPageActive)
        {
            float duration = .5f;
            float elapsedTime = 0f;

            Color startColor = new Color(0, 0, 0, 0);
            Color endColor = new Color(0, 0, 0, 1);

            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration;
                diaryOutline.effectColor = Color.Lerp(startColor, endColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration;
                diaryOutline.effectColor = Color.Lerp(endColor, startColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
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
                if (pageNum2 > recipeAuto.ControledBook.papers.Length * 2) pageNum2 = recipeAuto.ControledBook.papers.Length * 2 - 1;
                recipeAuto.enabled = true;
                recipeAuto.PageFlipTime = 0.2f;
                recipeAuto.TimeBetweenPages = 0;
                recipeAuto.StartFlipping((pageNum2 + 1) / 2);
                break;
        }
    }
}


