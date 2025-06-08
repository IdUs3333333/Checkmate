using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MysteryPanel : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button rollSelectButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI shadowText;

    [SerializeField] private List<MysteryEventSO> allMysteryEvents;
    [SerializeField] private List<GameObject> eventEntities;

    private MysteryEventSO selected;

    private bool isRolled;

    private void Start()
    {
        rollSelectButton.onClick.AddListener(ButtonInteract);
        rollSelectButton.interactable = false;
    }

    public void Init()
    {
        isRolled = false;
        rollSelectButton.interactable = true;
        icon.sprite = null;
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0);
        nameText.text = string.Empty;
        descText.text = string.Empty;
    }

    private void ButtonInteract()
    {
        if (isRolled) Confirm();
        else StartRoll();
    }

    private void StartRoll()
    {
        rollSelectButton.interactable = false;
        buttonText.text = "CONFIRM";
        shadowText.text = buttonText.text;
        isRolled = true;

        StartCoroutine(RollingCoroutine());
    }

    private IEnumerator RollingCoroutine()
    {
        float rollDuration = 2f;
        float elapsed = 0f;

        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1);
        while (elapsed < rollDuration)
        {
            selected = allMysteryEvents[Random.Range(0, allMysteryEvents.Count)];
            UpdateCardUI(selected);
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        card.GetComponent<Image>().color = new Color(0.31f, 0.33f, 0.44f, 1f);
        card.GetComponent<Image>().DOColor(Color.white, 0.2f).SetEase(Ease.OutSine);
        card.transform.DOShakeScale(0.25f, 0.5f);
        rollSelectButton.interactable = true;
    }

    private void Confirm()
    {
        rollSelectButton.interactable = false;
        buttonText.text = "ROLL";
        shadowText.text = buttonText.text;
        isRolled = false;

        ApplyEffect(selected);
    }

    private void UpdateCardUI(MysteryEventSO data)
    {
        icon.sprite = data.icon;
        nameText.text = data.eventName;
        descText.text = data.eventDesc;
    }

    private void ApplyEffect(MysteryEventSO data)
    {
        switch(data.type)
        {
            case EventType.Heal:
                GameManager.Instance.player.hp.Heal(1);
                break;
            case EventType.SpawnEnemy:
                Instantiate(eventEntities[0], (Vector2)GameManager.Instance.player.transform.position + new Vector2(0, 5), Quaternion.identity);
                break;
            case EventType.Chest:
                Instantiate(eventEntities[1], (Vector2)GameManager.Instance.player.transform.position, Quaternion.identity);
                break;
        }

        GameManager.Instance.CloseEventUI();
    }
}
