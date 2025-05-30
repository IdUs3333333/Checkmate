using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private SpriteRenderer portalIcon;
    [SerializeField] private List<Sprite> roomIcons;

    private Player player;
    public MapType portalType;

    public float interactDist = 1.5f;
    private bool isInteractRequired = false;

    private void Start()
    {
        interactionButton.SetActive(false);
        player = GameManager.Instance.player;
        player.input.OnInteract += OnInteract;
    }

    public void Init()
    {
        portalType = (MapType)Random.Range(0, 4);
        portalIcon.sprite = roomIcons[(int)portalType];
    }

    public void Init(MapType type)
    {
        portalType = type;
        portalIcon.sprite = roomIcons[(int)portalType];
    }

    private void OnInteract()
    {
        isInteractRequired = true;
    }

    private void Update()
    {
        bool isNear = player.transform.position.IsNear(transform.position, interactDist);
        interactionButton.SetActive(isNear);

        if(isInteractRequired && interactionButton.activeSelf && isNear)
        {
            isInteractRequired = false;
            Interact();
        }
    }

    public void Interact()
    {
        GameManager.Instance.PortalInteract(portalType);
        Destroy(gameObject);
    }
}
