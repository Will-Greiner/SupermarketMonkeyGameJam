using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class ShoppingCartController : MonoBehaviour
{
    public Transform originPosition;
    [Header("Movement Settings")]
    public float moveForce = 50f;         // Force applied to push the cart
    public float steerTorque = 5f;        // Torque for steering
    public float maxSpeed = 10f;          // Max linear speed
    public float brakeForce = 10f;        // Force to slow down cart

    [Header("Drag Settings")]
    public float angularDrag = 2f;
    public float linearDrag = 1f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundRayDistance = 1.2f;

    private Rigidbody rb;
    public int cartCapacity = 20;
    public int[] cartContents;
    public GameObject[] spawnGroceries;
    public int currentShelfIndex;
    public bool onShelf = false;
    public int currentCart = 0;
    public bool canMove = false;
    public TMP_Text capacity;
    public GameObject spawnPoint;
    public Vector3 origin;

    void Start()
    {
        origin = new Vector3(-19.3f, 0f, -4.82f);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = linearDrag;
        rb.angularDamping = angularDrag;
        updateCapacity();
    }

    void Update()
    {
        updateCapacity();
        if(onShelf && Input.GetKeyDown(KeyCode.E) && currentCart < cartCapacity)
        {
            if(cartContents[currentShelfIndex] < 5)
            {
                Instantiate(spawnGroceries[currentShelfIndex], spawnPoint.transform.position, Quaternion.identity);
                cartContents[currentShelfIndex]++;
                currentCart++;
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
         Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Input
        float vertical = Input.GetAxis("Vertical");   // W/S or Up/Down
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Only apply forces if cart is grounded

            // Forward/backward force
            Vector3 force = forward * vertical * moveForce;
            rb.AddForce(force, ForceMode.Force);

            // Steering torque
            rb.AddTorque(Vector3.up * horizontal * steerTorque, ForceMode.Force);

        // Limit speed
        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (horizontalVel.magnitude > maxSpeed)
        {
            Vector3 clampedVel = horizontalVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(clampedVel.x, rb.linearVelocity.y, clampedVel.z);
        }

        // Optional: braking when no input
        if (Mathf.Abs(vertical) < 0.01f)
        {
            rb.linearVelocity -= horizontalVel * Time.fixedDeltaTime * brakeForce;
        }   
        }
    }

    void OnTriggerEnter(Collider other)
    {
        onShelf = true;
        char check = other.GetComponent<ShelfLogic>().product;
        switch (check)
        {
            case 'm':
                currentShelfIndex = 0;
                break;
            case 'p':
                currentShelfIndex = 1;
                break;
            case 'b':
                currentShelfIndex = 2;
                break;
            case 'c':
                currentShelfIndex = 3;
                break;
            case 's':
                currentShelfIndex = 4;
                break;
            case 'w':
                currentShelfIndex = 5;
                break;

        }
    }

    void updateCapacity()
    {
        capacity.text = currentCart.ToString() + "/" + cartCapacity.ToString(); 
    }
    
    public void ResetPosition()
        {
    // Stop physics
    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    // Move to spawn point
        transform.position = origin;
        transform.rotation = Quaternion.Euler(0, 90, 0);
    

    // Reset gameplay state
        currentCart = 0;
        cartContents = new int[cartContents.Length];
        canMove = false;

        updateCapacity();
    }
}