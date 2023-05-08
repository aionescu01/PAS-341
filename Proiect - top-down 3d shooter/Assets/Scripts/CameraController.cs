using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] Vector3 targetOffset;                          //distance offset from target to camera

    [SerializeField] [Range(2f, 5f)] float movementSpeed = 5f;      //camera movement speed

    [SerializeField] [Range(60f, 70f)] float minFOV = 60f;          //max zoom in

    [SerializeField] [Range(90f, 110f)] float maxFOV = 100f;        //max zoom out

    [SerializeField] float zoomSensitivity = 10f;                   //scroll sensitivity for zooming

    void Update()
    {
        MoveCamera();
        HandleZoomInOut();
    }

    //Camera movement
    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }

    //Zoom in and out
    void HandleZoomInOut()
    {
        float fov = Camera.main.fieldOfView;

        fov += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * -1;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);

        Camera.main.fieldOfView = fov;
    }
}