using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventorySO", order = 1)]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemSO> _itemsSo;

    [SerializeField]private List<ItemSO> _uniformsSo = new List<ItemSO>();
    [SerializeField]private List<ItemSO> _weaponsSo = new List<ItemSO>();
    [SerializeField]private List<ItemSO> _bootsSo = new List<ItemSO>();

    public void InventorySetup()
    {
        _uniformsSo.Clear();
        _weaponsSo.Clear();
        _bootsSo.Clear();
        
        foreach (ItemSO itemSo in _itemsSo)
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
        this._itemsSo.Add(itemSo);
    }

    public bool SubstractItem(ItemSO itemSo)
    {
        switch (itemSo.GetItemType())
        {
            case ItemSO.ItemType.Uniform:
                if (_uniformsSo.Count == 1) return false;
                _uniformsSo.Remove(itemSo);
                break;
            case ItemSO.ItemType.Weapon:
                if (_weaponsSo.Count == 1) return false;
                _weaponsSo.Remove(itemSo);
                break;
            case ItemSO.ItemType.Boots:
                if (_bootsSo.Count == 1) return false;
                _bootsSo.Remove(itemSo);
                break;
        }
        this._itemsSo.Remove(itemSo);
        return true;
    }

    public List<ItemSO> GetUniformsSo() => _uniformsSo;
    public List<ItemSO> GetWeaponsSo() => _weaponsSo;
    public List<ItemSO> GetBootsSo() => _bootsSo;

    public List<ItemSO> GetAllItemsSo() => _itemsSo;
}
