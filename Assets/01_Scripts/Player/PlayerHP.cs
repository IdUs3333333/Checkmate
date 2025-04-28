using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] Image[] hpHearts;
    [SerializeField] Sprite[] heartSprites;
    public int playerHP = 4;

    private void Awake()
    {
        playerHP = 4;
        foreach(Image heart in hpHearts)
        {
            heart.sprite = heartSprites[0];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Damage();
        if (Input.GetKeyDown(KeyCode.T)) Heal();
    }

    public void Damage()
    {
        if(playerHP > -1)
        {
            playerHP--;
            playerHP = Mathf.Clamp(playerHP, 0, 4);

            hpHearts[playerHP].sprite = heartSprites[1];
            hpHearts[playerHP].gameObject.transform.DOShakePosition(0.5f, 10, 100);
        }
    }

    public void Heal()
    {
        if(playerHP < 4)
        {
            hpHearts[playerHP].sprite = heartSprites[0];
            hpHearts[playerHP].gameObject.transform.DOShakeScale(0.25f, 0.5f);

            playerHP++;
            playerHP = Mathf.Clamp(playerHP, 0, 4);
        }
    }
}
