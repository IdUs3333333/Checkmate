using UnityEngine;

public class Box_kjy : MonoBehaviour
{
    [SerializeField] private float boxHp; //몇번 맞아야 부서지는지

    private float currentBoxHp = 0; // 몇번 맞았는지
    private bool dieTrigger = false;

    private void Awake()
    {
        currentBoxHp = boxHp;
    }

    public void Damage()
    {
        currentBoxHp = Mathf.Clamp(currentBoxHp - 1, 0f, boxHp);
    }

    private void Update()
    {
        if (currentBoxHp <= 0 && !dieTrigger)
        {
            dieTrigger = !dieTrigger;
            Destroy(this.gameObject);
            return;
        }
    }

}
