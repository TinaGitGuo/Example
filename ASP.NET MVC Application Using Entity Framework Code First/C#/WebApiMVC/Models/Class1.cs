using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiMVC.Models
{
    public class Profile
    {
        public Profile() { FieldStatuss = new List<FieldStatus>(); }

        public int ProfileId { get; set; }
        public string OwnerID { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Department { get; set; }
        public string Office { get; set; }

       
        public virtual ICollection<FieldStatus> FieldStatuss { get; set; }

    }


    public class FieldStatus
    {
        public FieldStatus() { Profile = new Profile(); }
        [Key]
        public int FieldStatusID { get; set; }

        public string ColumnName { get; set; }// for example :City 
        public EnumFieldStatus EnumFieldStatusV { get; set; } //For example: EnumFieldStatus.OnlyMe  

        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }
    }
    public enum EnumFieldStatus
    {
        [Display(Name = "Only Me.")]
        OnlyMe,
        [Display(Name = "Visible To Everyone.")]
        VisibleToEveryone,
        [Display(Name = "Visible To Office.")]
        VisibleToOffice,
        [Display(Name = "Visible To Department.")]
        VisibleToDepartment,
        [Display(Name = "Not Visible.")]
        NotVisible
    }
}