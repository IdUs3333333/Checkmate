using System.Collections.Generic;
using UnityEngine;

public class ReinforcementPanel : MonoBehaviour
{
    [SerializeField] private ReinforcementCardUI[] cards;

    public List<PlayerReinforcementSO> allReinforcements;

    public void Init()
    {
        List<PlayerReinforcementSO> options = GetRandomChoices();
        for(int i = 0; i < 3; i++)
        {
            cards[i].SetCard(options[i]);
        }
    }

    public List<PlayerReinforcementSO> GetRandomChoices(int count = 3)
    {
        List<PlayerReinforcementSO> shuffled = new List<PlayerReinforcementSO>(allReinforcements);
        for(int i = 0; i < shuffled.Count; i++)
        {
            int j = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }
        return shuffled.GetRange(0, count);
    }
}
