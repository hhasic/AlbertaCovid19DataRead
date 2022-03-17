using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AlbertaCovid19DataRead
{ 
    public enum AreaType
    {
        Municipality = 1,
        LocalGeographicArea = 2
    }

    public enum CasesAreas
    {
        Canada = 1,
        Alberta = 2,
        CalgaryZone = 3,
        EdmontonZone = 4,
        NorthZone = 5,
        CentralZone = 6,
        SouthZone = 7,
        Unknown = 8
    }

    [Table("Cases")]
    public class Cases
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CasesArea { get; set; }
        public int? Confirmed { get; set; }
        public int? Active { get; set; }
        public int? Recovered { get; set; }
        public int? InHospital { get; set; }
        public int? InIntensiveCare { get; set; }
        public int? Deaths { get; set; }
        public int? Tests { get; set; }
    }

    [Table("LabTesting")]
    public class LabTesting
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EdmontonZone { get; set; }
        public int CalgaryZone { get; set; }        
        public int CentralZone { get; set; }
        public int NorthZone { get; set; }
        public int SouthZone { get; set; }
        public int UnknownZone { get; set; }
    }

    [Table("Area")]
    public class Area
    {
        public Area()
        {
            AreaCovidData = new HashSet<AreaCovidInfo>();
        }

        public int AreaId { get; set; }
        public int AreaType { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GeometryData { get; set; }
        public virtual ICollection<AreaCovidInfo> AreaCovidData { get; set; }
       
    }

    [Table("AreaCovidInfo")]
    public class AreaCovidInfo
    {
        public int Id { get; set; }
        [Required]
        public int AreaId { get; set; }
        public DateTime Date { get; set; }
        public int Cases { get; set; }
        public int Active { get; set; }
        public int Recovered { get; set; }
        public int Deaths { get; set; }
        public virtual Area Area { get; set; }
    }
}
