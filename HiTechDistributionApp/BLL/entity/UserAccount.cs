//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiTechDistributionApp.BLL.entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAccount
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public Nullable<int> StatusId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
    }
}
