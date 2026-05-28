using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool onCart = true;
    public Rigidbody playerRb;
    public Rigidbody playerNoCartRb;
    public Rigidbody playerCartRb;

    public Vector3 offset = new Vector3(0f, 5f, -8f);

    [Header("Distance Zoom")]
    public float zoomSpeed = 5f;
    public float minDistance = 3f;
    public float maxDistance = 15f;

    [Header("focal length Zoom")]
    public Camera cam;
    public float minfocallength = 40f;
    public float maxfocallength = 70f;
    public float focallengthSmoothness = 8f;

    [Header("Smoothing")]
    public float positionSmoothness = 12f;

    private float targetDistance;
    private float currentDistance;

    private float targetfocallength;
    public bool newPosition = true;

    void Start()
    {
        targetDistance = offset.magnitude;
        currentDistance = targetDistance;

        if (cam == null)
            cam = GetComponent<Camera>();

        targetfocallength = cam.focalLength;
    }

    void Update()
    {
        if (onCart)
    {
        playerRb = playerCartRb;
        offset = new Vector3(-6.41f, 7.63f, 0f);
    }
    else
    {
        playerRb = playerNoCartRb;
        offset = new Vector3(-6.41f, 7.63f, 0f);
    }
    }
    void LateUpdate()
    {
        HandleZoom();

        // Smooth distance
        currentDistance = Mathf.Lerp(
            currentDistance,
            targetDistance,
            Time.deltaTime * 8f
        );

        // Smooth focallength
        cam.focalLength = Mathf.Lerp(
            cam.focalLength,
            targetfocallength,
            Time.deltaTime * focallengthSmoothness
        );

        Vector3 direction = offset.normalized;

        Vector3 desiredPosition =
            playerRb.position + direction * currentDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * positionSmoothness
        );
    }

    void HandleZoom()
{
    float scroll = Input.GetAxis("Mouse ScrollWheel");

    if (Mathf.Abs(scroll) < 0.0001f)
        return;

    targetDistance -= scroll * zoomSpeed;

    targetDistance = Mathf.Clamp(
        targetDistance,
        minDistance,
        maxDistance
    );

    // INVERTED FOV EFFECT
    float t = Mathf.InverseLerp(minDistance, maxDistance, targetDistance);

    targetfocallength = Mathf.Lerp(minfocallength, maxfocallength, t);
}
}