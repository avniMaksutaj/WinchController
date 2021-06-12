using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MotorCalibrationMovement : MonoBehaviour
{
    public GameObject spool1;
    public GameObject spool3;
    public GameObject calibrationPanel;
    Calibration calibration;
    public Toggle minSpeed;
    public Toggle midSpeed;
    public Toggle maxSpeed;
    public float speedValue;
   


    // public float rotSpeed = 150f;

    HingeJoint joint1;
    HingeJoint joint3;
    // Start is called before the first frame update
    void Start()
    {     
        joint1 = spool1.GetComponent<HingeJoint>();
        joint3 = spool3.GetComponent<HingeJoint>();
        calibration = calibrationPanel.GetComponent<Calibration>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        JointMotor motor2 = joint1.motor;
        JointMotor motor3 = joint3.motor;
        if (calibration.moveUp1 > 0)
        {
            speedValue = (calibration.moveUp1/80);
        }
        else if (calibration.moveUp2 > 0)
        {
            speedValue = calibration.moveUp2/80;
        }
        else if (calibration.moveDown1 > 0)
        {
            speedValue = -(calibration.moveDown1/80);
        }
        if (calibration.moveDown2 > 0)
        {
            speedValue = -(calibration.moveDown2/80);
        }


        if (minSpeed.isOn && speedValue != 0 )
        {
            //speedValue = calibration.moveUp1;

            //speedValue = 4;
            //definir e svitesse en focntion de queqlque chose 
            spool1.transform.Rotate(0, 0, +speedValue);
            spool3.transform.Rotate(0, 0, -speedValue);
            
        }
        else if (midSpeed.isOn && speedValue != 0 )
        {
            //speedValue = 10;
            spool1.transform.Rotate(0, 0, +speedValue);
            spool3.transform.Rotate(0, 0, -speedValue);
           
        }
        else if (maxSpeed.isOn && speedValue != 0 )
        {
            //speedValue = 50;
            spool1.transform.Rotate(0, 0, +speedValue);
            spool3.transform.Rotate(0, 0, -speedValue);
           //motor2.targetVelocity = -speedValue;
          // motor3.targetVelocity = +speedValue;
        }
        else if (!minSpeed.isOn && !midSpeed.isOn && !maxSpeed.isOn) 
        {
            speedValue = 0;
            
            spool1.transform.Rotate(0, 0, +speedValue);
            spool3.transform.Rotate(0, 0, -speedValue);
            //motor2.targetVelocity = -speedValue;
            //motor3.targetVelocity = +speedValue;
        };
    }
}
