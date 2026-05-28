using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCameraTransform;

    void Start()
    {
        // Automatically find camera if not assigned
        if (mainCameraTransform == null)
        {
            if (Camera.main != null)
            {
                mainCameraTransform = Camera.main.transform;
            }
            else
            {
                Camera cam = FindFirstObjectByType<Camera>();

                if (cam != null)
                {
                    mainCameraTransform = cam.transform;
                }
            }
        }

        if (mainCameraTransform == null)
        {
            Debug.LogError("Billboard could not find a camera!");
        }
    }

    // LateUpdate ensures camera movement finishes first
    void LateUpdate()
    {
        // Prevent null reference errors
        if (mainCameraTransform == null)
            return;

        transform.LookAt(
            transform.position +
            mainCameraTransform.rotation * Vector3.forward,

            mainCameraTransform.rotation * Vector3.up
        );
    }
}