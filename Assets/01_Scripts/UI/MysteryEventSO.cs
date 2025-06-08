using UnityEngine;

[CreateAssetMenu(menuName = "SO/Mystery Events")]
public class MysteryEventSO : ScriptableObject
{
    public Sprite icon;
    public string eventName;
    public string eventDesc;
    public EventType type;
}
