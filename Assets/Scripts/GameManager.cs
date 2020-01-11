using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI Elements
    public Text displayMassage;
    public Text coinsRemainingMassage;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button newGameButton;
    public Text questionMassage;

    // Game State machine
    private GameState currentGameState;
    private RandomGenerator randomGenerator;

    // States
    enum GameState 
    { Busy , New, PlayersTurn , ComputersTurn, PickFirstPlayer,PlayerLost,ComputerLost }



    void Start()
    {
        button1.onClick.AddListener(() =>{ StartCoroutine(RemoveCoins(1)); });
        button2.onClick.AddListener(() =>{ StartCoroutine(RemoveCoins(2)); });
        button3.onClick.AddListener(() =>{ StartCoroutine(RemoveCoins(3)); });
        newGameButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });

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
            case GameState.PlayersTurn:
                currentGameState = GameState.Busy;
                StartCoroutine (ShowPlayerOptions());
                break;
            case GameState.ComputersTurn:
                currentGameState=GameState.Busy;
                StartCoroutine (ComputersTurn());
                break;
            case GameState.PlayerLost:
                currentGameState = GameState.Busy;
                StartCoroutine(ShowGameOver("You Lost!"));
                break;
            case GameState.ComputerLost:
                currentGameState = GameState.Busy;
                StartCoroutine(ShowGameOver("You Won"));
                break;
        }
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
                yield return ChangeDisplayMassage("Computer go first", 2f);
                currentGameState = GameState.ComputersTurn;
            }
            else if (nextPlayer == 1){
                yield return ChangeDisplayMassage("Player go first", 2f);
                currentGameState = GameState.PlayersTurn;
            }
            else{
                Debug.LogError("nextPlayer = " + nextPlayer + "! not a valid value");
            }
        }





        IEnumerator ShowPlayerOptions()
        {
            yield return ChangeDisplayMassage("Your turn !" , 3f);
            if(randomGenerator.GetRemainingCoins() == 1)
            {
                currentGameState = GameState.PlayerLost;
            }
            else{

                questionMassage.gameObject.SetActive(true);

                if (randomGenerator.GetRemainingCoins() == 2)
                {
                    button1.gameObject.SetActive(true);
                }
                else if (randomGenerator.GetRemainingCoins() == 3)
                {
                    button1.gameObject.SetActive(true);
                    button2.gameObject.SetActive(true);
                }
                else
                {
                    button1.gameObject.SetActive(true);
                    button2.gameObject.SetActive(true);
                    button3.gameObject.SetActive(true);
                }
            }
        }
    IEnumerator RemoveCoins(int numberOfCoins)
    {
        questionMassage.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        yield return ChangeDisplayMassage("Removing " + numberOfCoins + " Coin/s.", 1.5f); 
        randomGenerator.RemoveCoinObjects (numberOfCoins);
        SetRemainingMatchesText();
        currentGameState = GameState.ComputersTurn;
    }

    IEnumerator ComputersTurn(){
        if(randomGenerator.GetRemainingCoins() == 1){
            currentGameState = GameState.ComputerLost;
        }
        else{

            yield return new WaitForSeconds(1f);
            int numberOfCoinsToTake = NimHelper.CalculateCoinsToRemove(randomGenerator.GetRemainingCoins());
            yield return ChangeDisplayMassage("Computers takes " + numberOfCoinsToTake + " coin/s ", 2f);
            randomGenerator.RemoveCoinObjects(numberOfCoinsToTake);
            yield return new WaitForSeconds(1f);
            SetRemainingMatchesText();
            currentGameState = GameState.PlayersTurn;
        }
    }

    IEnumerator ShowGameOver(string massage){
        yield return ChangeDisplayMassage(massage,3f);
        newGameButton.gameObject.SetActive(true);
    }

    void SetRemainingMatchesText()
    {
        coinsRemainingMassage.text = "Coins Remaining : " + randomGenerator.GetRemainingCoins();
    }  
}
