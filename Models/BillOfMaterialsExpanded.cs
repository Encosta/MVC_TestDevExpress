using System;
using System.Collections.Generic;

namespace MVC_Test.Models
{
    public class BillOfMaterialsExpanded
    {
        public Guid BillOfMaterialsExpandedId { get; set; }
        public int? BomLevel { get; set; }
        public string TopLevelItem { get; set; }
        public string TopLevelDescription { get; set; }
        public string ParentItem { get; set; }
        public string ParentDescription { get; set; }
        public string ComponentItem { get; set; }
        public string ComponentDescription { get; set; }
        public decimal? QuantityPerTop { get; set; }
        public decimal? QuantityPerParent { get; set; }
        public string PurchasedOrManufactured { get; set; }
        public decimal? ScrapPercentage { get; set; }
        public int? BomSequence { get; set; }
        public string FullSequence { get; set; }
        public string ParentId { get; set; }
        public string HasChild { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? LineCost { get; set; }
        public string ManufacturerCodes { get; set; }
        public DateTime? BomDate { get; set; }
        public string BomReference { get; set; }
        public string BomRelease { get; set; }
    }
}
