using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SpeechHandler : MonoBehaviour
{
    [SerializeField] GameObject coinSpawner;
    [SerializeField] GameObject coin;

    [SerializeField] TextMeshProUGUI textPrompt;
    [SerializeField] TextMeshProUGUI textCurrentCards;
    [SerializeField] TextMeshProUGUI textLeft;
    [SerializeField] TextMeshProUGUI textRight;
    [SerializeField] TextMeshProUGUI TextWins;

    [SerializeField] int AiRisk;

    List<string> listCards = new List<string>();
    List<int> listPlayedCards = new List<int>();

    List<int> listPlayerCards = new List<int>();
    List<int> listAiCards = new List<int>();

    int playState = 0;
    int numOfWins = 0;

    int scorePlayer = 0;
    int aceCountPlayer = 0;

    int scoreAi = 0;
    int aceCountAi = 0;

    string textCardPlayer = "Current cards | ";
    string textCardAi = "Current cards | ";

    bool openingDeal = true;

    // Start is called before the first frame update
    void Start()
    {
        TextWins.text = "Number of wins: 0";
        for (int i = 0; i < 14; i++)
        {
            listPlayedCards.Add(0);
        }
        textPrompt.text = "Would you like to play Blackjack?";
        textCurrentCards.text = "";
        textLeft.text = "Sure!";
        textRight.text = "No thank you";
        listCards.Add("null");
        listCards.Add("A");
        listCards.Add("2");
        listCards.Add("3");
        listCards.Add("4");
        listCards.Add("5");
        listCards.Add("6");
        listCards.Add("7");
        listCards.Add("8");
        listCards.Add("9");
        listCards.Add("10");
        listCards.Add("J");
        listCards.Add("Q");
        listCards.Add("K");
    }

    public void buttonLeftPressed()
    {
        if (playState == 0)
        {
            playState = 1;
            textPrompt.text = "Here are your cards";
            textLeft.text = "Hit me";
            textRight.text = "Stay";
            newCard();
            newCard();
        }
        else if (playState == 1)
        {
            newCard();
        }
        else
        {
            for (int i = 0; i < listPlayedCards.Count; i++)
            {
                listPlayedCards[i] = 0;
            }
            listPlayerCards.Clear();
            listAiCards.Clear();
            textPrompt.text = "Would you like to play Blackjack?";
            textCurrentCards.text = "";
            textLeft.text = "Sure!";
            textRight.text = "No thank you";
            scorePlayer = 0;
            aceCountPlayer = 0;
            scoreAi = 0;
            aceCountAi = 0;
            playState = 0;
            textCardPlayer = "Current cards | ";
            textCardAi = "Current cards | ";
            openingDeal = true;
            //Scene currentScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(currentScene.name);
        }
    }

    public void buttonRightPressed()
    {
        if (playState == 0)
        {
            textPrompt.text = "I have been programmed to not take no as an answer!";
        }
        else if (playState == 1)
        {
            textLeft.text = "New game";
            textRight.text = "New game";
            while (ShouldAiHit(scoreAi, aceCountAi))
            {
                newCardAi();
            }
            Debug.Log("Player score Validation | " + ValidateScore(scorePlayer, aceCountPlayer));
            Debug.Log("Ai score Validation | " + ValidateScore(scoreAi, aceCountAi));
            if (ValidateScore(scorePlayer, aceCountPlayer) > 21 && ValidateScore(scoreAi, aceCountAi) > 21)
            {
                textPrompt.text = (textCardAi + "Dealer and Player bust, Dealer wins!");
            }
            else
            {
                textPrompt.text = textCardAi;
                if (ValidateScore(scorePlayer, aceCountPlayer) > 21)
                {
                    textPrompt.text += "Player busts, you lose";
                }
                else if (ValidateScore(scoreAi, aceCountAi) > 21)
                {
                    textPrompt.text += "Dealer busts, you win!";
                    Victory();
                }
                else
                {
                    if (ValidateScore(scorePlayer, aceCountPlayer) == ValidateScore(scoreAi, aceCountAi))
                    {
                        textPrompt.text += "Draw!";
                    }
                    else if (ValidateScore(scorePlayer, aceCountPlayer) > ValidateScore(scoreAi, aceCountAi))
                    {
                        textPrompt.text += "Player is closer to 21, you win!";
                        Victory();
                    }
                    else
                    {
                        textPrompt.text += "Dealer is closer to 21, you lose";
                    }
                }
            }
            playState = 2;
        }
        else
        {
            buttonLeftPressed();
        }
    }

    private void Victory()
    {
        numOfWins += 1;
        TextWins.text = "Number of wins: " + numOfWins;
        for (int i = 0; 10 > i; i++)
        {
            Instantiate(coin, coinSpawner.transform);
        }
    }

    private int ValidateScore(int score, int aceCount)
    {// score 0 | ace 2
        //Debug.Log("-----New Validation-----");
        int activeAces = aceCount;
        if (21 > score + (aceCount * 11))
        {
            //Debug.Log("Returned at max ace value | " + score + (aceCount * 11));
            return score + (aceCount * 11);
        }
        else
        {
            while (21 < score + (activeAces * 11) + (aceCount - activeAces))
            {
                //Debug.Log("Looping\nScore | " + score + "\nActive Aces | " + activeAces + "\nAce Count | " + aceCount);
                activeAces -= 1;
                if (activeAces < 0)
                {
                    //Debug.Log("Too many aces");
                    return score + (aceCount * 11);
                }
            }
            //Debug.Log("Returning optimal aces | " + activeAces);
            return score + (activeAces * 11) + (aceCount - activeAces);
        }
    }

    private void newCard()
    {
        if (scorePlayer + aceCountPlayer > 21)
        {
            buttonRightPressed();
            return;
        }
        scorePlayer = 0;
        int newCard = UnityEngine.Random.Range(1, listCards.Count);
        while (listPlayedCards[newCard] >= 4)
        {
            newCard = UnityEngine.Random.Range(1, listCards.Count);
        }

        textCardPlayer = "Current cards | ";

        listPlayerCards.Add(newCard);
        listPlayedCards[newCard] += 1;

        aceCountPlayer = 0;
        foreach (int card in listPlayerCards)
        {
            if (card == 1)
            {
                aceCountPlayer++;
            }
            else if (card > 10)
            {
                scorePlayer += 10;
            }
            else
            {
                scorePlayer += card;
            }
            textCardPlayer += (listCards[card] + " | ");
        }
        textCurrentCards.text = textCardPlayer;

        newCardAi();
    }

    private void newCardAi()
    {
        if (!ShouldAiHit(scoreAi, aceCountAi))
        {
            return;
        }

        scoreAi = 0;
        int newCard = UnityEngine.Random.Range(1, listCards.Count);
        while (listPlayedCards[newCard] >= 4)
        {
            Debug.Log("Too many of same card picked");
            newCard = UnityEngine.Random.Range(1, listCards.Count);
        }

        textCardAi = "Current cards | ";

        listAiCards.Add(newCard);
        listPlayedCards[newCard] += 1;

        aceCountAi = 0;
        string tmp = "";
        foreach (int card in listAiCards)
        {
            if (card == 1)
            {
                aceCountAi++;
            }
            else if (card > 10)
            {
                scoreAi += 10;
            }
            else
            {
                scoreAi += card;
            }
            textCardAi += (listCards[card] + " | ");
            tmp = (listCards[card] + " | ");
        }
        if (openingDeal)
        {
            openingDeal = false;
            textPrompt.text += (" | ? | ");
        }
        else
        {
            textPrompt.text += tmp;
        }
    }

    private bool ShouldAiHit(int score, int aceCount)
    {
        int activeAces = aceCount;
        if (21 > score + (aceCount * 11))
        {
            if (score + (aceCount * 11) >= AiRisk)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            while (21 < score + (activeAces * 11) + (aceCount - activeAces))
            {
                activeAces -= 1;
                if (activeAces < 0)
                {
                    return false;
                }
            }
            if (score + (activeAces * 11) + (aceCount - activeAces) >= AiRisk)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}