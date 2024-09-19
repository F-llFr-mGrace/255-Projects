using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScriptHighscore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textHighscore;

    int currentScore = 0;
    List<int> highscore = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        highscore.Add(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonAddScore()
    {
        currentScore++;
        textScore.text = ("Current Score ||\n" + currentScore);
    }

    public void ButtonSaveScore()
    {
        if (currentScore > highscore[0])
        {
            highscore.Add(currentScore);
        }
        highscore.Sort((a, b) => b.CompareTo(a));

        if (highscore.Count > 5)
        {
            highscore.RemoveAt(5);
        }

        string allScores = "Highscore ||\n";
        foreach (int i in highscore)
        {
            allScores += ((i + "\n").ToString());
        }
        textHighscore.text = (allScores);
    }
}
