using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IntroIcon : MonoBehaviour
{
    [SerializeField] private Object gameIcon;
    [SerializeField] private TextMeshProUGUI startKeyText;
    [SerializeField] private List<GameObject> point = new List<GameObject>();

    [SerializeField] private float moveDuration = 3f;
    private bool isStart = false;

    private void Awake()
    {
        startKeyText.gameObject.SetActive(false);
    }

    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(point[0].transform.position, moveDuration).SetEase(Ease.Linear));
        seq.Append(transform.DOMove(point[1].transform.position, 0).SetEase(Ease.Linear));
        seq.Append(transform.DOMove(point[2].transform.position, moveDuration).SetEase(Ease.Linear));
        seq.Append(transform.DOMove(point[3].transform.position, 0).SetEase(Ease.Linear));
        seq.Append(transform.DOMove(point[4].transform.position, moveDuration).SetEase(Ease.Linear));
        seq.Join(transform.DOScale(Vector3.zero, moveDuration));

        seq.Append(transform.DOScale(new Vector3(1, 1, 1), 0.7f));
        seq.Join(transform.DORotate(new Vector3(0, 0, 2160), 0.7f, RotateMode.FastBeyond360));
        seq.AppendCallback(StartTextOn);
    }

    void Update()
    {
        if(isStart is true)
        {
            if(Input.anyKeyDown)
            {
                //SceneManager.LoadScene();  한번 합치자
            }
        }
    }

    void StartTextOn()
    {
        startKeyText.gameObject.SetActive(true);
        startKeyText.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 0, 4), 2f).From(new Vector3(0,0,-4)).SetLoops(-1, LoopType.Yoyo);
        isStart = true;
    }
}
