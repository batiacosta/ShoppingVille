using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerOutfitManager : MonoBehaviour
{
    [SerializeField] private OutfitSO outfitSo;
    [SerializeField] private List<SpriteRenderer> uniformSprites;
    [SerializeField] private List<SpriteRenderer> weaponSprites;
    [SerializeField] private List<SpriteRenderer> bootsSprites;

    private InventorySO _playerInventorySo;
    private List<Sprite> _currentUniform = new List<Sprite>();
    private List<Sprite> _currentWeapon = new List<Sprite>();
    private List<Sprite> _currenBoots = new List<Sprite>();

    private void Start()
    {
        SetOutfitSo();
        SetOutfit();
    }

    private void SetOutfitSo()
    {
        _playerInventorySo = Player.Instance.GetPlayerInventory();
        outfitSo.SetUniform(_playerInventorySo.GetUniformsSo()[0].GetItemSprites());
        outfitSo.SetWeapon(_playerInventorySo.GetWeaponsSo()[0].GetItemSprites());
        outfitSo.SetBoots(_playerInventorySo.GetBootsSo()[0].GetItemSprites());
    }

    public void SetOutfit()
    {
        SetUniform();

        SetWeapons();

        SetBoots();
    }

    private void SetBoots()
    {
        _currenBoots = outfitSo.GetBoots();
        for (int i = 0; i < bootsSprites.Count; i++)
        {
            bootsSprites[i].sprite = _currenBoots[i];
        }
    }

    private void SetWeapons()
    {
        _currentWeapon = outfitSo.GetWeapon();
        for (int i = 0; i < weaponSprites.Count; i++)
        {
            weaponSprites[i].sprite = _currentWeapon[i];
        }
    }

    private void SetUniform()
    {
        _currentUniform = outfitSo.GetUniform();
        for (int i = 0; i < uniformSprites.Count; i++)
        {
            uniformSprites[i].sprite = _currentUniform[i];
        }
    }
}
