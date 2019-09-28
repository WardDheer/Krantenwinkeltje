using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomerScript : MonoBehaviour
{
    public GameObject Customer;
    public float Timer = 1.0f;

    [Header("Different Textbubbles")]
    public GameObject TextbubbleForOneItem;
    public GameObject TextbubbleForTwoItems;
    public GameObject TextbubbleForThreeItems;

    
    public List<GameObject> WantedItems = new List<GameObject>();
    private float _amountToPay = 0f;
    private GameBehaviour _game;

    
    

    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindGameObjectWithTag("GameBehaviour").GetComponent<GameBehaviour>();
        DetermineObjects(); //runs good, checked
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
                    //RejectionFunction
            Destroy(this.gameObject);
        }
    }


    //Tweak this if the pace of the game is wrong
    private int CalculateAmountOfItems()
    {
        int amountOfItems = 1;
        float randomValue = UnityEngine.Random.value;

        if (randomValue < 0.20f) amountOfItems -= 1;        //You have a slight percent chance to get an extra item or an item less.
        if (randomValue > 0.80f) amountOfItems += 1;


       return (int)Mathf.Max(amountOfItems, 1f);    //you can never get less than 1 item.
    }

    private void DetermineObjects()
    {
        /*WantedItems = new GameObject[CalculateAmountOfItems()];     //The size of the array is the amount of items he wants

 
        for (int i = 0; i < WantedItems.Length; i++)
        {
            int arrayIndex = GetRandomInteger();    //Get a random index between 0 and the length of the array with items
            WantedItems[i] = _game.GetComponent<GameBehaviour>().ItemsInTheShop[arrayIndex];                                  //Get the item on the number of the index from the gamebehaviour script

            _amountToPay += WantedItems[i].GetComponent<ShopItemScript>().ThisPrice;   //He calculates himself how much he needs to pay
        }*/

        for (int i = 0; i < CalculateAmountOfItems(); i++)
        {
            int arrayIndex = GetRandomInteger();    //Get a random index between 0 and the length of the array with items
            WantedItems.Add(_game.GetComponent<GameBehaviour>().ItemsInTheShop[arrayIndex]);
        }
    }

    private int GetRandomInteger()
    {
        return (int)Mathf.Round(UnityEngine.Random.Range(0, _game.GetComponent<GameBehaviour>().ItemsInTheShop.Count));
    }

    private void DisplayItems()
    {

        //maak correcte tekstbubbel aan

        switch (WantedItems.Count)
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
                TextbubbleForThreeItems = Instantiate(TextbubbleForThreeItems, this.transform.position, this.transform.rotation, this.transform);

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
            GameObject currentObject = WantedItems[i];
            Sprite textBubbleSprite = textBubble.transform.GetChild(0).GetChild(i + 1).GetComponent<Image>().sprite;
            Sprite currentObjectSprite = WantedItems[i].GetComponent<ShopItemScript>().ShopItemImage.sprite;

            textBubbleSprite = currentObjectSprite;
        }
    }
}
