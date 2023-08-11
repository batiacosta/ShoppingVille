using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        Uniform, Weapon, Boots
    }

    [SerializeField] private ItemType type;
    [SerializeField] private List<Sprite> itemSprites;
    [SerializeField] private Sprite icon;

    [SerializeField] private int _quantity;

    public void SetQuantity(int quantity)
    {
        _quantity = quantity;
    }

    public int GetQuantity() => _quantity;
    public ItemType GetItemType() => type;
    public Sprite GetIcon() => icon;
    public List<Sprite> GetItemSprites() => itemSprites;
}
