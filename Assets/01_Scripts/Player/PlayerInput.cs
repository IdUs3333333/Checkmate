using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    PlayerStats stats;
    public event Action<Vector2> OnMove;
    public event Action OnInteract;
    public event Action OnMainSkill;
    public event Action OnSubSkill;

    private void Awake()
    {
        player = GetComponent<Player>();
        stats = player.stats;
    }

    private void Update()
    {
        MoveInput();
        InteractInput();
    }

    private void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y) * stats.moveSpeed * Time.deltaTime;
        OnMove?.Invoke(movement);
    }

    private void InteractInput()
    {
        if(Input.GetKeyDown(KE.interact))
        {
            OnInteract?.Invoke();
        }
        if (Input.GetKeyDown(KE.mainSkill))
        {
            OnMainSkill?.Invoke();
        }
        if (Input.GetKeyDown(KE.subSkill))
        {
            OnSubSkill?.Invoke();
        }
    }
}
