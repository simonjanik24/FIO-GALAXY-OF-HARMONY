using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayerObserver : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private float maxDistance = 40;
    [SerializeField]
    private bool paintDistance = false;
    [Header("Whats going on at runtime?")]
    private bool isInRange = true;

    private Transform player;

    public bool IsInRange { get => isInRange; set => isInRange = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;
        Vector3 objectPosition = transform.position;
        float distance = Vector3.Distance(playerPosition, objectPosition);
        if (distance <= maxDistance)
        {
            isInRange = true;
            if (paintDistance)
            {
                DrawLine();
            }
            
        }
        else
        {
            isInRange = false;
        }
    }


    private void DrawLine()
    {
        // Instantiate a cube at the current position with a scale of (0.1, 0.1, 0.1) and white color
        GameObject newShape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newShape.transform.position = transform.position;
        newShape.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // Assign white material to the shape
        Renderer shapeRenderer = newShape.GetComponent<Renderer>();
        if (shapeRenderer != null)
        {
            shapeRenderer.material.color = Color.red;
        }
    }
}
