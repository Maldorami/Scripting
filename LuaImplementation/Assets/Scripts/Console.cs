using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour {
    

    public static Console instance;

    public delegate string _CommandOnConsole(string[] arg);
    public static event _CommandOnConsole CommandOnConsole;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
}