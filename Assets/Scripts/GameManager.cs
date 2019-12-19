using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text displayMassage;
    public Text coinsRemainingMassage;
    enum GameState 
    { Busy , New, PlayersTurn , ComputersTurn, PickFirstPlayer }

    private GameState currentGameState;
    private RandomGenerator randomGenerator;

    void Start()
    {
        randomGenerator = GetComponent<RandomGenerator>();
        SetRemainingMatchesText();
        currentGameState = GameState.New;
    }

    void Update()
    {
        switch(currentGameState)
        {
            case GameState.New:
                currentGameState = GameState.Busy;
                StartCoroutine (NewGame());
                break;
            case GameState.PickFirstPlayer:
                currentGameState = GameState.Busy;
                StartCoroutine (PickFirstPlayer()); 
                break;
        }
        IEnumerator NewGame()
        {
            yield return ChangeDisplayMassage("Welcome to Nim!" , 4f);
            yield return new WaitForSeconds(1f);
            currentGameState = GameState.PickFirstPlayer;
        }
        IEnumerator ChangeDisplayMassage(string newMassage, float duration)
        {
            displayMassage.enabled = true;

            displayMassage.text = newMassage;

            yield return new WaitForSeconds (duration);

            displayMassage.enabled = false;
        }
        IEnumerator PickFirstPlayer()
        {
            int nextPlayer = new System.Random().Next(2);

            if(nextPlayer == 0)
            {
                yield return ChangeDisplayMassage("Computer go first", 3f);
                currentGameState = GameState.ComputersTurn;
            }
            else if (nextPlayer == 1){
                yield return ChangeDisplayMassage("Player go first", 3f);
                currentGameState = GameState.PlayersTurn;
            }
            else{
                Debug.LogError("nextPlayer = " + nextPlayer + "! not a valid value");
            }
        }
    }
    void SetRemainingMatchesText()
    {
        coinsRemainingMassage.text = "Coins Remaining : " + randomGenerator.GetRemainingCoins();
    }
}
