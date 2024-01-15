using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/PlayerStats", fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public bool CanDash = true;
    public bool CanRoll = true;
}