using UnityEngine;

[CreateAssetMenu(menuName = "Player/Reinforcement")]
public class PlayerReinforcementSO : ScriptableObject
{
    public int id;
    public string displayName;
    public Sprite icon;
    public string description;

    public StatType statType;
    public float[] values;
}
