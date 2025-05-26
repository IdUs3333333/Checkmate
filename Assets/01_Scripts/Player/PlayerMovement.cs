using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;

    public bool canMove = true;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.OnMove += Move;
    }

    private void Move(Vector2 movement)
    {
        if(canMove) transform.position += (Vector3)movement;
    }

    private void OnDestroy()
    {
        input.OnMove -= Move;
    }
}
