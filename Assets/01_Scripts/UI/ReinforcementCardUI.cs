using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

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
        transform.DOShakeScale(0.25f, 0.2f);
        transform.DOShakePosition(0.1f, 10, 100);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            GameManager.Instance.player.stats.AddReinforces(data);
            GameManager.Instance.player.hp.RefreshHP();
            if (data.statType == StatType.HP) GameManager.Instance.player.hp.Heal();
            GameManager.Instance.reinforcePanel.available = 
                GameManager.Instance.reinforcePanel.allReinforcements
                .Where(r => GameManager.Instance.player.stats.CanUpgrade(r)).ToList();
            GameManager.Instance.CloseReinforceUI();
        });
    }
}
