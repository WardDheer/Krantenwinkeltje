using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashRegisterBehaviourScript : MonoBehaviour
{
    public GameObject TextBubble;
    public float EnteredNumber;
    public InputField input;

    [SerializeField]
    private float _dailyEarnings;



    private float _checkoutTotal;
    private GameObject currentTextBubble;


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
        //Debug.Log(_dailyEarnings);
        ExitPayMode();

        Debug.Log(_dailyEarnings);
    }

    private void ComparePricesWithCustomer()
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

        ComparePricesWithCustomer();
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
}
