using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Inputs: Object")]
    [SerializeField]
    private Transform target;
    [Header("Inputs: Rules")]
    [SerializeField]
    private bool followAtStart;
    [Header("Inputs: Physics")]
    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float offsetZ;
   
   

    private void Start()
    {
        if (followAtStart)
        {
            transform.position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, offsetZ);
        }
    }



    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, offsetZ);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }

}
