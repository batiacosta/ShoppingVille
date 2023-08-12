using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopManager : MonoBehaviour
{
    [SerializeField] private InventorySO shopInventorySo;
    [SerializeField] private InventorySO playerInventorySo;
    [SerializeField] private Button itemButtonPrefab;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private Transform playerScrollView;
    [SerializeField] private OutfitSO outfitSo;
    

    [SerializeField]private List<Button> _playerButtons = new List<Button>();
    [SerializeField]private List<Button> _shopButtons = new List<Button>();
    

    private void OnEnable()
    {
        shopInventorySo.InventorySetup();
        
        ShopButtonsSetup();

        PlayerButtonsSetup();
    }

    private void PlayerButtonsSetup()
    {
        foreach (ItemSO itemSo in playerInventorySo.GetAllItemsSo())
        {
            GameObject buttonGameObject = Instantiate(itemButtonPrefab.gameObject, playerScrollView);
            buttonGameObject.GetComponent<UIItemButton>().SetItemButtonPrefab(itemSo, false);
            Button button = buttonGameObject.GetComponent<Button>();
            button.onClick.AddListener(() => ShopBuys(itemSo));
            _playerButtons.Add(button.GetComponent<Button>());
        }
    }

    private void ShopButtonsSetup()
    {
        foreach (ItemSO itemSo in shopInventorySo.GetAllItemsSo())
        {
            GameObject buttonGameObject = Instantiate(itemButtonPrefab.gameObject, shopScrollView);
            buttonGameObject.GetComponent<UIItemButton>().SetItemButtonPrefab(itemSo, true);
            Button button = buttonGameObject.GetComponent<Button>();
            button.onClick.AddListener(() => ShopSells(itemSo));
            _shopButtons.Add(button.GetComponent<Button>());
        }
    }

    private void UpdatePlayerButtons()
    {
        foreach (var playerButton in _playerButtons)
        {
            Destroy(playerButton.gameObject);
        }
        _playerButtons.Clear();
        PlayerButtonsSetup();
    }

    private void OnDisable()
    {
        foreach (var playerButton in _playerButtons)
        {
            Destroy(playerButton.gameObject);
        }
        _playerButtons.Clear();
        foreach (var shopButton in _shopButtons)
        {
            Destroy(shopButton.gameObject);
        }
        _shopButtons.Clear();
    }

    public void ShopSells(ItemSO itemSo)
    {
        if (itemSo.GetPrice() <= GameManager.Instance.GetGold())
        {
            GameManager.Instance.AddGold(itemSo.GetPrice() * -1);
            playerInventorySo.AddItem(itemSo);
            UpdatePlayerButtons();
        }
    }

    public void ShopBuys(ItemSO itemSo)
    {
        int price = Mathf.RoundToInt(itemSo.GetPrice() * 0.3f);
        if (price == 0) price = 1;
        
        bool couldSell = playerInventorySo.SubstractItem(itemSo);
        if (couldSell)
        {
            GameManager.Instance.AddGold(price);
            UpdatePlayerButtons();
            switch (itemSo.GetItemType())
            {
                case ItemSO.ItemType.Uniform:
                    if (playerInventorySo.GetUniformsSo().Count == 1)
                    {
                        outfitSo.SetUniform(playerInventorySo.GetUniformsSo()[0].GetItemSprites());
                    }
                    break;
                case ItemSO.ItemType.Weapon:
                    if (playerInventorySo.GetWeaponsSo().Count == 1)
                    {
                        outfitSo.SetWeapon(playerInventorySo.GetWeaponsSo()[0].GetItemSprites());
                    }
                    break;
                case ItemSO.ItemType.Boots:
                    if (playerInventorySo.GetBootsSo().Count == 1)
                    {
                        outfitSo.SetBoots(playerInventorySo.GetBootsSo()[0].GetItemSprites());
                    }
                    break;
            }
        }
        else
        {
            switch (itemSo.GetItemType())
            {
                case ItemSO.ItemType.Uniform:
                    outfitSo.SetUniform(playerInventorySo.GetUniformsSo()[0].GetItemSprites());
                    break;
                case ItemSO.ItemType.Weapon:
                    outfitSo.SetWeapon(playerInventorySo.GetWeaponsSo()[0].GetItemSprites());
                    break;
                case ItemSO.ItemType.Boots:
                    outfitSo.SetBoots(playerInventorySo.GetBootsSo()[0].GetItemSprites());
                    break;
            }
        }
        Player.Instance.SetOutfit();
        
    }
}
