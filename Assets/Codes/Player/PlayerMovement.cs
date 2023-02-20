using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [Header("Rotation Settings")] 
   [SerializeField] float rotationSpeed = 100f;
   
   [Header("Movement Settings")]
   [SerializeField] float moveSpeed;

   [Header("Inputs")]
   float horizontalInput, verticalInput;

   [Header("Physics and Camera")]
    Rigidbody2D rb;
    Camera myCamera;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        myCamera = Camera.main;
    }
    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 movement = transform.up * verticalInput * moveSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        transform.Rotate(0, 0, -horizontalInput * rotationSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate() => myCamera.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10f);
}
