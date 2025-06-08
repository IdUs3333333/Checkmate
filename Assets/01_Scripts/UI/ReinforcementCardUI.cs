using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ReinforcementCardUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button selectButton;

    public void SetCard(PlayerReinforcementSO data)
    {
        icon.sprite = data.icon;
        nameText.text = data.displayName;
        descText.text = data.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            GameManager.Instance.player.stats.AddReinforces(data);
            GameManager.Instance.player.hp.RefreshHP();
            GameManager.Instance.player.hp.Heal();
            GameManager.Instance.reinforcePanel.available = 
                GameManager.Instance.reinforcePanel.allReinforcements
                .Where(r => GameManager.Instance.player.stats.CanUpgrade(r)).ToList();
            GameManager.Instance.CloseReinforceUI();
        });
    }
}
