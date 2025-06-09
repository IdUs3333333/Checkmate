using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject hpPrefab;
    [SerializeField] private Sprite[] heartSprites;

    private List<Image> hpHearts = new List<Image>();
    private Player player;

    public int playerHP = 4;
    public float invincibleDuration = 0.5f;
    public bool isInvincible = false;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerHP = player.stats.hp;
        for(int i = 0; i < playerHP; i++)
        {
            GameObject hpObject = Instantiate(hpPrefab, transform);
            Image heartImage = hpObject.GetComponent<Image>();
            hpHearts.Add(heartImage);
        }

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(0.25f, 1, 100);
        }
    }

    public void Damage(int value = 1, bool isDebug = false)
    {
        if (!isDebug)
        {
            int percent = Random.Range(1, 101);
            if (percent <= player.stats.evasionChance) return;
        }

        if (playerHP > 0)
        {
            int prevHP = playerHP;
            playerHP = Mathf.Clamp(playerHP - value, 0, player.stats.hp);

            for (int i = playerHP; i < prevHP; i++)
            {
                hpHearts[i].gameObject.transform.DOComplete();
                hpHearts[i].gameObject.transform.DOShakePosition(0.5f, 10, 100);
            }

            UpdateHeartSprites();

            if (playerHP <= 0)
            {
                GameManager.Instance.GameOver();
            }

            StartCoroutine(InvincibilityTimer());
        }
    }

    private IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    public void Heal(int value = 1)
    {
        if(playerHP < player.stats.hp)
        {
            int prevHP = playerHP;
            playerHP = Mathf.Clamp(playerHP + value, 0, player.stats.hp);
            
            for(int i = prevHP; i < playerHP; i++)
            {
                hpHearts[i].gameObject.transform.DOComplete();
                hpHearts[i].gameObject.transform.DOShakeScale(0.25f, 0.5f);
            }

            UpdateHeartSprites();
        }
    }

    public void RefreshHP()
    {
        int maxHP = player.stats.hp;

        while(hpHearts.Count < maxHP)
        {
            GameObject hpObject = Instantiate(hpPrefab, transform);
            Image heartImage = hpObject.GetComponent<Image>();
            heartImage.sprite = heartSprites[0];
            hpHearts.Add(heartImage);
        }

        while (hpHearts.Count < maxHP)
        {
            Destroy(hpHearts[hpHearts.Count - 1].gameObject);
            hpHearts.RemoveAt(hpHearts.Count - 1);
        }

        UpdateHeartSprites();
    }

    private void UpdateHeartSprites()
    {
        for (int i = 0; i < hpHearts.Count; i++)
        {
            if (i < playerHP) hpHearts[i].sprite = heartSprites[0];
            else hpHearts[i].sprite = heartSprites[1];
        }
    }
}
