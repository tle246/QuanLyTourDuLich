//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transport
    {
        public Transport()
        {
            this.TransportsTourGroups = new HashSet<TransportsTourGroup>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public Nullable<double> price { get; set; }
    
        public virtual Status Status1 { get; set; }
        public virtual ICollection<TransportsTourGroup> TransportsTourGroups { get; set; }
    }
}
