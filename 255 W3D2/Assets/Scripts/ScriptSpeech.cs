using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class ScriptSpeech : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextSpeech;

    const string FileDir = "/Data/";
    const string FileName = "speech.txt";
    string FilePath;

    string[] ArraySpeech;

    int currentLine = 1;

    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.dataPath + FileDir + FileName;
        //Debug.Log(FilePath);

        if (File.Exists(FilePath))
        {
            //Debug.Log("It work");
            ArraySpeech = File.ReadAllLines(FilePath);
            for (int i = 1; i < ArraySpeech.Length; i++)
            {
                Debug.Log(ArraySpeech[i]);
            }
        }
        else
        {
            //Debug.Log("It not work");
        }

        TextSpeech.text = ArraySpeech[currentLine];
        currentLine++;
    }

    public void ButtonNextClicked()
    {
        if (currentLine < ArraySpeech.Length)
        {
            TextSpeech.text = ArraySpeech[currentLine];
            currentLine++;
        }
        else
        {
            currentLine = 1;
            TextSpeech.text = ArraySpeech[currentLine];
            currentLine++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
