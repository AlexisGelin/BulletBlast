using BaseTemplate.Behaviours;
using System.Collections;
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
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();
        
        colliders.Add("Right", new GameObject().transform);
        colliders.Add("Left", new GameObject().transform);

        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>();
            valPair.Value.name = valPair.Key + "Collider";
            valPair.Value.parent = transform; 
            valPair.Value.tag = "ScreenBorder"; 

            if (valPair.Key == "Left" || valPair.Key == "Right") valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);

        }

        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
    }

}
