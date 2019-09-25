using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameState { SelectMode, PayMode, RestockMode, ComputerMode }

public class GameBehaviour : MonoBehaviour
{

    public static GameState gameState;
    public static int CurrentLevel = 1;
    public List<GameObject> ItemsInTheShop = new List<GameObject>();

    //public static int TotalAmountOfItems = 5;    //Not determined yet

    // Start is called before the first frame update
    void Start()
    {   
        gameState = GameState.SelectMode;       //NOT IN THE REAL GAME --> must be restockmode at first.

        //####################################################################
        //THERE IS A BUG WHILE SEARCHING THE GAMEOBJECT. ALWAYS RETURNS NULL##
        //####################################################################

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("shopitem"))
        {
            ItemsInTheShop.Add(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameState);
       
    }
}
