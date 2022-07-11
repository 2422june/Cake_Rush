using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//싱글톤 제작시 상속시킬 모노 싱글톤
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    public static T instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                return null;
            }
            
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if (m_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
