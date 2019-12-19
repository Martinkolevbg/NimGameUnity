using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject coin;

    System.Random rand;

    public int minCoins = 11;
    public int maxCoins = 39;

    void Start()
    {
        rand = new System.Random();

        float xPos;
        float yPos;
        float zPos;

        int numberOfCoins = rand.Next (maxCoins - minCoins) + minCoins;
      
        Debug.Log ("Instantiating " + numberOfCoins + " coins");

        for(int i= 0; i < numberOfCoins; i++)
        {
            xPos = rand.Next(100) / 100f;
            yPos = rand.Next(5) + 2f;
            zPos = rand.Next(100) / 100f;
            
            Instantiate (coin, new Vector3(0,1,0), Random.rotation);
        }
    }

    void Update()
    {
        
    }
}
