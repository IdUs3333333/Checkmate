using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Sprite interactionButton;

    private void Update()
    {
        
    }

    public void Interact()
    {
        MapType randomMapType = (MapType)Random.Range(0, 4);
        GameManager.Instance.PortalInteract(randomMapType);
    }
}
