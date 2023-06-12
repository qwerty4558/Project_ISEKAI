using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : SingletonMonoBehaviour<LoadingSceneController>
{
    private bool loadingFlag = false;

    [SerializeField] private GameObject visualGroupObj;
    [SerializeField] private Slider progressBar;
    [SerializeField] private DOTweenAnimation coverAnimation;

    [SerializeField] private Image backgroundImage;

    public Sprite[] backgroundSprite;

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;

        
    }

    public void LoadScene(string sceneName)
    {
        if (loadingFlag) return;

        if (backgroundSprite != null && backgroundSprite.Length > 0)
        {
            int randIndex = Random.Range(0, backgroundSprite.Length);
            backgroundImage.sprite = backgroundSprite[randIndex];
        }
        

        StopAllCoroutines();
        StartCoroutine(Cor_LoadNewScene(sceneName));
    }

    public IEnumerator YieldLoadScene(string sceneName)
    {
        if (loadingFlag) yield return null;

        StopAllCoroutines();
        yield return StartCoroutine(Cor_LoadNewScene(sceneName));
    }

    private IEnumerator Cor_LoadNewScene(string SceneName)
    {
        loadingFlag = true;

        Input.ResetInputAxes();
        var tw = coverAnimation.tween;
        coverAnimation.DORestartById("Loader_Close");
        yield return tw.WaitForCompletion();

        visualGroupObj.SetActive(true);

        float progress = 0f;
        float targetProgress = 0.9f; // 임의로 지정한 90%의 진행도

        while (progress < targetProgress)
        {
            progress += Time.deltaTime; // 시간에 따라 진행도를 증가시킴
            progressBar.value = progress;
            yield return null;
        }

        // 실제 로딩 처리
        var async = SceneManager.LoadSceneAsync(SceneName);

        while (!async.isDone)
        {
            progressBar.value = Mathf.Lerp(targetProgress, 1f, async.progress); // 90% 이후 실제 로딩 진행도 반영
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        visualGroupObj.SetActive(false);
 
        coverAnimation.DORestartById("Loader_Open");
        yield return tw.WaitForCompletion();

        loadingFlag = false;

        
    }
}

