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
        //  * ��Ƽ ����ó�� �������� ��� ���� ����߿� �ϳ�. ����Ƽ������ '�ý���' �� ����°� �������� ��ƴ�.

        //  * ��������δ�: ���� �������, �Ϲ����� c# �̱���(mono�� �ƴ�)�� ������, SriptableObject �� ������,
        //    �������δ�: ������(=Ÿ��Ʋ) ���� �̸� �ε��� �ϴ���, ��-��ǵ�(�ʿ��� ��) �����ϴ���.. �ؾ��ϰ�
        //    ��ġ��: ���õ�ũ / Resources ���� / StreamingAssets / ���¹���(�����-��ġ������) �̴���..
        //  * �� ���׵��� ������ �����ؼ� ����(��)�� ���⿡ ���� �����ȴ�.

        //  * ���� �񵿱� �ε���, "�ش� ������" �� �Ϸ���� �ʴ´�.
        //    ��, ���õ����͸� �̿��Ϸ��� ��� �غ�Ǿ����� üũ�� �ʿ��ϴ�.

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

        //  * GC �� ��������, �� �ڷ�ƾ�̳�, �� foreach ��, ���ۻ� ������ 1ȸ�� ȣ��ȴ�.
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
