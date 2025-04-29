using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }


}
