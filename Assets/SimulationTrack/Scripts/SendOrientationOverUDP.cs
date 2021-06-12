using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

// Reference for UDP config : https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services

public class SendOrientationOverUDP : MonoBehaviour
{
    // Network variables
    Socket UDP_Socket;
    IPAddress NodeMCU_IPAdd;
    IPEndPoint NodeMCU_EP;

    // Rotation variables
    private Vector3 localRot;
    private float[]  angles = new float[3];
    byte[] packetData = new byte[1 + (3 * sizeof(float))];

    // Debug
    bool DEBUG_ON = false;

    // Start is called before the first frame update
    void Start()
    {
        //init_UDP_communication("192.168.0.203");    // change with right NodeMUC IP
        init_UDP_communication("172.30.40.25");
    }

    // Update is called once per frame
    void Update()
    {
        UDP_send_updated_orientation();        
    }


    void init_UDP_communication(String NodeMCU_IP)
    {
        if(DEBUG_ON) Debug.Log("[Debug] starting program...");
        gameObject.SetActive(true);
        UDP_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        NodeMCU_IPAdd = IPAddress.Parse(NodeMCU_IP);
        NodeMCU_EP = new IPEndPoint(NodeMCU_IPAdd, 4210);
        
        if (DEBUG_ON) Debug.Log("[Debug] Program Started !");
    }

    void UDP_send_updated_orientation()
    {
        update_axis_values();
        if (DEBUG_ON) Debug.Log("Preparing message...");
        prepare_packet_data();
        UDP_Socket.SendTo(packetData, NodeMCU_EP);
        if (DEBUG_ON) Debug.Log("Message sent !");
    }

    private void update_axis_values()
    {
        localRot = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform) ;
        angles[0] = -localRot.x;    // pitch
        angles[1] = localRot.z;     // roll
        angles[2] = -localRot.y;    // yaw
    }

    private void prepare_packet_data()
    {
        packetData[0] = (byte) 'c';         // to tell the NodeMCU that this is a coord packet

        // precious help from https://stackoverflow.com/questions/4635769/how-do-i-convert-an-array-of-floats-to-a-byte-and-back
        var byteArray = new byte[angles.Length * 4];
        Buffer.BlockCopy(angles, 0, byteArray, 0, byteArray.Length);
        for (int i = 0; i < byteArray.Length; i++)
        {
            packetData[i+1] = byteArray[i];
        }
    }
}
