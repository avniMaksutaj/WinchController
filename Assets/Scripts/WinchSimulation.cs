using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System;
public class WinchSimulation : MonoBehaviour
{
    /////////////////// variable privée  ///////////////////
    
    double[] TabOldLenght;
    double[] TabNewLenght;
    //DMXController dmx;
    bool isSimulationStarted;
    //////////// variable publique ou sérializée /////////////
    public Toggle SimulationBool;
    [SerializeField]GameObject VirtualCam;
    [SerializeField] Vector3 Coord;
    
    ///Winch1
    /// Variable de winch avec tous ces channel
    
    public UInt16 positionRough1;//channel 1
    public UInt16 positionFine1; // channel 2
    public byte speed1;//channel 3
    public double positionM1;
    public int position1;
    
    /// <Winch2>
    /// Variable de winch avec tous ces channel
    public UInt16 positionRough2;//channel 1
    public UInt16 positionFine2; // channel 2
    public byte speed2;//channel 3
    public double positionM2;
    public int position2;
   


    //////////////  classe de constante mesurée en classe      ///////////////////
    public class Constants
    {
        ///////////////////// Hauteur max    //////////////
        public const double HMAX = 3.17;
        ///////// Hauteur de la table de soudure ///////// (qui n'est plus la)
        public const double HTABLE = 0.9;
        ///////////// distance mesurées  //////////////////
        public const double D1_2 = 6.75;
        public const double D1_3 = 7.75;
        public const double D3_4 = 6.95;
        public const double D3_5 = 3.33;
        public const double D1_4 = 3.42928564; //D1_4 = Math.Sqrt((D1_3 * D1_3) - (D3_4 * D3_4))=3.42928564;
        public const double D2_4 = 3.32071436;//D1_2 - D1_4;
        public const double D2_3 = 7.702573846;//Math.Sqrt((D2_4 * D2_4) + (D3_4 * D3_4));
        public const double D4_5 = 3.62;//D3_4 - D3_5;
    }
    double[] CalculLongueur(double CoordX, double CoordY, double CoordZ)
    {
        double[] TabLength = new double[3];
        double L1 = (Constants.D1_4 - CoordX) / (Math.Cos(Math.Atan((Constants.D4_5 + CoordY) / (Constants.D1_4 - CoordX))));
        double L2 = (Constants.D2_4 + CoordX) / (Math.Cos(Math.Atan((Constants.D4_5 + CoordY) / (Constants.D2_4 + CoordX))));
        double L3 = (Constants.D3_5 - CoordY) / (Math.Cos(Math.Atan(Math.Abs(CoordX) / (Constants.D3_5 - CoordY))));
        TabLength[0] = Math.Sqrt((L1 * L1) + (CoordZ * CoordZ));
        TabLength[1] = Math.Sqrt((L2 * L2) + (CoordZ * CoordZ));
        TabLength[2] = Math.Sqrt((L3 * L3) + (CoordZ * CoordZ));
        return TabLength;
    }
    public void SimulationStart()
    {

        VirtualCam = GameObject.FindGameObjectWithTag("Player");
        VirtualCam.GetComponent<PlayableDirector>().enabled = true;
        VirtualCam.GetComponent<Animator>().enabled = true;
        isSimulationStarted = true;
    }
    public void SimulationStop()
    {

        VirtualCam = GameObject.FindGameObjectWithTag("Player");
        VirtualCam.GetComponent<PlayableDirector>().enabled = false;
        VirtualCam.GetComponent<Animator>().enabled = false;
        isSimulationStarted = false;
    }
   
    private void Start()
    {
        speed1 = 0;
        speed2 = 0;
       // speed3 = 0;
        Coord = VirtualCam.transform.position;

        TabNewLenght = new double[3];
        TabOldLenght = new double[3];

        //Mettre le point de départ ici pour l'adapation des vitesses
        //TabOldLenght = CalculLongueur(0, -1.3, 0.2);
        TabOldLenght = CalculLongueur(0, 0, 0);
        //dmx = new DMXController("COM3");
    }
    void Update()
    {
        Coord = VirtualCam.transform.position;

        if (SimulationBool.isOn == true)
        {
            SimulationStart();
            if (isSimulationStarted == true) { 
                TabNewLenght = CalculLongueur((Coord.x), (-Coord.z), (-Coord.y));

                positionM1 = TabNewLenght[0];
                positionM2 = TabNewLenght[1];
               // positionM3 = TabNewLenght[2];

                double DeltaL1 = Math.Abs(TabOldLenght[0] - TabNewLenght[0]);
                double DeltaL2 = Math.Abs(TabOldLenght[1] - TabNewLenght[1]);
               // double DeltaL3 = Math.Abs(TabOldLenght[2] - TabNewLenght[2]);
                //On évite les divisions avec 0
                if (DeltaL1 == 0) 
                { 
                    DeltaL1 = 0.01; 
                }
                if (DeltaL2 == 0) 
                {
                    DeltaL2 = 0.01; 
                }
               // if (DeltaL3 == 0) { DeltaL3 = 0.01; }

                /// Set la vitesse des winch
                speed1 = 0;
                speed2 = 0;
                //speed3 = 200;
                //Conversion mètres en position winch1
                position1 = (int)(1*(positionM1 * positionM1) + 1*positionM1 );
                positionFine1 = (UInt16)(position1 % 256);   // Modulo --> Retourne le reste de la division --> 8 LSB
                positionRough1 = (UInt16)(position1 / 256);  // Division entière (floor division) --> 8 MSB
                //Conversion mètres en position winch2
                position2 = (int)( (positionM2 * positionM2) + positionM2);
                positionFine2 = (UInt16)(position2 % 256);   // Modulo --> Retourne le reste de la division --> 8 LSB
                positionRough2 = (UInt16)(position2 / 256);  // Division entière (floor division) --> 8 MSB


                //Les anciennes longueurs câble deviennent les nouvelles
                TabOldLenght = TabNewLenght;
                ///// lancement de l'animation et du playable director dans unity
            }
        }
        else if (SimulationBool.isOn == false)
        {
            SimulationStop();
            speed1 = 0;
            speed2 = 0;
          //  speed3 = 0;

}
    }
    
}
