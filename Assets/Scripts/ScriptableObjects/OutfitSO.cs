using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu()]
public class OutfitSO : ScriptableObject
{
    [SerializeField] private List<Sprite> uniform;
    [SerializeField] private List<Sprite> weapon;
    [SerializeField] private List<Sprite> boots;

    public void SetUniform(List<Sprite> newUniform)
    {
        uniform = newUniform;
    }
    public List<Sprite> GetUniform() => uniform;
    
    public void SetBoots(List<Sprite> newBoots)
    {
        boots = newBoots;
    }
    public List<Sprite> GetBoots() => boots;
    
    public void SetWeapon(List<Sprite> newWeapon)
    {
        weapon = newWeapon;
    }
    public List<Sprite> GetWeapon() => weapon;
}
