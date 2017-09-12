using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Methods
{
    public class PeopleStateViewModel
    {
        [Key]
        public int ID { get; set; }
        public People People { get; set; }
        public Dictionary<string, string> States { get; set; }

        //public PeopleStateViewModel(People people)
        //{
        //    People = people;
        //    //States = new StatesDictionary();
        //}
        //public static SelectList StateSelectList
        //{
        //    get { return new SelectList(StateDictionary, "Value", "Key"); }
        //}
        //public static readonly IDictionary<string, string>
        //    StateDictionary = new Dictionary<string, string> {
        //        {"Choose...",""}
        //        , { "Alabama", "AL" }
        //        , { "Alaska", "AK" }
        //        , { "Arizona", "AZ" }
        //        , { "Arkansas", "AR" }
        //        , { "California", "CA" }
        //        // code continues to add states...
        //    };
    }

    //I Have these two models:
    public class People
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }
    }

}