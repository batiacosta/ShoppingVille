using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UICabinManager : MonoBehaviour
{
    
    [SerializeField] private Button uniformLeftButton;
    [SerializeField] private Button uniformRightButton;
    [SerializeField] private Button weaponLeftButton;
    [SerializeField] private Button weaponRightButton;
    [SerializeField] private Button bootLeftButton;
    [SerializeField] private Button bootRightButton;
    [SerializeField] private OutfitSO outfitSo;
    [SerializeField] private List<Image> uniformSpritesUI;
    [SerializeField] private List<Image> weaponSpritesUI;
    [SerializeField] private List<Image> bootsSpritesUI;

    private int _uniformIndex = 0;
    private int _weaponIndex = 0;
    private int _bootsIndex = 0;
    private void Start()
    {
        uniformLeftButton.onClick.AddListener(() => ChangeUniform(false));
            uniformRightButton.onClick.AddListener(() => ChangeUniform(false));
        weaponLeftButton.onClick.AddListener(() => ChangeWeapon(false));
            weaponRightButton.onClick.AddListener(() => ChangeWeapon(true));
        bootLeftButton.onClick.AddListener(() => ChangeBoots(false));
            bootRightButton.onClick.AddListener(() => ChangeBoots(true));
    }

    private void ChangeUniform(bool isNext)
    {
        if (isNext)
        {
            _uniformIndex++;
            if (_uniformIndex >= Player.Instance.GetPlayerInventory().GetUniformsSo().Count) _uniformIndex = 0;
        }
        else
        {
            _uniformIndex--;
            if (_uniformIndex < 0) _uniformIndex = Player.Instance.GetPlayerInventory().GetUniformsSo().Count -1;
        }

        List<Sprite> uniformSprites = Player.Instance.GetPlayerInventory().GetUniformsSo()[_uniformIndex].GetItemSprites();
        for (int i = 0; i < this.uniformSpritesUI.Count; i++)
        {
            uniformSpritesUI[i].sprite = uniformSprites[i];
        }

        outfitSo.SetUniform(uniformSprites);
        Player.Instance.SetOutfit();
    }

    private void ChangeWeapon(bool isNext)
    {
        if (isNext)
        {
            _weaponIndex++;
            if (_weaponIndex >= Player.Instance.GetPlayerInventory().GetWeaponsSo().Count) _weaponIndex = 0;
        }
        else
        {
            _weaponIndex--;
            if (_weaponIndex <= 0) _weaponIndex = Player.Instance.GetPlayerInventory().GetWeaponsSo().Count -1;
        }
        
        List<Sprite> weaponSprites = Player.Instance.GetPlayerInventory().GetWeaponsSo()[_weaponIndex].GetItemSprites();
        for (int i = 0; i < this.weaponSpritesUI.Count; i++)
        {
            weaponSpritesUI[i].sprite = weaponSprites[i];
        }

        outfitSo.SetWeapon(weaponSprites);
        Player.Instance.SetOutfit();
    }
    private void ChangeBoots(bool isNext)
    {
        if (isNext)
        {
            _bootsIndex++;
            if (_bootsIndex >= Player.Instance.GetPlayerInventory().GetBootsSo().Count) _bootsIndex = 0;
        }
        else
        {
            _bootsIndex--;
            if (_bootsIndex <= 0) _bootsIndex = Player.Instance.GetPlayerInventory().GetBootsSo().Count -1;
        }
        
        List<Sprite> bootsSprites = Player.Instance.GetPlayerInventory().GetBootsSo()[_bootsIndex].GetItemSprites();
        for (int i = 0; i < this.bootsSpritesUI.Count; i++)
        {
            bootsSpritesUI[i].sprite = bootsSprites[i];
        }

        outfitSo.SetBoots(bootsSprites);
        Player.Instance.SetOutfit();
    }
}
