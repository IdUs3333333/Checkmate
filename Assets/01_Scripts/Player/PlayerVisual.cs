using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Dictionary<ChessType, Sprite> chessSprite;
    PlayerInput input;
    private bool isMoving = false;

    private void Awake()
    {
        input = transform.parent.GetComponent<PlayerInput>();
        input.OnMove += MoveVisual;
    }

    private void MoveVisual(Vector2 movement)
    {
        if(isMoving == false && movement.magnitude > 0)
        {
            StartCoroutine("MoveCoroutine");
            isMoving = true;
        }
        else if(isMoving == true && movement.magnitude == 0)
        {
            StopCoroutine("MoveCoroutine");
            DOTween.Clear();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            isMoving = false;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        transform.DORotate(new Vector3(0, 0, 10), 0.15f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(0.2f);
        yield return null;
        transform.DORotate(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.15f);
        yield return null;
        transform.DORotate(new Vector3(0, 0, -10), 0.15f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(0.2f);
        yield return null;
        transform.DORotate(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.15f);
        yield return null;
        StartCoroutine("MoveCoroutine");
    }

    private void OnDestroy()
    {
        input.OnMove -= MoveVisual;
    }
}
