using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class SimulationScript : MonoBehaviour
{
    //Deplacement 
    public uint STOP;
    public double x;
    [SerializeField] Vector3 Coord;

    //distances à mesurer
    public double Hmax;//set les toplimites des manière à ce que les hauteurs soient identiques
    public double Htable;
    public double d1_2;
    public double d1_3;
    public double d3_4;
    public double d3_5;
    //distance à calculer
    public double d2_3;
    public double d1_4;
    public double d2_4;
    public double d4_5;
    ////angles du triangles
    //public double alpha;
    //public double beta;
    //public double gamma;
    //Tableau Longueurs de cables
    double[] TabOldLenght;
    double[] TabNewLenght;

    //les delta L
    public double DeltaL1;
    public double DeltaL2;
    public double DeltaL3;
    public double DeltaMax;

    DMXController dmx;
    //variables pour le winch 1
    public double positionFine1;
    public double positionRough1;
    public double positionM1;
    public int position1;
    //public uint setSoftTopLimit1;
    public double speed1;
    //public uint setSoftBottomLimit1;
    public uint movingUpToTopLimit1;
    public uint movingDown1;

    //variables pour le winch 2
    public double positionFine2;
    public double positionRough2;
    public double positionM2;
    public int position2;
    //public uint setSoftTopLimit2;
    public double speed2;
    //public uint setSoftBottomLimit2;
    public uint movingUpToTopLimit2;
    public uint movingDown2;

    //variables pour le winch 3
    double positionFine3;
    double positionRough3;
    public double positionM3;
    public int position3;
    //public uint setSoftTopLimit3;
    public double speed3;
    //public uint setSoftBottomLimit3;
    public uint movingUpToTopLimit3;
    public uint movingDown3;
  

    double[] CalculLongueur(double CoordX, double CoordY, double CoordZ)
    {
        double[] TabLength = new double[3];
        double L1 = (d1_4 - CoordX) / (Math.Cos(Math.Atan((d4_5 + CoordY) / (d1_4 - CoordX))));
        double L2 = (d2_4 + CoordX) / (Math.Cos(Math.Atan((d4_5 + CoordY) / (d2_4 + CoordX))));
        double L3 = (d3_5 - CoordY) / (Math.Cos(Math.Atan(Math.Abs(CoordX) / (d3_5 - CoordY))));
        TabLength[0] = Math.Sqrt((L1 * L1) + (CoordZ * CoordZ));
        TabLength[1] = Math.Sqrt((L2 * L2) + (CoordZ * CoordZ));
        TabLength[2] = Math.Sqrt((L3 * L3) + (CoordZ * CoordZ));
        return TabLength;
    }
    private void Awake()
    {
        dmx = new DMXController("COM3");
    }
    // Start is called before the first frame update
    void Start()
    {
       
        STOP = 1;
        speed1 = 0;
        speed2 = 0;
        speed3 = 0;

        Coord = gameObject.transform.position;
        // Hauteur max
        Hmax = 3.17;
        // Hauteur de la table de soudure
        Htable = 0.9;
        //distance mesurées
        d1_2 = 6.75;
        d1_3 = 7.75;
        d3_4 = 6.95;
        d3_5 = 3.33;
        //Distances et angles calculées
        d1_4 = Math.Sqrt((d1_3 * d1_3) - (d3_4 * d3_4));
        d2_4 = d1_2 - d1_4;
        d2_3 = Math.Sqrt((d2_4 * d2_4) + (d3_4 * d3_4));
        d4_5 = d3_4 - d3_5;

        TabNewLenght = new double[3];
        TabOldLenght = new double[3];

        //Mettre le point de départ ici pour l'adapation des vitesses
       TabOldLenght = CalculLongueur(0, -1.3, 0.2);
        //TabNewLenght = CalculLongueur(0, -1.3, 0.2);

       // dmx = new DMXController("COM3");
    }

    // Update is called once per frame
    void Update()
    {
        Coord = gameObject.transform.position;
        if (STOP == 0)
        {
            speed1 = 0;
            speed2 = 0;
            speed3 = 0;
            /*1-2*/
            dmx.Channels[3] = (byte)speed1;
            dmx.Channels[10] = (byte)speed2;
            dmx.Channels[17] = (byte)speed3;

            dmx.Channels[6] = (byte)movingUpToTopLimit1;//Utiliser pour les top limits
            dmx.Channels[13] = (byte)movingUpToTopLimit2;//Utiliser pour les top limits
            dmx.Channels[20] = (byte)movingUpToTopLimit3;//Utiliser pour les top limits

            dmx.Channels[7] = (byte)movingDown1;
            dmx.Channels[14] = (byte)movingDown2;
            dmx.Channels[21] = (byte)movingDown3;
            dmx.Send();
        }
        else
        {
            //Coordonnée de départ de la trajectoire circulaire
            //TabNewLenght = CalculLongueur(0, -1.3, 0.2);

            TabNewLenght = CalculLongueur((Coord.x) , -(Coord.z), -(Coord.y));

            positionM1 = TabNewLenght[0];
            positionM2 = TabNewLenght[1];
            positionM3 = TabNewLenght[2];

            DeltaL1 = Math.Abs(TabOldLenght[0] - TabNewLenght[0]);
            DeltaL2 = Math.Abs(TabOldLenght[1] - TabNewLenght[1]);
            DeltaL3 = Math.Abs(TabOldLenght[2] - TabNewLenght[2]);

            //On évite les divisions avec 0
            if (DeltaL1 == 0)
            {
                DeltaL1 = 0.01;
            }
            if (DeltaL2 == 0)
            {
                DeltaL2 = 0.01;
            }
            if (DeltaL3 == 0)
            {
                DeltaL3 = 0.01;
            }
            //Adaptation des vitesses

            //On cherche le delta maximum pour savoir quel winch doit être à la vitesse max
            //if (DeltaL1 == Math.Max(DeltaL3, Math.Max(DeltaL1, DeltaL2)))
            //{
            //    speed1 = 255;
            //    speed2 = 255 * (DeltaL2 / DeltaL1);
            //    speed3 = 255 * (DeltaL3 / DeltaL1);
            //}
            //else if (DeltaL2 == Math.Max(DeltaL3, Math.Max(DeltaL1, DeltaL2)))
            //{
            //    speed2 = 255;
            //    speed1 = 255 * (DeltaL1 / DeltaL2);
            //    speed3 = 255 * (DeltaL3 / DeltaL2);
            //}
            //else if (DeltaL3 == Math.Max(DeltaL3, Math.Max(DeltaL1, DeltaL2)))
            //{
            //    speed3 = 255;
            //    speed1 = 255 * (DeltaL1 / DeltaL3);
            //    speed2 = 255 * (DeltaL2 / DeltaL3);
            //}

            ////On set la vitesse minimum à 225 
            //if (speed1 < 200)
            //{
            //    speed1 = 225;
            //}
            //if (speed2 < 200)
            //{
            //    speed2 = 225;
            //}
            //if (speed3 < 200)
            //{
            //    speed3 = 225;
            //}

            speed1 = 255;
            speed2 = 255;
            speed3 = 255;
            /***************************************************************************/
           /* //Conversion mètres en position
            position1 = (int)(-60* (positionM1 * positionM1) - 60 * positionM1 + 350);
            positionFine1 = position1 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough1 = position1 / 256;  // Division entière (floor division) --> 8 MSB


            position2 = (int)(-60 * (positionM2 * positionM2) - 60* positionM2 + 350);
            positionFine2 = position2 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough2 = position2 / 256;  // Division entière (floor division) --> 8 MSB

            position3 = (int)(-60 * (positionM3 * positionM3) - 60 * positionM3 + 350);
            positionFine3 = position3 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough3 = position3 / 256;  // Division entière (floor division) --> 8 MSB
           */
           //rajouter une commande manuelle pour unseul moteur 
            //////////////////////////////////////////////////////////////////////////////
           //Conversion mètres en position
           /* position1 = (int)(-50.114 * (positionM1 * positionM1) - 5449.5 * positionM1 + 65535);
            positionFine1 = position1 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough1 = position1 / 256;  // Division entière (floor division) --> 8 MSB
           */

            position2 = (int)(-50.114 * (positionM2 * positionM2) - 5449.5 * positionM2 + 65535);
            positionFine2 = position2 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough2 = position2 / 256;  // Division entière (floor division) --> 8 MSB
            
            position3 = (int)(-50.114 * (positionM3 * positionM3) - 5449.5 * positionM3 + 65535);
            positionFine3 = position3 % 256;   // Modulo --> Retourne le reste de la division --> 8 LSB
            positionRough3 = position3 / 256;  // Division entière (floor division) --> 8 MSB
           
            
            //envoi positions et vitesses
            dmx.Channels[1] = (byte)positionRough1;
            dmx.Channels[2] = (byte)positionFine1;
            dmx.Channels[3] = (byte)speed1;
            //dmx.Channels[4] = (byte)setSoftTopLimit1;
            //dmx.Channels[5] = (byte)setSoftBottomLimit1;
            //dmx.Channels[6] = (byte)movingUpToTopLimit1;
            //dmx.Channels[7] = (byte)movingDown1;

            dmx.Channels[8] = (byte)positionRough2;
            dmx.Channels[9] = (byte)positionFine2;
            dmx.Channels[10]= (byte)speed2;
            //dmx.Channels[11] = (byte)setSoftTopLimit2;
            //dmx.Channels[12] = (byte)setSoftBottomLimit2;
            //dmx.Channels[13] = (byte)movingUpToTopLimit2;
            //dmx.Channels[14] = (byte)movingDown2;

           // dmx.Channels[15] = (byte)positionRough3;
            //dmx.Channels[16] = (byte)positionFine3;
            //dmx.Channels[17] = (byte)speed3;
            //dmx.Channels[18] = (byte)setSoftTopLimit3;
            //dmx.Channels[19] = (byte)setSoftBottomLimit3;
            //dmx.Channels[20] = (byte)movingUpToTopLimit3;
            //dmx.Channels[21] = (byte)movingDown3;

            //Envoies du DMX
            dmx.Send();
            
            //Les anciennes longueurs câble deviennent les nouvelles
            TabOldLenght = TabNewLenght;
        }
    }
}
