using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (IsFrameworkSceneReady() == false)
        {
            StartCoroutine(LoadFrameworkAsync());
        }
    }

    //protected IEnumerator Start()
    //{
    //    yield break;
    //}

    IEnumerator LoadFrameworkAsync()
    {
        //  * 안티 패턴처럼 보이지만 사실 많은 방법중에 하나. 유니티에서는 '시스템' 을 만드는게 생각보다 어렵다.

        //  * 방법적으로는: 씬을 만들던지, 일반적인 c# 싱글턴(mono가 아닌)을 쓰던지, SriptableObject 를 쓰던지,
        //    시점으로는: 진입점(=타이틀) 에서 미리 로딩을 하던지, 온-디맨드(필요할 때) 생성하던지.. 해야하고
        //    위치는: 로컬디스크 / Resources 폴더 / StreamingAssets / 에셋번들(모바일-패치데이터) 이던지..
        //  * 이 사항들을 적절히 조합해서 개인(팀)의 취향에 의해 결정된다.

        //  * 씬의 비동기 로딩은, "해당 프레임" 에 완료되지 않는다.
        //    즉, 관련데이터를 이용하려면 적어도 준비되었는지 체크가 필요하다.

        Scene frameworkScene = SceneManager.LoadScene("Framework", new LoadSceneParameters
        {
            loadSceneMode = LoadSceneMode.Additive,
            localPhysicsMode = LocalPhysicsMode.None
        });

        while (frameworkScene.isLoaded == false)
        {
            yield return null;
        }

        Debug.Assert(IsFrameworkSceneReady() == true);

        //  * GC 에 쫄지말자, 이 코루틴이나, 이 foreach 나, 동작상 무조건 1회만 호출된다.
        foreach (GameObject root in frameworkScene.GetRootGameObjects())
        {
            if (root.name == "Framework")
            {
                FrameWork framework = root.GetComponent<FrameWork>();
                Debug.Assert(framework != null);
            }
        }
    }

    bool IsFrameworkSceneReady()
    {
        if (FrameWork.Instance == null)
            return false;

        int len = SceneManager.sceneCount;
        for (int i = 0; i < len; ++i)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name == "Framework")
            {
                if (s.isLoaded)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
