﻿using UnityEngine;
using UnityEngine.UI;

public class ConsoleView : MonoBehaviour {

    public Text scrollText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyText(Text _text)
    {
        scrollText.text += Console.instance.ExecuteCommandByLines(_text.text.ToLower()) + "\n";
        //Debug.Log(_text.text);
    }
}
