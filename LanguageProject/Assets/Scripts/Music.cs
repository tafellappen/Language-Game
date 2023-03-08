using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;

    void Start()
    {
        if(instance != null) {
            // no duplicates
            Destroy(gameObject);
            return;
        }

        // travel between scenes
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
