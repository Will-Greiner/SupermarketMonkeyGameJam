using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ResortPlayer : MonoBehaviour
{
    public GameManager gameManager;
    public string gameState;
    private Camera cam;
    private Rigidbody rb;

    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;

    private Vector3 moveDirection;
    public Transform target;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!gameManager.playerCart)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Transform cam = Camera.main.transform;

            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * vertical + right * horizontal).normalized;
        }
    }

    void FixedUpdate()
    {
        if(!gameManager.playerCart){
        // Movement
        Vector3 targetVelocity = moveDirection * moveSpeed;

        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            rb.linearVelocity.y,
            targetVelocity.z
        );



        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(moveDirection);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRot,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
        }
    }

    void OnTriggerEnter(Collider other) { 
        if(other.CompareTag("Empty Tower") &&
   gameManager.gameState == "Building")
{
    gameManager.CheckCosts();

    target = other.transform;

    target.GetChild(0).gameObject.SetActive(true);

    TowerMenu menu = target.GetComponent<TowerMenu>();

    // Turn EVERYTHING off first
    for(int i = 0; i < 6; i++)
    {
        menu.validTowerOptions[i].SetActive(false);
    }

    // Single tower
    if(gameManager.purchasableItems[0])
    {
        menu.validTowerOptions[0].SetActive(true);
    }
    else
    {
        menu.validTowerOptions[1].SetActive(true);
    }

    // Spread tower
    if(gameManager.purchasableItems[3])
    {
        menu.validTowerOptions[2].SetActive(true);
    }
    else
    {
        menu.validTowerOptions[3].SetActive(true);
    }

    // Slow tower
    if(gameManager.purchasableItems[6])
    {
        menu.validTowerOptions[4].SetActive(true);
    }
    else
    {
        menu.validTowerOptions[5].SetActive(true);
    }
}
        if((other.gameObject.tag == "Tower"|| other.gameObject.tag == "Slow") && gameManager.gameState == "Building")
        {
            gameManager.CheckCosts();
            target = other.gameObject.transform;
            target.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) { 
        if(other.CompareTag("Empty Tower"))
{
    TowerMenu menu = other.GetComponent<TowerMenu>();

    for(int i = 0; i < 6; i++)
    {
        menu.validTowerOptions[i].SetActive(false);
    }

    other.transform.GetChild(0).gameObject.SetActive(false);
}
        if(other.gameObject.tag == "Tower"|| other.gameObject.tag == "Slow")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}