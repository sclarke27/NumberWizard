using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberWizard : MonoBehaviour {

    public static NumberWizard numberWizard;
    GameData gameData;

    public Text guessNumberText;
    public Text introText;

	// Use this for initialization
	void Start () {
        gameData = GameData.gameData;
        gameData.currGuess = 0;
        gameData.guessedValues.Clear();
        Debug.Log("start");
        StartGame();
	}
	
    void SetRandomStartValues()
    {
        gameData.maxValue = Random.Range(gameData.minRange, gameData.maxRange);
        gameData.minValue = Random.Range(gameData.minRange, gameData.maxValue);
        Debug.Log("new values picked");
    }

    int PickNewGuess()
    {
        int halfVal = (gameData.maxValue + gameData.minValue) / 2;
        int bufferVal = 5;
        int start = halfVal - bufferVal;
        int end = halfVal + bufferVal;
        int newGuess = Random.Range((start >= gameData.minValue) ? start : gameData.minValue, (end <= gameData.maxValue) ? end : gameData.maxValue);
        int maxChecks = (gameData.guessedValues == null) ? 0 : gameData.guessedValues.Count;
        while (HasBeenGuessed(newGuess) && maxChecks >= 0)
        {
            newGuess = Random.Range(halfVal - bufferVal, halfVal + bufferVal);
            maxChecks = maxChecks - 1;
        }
        gameData.guessedValues.Add(newGuess);
        return newGuess;
    }

    bool HasBeenGuessed(int testGuess)
    {
        int len = (gameData.guessedValues == null) ? 0 : gameData.guessedValues.Count;
        for(int i=0; i<len; i++) {
            if (gameData.guessedValues[i] == testGuess)
            {
                return true;
            }
        }
        return false;
    }

    public void StartGame()
    {
        Debug.Log("startGame");
        if (introText != null)
        {
            SetRandomStartValues();
            introText.text = "Pick a number between " + gameData.minValue + " and " + gameData.maxValue + " and i will guess it.";
        }
        if (guessNumberText != null)
        {
            gameData.guess = PickNewGuess();
            guessNumberText.text = gameData.guess.ToString();
        }
        if (guessNumberText != null && introText != null)
        {
            print("text fields not found");
        }
    }

    void NextGuess()
    {
        if (gameData.currGuess == gameData.maxGuesses)
        {
            Application.LoadLevel("Win");
        }
        else
        {
            gameData.guess = PickNewGuess();
            gameData.currGuess = gameData.currGuess + 1;
            guessNumberText.text = gameData.guess.ToString();
        }
    }

    public void GuessHigher()
    {
        gameData.minValue = gameData.guess;
        NextGuess();
    }

    public void GuessLower()
    {
        gameData.maxValue = gameData.guess;
        NextGuess();
    }



}
