using UnityEngine;


public class Billboard : MonoBehaviour
{
    Transform CameraTransform;

    private void Start()
    {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + CameraTransform.forward);
    }
}