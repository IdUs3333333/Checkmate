using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    [SerializeField] private Image logoImage;
    [SerializeField] private TextMeshProUGUI pressKeyText;

    private bool canInteract = true;
    private bool isLogoLeft = false;

    private void Awake()
    {
        LogoImageAnimation();
        PressKeyAnimation();
        canInteract = true;
    }

    private void LogoImageAnimation()
    {
        logoImage.transform.DORotate(isLogoLeft ? new Vector3(0, 0, -5f) : new Vector3(0, 0, 5f), 0.8f)
            .SetEase(Ease.InOutSine);
        isLogoLeft = !isLogoLeft;
        Invoke("LogoImageAnimation", 1.2f);
    }

    private void PressKeyAnimation()
    {
        pressKeyText.color = new Color(1, 1, 1, (pressKeyText.color.a == 1) ? 0 : 1);
        Invoke("PressKeyAnimation", 0.5f);
    }

    private void Update()
    {
        if(Input.anyKeyDown && canInteract)
        {
            canInteract = false;
            SE.LoadScene(SE.lobby);
        }
    }
}
