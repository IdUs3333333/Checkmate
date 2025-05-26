using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [Header("Attack Particles")]
    public GameObject pawnAttackParticle;

    private Camera mainCam;
    private Dictionary<ChessType, IAttackStrategy> attackStrategies;

    private bool isAttacking = false;
    private float attackRange = 0f;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        mainCam = Camera.main;

        attackStrategies = new Dictionary<ChessType, IAttackStrategy>
        {
            { ChessType.Pawn, new PawnAttack() }
        };
    }

    public void Attack()
    {
        if (!isAttacking && attackStrategies.TryGetValue(player.type, out var strategy))
        {
            StartCoroutine(AttackCoroutine(strategy));
        }
    }

    private IEnumerator AttackCoroutine(IAttackStrategy strategy)
    {
        isAttacking = true;
        attackRange = player.stats.attackRange[(int)player.type];
        yield return strategy.ExecuteAttack(player, transform, mainCam);
        yield return new WaitForSeconds(player.stats.attackSpeed);
        isAttacking = false;
    }

    public void SubSkill()
    {

    }

    public void MainSkill()
    {

    }
}
