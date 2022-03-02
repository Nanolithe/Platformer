using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterController : MonoBehaviour
{
    public float runForce = 50f;
    public float maxRunSpeed = 6f;
    public float jumpForce = 20f;
    public float jumpBonus = 3f;

    public float jumpImpulseForce = 20f;
    public float jumpSustainForce = 7.5f;
    public float maxHorizontalSpeed = 6f;

    public bool feetInContactWithGround = false;
    private Rigidbody body;
    private Collider collider;

    private Animator animComp;

    public Transform playerCameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float castDistance = collider.bounds.extents.y + 0.1f;
        feetInContactWithGround= Physics.Raycast(transform.position, Vector3.down, bounds.extents.y + 0.1f);
        
        float axis = Input.GetAxis("Horizontal");
        //Debug.Log(axis);
        //Debug.Log(axis > 0 ? "right" : "left");
        body.AddForce(Vector3.right * axis * runForce, ForceMode.Force);
        if (axis > 0.1f || axis < -0.1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, axis > 0 ? 0 : 180, 0));
        }

        if (feetInContactWithGround && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (body.velocity.y > 0f && Input.GetKey(KeyCode.Space))
        {
            body.AddForce(Vector3.up * jumpBonus, ForceMode.Force);
        }

        if (Mathf.Abs(body.velocity.x) > maxRunSpeed)
        {
            float newX = maxRunSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }

        if (Mathf.Abs(axis) < 0.1f)  
        {
            float newX = body.velocity.x * (1f - Time.deltaTime * 5f);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }
        animComp.SetFloat("Speed", body.velocity.magnitude);

        playerCameraTransform.position = new Vector3(this.transform.position.x, playerCameraTransform.position.y,
            playerCameraTransform.position.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Mario Died");
            ResetMario();
        }

        if (other.CompareTag("FlagGoal"))
        {
            Debug.Log("Game Ended");
            ResetMario();
        }
    }

    public void ResetMario()
    {
        transform.position = new Vector3(9.015001f, 2.18f, 0f); 
    }
}
