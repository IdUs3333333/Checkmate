using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private float interactDist = 1f;

    private Player player;
    private Animator animator;
    private bool isNear = false;

    private void Start()
    {
        interactionButton.SetActive(false);
        player = GameManager.Instance.player;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isNear = player.transform.position.IsNear(transform.position, interactDist);

        if (isNear)
        {
            player.currentChest = this;
            interactionButton.SetActive(true);
        }
        else
        {
            interactionButton.SetActive(false);
        }
    }

    public void Interact()
    {
        animator.SetTrigger("Open");
        Invoke("OpenUI", 0.6f);
    }


    private void OpenUI()
    {
        GameManager.Instance.OpenReinforceUI();
    }
}
