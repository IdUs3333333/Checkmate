using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    private GameObject attackRange;
    private GameObject rangeGauge;

    private void Awake()
    {
        attackRange = transform.Find("AttackRange").gameObject;
        rangeGauge = attackRange.transform.GetChild(0).gameObject;

        Active(false);
    }

    public void Active(bool value = true)
    {
        attackRange.SetActive(value);

        if(value)
        {
            Vector2 dir = (GameManager.Instance.player.transform.position - gameObject.transform.position).normalized;
            attackRange.transform.position = (Vector2)transform.position + dir * 2f;
            attackRange.transform.right = dir;
        }
    }

    public void SetGauge(float percent)
    {
        percent = Mathf.Clamp01(percent);
        rangeGauge.transform.localPosition = new Vector2(percent - 1f, 0f);
    }
}
