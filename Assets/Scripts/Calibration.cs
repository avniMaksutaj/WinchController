using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class Calibration : MonoBehaviour
{
    [SerializeField] Vector3 Coord;
    //[SerializeField] GameObject Objet;
    
    public Toggle minSpeed;
    public Toggle midSpeed;
    public Toggle maxSpeed;
    public byte speedValue;
    public byte moveUp1;
    public byte moveDown1;
    public byte moveUp2;
    public byte moveDown2;
    //public bool isCalibrated;
    //public bool w2isSet;

    DMXController dmx;
    public Button startButton, nextTargetButton, stopButton;

    private void Start()
    {
        startButton.onClick.AddListener(CalibrationStart);
        nextTargetButton.onClick.AddListener(NextTarget);
        stopButton.onClick.AddListener(CalibrationStop);
        dmx = new DMXController("COM3");
    }
    void ChangeSpeed()
    {
        if (minSpeed.isOn)
        {
            speedValue = 104;
        }
        else if (midSpeed.isOn)
        {
            speedValue = 179;
        }
        else if (maxSpeed.isOn)
        {
            speedValue = 255;
        }
        else if (!minSpeed.isOn || !midSpeed.isOn || !maxSpeed.isOn)
        {
            speedValue = 0;
        }
    }
    void CalibrationStart()
    {
        if(moveUp1==0 && moveDown2==0 && moveUp2==0  && moveDown1 == 0)
        {
            moveUp1 = speedValue;
            moveDown2 = speedValue;
            moveUp2 = 0;
            moveDown1 = 0;
        }
        else if(moveUp1 == 0 && moveDown2 == 0 && moveUp2 > 0 && moveDown1 > 0)
        {
            moveUp1 = 0;
            moveDown2 = 0;
            moveUp2 = speedValue;
            moveDown1 = speedValue;
        }
        else if (moveUp1 > 0 && moveDown2 > 0 && moveUp2 == 0 && moveDown1 == 0)
        {
            moveUp1 = speedValue;
            moveDown2 = speedValue;
            moveUp2 = 0;
            moveDown1 = 0;
        }
       
    }
    void NextTarget()
    {
        if(moveUp1 == 0 && moveDown2 == 0 && moveUp2 == 0 && moveDown1 == 0)
        {
            Debug.Log("Calibration not started !! please click on start !!!!");
            Debug.Log("Set the speed to 40");
        }
        else if(moveUp1 == 0 && moveDown2 == 0 && moveUp2 > 0 && moveDown1 > 0)
        {
            moveUp1 = speedValue;
            moveDown2 = speedValue;
            moveUp2 = 0;
            moveDown1 = 0;
        }
        else if (moveUp1 > 0 && moveDown2 > 0 && moveUp2 == 0 && moveDown1 == 0)
        {
            moveUp1 = 0;
            moveDown2 = 0;
            moveUp2 = speedValue;
            moveDown1 = speedValue;
           
        }

    }
    void CalibrationStop()
    {
        moveUp1 = 0;
        moveDown2 = 0;
        moveUp2 = 0;
        moveDown1 = 0;
        minSpeed.isOn = false;
        midSpeed.isOn = false;
        maxSpeed.isOn = false;
        SceneManager.LoadScene("TestChannelScene");
    }



    private void Update()
    {
        ChangeSpeed();
       
        dmx.Channels[13] = moveUp1;//Utiliser pour les top limits motor 1
        dmx.Channels[14] = moveDown1;
        dmx.Channels[20] = moveUp2;//Utiliser pour les top limits motor 2
        dmx.Channels[21] = moveDown2;
        dmx.Send();
      
    }
}
