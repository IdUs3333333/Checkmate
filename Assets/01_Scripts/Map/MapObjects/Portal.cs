using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;

    private Player player;
    public MapType portalType;

    public float interactDist = 1.5f;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    public void Init()
    {

    }

    private void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        interactionButton.SetActive(dist <= interactDist);
    }

    public void Interact()
    {
        MapType randomMapType = (MapType)Random.Range(0, 4);
        GameManager.Instance.PortalInteract(randomMapType);
    }
}
