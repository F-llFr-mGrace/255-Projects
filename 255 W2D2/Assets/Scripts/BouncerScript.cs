using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textAge;
    [SerializeField] TextMeshProUGUI textSpeech;

    int age;

    void Start()
    {
        NewAge();
    }

    public void NewAge()
    {
        age = Random.Range(0, 50);
        textAge.text = age.ToString();
        textSpeech.text = "I am the Bouncer!";
    }

    public void ShowId()
    {
        if (age < 21)
        {
            textSpeech.text = "You are too young!";
        }
        else
        {
            textSpeech.text = "Have fun!";
        }
        
    }

}