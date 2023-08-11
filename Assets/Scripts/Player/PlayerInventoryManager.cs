using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO playerInventory;

    public void InventorySetUp()
    {
        playerInventory.InventorySetup();
    }

    public InventorySO GetInventory() => playerInventory;
}
