using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinchMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    public GameObject spool1;
    public GameObject spool2;
    public GameObject spool3;
    public List<Button> buttons;
    public List<int> moveList;
     
    public float rotSpeed = 150f;
    HingeJoint joint1;
    HingeJoint joint2;
    HingeJoint joint3;

    // Use this for initialization
    void Start()
    {
        joint1 = spool1.GetComponent<HingeJoint>();
        joint2 = spool2.GetComponent<HingeJoint>();
        joint3 = spool3.GetComponent<HingeJoint>();

    }

    // Update is called once per frame
    void Update()
    {
        JointMotor motor1 = joint1.motor;
        JointMotor motor2 = joint2.motor;
        JointMotor motor3 = joint3.motor;

        if (Input.GetKey(KeyCode.Keypad0))
        {
            motor1.targetVelocity = -rotSpeed;
            motor2.targetVelocity = -rotSpeed;
            motor3.targetVelocity = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad1))
        {
            motor1.targetVelocity = -rotSpeed;
            motor2.targetVelocity = -rotSpeed;
            motor3.targetVelocity = +rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            motor1.targetVelocity = -rotSpeed;
            motor2.targetVelocity = +rotSpeed;
            motor3.targetVelocity = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad3))
        {
            motor1.targetVelocity = -rotSpeed;
            motor2.targetVelocity = +rotSpeed;
            motor3.targetVelocity = +rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            motor1.targetVelocity = +rotSpeed;
            motor2.targetVelocity = -rotSpeed;
            motor3.targetVelocity = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            motor1.targetVelocity = +rotSpeed;
            motor2.targetVelocity = -rotSpeed;
            motor3.targetVelocity = +rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            motor1.targetVelocity = rotSpeed;
            motor2.targetVelocity = rotSpeed;
            motor3.targetVelocity = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.Keypad7))
        {
            motor1.targetVelocity = rotSpeed;
            motor2.targetVelocity = rotSpeed;
            motor3.targetVelocity = rotSpeed;
        }
        else
        {
            motor1.targetVelocity = 0;
            motor2.targetVelocity = 0;
            motor3.targetVelocity = 0;
        }
        joint1.motor = motor1;
        joint2.motor = motor2;
        joint3.motor = motor3;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    public void move0()
    {
        Debug.Log("move0");
        spool1.transform.Rotate(0, 0, -rotSpeed);
        spool2.transform.Rotate(0, 0, -rotSpeed);
        spool3.transform.Rotate(0, 0, -rotSpeed);
        moveList.Add(0);
    }
    public void move1()
    {
        Debug.Log("move1");
        spool1.transform.Rotate(0, 0, -rotSpeed);
        spool2.transform.Rotate(0, 0, -rotSpeed);
        spool3.transform.Rotate(0, 0, +rotSpeed);
        moveList.Add(1);
    }

    public void move2()
    {
        Debug.Log("move2");
        spool1.transform.Rotate(0, 0, -rotSpeed);
        spool2.transform.Rotate(0, 0, +rotSpeed);
        spool3.transform.Rotate(0, 0, -rotSpeed);
        moveList.Add(2);
    }
    public void move3()
    {
    Debug.Log("move3");
    spool1.transform.Rotate(0, 0, -rotSpeed);
    spool2.transform.Rotate(0, 0, +rotSpeed);
    spool3.transform.Rotate(0, 0, +rotSpeed);
        moveList.Add(3);
    }
    public void move4()
    {
    Debug.Log("move4");
    spool1.transform.Rotate(0, 0, +rotSpeed);
    spool2.transform.Rotate(0, 0, -rotSpeed);
    spool3.transform.Rotate(0, 0, -rotSpeed);
        moveList.Add(4);
    }
    public void move5()
    {
    Debug.Log("move5");
    spool1.transform.Rotate(0, 0, +rotSpeed);
    spool2.transform.Rotate(0, 0, -rotSpeed);
    spool3.transform.Rotate(0, 0, +rotSpeed);
        moveList.Add(5);
    }
    public void move6()
    {
    Debug.Log("move6");
    spool1.transform.Rotate(0, 0, +rotSpeed);
    spool2.transform.Rotate(0, 0, +rotSpeed);
    spool3.transform.Rotate(0, 0, -rotSpeed);
        moveList.Add(6);
    }
    public void move7()
    {
        Debug.Log("move7");
        spool1.transform.Rotate(0, 0, +rotSpeed);
        spool2.transform.Rotate(0, 0, +rotSpeed);
        spool3.transform.Rotate(0, 0, +rotSpeed);
        moveList.Add(7);
    }
   
}
