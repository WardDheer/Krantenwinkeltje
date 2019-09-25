using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScript : MonoBehaviour
{
    public bool IsSelected = false;
    public bool IsInspected = false;

    public int InactiveDays = 0;        //Amount of days that the customers won't ask about the item, decreases every day

    [Header("Amount Info")]
    public int TotalSelected = 0;
    public int InStock = 5;

    [Header("Price Info")]
    public float ThisPrice = 1.5f;      //Price for this object

    public Image ShopItemImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
