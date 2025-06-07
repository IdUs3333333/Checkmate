using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private float padding = 50f;

    private Dictionary<Transform, GameObject> enemyArrows = new Dictionary<Transform, GameObject>();

    private void Update()
    {
        Enemy[] foundEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach(Enemy enemy in foundEnemies)
        {
            Transform enemyPos = enemy.transform;
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyPos.position);
            bool isVisible = viewportPos.x >= 0 && viewportPos.x <= 1
                && viewportPos.y >= 0 && viewportPos.y <= 1
                && viewportPos.z >= 0;

            if(isVisible)
            {
                if(enemyArrows.ContainsKey(enemyPos))
                {
                    Destroy(enemyArrows[enemyPos]);
                    enemyArrows.Remove(enemyPos);
                }
                continue;
            }

            if (!enemyArrows.ContainsKey(enemyPos))
            {
                GameObject arrow = Instantiate(arrowPrefab, canvasRect);
                enemyArrows[enemyPos] = arrow;
            }
            UpdateArrowPosition(enemyPos, enemyArrows[enemyPos]);
        }
    }

    void UpdateArrowPosition(Transform enemyPos, GameObject arrowObj)
    {
        RectTransform arrow = arrowObj.GetComponent<RectTransform>();

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyPos.position);
        bool isVisible = viewportPos.x >= 0 && viewportPos.x <= 1
            && viewportPos.y >= 0 && viewportPos.y <= 1
            && viewportPos.z >= 0;
        if(isVisible)
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemyPos.position);
        if(screenPos.z < 0) screenPos *= -1f;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 dir = ((Vector2)screenPos - screenCenter).normalized;

        float clampedX = Mathf.Clamp(screenCenter.x + dir.x * ((Screen.width / 2f) - padding), padding, Screen.width - padding);
        float clampedY = Mathf.Clamp(screenCenter.y + dir.y * ((Screen.width / 2f) - padding), padding, Screen.height - padding);

        arrow.position = new Vector2(clampedX, clampedY);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
