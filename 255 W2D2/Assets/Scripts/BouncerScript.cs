using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textAge;
    [SerializeField] TextMeshProUGUI textSpeech;

    int age;
    string[] thingsToSay = new string[4] {"Get out of here!", "You're too young", "Come on in!", "Enjoy your time"};

    void Start()
    {
        NewAge();
    }

    public void NewAge()
    {
        age = UnityEngine.Random.Range(0, 50);
        textAge.text = age.ToString();
        textSpeech.text = "I am the Bouncer!";
    }

    public void ShowId()
    {
        if (age < 21)
        {
            var index = UnityEngine.Random.Range(0, 2);
            textSpeech.text = thingsToSay[index];
        }
        else
        {
            var index = UnityEngine.Random.Range(2, 4);
            textSpeech.text = thingsToSay[index];
        }
        
    }

}