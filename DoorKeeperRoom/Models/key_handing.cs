//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoorKeeperRoom.Models
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class key_handing
    {
        public int id { get; set; }
        public int id_worker { get; set; }
        public int id_key { get; set; }
        public System.DateTime handing_date { get; set; }
        public Nullable<System.DateTime> return_date { get; set; }
    
        public virtual keys keys { get; set; }
        public virtual workers workers { get; set; }
    }
}