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
    private float followDelay;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float offsetZ;

    private Vector3 lastPosition;

    private Vector3 velocity = Vector3.zero;

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

        
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
           
            Vector3 newPos = new Vector3(target.position.x - offsetX, target.position.y + offsetY, offsetZ);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);
            // transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
         

        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            Vector3 newPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, offsetZ);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);
            //transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
           
        }
     /*   else
        {
            
             Vector3 newPos = new Vector3(target.position.x, target.position.y + offsetY, offsetZ);
             transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);
            
        }*/


    }



}
