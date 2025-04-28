using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.OnMove += Move;
    }

    private void Move(Vector2 movement)
    {
        transform.position += (Vector3)movement;
    }

    private void OnDestroy()
    {
        input.OnMove -= Move;
    }
}
