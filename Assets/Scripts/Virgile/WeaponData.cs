using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")] 
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int id;
    public float weaponPrice;
    public float weaponPaintVolumeRequire;
    public float weaponFireRate;
    //public GameObject weaponPrefab;
    //public GameObject weaponProjectilePrefab;
    public Sprite weaponIcon;
}
