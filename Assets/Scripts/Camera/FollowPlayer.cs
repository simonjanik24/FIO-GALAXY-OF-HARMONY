using System.Collections;
using System.Collections.Generic;
using System.Security;
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
            transform.position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
        }
    }



    // Update is called once per frame
    void Update()
    {
       /*   float horizontalX = Input.GetAxisRaw("Horizontal");
          float verticalX = Input.GetAxisRaw("Vertical");
            if (horizontalX < 0)
            {

                Vector3 newPos = new Vector3(target.position.x - offsetX, target.position.y + offsetY, target.position.z + offsetZ);
                transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);
            }
            else if (horizontalX > 0)
            {
                Vector3 newPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
                transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);
            }

        */
        Vector3 newPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);

    }



    public void ResetPosition()
    {
        transform.position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
    }


}