//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyTourDuLich.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transport
    {
        public Transport()
        {
            this.TourGroupDetails = new HashSet<TourGroupDetail>();
        }
    
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    
        public virtual Status Status1 { get; set; }
        public virtual ICollection<TourGroupDetail> TourGroupDetails { get; set; }
    }
}
