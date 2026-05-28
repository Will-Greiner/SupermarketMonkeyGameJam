using System;
using UnityEngine;
using TMPro;
using System.Collections;
public class ResortPlayer : MonoBehaviour
{
    private bool isAccelerating = false;
    private bool canMove = true;

    private Rigidbody playerRigidbody;

    public float playerAcceleration;
    public float playerMaxVelocity;
    public float rotationSpeed = 10f;

    private Vector3 moveDirection;
    public float raycastDistance = 3f;
    public LayerMask raycastLayers;
    private Transform target;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = -Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create movement direction vector
        moveDirection = new Vector3(vertical, 0f, horizontal);

        // Check if player is moving
        isAccelerating = moveDirection.magnitude > 0.1f;

        // Rotate player toward movement direction
        if (isAccelerating)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, raycastLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }

// Debug ray visualization
        Debug.DrawRay(transform.position,transform.forward * raycastDistance,Color.red);
    }

    void FixedUpdate()
    {
        if (isAccelerating && canMove)
        {
            // Apply movement force
            playerRigidbody.AddForce(moveDirection * playerAcceleration,ForceMode.Force);

        // Clamp max speed
            playerRigidbody.linearVelocity = Vector3.ClampMagnitude(playerRigidbody.linearVelocity,playerMaxVelocity);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Empty Tower")
        {
            target = other.gameObject.transform;
            if(target.GetComponent<TowerMenu>().towerEmpty == true)
            {
                target.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Empty Tower")
        {
            target.GetChild(0).gameObject.SetActive(false);
        }
    }
}