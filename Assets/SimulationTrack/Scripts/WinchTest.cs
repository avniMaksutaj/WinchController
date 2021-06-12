//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;


//public class WinchTest : MonoBehaviour
//{
//    //Deplacement 
//    public uint STOP;
//    public double x;
//    //[SerializeField] Vector3 Coord;

//    //distances à mesurer
//    public double Hmax;//set les toplimites des manière à ce que les hauteurs soient identiques
//    public double Htable;
//    public double d1_2;
//    public double d1_3;
//    public double d3_4;
//    public double d3_5;
//    //distance à calculer
//    public double d2_3;
//    public double d1_4;
//    public double d2_4;
//    public double d4_5;
//    ////angles du triangles
//    //public double alpha;
//    //public double beta;
//    //public double gamma;
//    //Tableau Longueurs de cables
//    double[] TabOldLenght;
//    double[] TabNewLenght;

//    DMXController dmx;
//    //variables pour le winch 1
//    uint positionFine1;
//    uint positionRough1;
//    public double positionM1;
//    public uint position1;
//    //public uint setSoftTopLimit1;
//    public uint speed1;
//    //public uint setSoftBottomLimit1;
//    public uint movingUpToTopLimit1;
//    //public uint movingDown1;

//    //variables pour le winch 2
//    uint positionFine2;
//    uint positionRough2;
//    public double positionM2;
//    public uint position2;
//    //public uint setSoftTopLimit2;
//    public uint speed2;
//    //public uint setSoftBottomLimit2;
//    public uint movingUpToTopLimit2;
//    //public uint movingDown2;

//    //variables pour le winch 3
//    uint positionFine3;
//    uint positionRough3;
//    public double positionM3;
//    public uint position3;
//    //public uint setSoftTopLimit3;
//    public uint speed3;
//    //public uint setSoftBottomLimit3;
//    public uint movingUpToTopLimit3;
//    //public uint movingDown3;


//    double[] CalculLongueur(double CoordX, double CoordY, double CoordZ)
//    {
//        double[] TabLength = new double[3];
//        double L1 = (d1_4 - CoordX) / (Math.Cos(Math.Atan((d4_5 + CoordY) / (d1_4 - CoordX))));
//        double L2 = (d2_4 + CoordX) / (Math.Cos(Math.Atan((d4_5 + CoordY) / (d2_4 + CoordX))));
//        double L3 = (d3_5 - CoordY) / (Math.Cos(Math.Atan(Math.Abs(CoordX) / (d3_5 - CoordY))));
//        TabLength[0] = Math.Sqrt((L1 * L1) + (CoordZ * CoordZ));
//        TabLength[1] = Math.Sqrt((L2 * L2) + (CoordZ * CoordZ));
//        TabLength[2] = Math.Sqrt((L3 * L3) + (CoordZ * CoordZ));
//        return TabLength;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {   //Distance mesurées en mètre
//        STOP = 0;
//        //x = gameObject.transform.position.x;
//        //Coord = gameObject.transform.position;
//        // Hauteur max
//        Hmax = 3.17;
//        // Hauteur de la table
//        Htable = 0.9;
//        //distance mesurées
//        d1_2 = 6.75;
//        d1_3 = 7.75;
//        d3_4 = 6.95;
//        d3_5 = 3.33;
//        //Distances et angles calculées
//        d1_4 = Math.Sqrt((d1_3 * d1_3) - (d3_4 * d3_4));
//        d2_4 = d1_2 - d1_4;
//        d2_3 = Math.Sqrt((d2_4 * d2_4) + (d3_4 * d3_4));
//        d4_5 = d3_4 - d3_5;

//        TabNewLenght = new double[3];
//        TabOldLenght = new double[3];

//        TabOldLenght[0] = 5.48;//coordonnée au point (0,0)
//        TabOldLenght[1] = 5.41;
//        TabOldLenght[2] = 4.03;

//        //dmx = new DMXController("COM7");

//        //angles du triangle
//        //alpha = Math.Acos((-Pow(D2_3,2) + Math.Pow(D1_2,2) + Math.Pow(D1_3,2))/(2* D1_2* D1_3));
//        //beta = Math.Acos((+Pow(D2_3, 2) + Math.Pow(D1_2, 2) - Math.Pow(D1_3, 2)) / (2 * D1_2 * D2_3));
//        //gamma = Math.Acos((+Pow(D2_3, 2) - Math.Pow(D1_2, 2) + Math.Pow(D1_3, 2)) / (2 * D1_3 * D2_3));
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Coord = gameObject.transform.position;

//        if (STOP == 0)
//        {
//            speed1 = 0;
//            speed2 = 0;
//            speed3 = 0;
//            dmx.Channels[3] = (byte)speed1;
//            dmx.Channels[10] = (byte)speed2;
//            dmx.Channels[17] = (byte)speed3;
//            dmx.Send();
//        }
//        else
//        {
//            ////// TEST DE DEPLACEMENT DE 1m en X


//            //TabNewLenght = CalculLongueur(1.3, 0, 2);

//            //positionM1 = TabNewLenght[0];
//            //positionM2 = TabNewLenght[1];
//            //positionM3 = TabNewLenght[2];

//            //speed1 = 255;
//            //speed2 = 255;
//            //speed3 = 255;

//            position1 = (uint)(-50.114 * (positionM1 * positionM1) - 5449.5 * positionM1 + 65535);
//            positionFine1 = (uint)position1 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
//            positionRough1 = (uint)position1 / 256;  // Division entière (floor division) --> 8 MSB

//            position2 = (uint)(-50.114 * (positionM2 * positionM2) - 5449.5 * positionM2 + 65535);
//            positionFine2 = (uint)position2 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
//            positionRough2 = (uint)position2 / 256;  // Division entière (floor division) --> 8 MSB

//            position3 = (uint)(-50.114 * (positionM3 * positionM3) - 5449.5 * positionM3 + 65535);
//            positionFine3 = (uint)position3 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
//            positionRough3 = (uint)position3 / 256;  // Division entière (floor division) --> 8 MSB

//            //envoi de paquets
//            dmx.Channels[1] = (byte)positionRough1;
//            dmx.Channels[2] = (byte)positionFine1;
//            dmx.Channels[3] = (byte)speed1;
//            //dmx.Channels[4] = (byte)setSoftTopLimit1;
//            //dmx.Channels[5] = (byte)setSoftBottomLimit1;
//            dmx.Channels[6] = (byte)movingUpToTopLimit1;//Utiliser pour les top limits
//            //dmx.Channels[7] = (byte)movingDown1;

//            dmx.Channels[8] = (byte)positionRough2;
//            dmx.Channels[9] = (byte)positionFine2;
//            dmx.Channels[10] = (byte)speed2;
//            //dmx.Channels[11] = (byte)setSoftTopLimit2;
//            //dmx.Channels[12] = (byte)setSoftBottomLimit2;
//            dmx.Channels[13] = (byte)movingUpToTopLimit2;//Utiliser pour les top limits
//            //dmx.Channels[14] = (byte)movingDown2;

//            dmx.Channels[15] = (byte)positionRough3;
//            dmx.Channels[16] = (byte)positionFine3;
//            dmx.Channels[17] = (byte)speed3;
//            //dmx.Channels[18] = (byte)setSoftTopLimit3;
//            //dmx.Channels[19] = (byte)setSoftBottomLimit3;
//            dmx.Channels[20] = (byte)movingUpToTopLimit3;//Utiliser pour les top limits
//            //dmx.Channels[21] = (byte)movingDown3;
//            dmx.Send();
//            // 5) On set les Old longueurs comme étant les nouvelles
//        }
//    }
//}