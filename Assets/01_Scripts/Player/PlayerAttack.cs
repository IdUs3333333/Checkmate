using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;

    public void Attack()
    {
        Debug.Log("Attack");
        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    public void SubSkill()
    {

    }

    public void MainSkill()
    {

    }
}
