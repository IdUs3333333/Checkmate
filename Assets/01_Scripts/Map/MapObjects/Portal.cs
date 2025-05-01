using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;

    private Player player;
    public MapType portalType;

    public float interactDist = 1.5f;

    private void Start()
    {
        interactionButton.SetActive(false);
        player = GameManager.Instance.player;
    }

    public void Init()
    {
        portalType = (MapType)Random.Range(0, 4);
    }

    public void Init(MapType exception)
    {
        portalType = exception;
    }

    private void Update()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        Debug.Log("dist : " + dist);
        interactionButton.SetActive(dist <= interactDist);

        if(Input.GetKeyDown(KeyCode.F) && interactionButton.activeSelf)
        {
            Interact();
        }
    }

    public void Interact()
    {
        GameManager.Instance.PortalInteract(portalType);
    }
}
