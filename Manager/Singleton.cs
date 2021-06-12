using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get 
        { 
            if(null == instance)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }
    private static T instance;
}
