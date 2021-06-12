using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmxDataSender : MonoBehaviour
{
    DMXController dmx;
    Calibration calibration;
    WinchSimulation simulation;
    public Toggle SetTopLimitBool;
    public GameObject calibrationPanel;
    public GameObject effecteur;
    Vector3 Coord;
   
    void Start()
    {
        calibration = calibrationPanel.GetComponent<Calibration>();
        simulation = calibrationPanel.GetComponent<WinchSimulation>();
        dmx = new DMXController("COM3");
    }

    void Update()
    {
        Coord = gameObject.transform.position;


        if (SetTopLimitBool.isOn == false)
        {
            dmx.Channels[8] = (byte)simulation.positionRough1;//8
            dmx.Channels[9] = (byte)simulation.positionFine1;//9
            dmx.Channels[10] = (byte)simulation.speed1;//10
            dmx.Channels[15] = (byte)simulation.positionRough2;//15
            dmx.Channels[16] = (byte)simulation.positionFine2;//16
            dmx.Channels[17] = (byte)simulation.speed2;//17
            dmx.Send();

        }
        else if (SetTopLimitBool.isOn == true)
        {
            dmx.Channels[13] = calibration.moveUp1;//Utiliser pour les top limits//13
            dmx.Channels[14] = calibration.moveDown1;//14
            dmx.Channels[20] = calibration.moveUp2;//Utiliser pour les top limits//20
            dmx.Channels[21] = calibration.moveDown2;//21
            dmx.Send();
        }


    }
 
}
