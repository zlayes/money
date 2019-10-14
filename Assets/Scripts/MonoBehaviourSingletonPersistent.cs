using UnityEngine;

namespace Money
{
    public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour where T : Component
    {
        private static bool isShuttingDown;
        private static T instance;
        public static T Instance
        {
            get
            {
                if (isShuttingDown)
                {
                    //Debug.LogWarning("[Singleton] Instance \"" + typeof(T) + "\" already destroyed. Returning null.");
                    return null;
                }
                if (instance == null)
                {
                    var objs = FindObjectsOfType<T>();
                    if (objs.Length == 0)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                    else if (objs.Length == 1)
                    {
                        instance = objs[0];
                        DontDestroyOnLoad(instance);
                    }
                    else
                        Debug.LogError("You must have at most one " + typeof(T).Name + " in the scene.");
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(Instance);
            }
            else if (instance != this)
                Destroy(gameObject);
        }

        private void OnApplicationQuit()
        {
            isShuttingDown = true;
        }

        private void OnDestroy()
        {
            isShuttingDown = true;
        }
    }
}
