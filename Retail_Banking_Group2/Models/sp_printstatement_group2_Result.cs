//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Retail_Banking_Group2.Models
{
    using System;
    
    public partial class sp_printstatement_group2_Result
    {
        public int transactionid { get; set; }
        public Nullable<long> AccountId { get; set; }
        public string AccountType { get; set; }
        public string typeoftransaction { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<decimal> beforebalance { get; set; }
        public Nullable<decimal> afterbalance { get; set; }
        public Nullable<System.DateTime> dateoftransaction { get; set; }
    }
}
