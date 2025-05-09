using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private SpriteRenderer portalIcon;
    [SerializeField] private List<Image> roomIcons;

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
        portalIcon.sprite = roomIcons[(int)portalType].sprite;
    }

    public void Init(MapType type)
    {
        portalType = type;
        portalIcon.sprite = roomIcons[(int)portalType].sprite;
    }

    private void OnInteract()
    {
        isInteractRequired = true;
    }

    private void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        interactionButton.SetActive(dist <= interactDist);

        if(isInteractRequired && interactionButton.activeSelf)
        {
            isInteractRequired = false;
            Interact();
        }
    }

    public void Interact()
    {
        GameManager.Instance.PortalInteract(portalType);
    }
}
