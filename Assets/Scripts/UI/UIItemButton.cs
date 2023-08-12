using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemButton : MonoBehaviour
{
    
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI priceText;
    
    private ItemSO _itemSo;

    public void SetItemButtonPrefab(ItemSO itemSo, bool isShop)
    {
        _itemSo = itemSo;
        icon.sprite = itemSo.GetIcon();
        if (isShop)
        {
            priceText.text = itemSo.GetPrice().ToString();
        }
        else
        {
            int itemPrice = Mathf.RoundToInt(itemSo.GetPrice() * 0.3f);
            if (itemPrice == 0) itemPrice = 1;
            priceText.text = itemPrice.ToString();
        }
    }
}
