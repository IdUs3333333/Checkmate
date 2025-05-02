using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerInput input;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInput>();
    }


}
