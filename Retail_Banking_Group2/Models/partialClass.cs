using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Retail_Banking_Group2.Models
{
    //[MetadataType(typeof(loginexecutive_group2Metadata))]
    //public partial class loginexecutive_group2
    //{
    //}
    //public partial class loginexecutive_group2Metadata
    //{
    //    public int loginid { get; set; }
    //    [Required(ErrorMessage = "Please enter Username")]
    //    public string Username { get; set; }
    //    [Required(ErrorMessage = "Please enter Password")]
    //    [DataType(DataType.Password)]
    //    [RegularExpression("([a-z]&[A-Z]&[0-9]&[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
    //    public string userpassword { get; set; }
    //    public Nullable<int> userroleid { get; set; }
    //}


    [MetadataType(typeof(sp_loginexecutive_group2_ResultMetadata))]
    public partial class sp_loginexecutive_group2_Result
    {

    }
    public partial class sp_loginexecutive_group2_ResultMetadata
    {
        public int loginid { get; set; }
        [Required(ErrorMessage = "Please enter Username")]
        [MaxLength(20, ErrorMessage = "UserName should be less than 20 character")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression("([a-z]&[A-Z]&[0-9]&[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string userpassword { get; set; }
        public Nullable<int> userroleid { get; set; }
    }
    [MetadataType(typeof(CustomerTable_group2Metadata))]
    public partial class CustomerTable_group2
    {

    }
    public partial class CustomerTable_group2Metadata
    {

        public long CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter a Aadhar Number")]
        [Range(11111111111, 999999999999, ErrorMessage = "it is not a valid Aadhar No")]
        public long AadhaarID { get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Invalid Name ")]
        [MaxLength(30, ErrorMessage = "Name should be less than 20 character")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please select Date Of Birth")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Dob { get; set; }

        [Required(ErrorMessage = "Please enter your Address")]
        [DataType(DataType.MultilineText)]
        [MaxLength(30, ErrorMessage = "AddressLine1 should be less than 30 character")]
        public string AddressLine1 { get; set; }

        //[Required(ErrorMessage = "Please enter your Address")]
        [DataType(DataType.MultilineText)]
        [MaxLength(30, ErrorMessage = "AddressLine2 should be less than 30 character")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please select a State")]
        public Nullable<int> State { get; set; }

        [Required(ErrorMessage = "Please select a City")]
        public Nullable<int> City { get; set; }


        [Required(ErrorMessage = "Please enter your contact Number")]
        [Range(1111111111, 9999999999, ErrorMessage = "enter a valid 10digit mobile number")]
        public Nullable<long> Contact { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please Enter Correct Email Address Format")]
        [Required]
        public string Email { get; set; }


        public Nullable<int> NoofAccounts { get; set; }


        public string CustomerStatus { get; set; }


        public virtual city_group2 city_group2 { get; set; }
        public virtual state_group2 state_group2 { get; set; }
    }
    [MetadataType(typeof(addaccount_group2Metadata))]
    public partial class addaccount_group2
    {
    }
    public partial class addaccount_group2Metadata
    {
        public long AccountId { get; set; }
        public Nullable<long> CustomerID { get; set; }
        [Required(ErrorMessage = "Please Select account type")]
        public Nullable<int> AccountType { get; set; }
        [Required(ErrorMessage = "Please Enter Amount ")]
        public Nullable<decimal> DepositAmount { get; set; }

        public virtual accountdropdown_group2 accountdropdown_group2 { get; set; }
        public virtual CustomerTable_group2 CustomerTable_group2 { get; set; }
    }
    [MetadataType(typeof(sp_viewbyid_CustomerTable_group2_ResultMetadata))]
    public partial class sp_viewbyid_CustomerTable_group2_Result
    {

    }
    public partial class sp_viewbyid_CustomerTable_group2_ResultMetadata
    {
        public long CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter a Aadhar Number")]
        [Range(11111111111, 999999999999, ErrorMessage = "it is not a valid Aadhar No")]
        public long AadhaarID { get; set; }
        [Required(ErrorMessage = "Please enter your Name")]
        [MaxLength(30, ErrorMessage = "Name should be less than 20 character")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please select Date Of Birth")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Dob { get; set; }
        [Required(ErrorMessage = "Please enter your Address")]
        [DataType(DataType.MultilineText)]
        [MaxLength(30, ErrorMessage = "AddressLine1 should be less than 30 character")]
        public string AddressLine1 { get; set; }
        //[Required(ErrorMessage = "Please enter your Address")]
        [DataType(DataType.MultilineText)]
        [MaxLength(30, ErrorMessage = "AddressLine2 should be less than 30 character")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please select a State")]
        public Nullable<int> State { get; set; }
        [Required(ErrorMessage = "Please select a City")]
        public Nullable<int> City { get; set; }

        [Required(ErrorMessage = "Please enter your contact Number")]
        [Range(7000000000, 9999999999, ErrorMessage = "enter a valid 10digit mobile number")]
        public Nullable<long> Contact { get; set; }
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
          ErrorMessage = "Please Enter Correct Email Address Format")]
        [Required]
        public string Email { get; set; }
        public Nullable<int> NoofAccounts { get; set; }
        public string CustomerStatus { get; set; }
    }

}