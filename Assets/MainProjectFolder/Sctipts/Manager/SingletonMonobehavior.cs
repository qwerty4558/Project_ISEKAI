using Unity.VisualScripting;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _shuttingDown = false;
    private static object _lock = new object();
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_shuttingDown)
            {
                return null;
            }

            lock (_lock)
            {
                if(_instance== null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if(_instance == null)
                    {
                        var singletObject = new GameObject();

                        _instance = singletObject.AddComponent<T>();

                        singletObject.name = typeof(T).ToString() + "(singleton)";

                        DontDestroyOnLoad(singletObject);
                    }
                }
                return _instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _shuttingDown = true;
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
        _instance = null;
    }
}