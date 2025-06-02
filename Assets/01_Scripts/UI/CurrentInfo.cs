using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrentInfo : MonoBehaviour
{
    [SerializeField] private Image RoomIcon;
    [SerializeField] private TextMeshProUGUI FloorText;

    [SerializeField] private List<Sprite> roomIcons;

    public void SetInfo()
    {
        RoomIcon.sprite = roomIcons[(int)MapGenerator.Instance.currentMapType];
        FloorText.text = $"{GameManager.Instance.currentFloor}F";
    }
}
