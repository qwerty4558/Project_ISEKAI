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

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    public static void LoadScene(string sceneName)
    {
        if (Instance.loadingFlag) return;

        Instance.StopAllCoroutines();
        Instance.StartCoroutine(Instance.Cor_LoadNewScene(sceneName));    
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

        var tw = coverAnimation.tween;
        coverAnimation.DORestartById("Loader_Close");
        yield return tw.WaitForCompletion();

        visualGroupObj.SetActive(true);

        var async = SceneManager.LoadSceneAsync(SceneName);

        for (float asy = async.progress; async.progress < 1.0f;)
        {
            progressBar.value = asy;
            yield return asy;
        }

        yield return new WaitForEndOfFrame();

        visualGroupObj.SetActive(false);
        coverAnimation.DORestartById("Loader_Open");
        yield return tw.WaitForCompletion();

        loadingFlag = false;
    }
}
