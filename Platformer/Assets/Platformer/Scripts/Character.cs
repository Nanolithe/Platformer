using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    private void Move()
    {
        //controller.Move(Vector3.right * direction * movementSpeed * Time.deltaTime);

        //verticalVel += gravity * Time.deltaTime;

       // controller.Move(Vector3.up * verticalVel * Time.deltaTime);
    }

    void Jump()
    {
        //verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
