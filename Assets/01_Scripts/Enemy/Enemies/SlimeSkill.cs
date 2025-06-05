using UnityEngine;

public class SlimeSkill : MonoBehaviour
{
    private GameObject skillRange;
    private GameObject rangeGauge;

    private void Awake()
    {
        skillRange = transform.Find("SkillRange").gameObject;
        rangeGauge = skillRange.transform.GetChild(0).gameObject;

        Active(false);
    }

    public void Active(bool value = true)
    {
        skillRange.SetActive(value);
    }

    public void SetGauge(float percent)
    {
        percent = Mathf.Clamp01(percent);
        rangeGauge.transform.localScale = Vector3.one * percent;
    }
}
