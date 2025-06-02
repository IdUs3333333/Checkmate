using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SkeletonBone : MonoBehaviour
{
    private Vector2 dir = Vector2.right;
    private float lifetime = 3f;
    private float range = 7.5f;

    public void Shoot(Vector2 _dir, float _lifetime, float _range)
    {
        dir = _dir;
        lifetime = _lifetime;
        range = _range;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        lifetime -= 0.25f;

        transform.DOMove((Vector2)transform.position + dir * range, lifetime / 2f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(lifetime / 2f + 0.25f);
        transform.DOMove((Vector2)transform.position - dir * range, lifetime / 2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(lifetime / 2f + 0.1f);
        RemoveSelf();
    }

    private void RemoveSelf()
    {
        Destroy(gameObject);
    }
}
