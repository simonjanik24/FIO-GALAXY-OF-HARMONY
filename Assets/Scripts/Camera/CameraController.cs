using System.Collections;
using UnityEngine;
using static System.TimeZoneInfo;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Follow: Object")]
    [SerializeField]
    private Transform target;
    [Header("Inputs: Following")]
    [SerializeField]
    private bool follow;
    [SerializeField]
    private float followDelay;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float offsetZ;
    [Header("Inputs: Zoom")]
    [SerializeField]
    private bool zoom;
    [SerializeField]
    private float targetZoomValue;
    [SerializeField]
    private float transitionTime = 2f;
    [SerializeField]
    private float zoomOffsetX;
    [SerializeField]
    private float zoomOffsetY;
    [SerializeField]
    private float zoomOffsetZ;
    
    [Header("What's going on at runtime?")]
    [SerializeField]
    private float currentZoom;
    [SerializeField]
    private float initalZoomValue;

    private Camera camera;
    private Vector3 velocity = Vector3.zero;
    
    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    public bool IsTransitioning { get => isTransitioning; set => isTransitioning = value; }

    private void Start()
    {
        camera = GetComponent<Camera>();
        initalZoomValue = camera.orthographicSize;

        if (follow)
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

        if (follow)
        {
            Vector3 newPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);

            if(camera.orthographicSize != initalZoomValue)
            {
                // Update transition timer
                transitionTimer += Time.deltaTime;

                // Calculate transition progress
                float progress = Mathf.Clamp01(transitionTimer / transitionTime);
                camera.orthographicSize = Mathf.Lerp(targetZoomValue, initalZoomValue, progress);
            }
            else
            {
                IsTransitioning = false;
                transitionTimer = 0f;
            }
            
        }
        else if(zoom)
        {
            Vector3 newPos = new Vector3(target.position.x + zoomOffsetX, target.position.y + zoomOffsetY, target.position.z + zoomOffsetZ);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, followDelay);

            if (IsTransitioning)
            {
                if(camera.orthographicSize != targetZoomValue)
                {
                    // Update transition timer
                    transitionTimer += Time.deltaTime;

                    // Calculate transition progress
                    float progress = Mathf.Clamp01(transitionTimer / transitionTime);
                    camera.orthographicSize = Mathf.Lerp(initalZoomValue, targetZoomValue, progress);

                    // Check if transition is complete
                 /*   if (transitionTimer >= transitionDurationTime)
                    {
                        isTransitioning = false;
                        transitionTimer = 0f;
                    }*/
                }
                else
                {
                    IsTransitioning = false;
                    transitionTimer = 0f;
                }
                
            }
            else
            {
                camera.orthographicSize = targetZoomValue;
            }

            currentZoom = camera.orthographicSize;
        }
    }

    private IEnumerator ZoomToMe()
    {
        yield return new WaitForSeconds(1.0f);
        Zoom();
        yield return new WaitForSeconds(4.0f);
        Follow();
    }


    public void Follow()
    {
        follow = true;
        zoom = false;
        IsTransitioning = true;
    }

    public void Zoom()
    {
        follow = false;
        zoom = true;
        IsTransitioning = true;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z + offsetZ);
    }


}