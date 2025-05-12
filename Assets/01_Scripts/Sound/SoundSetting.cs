using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

namespace Sound.System
{
    public class SoundSetting : MonoBehaviour
    {
        Sequence _open;
        Sequence _close;

        [SerializeField] private Image _backgroundImg;
        [SerializeField] private Image _ExitBtn;

        [SerializeField] private List<TextMeshProUGUI> _txtList = new List<TextMeshProUGUI>();
        [SerializeField] private List<Slider> _sliderList = new List<Slider>();

        private bool _UIonoff = false;

        private void Awake()
        {
            _open = DOTween.Sequence();
            _close = DOTween.Sequence();
        }

        public void OnUIOpening()
        {
            _open.Append(_backgroundImg.rectTransform.DOAnchorPosY(0, 0.5f).SetEase(Ease.Linear));
            _open.Join(_ExitBtn.rectTransform.DOAnchorPosY(320, 0.6f).SetEase(Ease.Linear));

            _open.Join(_txtList[0].rectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutSine));
            _open.Join(_txtList[1].rectTransform.DOAnchorPosX(0, 0.6f).SetEase(Ease.OutSine));
            _open.Join(_txtList[2].rectTransform.DOAnchorPosX(0, 0.7f).SetEase(Ease.OutSine));

            _open.Join(_sliderList[0].GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f).SetEase(Ease.OutSine));
            _open.Join(_sliderList[1].GetComponent<RectTransform>().DOAnchorPosX(0, 0.6f).SetEase(Ease.OutSine));
            _open.Join(_sliderList[2].GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).SetEase(Ease.OutSine));
        }

        public void OnUICloseing()
        {
            _close.Append(_backgroundImg.rectTransform.DOAnchorPosY(1200, 0.5f).SetEase(Ease.Linear));
            _close.Join(_ExitBtn.rectTransform.DOAnchorPosY(1520, 0.6f).SetEase(Ease.Linear));

            _close.Join(_txtList[0].rectTransform.DOAnchorPosX(1300, 0.5f).SetEase(Ease.InSine));
            _close.Join(_txtList[1].rectTransform.DOAnchorPosX(1300, 0.6f).SetEase(Ease.InSine));
            _close.Join(_txtList[2].rectTransform.DOAnchorPosX(1300, 0.7f).SetEase(Ease.InSine));

            _close.Join(_sliderList[0].GetComponent<RectTransform>().DOAnchorPosX(-1300, 0.5f).SetEase(Ease.InSine));
            _close.Join(_sliderList[1].GetComponent<RectTransform>().DOAnchorPosX(-1300, 0.6f).SetEase(Ease.InSine));
            _close.Join(_sliderList[2].GetComponent<RectTransform>().DOAnchorPosX(-1300, 0.7f).SetEase(Ease.InSine));
        }
    }
}