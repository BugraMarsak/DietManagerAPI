using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
        public class MeasurementResult
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime MeasurementTime { get; set; }
        public double Weight { get; set; } //ağırlık
        public double BKI { get; set; } // beden kitle indeksi
        public double BodyFatRatio { get; set; } //vücut yağ oranı
        public double IKA { get; set; } //iskelet kası ağırlığı
        public double Protein { get; set; }
        public double Mineral { get; set; }
        public double TargetWeight { get; set; }
        public double TotalBodyWater { get; set; }
        public double BodyFatWeight { get; set; }
        public double VisceralFatLevel { get; set; }
        public double TargetVisceralFatLevel { get; set; }
    }
}
