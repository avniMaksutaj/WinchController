using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed;
    public float rotationRate = 95;
    public float rotationRate1 = 25;
    //public float  jumpRate = 3 ;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    private string jumpInputAxis = "Jump";
    private string turn2InputAxis = "Fire1";
    private string turn3InputAxis = "Fire2";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        float jumpAxis = Input.GetAxis(jumpInputAxis);
        float turn2Axis = Input.GetAxis(turn2InputAxis);
        float turn3Axis = Input.GetAxis(turn3InputAxis);
        ApplyInput(moveAxis, turnAxis, jumpAxis, turn2Axis, turn3Axis);
    }
    private void ApplyInput(float moveInput, float turnInput, float jumpInput, float turn2Input, float turn3Input)
    {
        Move(moveInput);
        Turn(turnInput);
        jum(jumpInput);
        turn2(turn2Input);
        turn3(turn3Input);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.right * input * speed * Time.deltaTime);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }


    private void jum(float input)
    {
        transform.Translate(Vector3.up * input * speed * Time.deltaTime);
    }

    private void turn2(float input)
    {
        transform.Rotate(0, 0, input * rotationRate1 * Time.deltaTime);
        //transform.Translate(Vector3.down * input * speed * Time.deltaTime); 
    }
    private void turn3(float input)
    {
        transform.Rotate(input * rotationRate * Time.deltaTime, 0, 0);
    }

    //private void jum(float input)
    //{
    //   transform.Translate(Vector3.down * input * speed * Time.deltaTime); 
    //}
    //if (NumberJumps > MaxJumps - 1)
    //    {
    //        isGrounded = false;
    //    }

    //    if (isGrounded)
    //    {
    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            rb.AddForce(Vector3.up * jumpHeight);
    //            NumberJumps += 1;
    //        }
    //    }
    //}

    //void OnCollisionEnter(Collision other)
    //{
    //    isGrounded = true;
    //    NumberJumps = 0;
    //}
    //void OnCollisionExit(Collision other)
    //    {
}