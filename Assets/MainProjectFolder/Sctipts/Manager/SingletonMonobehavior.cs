using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            // 객체 직접 생성 대신 게임오브젝트&컴포넌트 생성
            if (_instance == null)
            {
                // 씬 전체에서 탐색
                _instance = FindObjectOfType<T>();

                // 그래도 없는 경우, 새로 생성
                if (_instance == null)
                {
                    GameObject container = new GameObject("Singleton");
                    _instance = container.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    // 동적으로 Instance를 호출하여 생성하지 않고,
    // 씬에 미리 배치해두는 경우 대비
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            // 선택사항 : Don't Destroy On Load
            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
        else
        {
            // 인스턴스가 이미 존재하지만 본인이 아닌 경우,
            // 스스로를 파괴
            if (_instance != this)
            {
                if (GetComponents<Component>().Length <= 2)
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }
    }
}