using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image[] hpHearts;
    [SerializeField] private Sprite[] heartSprites;

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
        // Debug
        if (Input.GetKeyDown(KeyCode.R)) Damage();
        if (Input.GetKeyDown(KeyCode.T)) Heal();
    }

    public void Damage(int value = 1)
    {
        if(playerHP > 0)
        {
            playerHP = Mathf.Clamp(playerHP - value, 0, 4);

            Image heart = hpHearts[playerHP];
            heart.sprite = heartSprites[1];
            heart.gameObject.transform.DOComplete();
            heart.gameObject.transform.DOShakePosition(0.5f, 10, 100);

            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(0.25f, 1, 100);

            if(playerHP <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public void Heal(int value = 1)
    {
        if(playerHP < 4)
        {
            Image heart = hpHearts[playerHP];
            heart.sprite = heartSprites[0];
            heart.gameObject.transform.DOComplete();
            heart.gameObject.transform.DOShakeScale(0.25f, 0.5f);

            playerHP = Mathf.Clamp(playerHP + value, 0, 4);
        }
    }
}
