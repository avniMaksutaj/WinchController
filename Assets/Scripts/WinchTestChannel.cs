using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinchTestChannel : MonoBehaviour
{
    /// <summary>
    /// Variable de  DMX channels
    ///  1) Position rough (Hi of a 16 bit DMX channel)
    ///  2) Position fine(Lo of a 16 bit DMX channel)
    ///  3)Set the maximum speed
    ///  4)Set the soft TOP limit
    ///  5)Set the soft BOTTOM limit
    ///  6)Find hard TOP limit, moving up (reset speed)
    ///  7)Moving down
    /// </summary>
    public GameObject[] panels;
    public GameObject[] sliders;
    public GameObject spool1;
    public GameObject spool2;
    
    // public List<byte> winch1PosList;
    // public List<byte> winch2PosList;
    public Toggle SetTopLimitBool;
    public Toggle SimulationToggle;
    public DMXController dmx;
    public Vector3 Coord;
    public byte positionRough1;
    public byte positionFine1;
    public byte speed1 ;
    public byte moveUp1 ;
    public byte moveDown1 ;
    public byte positionRough2;
    public byte positionFine2;
    public byte speed2 ;
    public byte moveUp2 ;
    public byte moveDown2 ;
    public byte speed3 ;
    public byte moveUp3 ;
    public byte moveDown3 ;
    /// <summary>
    /// variable privée
    /// </summary>
    HingeJoint joint1;
    HingeJoint joint2;
    //HingeJoint joint3;
    
   
    private void Start()
    {
        dmx = new DMXController("COM3");
        panels = GameObject.FindGameObjectsWithTag("Panel");
        sliders = GameObject.FindGameObjectsWithTag("Slider");
        joint1 = spool1.GetComponent<HingeJoint>();
        joint2 = spool2.GetComponent<HingeJoint>();
        speed1 = 0;
        speed2 = 0;
       

        Coord = gameObject.transform.position;

    }
    
    private void Awake()
    {
        dmx = new DMXController("COM3");    
    }
    void Update()
    {
        Coord = gameObject.transform.position;
       

        if (SetTopLimitBool.isOn == false)
        {

            //panels[0].SetActive(false);
            //panels[1].SetActive(false);
            //panels[2].SetActive(false);
           
            if (Input.GetKey(KeyCode.Keypad1))
            {
                speed1 = 0;
                speed2 = 0;
                positionRough1 = 201;
                positionRough2 = 151;
                speed1 = 240;
                speed2 = 240;
                dmx.Channels[1] = positionRough1;
                dmx.Channels[3] = speed1;
                dmx.Channels[8] = positionRough2;
                dmx.Channels[10] = speed2;
                dmx.Send();
            }
            else if (Input.GetKey(KeyCode.Keypad2))
            {
                //speed1 = 0;
                //speed2 = 0;
                positionRough1 = 98;
                positionRough2 = 180;
                speed1 = 240;
                speed2 = 240;
                dmx.Channels[1] = positionRough1;
                dmx.Channels[3] = speed1;
                dmx.Channels[8] = positionRough2;
                dmx.Channels[10] = speed2;
                dmx.Send();
            }
            else if (Input.GetKey(KeyCode.Keypad3))
            {
                //speed1 = 0;
                //speed2 = 0;
                positionRough1 = 165;
                positionRough2 = 172;
                speed1 = 240;
                speed2 = 240;
                dmx.Channels[1] = positionRough1;
                dmx.Channels[3] = speed1;
                dmx.Channels[8] = positionRough2;
                dmx.Channels[10] = speed2;
                dmx.Send();
            }
            
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                //speed1 = 0;
                //speed2 = 0;
                positionRough1 = 180;
                positionRough2 = 98;
                speed1 = 240;
                speed2 = 240;
                dmx.Channels[1] = positionRough1;
                dmx.Channels[3] = speed1;
                dmx.Channels[8] = positionRough2;
                dmx.Channels[10] = speed2;
                dmx.Send();
            }
            else if (Input.GetKey(KeyCode.Keypad5))
            {
                //speed1 = 0;
                //speed2 = 0;
                positionRough1 = 172;
                positionRough2 = 165;
                speed1 = 240;
                speed2 = 240;
                dmx.Channels[1] = positionRough1;
                dmx.Channels[3] = speed1;
                dmx.Channels[8] = positionRough2;
                dmx.Channels[10] = speed2;
                dmx.Send();
            }
            else if (SimulationToggle.isOn == true)
            {
                SceneManager.LoadScene("Simulation2Moteurs");
            }


        }
        else if (SetTopLimitBool.isOn == true)
        {
            
            //panels[0].SetActive(true);
            //panels[1].SetActive(true);
           
            // panels[2].SetActive(true);
            

            positionRough1 = (byte)sliders[0].GetComponent<Slider>().value;
            positionFine1 =(byte)sliders[1].GetComponent<Slider>().value;
            speed1 =(byte)sliders[2].GetComponent<Slider>().value;
            moveUp1= (byte)sliders[3].GetComponent<Slider>().value;
            moveDown1= (byte)sliders[4].GetComponent<Slider>().value;
            positionRough2 = (byte)sliders[5].GetComponent<Slider>().value;
            positionFine2 = (byte)sliders[6].GetComponent<Slider>().value;
            speed2 = (byte)sliders[7].GetComponent<Slider>().value;
            moveUp2= (byte)sliders[8].GetComponent<Slider>().value;
            moveDown2= (byte)sliders[9].GetComponent<Slider>().value;
            
            dmx.Channels[8] = positionRough1;
            dmx.Channels[9] = positionFine1;
            dmx.Channels[10] = speed1;
            dmx.Channels[13] = moveUp1;//Utiliser pour les top limits
            dmx.Channels[14] = moveDown1;
            dmx.Channels[15] = positionRough2;
            dmx.Channels[16] = positionFine2;
            dmx.Channels[17] = speed2;
            dmx.Channels[20] = moveUp2;//Utiliser pour les top limits
            dmx.Channels[21] = moveDown2;
         
            dmx.Send();
        }
     
    }

}
