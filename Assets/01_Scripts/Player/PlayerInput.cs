using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    PlayerStats stats;
    public event Action<Vector2> OnMove;

    private void Awake()
    {
        player = GetComponent<Player>();
        stats = player.stats;
    }

    private void Update()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y) * stats.moveSpeed * Time.deltaTime;
        OnMove?.Invoke(movement);
    }
}
