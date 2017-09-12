
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using WebApiMVC.Models;

namespace WebApiMVC.Controllers
{
    #region 0906
    #region 0905
    public class Country
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Population")]
        public int Population { get; set; }
        [XmlElement(ElementName = "Money")]
        public string Money { get; set; }
        [XmlElement(ElementName = "Cities")]
        public List<City> Cities { get; set; }
    }
    public class City
    {
        [XmlElement(ElementName = "CityName")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "AreaCode")]
        public int AreaCode { get; set; }
    }
    [XmlRoot(ElementName = "Data")]
    public class Data
    {
        [XmlElement(ElementName = "Country")]
        public List<Country> Country { get; set; }

    }
    #endregion

    // EnumFieldStatus as a class if you need

    #endregion

    public class HomeController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();
        public ApplicationUserManager UserManager;
        //public HomeController(ApplicationUserManager userManager
        //   )
        //{
        //    UserManager = userManager;

        //}
        public class XmlUtil
        {
            //序列化
            //接收4个参数:srcObject(对象的实例),type(对象类型),xmlFilePath(序列化之后的xml文件的绝对路径),xmlRootName(xml文件中根节点名称)
            //当需要将多个对象实例序列化到同一个XML文件中的时候,xmlRootName就是所有对象共同的根节点名称,如果不指定,.net会默认给一个名称(ArrayOf+实体类名称)
            public static void SerializeToXml(object srcObject, Type type, string xmlFilePath, string xmlRootName)
            {
                if (srcObject != null && !string.IsNullOrEmpty(xmlFilePath))
                {
                    type = type != null ? type : srcObject.GetType();

                    using (StreamWriter sw = new StreamWriter(xmlFilePath))
                    {
                        XmlSerializer xs = string.IsNullOrEmpty(xmlRootName) ?
                            new XmlSerializer(type) :
                            new XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                        xs.Serialize(sw, srcObject);
                    }
                }
            }
        }
        public ActionResult Index()
        {


            //ItemModel Item = new ItemModel { INVNO="1", INVORG="2", ITEM="3", ITEMTYPE="4", LOTNO="5", ONHANDQTY="6" };
            //InsertItemLastFair(Item);
            #region 0906
            Data data = new Data();

            Country country = new Country
            {
                Name = "USA",
                Population = 1000,
                Money = "DOLLAR",
                Cities = new List<City> {
                new City() { AreaCode = 560, CityName = "NEWYORK" },
                new City() { AreaCode = 450, CityName = "WASHINGTON" }
            }
            };
            Country country2 = new Country
            {
                Name = "FRA",
                Population = 300,
                Money = "EURO",
                Cities = new List<City> {
                new City() { AreaCode = 860, CityName = "PARIS" },
                new City() { AreaCode = 40, CityName = "LYON" }
            }
            };
            data.Country = new List<Country>();
            data.Country.Add(country);
            data.Country.Add(country2);
            XmlUtil.SerializeToXml(data, data.GetType(), Path.Combine(HttpContext.Server.MapPath("~\\Content"), "a.xml"), null);
            //XmlDocument document = new XmlDocument();
            //XmlNode xn = 
            //document.AppendChild()
            //string path = Server.MapPath("~/XMLFile1.xml");
            //document.Load(path);

            #endregion

            ViewBag.Title = "Home Page";

            return View();

        }

        public string InsertItemLastFair(ItemModel Item)
        {
            var query = "INSERT into LASTFAIR(";
            query += "LNO,";
            query += "INVORG,";
            query += "ITEM,";
            query += "ITEMTYPE,";
            query += "DESCRIPTION,";
            query += "LOTNO,";
            query += "EXPIRATIONDATE ,";
            query += "ORIGINALDATERECIEVE,";
            query += "SYSTEMQTY,";
            query += "ORACLEQTY,";
            query += "DISCREPANCYQTY,";
            query += "LASTFAIRDATE,";
            query += "LASTFAIRQTY,";
            query += "FAIRSTATUS";
            query += ")";
            query += " VALUES(";
            query += "'" + Item.INVNO + "',"; //Ok
            query += "'" + Item.INVORG + "',"; //ok
            query += "'" + Item.ITEM + "',"; // ok
            query += "'" + Item.ITEMTYPE + "',"; // ok

            query += "'NONE',"; // ok
            query += "'" + Item.LOTNO + "',"; // ok
            query += "TO_DATE('" + DateTime.Now + "','MM/DD/YYYY HH:MI:SS AM'),"; // ok
            query += "TO_DATE('" + DateTime.Now + "','MM/DD/YYYY HH:MI:SS AM'),"; // ok                     
            query += Item.ONHANDQTY + ","; // ok
            query += Item.ONHANDQTY + ","; // ok
            query += 0 + ","; // ok
            query += "TO_DATE('" + DateTime.Now + "','MM/DD/YYYY HH:MI:SS AM'),"; // ok                           
            query += Item.ONHANDQTY + ","; // ok
            query += "'FAIR'";
            query += ")";
            Debug.WriteLine(query); // check output result

            query = "INSERT into LASTFAIR(LNO,INVORG,ITEM,ITEMTYPE,DESCRIPTION,LOTNO,EXPIRATIONDATE ,ORIGINALDATERECIEVE,SYSTEMQTY,ORACLEQTY,DISCREPANCYQTY,LASTFAIRDATE,LASTFAIRQTY,FAIRSTATUS) VALUES('1', '2', '3', '4', 'NONE', '5', TO_DATE('9/7/2017 2:31:38 PM', 'MM/DD/YYYY HH:MI:SS AM'),TO_DATE('9/7/2017 2:31:38 PM', 'MM/DD/YYYY HH:MI:SS AM'), 6, 6, 0, TO_DATE('9/7/2017 2:31:38 PM', 'MM/DD/YYYY HH:MI:SS AM'), 6, 'FAIR')";
            string connectionString = "User Id=scott;Password=tiger;Data Source=oracle";
            using (OracleConnection   connection = new OracleConnection   (
       connectionString))
            {
                OracleCommand command = new OracleCommand(query, connection);
                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally {
                    command.Connection.Close();

                }
                
            }

            return "Seccessfully inserted";
        }

        public IEnumerable<ApplicationUser> Index1()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Users.FirstOrDefault();
            return db.Users.ToList().AsEnumerable();

            //   return View();
        }
        public IEnumerable<ApplicationUser> Index2()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Users.FirstOrDefault();
            return db.Users;

            //   return View();
        }

      
    }
}
