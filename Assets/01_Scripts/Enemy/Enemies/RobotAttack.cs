using System.Collections;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    [SerializeField] private GameObject lazer;
    [SerializeField] private GameObject subLazer;

    private SpriteRenderer lazerSprite;
    private SpriteRenderer subLazerSprite;
    private BoxCollider2D lazerCollider;

    private void Awake()
    {
        lazer = transform.Find("Lazer").gameObject;
        subLazer = transform.Find("SubLazer").gameObject;

        lazerSprite = lazer.GetComponent<SpriteRenderer>();
        subLazerSprite = subLazer.GetComponent<SpriteRenderer>();
        lazerCollider = lazer.GetComponent<BoxCollider2D>();

        Active(false);
    }

    public void Active(float duration)
    {
        Active(true, duration);
    }

    public void Active(bool value = true, float duration = 1f)
    {
        Debug.Log($"Active {value}");

        lazerSprite.color = new Color(1, 0, 0, 0f);
        subLazerSprite.color = new Color(1, 0, 0, 0f);
        lazerCollider.enabled = false;

        if (value)
        {
            Debug.Log("Robot Attack!");

            Vector2 dir = (GameManager.Instance.player.transform.position - gameObject.transform.position).normalized;

            lazer.transform.position = (Vector2)transform.position + dir * transform.localScale.x * 5;
            lazer.transform.right = dir;
            lazer.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);

            subLazer.transform.position = lazer.transform.position;
            subLazer.transform.right = lazer.transform.right;
            subLazer.GetComponent<SpriteRenderer>().color = lazer.GetComponent<SpriteRenderer>().color;

            StartCoroutine(LazerCoroutine(duration));
        }
    }

    private IEnumerator LazerCoroutine(float duration)
    {
        for (int i = 0; i < 9; i++)
        {
            subLazerSprite.color += new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(duration * 0.1f);
        }

        lazerSprite.color = new Color(1, 0, 0, 1);
        lazerCollider.enabled = true;
        subLazerSprite.color = new Color(1, 0, 0, 0);

        yield return new WaitForSeconds(duration * 0.1f);
        lazerCollider.enabled = false;

        for (int i = 0; i < 9; i++)
        {
            lazerSprite.color -= new Color(0, 0, 0, 0.11f);
            yield return new WaitForSeconds(duration * 0.01f);
        }

        lazerSprite.color = new Color(0, 0, 0, 0f);
    }
}
