using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoSingleton<WorldManager>
{
    public Transform _afterStartTranform, _afterSpawnEnnemyPosTransform;

    public float colThickness = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;

    public void Init()
    {
        ScreenBorder();
        WorldBorder();
    }

    private void ScreenBorder()
    {
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();

        colliders.Add("RightScreen", new GameObject().transform);
        colliders.Add("LeftScreen", new GameObject().transform);

        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>();
            valPair.Value.name = valPair.Key + "Collider";
            valPair.Value.parent = transform;
            valPair.Value.tag = "ScreenBorder";

            if (valPair.Key == "LeftScreen" || valPair.Key == "RightScreen") valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 4, colThickness);

        }

        colliders["RightScreen"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["RightScreen"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["LeftScreen"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["LeftScreen"].localScale.x * 0.5f), cameraPos.y, zPosition);
    }

    private void WorldBorder()
    {
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();

        colliders.Add("RightWorld", new GameObject().transform);
        colliders.Add("LeftWorld", new GameObject().transform);
        colliders.Add("TopWorld", new GameObject().transform);
        colliders.Add("BottomWorld", new GameObject().transform);

        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 2, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 2))) * 0.5f;

        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>();
            valPair.Value.name = valPair.Key + "Collider";
            valPair.Value.parent = transform;
            valPair.Value.tag = "WorldBorder";

            if (valPair.Key == "LeftWorld" || valPair.Key == "RightWorld") valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            if (valPair.Key == "BottomWorld" || valPair.Key == "TopWorld") valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);

        }

        colliders["RightWorld"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["RightWorld"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["LeftWorld"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["LeftWorld"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["TopWorld"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["TopWorld"].localScale.y * 0.5f), zPosition);
        colliders["BottomWorld"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["BottomWorld"].localScale.y * 0.5f), zPosition);
    }
}
