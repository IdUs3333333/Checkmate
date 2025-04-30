using UnityEngine;
using TMPro;
using SaveSystem.Manager;
using SaveSystem.Data;
public class ManagerTest : MonoBehaviour
{
    public TMP_InputField scroe;
    public TMP_InputField record;

    public DataManager dataManager;

    

    private string scoretxt;
    private string recordtxt;

    public void Checkscore()
    {
        scoretxt = scroe.text;
        dataManager.TrySetHighScore(Difficulty.Normal, int.Parse(scoretxt));
    }
    
    public void Checkrecord()
    {
        recordtxt = record.text;
        dataManager.TrySetPlayTime(Difficulty.Normal, int.Parse(recordtxt));
    }
}
