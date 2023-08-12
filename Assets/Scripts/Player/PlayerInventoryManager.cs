using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO playerInventorySo;

    public void InventorySetUp()
    {
        playerInventorySo.InventorySetup();
    }

    public InventorySO GetInventory() => playerInventorySo;
}
