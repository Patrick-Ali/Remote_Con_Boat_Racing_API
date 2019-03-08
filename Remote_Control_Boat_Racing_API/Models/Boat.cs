using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Remote_Control_Boat_Racing_API.Models
{
    public class Boat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        [BsonElement("beam")]
        public string Beam { get; set; } // 17.3 in (440mm)

        [BsonElement("type")]
        public string Type { get; set; } //Catamaran

        [BsonElement("driveSystem")]
        public string DriveSystem { get; set; } //Flex shaft

        [BsonElement("hullHeight")]
        public string HullHeight { get; set; } //9.5 in (241mm)

        [BsonElement("hullMaterial")]
        public string HullMaterial { get; set; } //Fiberglass

        [BsonElement("length")]
        public string Length { get; set; } //48 in (1245mm)

        [BsonElement("motorSize")]
        public string MotorSize { get; set; } //6-pole 1000Kv 56×87mm

        [BsonElement("propellerSize")]
        public string PropellerSize { get; set; } //1.4×1.90 and 1.4×2.0

        [BsonElement("radio")]
        public string Radio { get; set; } //Spektrum DX2E

        [BsonElement("scale")]
        public string Scale { get; set; }  //48-inch

        [BsonElement("speed")]
        public string Speed { get; set; } //55+ mph with 8S Li-Po

        [BsonElement("speedControl")]
        public string SpeedControl { get; set; } //Dynamite 160A HV 2S-8S

        [BsonElement("steering")]
        public string Steering { get; set; } //In-line rudder with break away

        [BsonElement("coluors")]
        public string Coluors { get; set; } //Orange, Gray, White

        [BsonElement("wieght")]
        public string Weight { get; set; } //12.5 lb(7.5kg)

        [BsonElement("captainID")]
        public string CaptainID { get; set; }


        //Boat(double beam, string type, string driveSystem, double hullHeight,
        //    string hullMaterial, double length, string motorSize, string propellerSize,
        //    string radio, double scale, int speed, string speedControl, string steering,
        //    string coluors, double weight) 
        //{
        //     this.beam = beam;
        //     this.type = type;
        //     this.driveSystem = driveSystem;
        //     this.hullHeight = hullHeight;
        //     this.hullMaterial = hullMaterial;
        //     this.length = length;
        //     this.motorSize = motorSize;
        //     this.propellerSize = propellerSize;
        //     this.radio = radio;
        //     this.scale = scale;
        //     this.speed = speed;
        //     this.speedControl = speedControl;
        //     this.steering = steering;
        //     this.coluors = coluors;
        //     this.weight = weight;
        //}
    }
}
