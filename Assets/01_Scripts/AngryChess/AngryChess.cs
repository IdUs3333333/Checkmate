using UnityEngine;

public class AngryChess : MonoBehaviour
{
    [SerializeField] private Transform LaunchPoint;
    [SerializeField] private GameObject PlayerChess;
    [SerializeField] private GameObject EnemyChess;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

        }
    }
}
