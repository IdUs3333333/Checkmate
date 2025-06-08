using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReinforcementPanel : MonoBehaviour
{
    [SerializeField] private ReinforcementCardUI[] cards;

    public List<PlayerReinforcementSO> allReinforcements;
    public List<PlayerReinforcementSO> available;

    private void Start()
    {
        available = allReinforcements.Where(r => GameManager.Instance.player.stats.CanUpgrade(r)).ToList();
    }

    public void Init()
    {
        available = allReinforcements.Where(r => GameManager.Instance.player.stats.CanUpgrade(r)).ToList();

        if(available.Count == 0)
        {
            GameManager.Instance.CloseReinforceUI();
        }

        List<PlayerReinforcementSO> options = GetRandomChoices(Mathf.Min(3, available.Count));
        for(int i = 0; i < cards.Length; i++)
        {
            if(i < options.Count)
            {
                cards[i].SetCard(options[i]);
                cards[i].gameObject.SetActive(true);
            }
            else
            {
                cards[i].gameObject.SetActive(false);
            }
        }
    }

    public List<PlayerReinforcementSO> GetRandomChoices(int count = 3)
    {
        List<PlayerReinforcementSO> shuffled = new List<PlayerReinforcementSO>(available);
        for(int i = 0; i < shuffled.Count; i++)
        {
            int j = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }
        return shuffled.Take(count).ToList();
    }
}
