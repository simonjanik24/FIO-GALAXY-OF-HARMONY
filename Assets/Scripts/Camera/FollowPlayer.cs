using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Inputs: Object")]
    [SerializeField]
    private Transform target;
    [Header("Inputs: Physics")]
    [SerializeField]
    private float followSpeed;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x+10f, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
