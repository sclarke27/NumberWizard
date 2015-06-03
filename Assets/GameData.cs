using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

    public static GameData gameData;

    public int maxValue;
    public int minValue;
    public int minRange = 1;
    public int maxRange = 1000;
    public int maxGuesses = 10;
    public int currGuess = 0;
    public int guess = 0;
    public List<int> guessedValues;


    void Awake()
    {
        Debug.Log("gamedata awake");
        if (gameData == null)
        {
            DontDestroyOnLoad(gameObject);
            gameData = this;
        }
        else if (this != gameData)
        {
            Destroy(gameObject);
        }
    }
}
