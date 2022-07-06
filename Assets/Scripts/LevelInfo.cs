using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public string id;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
