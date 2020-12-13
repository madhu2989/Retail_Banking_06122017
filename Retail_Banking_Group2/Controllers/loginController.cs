using Retail_Banking_Group2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retail_Banking_Group2.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        bankEntities bank = new bankEntities();
        public ActionResult login()
        {
            return View();
        }
         

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(sp_loginexecutive_group2_Result byname)
        {
            try
            {
                sp_loginexecutive_group2_Result obj = new sp_loginexecutive_group2_Result();
                obj = bank.sp_loginexecutive_group2(byname.Username).FirstOrDefault();
                if (obj != null)
                {
                    if (obj.Username == byname.Username && obj.userpassword == byname.userpassword)
                    {
                        Session["userName"] = obj.Username.ToString();
                        Session["userroleId"] = obj.userroleid.ToString();
                        if (obj.userroleid.ToString() == "1")
                        {
                            return RedirectToAction("../executivecustomer/customerexecutive");
                        }
                        else
                        {
                            return RedirectToAction("../cashieroperation/cashieroperation");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "invalid Credentials";
                        return RedirectToAction("login");
                    }
                }
                else
                {
                    ViewBag.Message = "please enter credentials";
                    return View();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
            
        }

        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return View("../frontpage/frontpage");
        }
    }
}