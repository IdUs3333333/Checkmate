using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private float interactDist = 1f;

    private Player player;
    private bool isNear = false;

    private void Start()
    {
        interactionButton.SetActive(false);
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        isNear = player.transform.position.IsNear(transform.position, interactDist);

        if (isNear)
        {
            player.currentStatue = this;
            interactionButton.SetActive(true);
        }
        else
        {
            interactionButton.SetActive(false);
        }
    }

    public void Interact()
    {
        GameManager.Instance.OpenEventUI();
    }
}
