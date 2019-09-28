using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    

    //public List<GameObject> SelectedItems = new List<GameObject>();
    public float TotalPriceToPay;
    public GameObject inventorySlot;

    public List<GameObject> SelectedItems = new List<GameObject>();



    private List<GameObject> InventoryList = new List<GameObject>(); 
    private GameObject _inspectedItem;

    private void Start()
    {
        
        TotalPriceToPay = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameBehaviour.gameState == GameState.SelectMode)
        {
            SelectShopItems();
            InspectShopItems();
        }


        TotalPriceToPay = Mathf.Max(TotalPriceToPay, 0f);
        
    }

    private void SelectShopItems()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LayerMask layerMask = 9;

            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider != null)
            {
                SelectItem(hit.transform.gameObject);
            }
   
        }
        if (Input.GetMouseButtonDown(1))
        {
            LayerMask layerMask = 9;

            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider != null)
            {
                DeSelectItem(hit.transform.gameObject);
            }
        }
    }

    private void InspectShopItems()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            int shopItemsLayer = 9;  //Layer of Selectable Shop Items
            int layermask = (1 << shopItemsLayer); //Make sure the raycast only collides with the items in the shop

            //##Fix deze raycast

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                ShopItemScript currentScript = hit.transform.gameObject.GetComponent<ShopItemScript>();
                currentScript.IsInspected = true;

                _inspectedItem = hit.transform.gameObject;   
            }
        }*/

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ShopItemScript currentScript = _inspectedItem.transform.gameObject.GetComponent<ShopItemScript>();
            currentScript.IsInspected = false;
            
        }

        InspectItem();

    }

    private void InspectItem()
    {
        if (_inspectedItem == null) return;
        
        ShopItemScript currentScript = _inspectedItem.GetComponent<ShopItemScript>();

        if (currentScript.IsInspected)
        {
            //Insert behaviour when inspecting the item
        }
       
    }

    private void SelectItem(GameObject currentObject)
    {
        ShopItemScript currentScript = currentObject.GetComponent<ShopItemScript>();
        currentScript.IsSelected = true;

      
        
        if(currentScript.InStock > 0)
        {
            currentScript.InStock--;
            currentScript.TotalSelected++;
            SelectedItems.Add(currentObject);
            TotalPriceToPay += currentScript.ThisPrice;
        }   
    }

    private void DeSelectItem(GameObject currentObject)
    {
        ShopItemScript currentScript = currentObject.GetComponent<ShopItemScript>();
        currentScript.IsSelected = false;

        if(currentScript.TotalSelected > 0)
        {
            currentScript.TotalSelected--;
            currentScript.InStock++;
            SelectedItems.Remove(currentObject);
            TotalPriceToPay -= currentScript.ThisPrice;
        }
    }
    //private void DeInspectItem(GameObject currentObject)
    //{
    //    ShopItemScript currentScript = currentObject.GetComponent<ShopItemScript>();
    //    currentScript.IsInspected = false;
    //}
}
