using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Remote_Control_Boat_Racing_API.Models
{
    public class Boat
    {
        private double beam; // 17.3 in (440mm)
        private string type; //Catamaran
        private string driveSystem; //Flex shaft
        private double hullHeight; //9.5 in (241mm)
        private string hullMaterial; //Fiberglass
        private double length; //48 in (1245mm)
        private string motorSize; //6-pole 1000Kv 56×87mm
        private string propellerSize; //1.4×1.90 and 1.4×2.0
        private string radio; //Spektrum DX2E
        private double scale;  //48-inch
        private int speed; //55+ mph with 8S Li-Po
        private string speedControl; //Dynamite 160A HV 2S-8S
        private string steering; //In-line rudder with break away
        private string coluors; //Orange, Gray, White
        private double weight; //12.5 lb(7.5kg)

        Boat(double beam, string type, string driveSystem, double hullHeight,
            string hullMaterial, double length, string motorSize, string propellerSize,
            string radio, double scale, int speed, string speedControl, string steering,
            string coluors, double weight) 
        {
             this.beam = beam;
             this.type = type;
             this.driveSystem = driveSystem;
             this.hullHeight = hullHeight;
             this.hullMaterial = hullMaterial;
             this.length = length;
             this.motorSize = motorSize;
             this.propellerSize = propellerSize;
             this.radio = radio;
             this.scale = scale;
             this.speed = speed;
             this.speedControl = speedControl;
             this.steering = steering;
             this.coluors = coluors;
             this.weight = weight;
        }
    }
}
