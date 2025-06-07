using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private SpriteRenderer portalIcon;
    [SerializeField] private List<Sprite> roomIcons;
    [SerializeField] private TextMeshPro portalText;

    private Player player;
    public MapType portalType;
    public Difficulty portalDifficulty;

    public bool isStartPortal = false;
    public bool isNear = false;
    public float interactDist = 1f;

    private void Start()
    {
        interactionButton.SetActive(false);
        player = GameManager.Instance.player;
    }

    public void Init(bool type = false)
    {
        Debug.Log($"Init({type})");

        isStartPortal = type;

        portalType = (MapType)Random.Range(0, 4);
        portalIcon.sprite = roomIcons[(int)portalType];
        portalIcon.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, type ? 0 : 1);

        portalText.color = new Color(1, 1, 1, type ? 1 : 0);
    }

    public void Init(MapType type)
    {
        Debug.Log($"Init({type})");

        isStartPortal = false;

        portalType = type;
        portalIcon.sprite = roomIcons[(int)portalType];
        portalIcon.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        portalText.color = new Color(1, 1, 1, 0);
    }

    public void Init(Difficulty diff)
    {
        Debug.Log($"Init({diff})");

        isStartPortal = true;

        portalType = (MapType)Random.Range(0, 4);
        portalIcon.sprite = roomIcons[(int)portalType];
        portalIcon.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        portalDifficulty = diff;
        portalText.text = diff == Difficulty.Easy ? "EASY" : "HARD";
        portalText.color = new Color(1, 1, 1, 1);
    }

    public void Init(MapType type, Difficulty diff, bool isStart)
    {
        isStartPortal = isStart;

        portalType = type;
        portalIcon.sprite = roomIcons[(int)portalType];
        portalIcon.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, isStart ? 0 : 1);

        portalDifficulty = diff;
        portalText.text = diff == Difficulty.Easy ? "EASY" : "HARD";
        portalText.color = new Color(1, 1, 1, isStart ? 1 : 0);
    }

    private void Update()
    {
        isNear = player.transform.position.IsNear(transform.position, interactDist);

        if(isNear)
        {
            player.currentPortal = this;
            interactionButton.SetActive(true);
        }
        else
        {
            interactionButton.SetActive(false);
        }
    }

    public void Interact()
    {
        if(isNear)
        {
            Debug.Log($"<color=#FFFF77>mapType</color> : <color=#FFFF77>{portalType}</color>");
            MapGenerator.Instance.GenerateMap(portalType);

            Debug.Log($"<color=#FFFF77>difficulty</color> : <color=#FFFF77>{portalDifficulty}</color>");
            GameManager.Instance.difficulty = portalDifficulty;
            MapGenerator.Instance.currentDifficulty = portalDifficulty;

            foreach(var portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            {
                Destroy(portal.gameObject);
            }
        }
    }
}
