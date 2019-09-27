using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject Customer;
    public Transform CustomerParent;

    private GameObject _currentCustomer;

    // Update is called once per frame
    void Update()
    {
        if(GameBehaviour.gameState == GameState.SelectMode)
        {
            if(_currentCustomer == null)
            {
                _currentCustomer = Instantiate(Customer, this.transform.position, this.transform.rotation, CustomerParent);
            }
        }
    }

   
}
