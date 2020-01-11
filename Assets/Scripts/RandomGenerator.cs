using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject coin;

    System.Random rand;

    GameObject[] coins;

    int numberOfCoins;

    public int minCoins = 11;
    public int maxCoins = 39;

    void Start()
    {
        rand = new System.Random();

        float xPos;
        float yPos;
        float zPos;

        numberOfCoins = rand.Next (maxCoins - minCoins) + minCoins;
        coins = new GameObject[numberOfCoins];
        Debug.Log ("Instantiating " + numberOfCoins + " coins");

        for(int i= 0; i < numberOfCoins; i++)
        {
            xPos = rand.Next(100) / 100f;
            yPos = rand.Next(5) + 2f;
            zPos = rand.Next(100) / 100f;

            coins[i]  = Instantiate (coin, new Vector3(0,1,0), Random.rotation) as GameObject;
        }
    }

    void Update()
    {
        
    }
    public int GetRemainingCoins() => numberOfCoins;

    public void RemoveCoinObjects(int numberOfCoinsToTake)
    {
        for(int i =0 ; numberOfCoinsToTake > 0 ;i++){

            if (coins != null){
                Destroy(coins[i]);
                numberOfCoinsToTake --;
                numberOfCoins --;
                Debug.Log("i removed");
                
            }
        }
    }
}
