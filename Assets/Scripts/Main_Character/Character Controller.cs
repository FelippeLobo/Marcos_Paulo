using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_data : MonoBehaviour
{
    private float _maxHealth;
    private float _actualHealth; 
    private int _dorFlexNum;
    private Weapons [] _inventoryWeapons;
    void Start()
    {
        _maxHealth = 100;
        _actualHealth = _maxHealth;
        _dorFlexNum = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
