using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AYellowpaper.SerializedCollections;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ChessType, Sprite> chessSprite;

    private PlayerInput input;
    private SpriteRenderer sprite;

    [SerializeField] private Material whiteMaterial;
    
    private bool isMoving = false;

    private void Awake()
    {
        input = transform.parent.GetComponent<PlayerInput>();
        sprite = GetComponent<SpriteRenderer>();
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
            transform.DOComplete();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            isMoving = false;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        WaitForSeconds ws1 = new WaitForSeconds(0.2f);
        WaitForSeconds ws2 = new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, 0, 10), 0.15f).SetEase(Ease.OutSine);
        yield return ws1;
        transform.DORotate(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InSine);
        yield return ws2;
        transform.DORotate(new Vector3(0, 0, -10), 0.15f).SetEase(Ease.OutSine);
        yield return ws1;
        transform.DORotate(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.InSine);
        yield return ws2;
        StartCoroutine("MoveCoroutine");
    }

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        Material baseMaterial = sprite.material;
        sprite.material = whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        sprite.material = baseMaterial;
    }

    private void OnDestroy()
    {
        input.OnMove -= MoveVisual;
    }
}
