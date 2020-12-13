using Retail_Banking_Group2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retail_Banking_Group2.Controllers
{
    public class NewAccountController : Controller
    {
        // GET: NewAccount
        bankEntities bank = new bankEntities();
        public ActionResult ViewAccount()
        {
            try
            {
                ViewBag.user_name = Session["userName"];
                //if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                //{

                //    }
                List<sp_viewAccount_group2_Result> li = new List<sp_viewAccount_group2_Result>();
                li = bank.sp_viewAccount_group2().ToList();
                return View(li);
                //else
                //{
                //    return Redirect("/Login/login");
                //}
            }
            catch (Exception e)
            {

                throw e;
            }

        }



    }
}