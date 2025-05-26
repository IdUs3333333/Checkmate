using System.Collections;
using UnityEngine;

public interface IAttackStrategy
{
    IEnumerator ExecuteAttack(Player player, Transform attackTransform, Camera maincam);
}
