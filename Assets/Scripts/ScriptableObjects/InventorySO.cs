using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventorySO", order = 1)]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemSO> itemsSo;

    private List<ItemSO> _uniformsSo = new List<ItemSO>();
    private List<ItemSO> _weaponsSo = new List<ItemSO>();
    private List<ItemSO> _bootsSo = new List<ItemSO>();

    public void InventorySetup()
    {
        foreach (ItemSO itemSo in itemsSo)
        {
            switch (itemSo.GetItemType())
            {
                case ItemSO.ItemType.Uniform:
                    _uniformsSo.Add(itemSo);
                    break;
                case ItemSO.ItemType.Weapon:
                    _weaponsSo.Add(itemSo);
                    break;
                case ItemSO.ItemType.Boots:
                    _bootsSo.Add(itemSo);
                    break;
            }
        }
    }

    public void AddItem(ItemSO itemSo)
    {
        switch (itemSo.GetItemType())
        {
            case ItemSO.ItemType.Uniform:
                _uniformsSo.Add(itemSo);
                break;
            case ItemSO.ItemType.Weapon:
                _weaponsSo.Add(itemSo);
                break;
            case ItemSO.ItemType.Boots:
                _bootsSo.Add(itemSo);
                break;
        }
    }

    public void SubstractItem(ItemSO itemSo)
    {
        switch (itemSo.GetItemType())
        {
            case ItemSO.ItemType.Uniform:
                _uniformsSo.Remove(itemSo);
                break;
            case ItemSO.ItemType.Weapon:
                _weaponsSo.Remove(itemSo);
                break;
            case ItemSO.ItemType.Boots:
                _bootsSo.Remove(itemSo);
                break;
        }
    }

    public List<ItemSO> GetUniformsSo() => _uniformsSo;
    public List<ItemSO> GetWeaponsSo() => _weaponsSo;
    public List<ItemSO> GetBootsSo() => _bootsSo;
}
