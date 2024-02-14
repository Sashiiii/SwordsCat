using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Weapon", fileName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    [System.Serializable]
    public struct Combo
    {
        public uint Damage;
        public string Animation;
    }

    public List<Combo> Combos = new List<Combo>();
}