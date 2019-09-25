using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    public GameBehaviour Game;
    public GameObject Customer;
    public float Timer = 10.0f;

    [Header("Different Textbubbles")]
    public GameObject TextbubbleForOneItem;
    public GameObject TextbubbleForTwoItems;
    public GameObject TextbubbleForThreeItems;

    private int AmountOfItems;
    private GameObject[] _wantedItems;
    private float _amountToPay = 0f;

    
    

    // Start is called before the first frame update
    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("GameBehaviour").GetComponent<GameBehaviour>();
        CalculateAmountOfItems();
        //DELETE THIS
        AmountOfItems = UnityEngine.Random.Range(1,4);
        DetermineObjects();
        DisplayItems();
    }

   


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_amountToPay);
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = 10;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
            Instantiate(Customer);
        }
    }


    //Tweak this if the pace of the game is wrong
    private void CalculateAmountOfItems()
    {
        AmountOfItems = GameBehaviour.CurrentLevel;     // Is declared by the level of the game
        float randomValue = UnityEngine.Random.value;

        if (randomValue < 0.20f) AmountOfItems -= 1;        //You have a slight percent chance to get an extra item or an item less.
        if (randomValue > 0.80f) AmountOfItems += 1;


        AmountOfItems = (int)Mathf.Max(AmountOfItems, 1f);  //you can never get less than 1 item.
    }

    private void DetermineObjects()
    {
        _wantedItems = new GameObject[AmountOfItems-1];     //The size of the array is the amount of items he wants

 
        for (int i = 0; i < _wantedItems.Length; i++)
        {
            int index = (int)Mathf.Round(UnityEngine.Random.Range(0, Game.GetComponent<GameBehaviour>().ItemsInTheShop.Count));     //Get a random index between 0 and the length of the array with items
            _wantedItems[i] = Game.GetComponent<GameBehaviour>().ItemsInTheShop[index];                                  //Get the item on the number of the index from the gamebehaviour script

            _amountToPay += _wantedItems[i].GetComponent<ShopItemScript>().ThisPrice;   //He calculates himself how much he needs to pay
        }
    }

    private void DisplayItems()
    {

        //maak correcte tekstbubbel aan

        switch (_wantedItems.Length)
        {
            case 1:
                TextbubbleForOneItem = Instantiate(TextbubbleForOneItem, this.transform);

                GetIconsForTextBubble(1, TextbubbleForOneItem);
                break;
            case 2:
                TextbubbleForTwoItems = Instantiate(TextbubbleForTwoItems, this.transform);

                GetIconsForTextBubble(2, TextbubbleForTwoItems);
                break;
            case 3:
                TextbubbleForThreeItems = Instantiate(TextbubbleForThreeItems, this.transform);

                GetIconsForTextBubble(3, TextbubbleForThreeItems);
                break;
            default:
                Debug.Log("Error With switch Statement, Wrong textbubble selected");
                break;

        }

    }

    private void GetIconsForTextBubble(int amountOfIcons, GameObject textBubble)
    {
        for (int i = 0; i < amountOfIcons; i++)
        {
            textBubble.transform.GetChild(0).GetChild(i + 1).GetComponent<Image>().sprite = _wantedItems[i].GetComponent<ShopItemScript>().ShopItemImage.sprite;
        }
    }
}
