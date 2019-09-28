using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashRegisterBehaviourScript : MonoBehaviour
{
    public GameObject TextBubble;
    public float EnteredNumber;
    public InputField input;
    public PlayerBehaviour Player;

    [SerializeField]
    private float _dailyEarnings;



    private float _checkoutTotal;
    private GameObject currentTextBubble;

    [SerializeField]
    private CustomerScript _customer;
    


    void Start()
    {
        _dailyEarnings = 0f;
        currentTextBubble = Instantiate(TextBubble, TextBubble.transform.position, TextBubble.transform.rotation);
        currentTextBubble.GetComponentInChildren<InputField>().onEndEdit.AddListener(InputNumbers);
        currentTextBubble.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        ExitPayMode();

        Debug.Log(_dailyEarnings);
    }

    private void ComparePrices()
    {
        float litteralPriceForcustomer = GameObject.FindGameObjectWithTag("Mouse").GetComponent<PlayerBehaviour>().TotalPriceToPay;
        if(EnteredNumber > litteralPriceForcustomer)
        {
            //Debug.Log(_dailyEarnings);
            _dailyEarnings += 0f;

            //##Write script that customer runs away
        }
        else
        {
            Debug.Log(_dailyEarnings);
           _dailyEarnings += EnteredNumber;
        }
    }

    public void ActivateTextBubble()
    {
            InputField inputField = currentTextBubble.GetComponentInChildren<InputField>();

        currentTextBubble.SetActive(true);

            inputField.Select();
            inputField.characterValidation = InputField.CharacterValidation.Decimal;
    }
    public void InputNumbers(string priceToPay)
    {

        //currentTextBubble = GameObject.FindGameObjectWithTag("TextBubble"); //Otherwise he disables the prefab, not the active textbubble
        //currentTextBubble = TextBubble;
        
        if (priceToPay.Contains("."))
        {
            string[] splitParts = priceToPay.Split('.');
            string convertedPrice = splitParts[0] + "," + splitParts[1];
            EnteredNumber = float.Parse(convertedPrice);
        }
        else
        {
            if (priceToPay == "")
            {
                InputField inputField = currentTextBubble.GetComponentInChildren<InputField>();
                Debug.Log("Value" + inputField.text);
                inputField.text = "";
                currentTextBubble.SetActive(false);
                return;
            } //If player presses Esc when he didn't enter a number, the game doesn't crash
            else EnteredNumber = float.Parse(priceToPay);
        }

        ComparePlayerWithCustomer();
        currentTextBubble.GetComponentInChildren<InputField>().DeactivateInputField();
        currentTextBubble.SetActive(false);

        GameObject.FindGameObjectWithTag("Mouse").GetComponent<PlayerBehaviour>().TotalPriceToPay = 0;
       

        GameBehaviour.gameState = GameState.SelectMode;
    }
    private void ExitPayMode()      //We will need more options on how to skip the pay method
    {
        if (GameBehaviour.gameState == GameState.PayMode && Input.GetKeyDown(KeyCode.Escape))
        {
            GameBehaviour.gameState = GameState.SelectMode;
            currentTextBubble.SetActive(false); //Do not destroy, but hide
        }
    }
    public void SetGameStateToPayMode()
    {
        GameBehaviour.gameState = GameState.PayMode;
    }
    private void ComparePlayerWithCustomer()
    {
        CompareOrders();
        ComparePrices();
    }
    private bool CompareOrders()
    {
        _customer = GameObject.FindGameObjectWithTag("Customer").GetComponent<CustomerScript>();
        List<GameObject> playerList = Player.SelectedItems;
        List<GameObject> customerList = _customer.WantedItems;

        if(playerList.Count != customerList.Count)        //if the amount of items is different, the order is wrong
        {
            return false;
        }

        /*foreach( GameObject item in playerList)
        {
            if (customerList.Contains(item) && item != null)
            {
                customerList.Remove(item);
                playerList.Remove(item);
            }
        }*/

        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject currentObject;
            if (playerList[i] != null)
            {
             currentObject = playerList[i];
            }
            else
            {
                Debug.Log("Item in list is null");
                currentObject = this.gameObject;
            }

            if (customerList.Contains(currentObject))
            {
                customerList.Remove(currentObject);
                playerList.RemoveAt(i);
            }
        }

        if(playerList.Count == 0 && customerList.Count == 0)
        {
            return true;
        }

        return false;
    }
}
