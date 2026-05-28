using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCameraTransform;

    void Start()
    {
        
    }

    // LateUpdate ensures the camera has finished moving before the billboard rotates
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,mainCameraTransform.rotation * Vector3.up);
    }
}