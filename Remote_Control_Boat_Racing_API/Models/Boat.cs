using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Remote_Control_Boat_Racing_API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Boat
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("beam")]
        public string Beam { get; set; } // 17.3 in (440mm)

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("beamM")]
        public string BeamM { get; set; } // in 

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("type")]
        public string Type { get; set; } //Catamaran

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("driveSystem")]
        public string DriveSystem { get; set; } //Flex shaft

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("hullHeight")]
        public string HullHeight { get; set; } //9.5 in (241mm)

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("hullHeightM")]
        public string HullHeightM { get; set; } // in 

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("hullMaterial")]
        public string HullMaterial { get; set; } //Fiberglass

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("length")]
        public string Length { get; set; } //48 in (1245mm)

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("lengthM")]
        public string LengthM { get; set; } // in 

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("motorSize")]
        public string MotorSize { get; set; } //6-pole 1000Kv 56×87mm

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("propellerSize")]
        public string PropellerSize { get; set; } //1.4×1.90 and 1.4×2.0

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("radio")]
        public string Radio { get; set; } //Spektrum DX2E

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("scale")]
        public string Scale { get; set; }  //48-inch

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("scaleM")]
        public string ScaleM { get; set; }  // inch

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("speed")]
        public string Speed { get; set; } //55+ mph with 8S Li-Po

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("speedM")]
        public string SpeedM { get; set; } // mph 

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("speedControl")]
        public string SpeedControl { get; set; } //Dynamite 160A HV 2S-8S

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("steering")]
        public string Steering { get; set; } //In-line rudder with break away

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("coluors")]
        public string Coluors { get; set; } //Orange, Gray, White

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("wieght")]
        public string Weight { get; set; } //12.5 lb(7.5kg)

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("wieghtM")]
        public string WeightM { get; set; } // lb

        /// <summary>
        /// 
        /// </summary>
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
