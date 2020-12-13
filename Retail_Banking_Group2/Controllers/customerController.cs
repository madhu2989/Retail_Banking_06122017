using Retail_Banking_Group2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retail_Banking_Group2.Controllers
{
    public class customerController : Controller
    {
        // GET: customer
        bankEntities bank = new bankEntities();
        public JsonResult GetStates()
        {
            return Json(bank.sp_state_group2().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCitiesByStateId(string stateid)
        {
            int Id = Convert.ToInt32(stateid);

            return Json(bank.sp_city_group2(Id).ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListCustomer()
        {

            try
            {
                ViewBag.userName = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    //List<sp_view_CustomerTable_group2_Result> lstCustomer = new List<sp_view_CustomerTable_group2_Result>();
                    //lstCustomer = bank.sp_view_CustomerTable_group2().ToList();

                    List<sp_view_CustomerTable_CONCAT_GROUP2_Result1> lstCustomer = new List<sp_view_CustomerTable_CONCAT_GROUP2_Result1>();
                    lstCustomer = bank.sp_view_CustomerTable_CONCAT_GROUP2().ToList();
                    return View(lstCustomer);
                }
                else
                {
                    return Redirect("../login/login");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ActionResult addAccountlist()
        {

            try
            {
                ViewBag.userName = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    //List<sp_view_CustomerTable_group2_Result> lstCustomer = new List<sp_view_CustomerTable_group2_Result>();
                    //lstCustomer = bank.sp_view_CustomerTable_group2().ToList();
                    //return View(lstCustomer);
                    List<sp_view_CustomerTable_CONCAT_GROUP2_Result1> lstCustomer = new List<sp_view_CustomerTable_CONCAT_GROUP2_Result1>();
                    lstCustomer = bank.sp_view_CustomerTable_CONCAT_GROUP2().ToList();
                    return View(lstCustomer);
                }
                else
                {
                    return Redirect("../login/login");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ActionResult editpage()
        {

            try
            {
                ViewBag.userName = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    //List<sp_view_CustomerTable_group2_Result> lstCustomer = new List<sp_view_CustomerTable_group2_Result>();
                    //lstCustomer = bank.sp_view_CustomerTable_group2().ToList();
                    //return View(lstCustomer);
                    List<sp_view_CustomerTable_CONCAT_GROUP2_Result1> lstCustomer = new List<sp_view_CustomerTable_CONCAT_GROUP2_Result1>();
                    lstCustomer = bank.sp_view_CustomerTable_CONCAT_GROUP2().ToList();
                    return View(lstCustomer);

                }
                else
                {
                    return Redirect("../login/login");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ActionResult deletepage()
        {

            try
            {
                ViewBag.userName = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    //List<sp_view_CustomerTable_group2_Result> lstCustomer = new List<sp_view_CustomerTable_group2_Result>();
                    //lstCustomer = bank.sp_view_CustomerTable_group2().ToList();
                    //return View(lstCustomer);

                    List<sp_view_CustomerTable_CONCAT_GROUP2_Result1> lstCustomer = new List<sp_view_CustomerTable_CONCAT_GROUP2_Result1>();
                    lstCustomer = bank.sp_view_CustomerTable_CONCAT_GROUP2().ToList();
                    return View(lstCustomer);
                }
                else
                {
                    return Redirect("../login/login");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }


        // GET : add customer 
        public ActionResult AddCustomer()
        {
            ViewBag.user_name = Session["userName"];

            try
            {


                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    return View();
                }

                else
                {
                    return Redirect("../Login/login");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerTable_group2 tablecustomer)
        {
            try
            {
                ViewBag.userName = Session["userName"];
                ObjectParameter retCustID = new ObjectParameter("customeridout", typeof(long));

                DateTime date = new DateTime();
                date = Convert.ToDateTime(tablecustomer.Dob);
                int age = DateTime.Today.Year - date.Year;
                if (age < 18 || age > 90 || age==0)
                {
                    TempData["Show"] = "Age should be between 18-90 years";
                    return View();

                }
                else
                {
                    int State = Convert.ToInt32(Request.Form["dropdownState"]);
                    int City = Convert.ToInt32(Request.Form["dropdownCity"]);


                    int res = bank.sp_CustomerTable_group2(tablecustomer.CustomerID, tablecustomer.AadhaarID, tablecustomer.CustomerName,
                        tablecustomer.Dob, tablecustomer.AddressLine1, tablecustomer.AddressLine2, State, City,
                        tablecustomer.Contact, tablecustomer.Email, retCustID);
                    this.bank.SaveChanges();
                    long rescust = Convert.ToInt64(retCustID.Value);
                    ViewBag.message = "Customer added successfully and the Customer ID is " + rescust;
                    
                }   

                return View();

            }
            catch (Exception e)
            {
                TempData["Aadharno"] = "This aadhar number has already an account";
                return RedirectToAction("AddCustomer");
                
                throw e;
            }

        }

        public ActionResult Delete(long? id)
        {
            
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {

                    if (id != null)
                    {
                        sp_viewbyid_CustomerTable_group2_Result byid = new sp_viewbyid_CustomerTable_group2_Result();
                        byid = bank.sp_viewbyid_CustomerTable_group2(id).FirstOrDefault();
                        return View(byid);
                    }

                }
                else
                {
                    return Redirect("../login/login");
                }
                return View();
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public ActionResult Delete(long id, sp_viewbyid_CustomerTable_group2_Result byid)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (byid.NoofAccounts == 0)
                {
                    int res = bank.sp_deletebyid_CustomerTable_group2(id);
                    this.bank.SaveChanges();

                    TempData["Mess5"] = "Customer Deactivated Succesfully";
                    return RedirectToAction("ListCustomer");
                }
                else
                {
                    TempData["mess6"] = "Unable To Delete as the account has money";
                    return View();
                }

            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult customerEdit(long? id)
        {
            ViewBag.username = Session["userName"];

            if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
            {
                try
                {
                    sp_viewbyid_CustomerTable_group2_Result ob = bank.sp_viewbyid_CustomerTable_group2(id).FirstOrDefault();
                    Session["state"] = ob.State;
                    Session["city"] = ob.City;

                    return View(ob);

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            else
            {
                return Redirect("/login/login");
            }
        }


        [HttpPost]
        public ActionResult customerEdit(sp_viewbyid_CustomerTable_group2_Result tablecustomer)
        {
            try
            {
                ViewBag.username = Session["userName"];
                ObjectParameter retCustID = new ObjectParameter("customeridout", typeof(int));

                DateTime date = new DateTime();
                date = Convert.ToDateTime(tablecustomer.Dob);
                int age = DateTime.Today.Year - date.Year;
                if (age < 18 || age > 90 || age==0)
                {
                    ViewBag.Show = "Customer is not in the age range of 18 to 90";
                    return View();

                }
                else
                {
                    int State = Convert.ToInt32(Request.Form["State"]);
                    int City = Convert.ToInt32(Request.Form["City"]);


                    int res = bank.sp_CustomerTable_group2(tablecustomer.CustomerID, tablecustomer.AadhaarID, tablecustomer.CustomerName,
                        tablecustomer.Dob, tablecustomer.AddressLine1, tablecustomer.AddressLine2, State, City,
                        tablecustomer.Contact, tablecustomer.Email, retCustID);
                    this.bank.SaveChanges();

                    TempData["Message"] = "Customer Updated Successfully";
                    return RedirectToAction("ListCustomer");
                }

              

            }
            catch (Exception e)
            {

                throw e;
            }

        }



        //Accounts add
       
        public ActionResult listofaccounts()
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    List<sp_viewaccounttablewithstatus_Result> lst = new List<sp_viewaccounttablewithstatus_Result>();
                    lst = bank.sp_viewaccounttablewithstatus().ToList();
                    return View(lst);
                }
                else
                {
                    return Redirect("../login/login");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public ActionResult addaccountfromlist(long? id, int? noofacc)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {
                    Session["CustomerId"] = id;
                    int Noofacc = Convert.ToInt32(bank.sp_checknoofAccounts_group2(id).FirstOrDefault());

                    if (Noofacc != 2)
                    {
                        if(Noofacc==0)
                        {
                            IEnumerable<SelectListItem> items = bank.accountdropdown_group2.Select(c => new SelectListItem
                            {
                                Value = c.id.ToString(),
                                Text = c.Accounttype
                            });

                            ViewBag.Message = items;

                            return View();
                            //return RedirectToAction("addnewaccounts");
                        }
                        else
                        {
                            int acctype = Convert.ToInt32(bank.sp_accountTypecheck_gropu2(id).FirstOrDefault());
                            if (acctype == 2)
                            {

                                Session["accounttype"] = 1;
                                Session["accounttypename"] = "Savings";
                                return View("addaccountsaving");

                            }
                            else
                            {

                                Session["accounttype"] = 2;
                                Session["accounttypename"] = "Current";
                                return View("addaccountcurrent");
                            }

                        }
                    


                    }
                    else
                    {
                        TempData["already two accounts"] = "you have already two accounts";
                        return RedirectToAction("addAccountlist");
                    }

                }
                else
                {
                    return Redirect("../login/login");
                }

               

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult addaccountfromlist(addaccount_group2 addacc, long id)
        {
            try
            {
                ViewBag.username = Session["userName"];
                Session["CustomerId"] = id;
                int Noofacc = Convert.ToInt32(bank.sp_checknoofAccounts_group2(id).FirstOrDefault());

                if (Noofacc != 0)
                {
                    Session["CustomerId"] = id;
                    ObjectParameter retAcc = new ObjectParameter("Accountidout", typeof(long));
                    int acctypeid = Convert.ToInt16(Session["accounttype"]);
                    if (acctypeid==1)
                    {
                        if (addacc.DepositAmount <= 0 || addacc.DepositAmount < 1000 || addacc.DepositAmount > 100000)
                        {
                            TempData["amtdepositmin"] = "minimum amount should be more than 1000 or no negative amount";
                            return RedirectToAction("addaccountfromlist");                          
                        }
                        else
                        {
                            int res = bank.sp_addaccount_group2(addacc.AccountId, id, acctypeid, addacc.DepositAmount, retAcc);
                            this.bank.SaveChanges();
                            long resacc = Convert.ToInt64(retAcc.Value);
                            return RedirectToAction("listofaccounts");
                        }

                  
                    }
               
                }
                else
                {
                    if (addacc.DepositAmount <= 0 || addacc.DepositAmount < 5000 || addacc.DepositAmount > 100000)
                        {
                            TempData["amtdepositmincurrent"] = "minimum amount should be 5000";
                            return RedirectToAction("addaccountfromlist");
                            
                        }
                        else
                        {

                            ObjectParameter retAcc = new ObjectParameter("Accountidout", typeof(long));
                            id = Convert.ToInt64(Session["CustomerId"]);

                            int res = bank.sp_addaccount_group2(addacc.AccountId, id, addacc.AccountType, addacc.DepositAmount, retAcc);
                            this.bank.SaveChanges();
                            long resacc = Convert.ToInt64(retAcc.Value);


                            return RedirectToAction("listofaccounts");
                        }

                }
                   
               // }
                //else
                //{

                //    ViewBag.Message = "Two accounts already exist for this CustomerId " + id;
                //    return View("listofaccounts");

                //}

                return View();



            }

            catch (Exception)
            {
                
                throw;
            }



        }


        public ActionResult deleteaccount(long? accid)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "1")
                {


                    sp_viewbyAccountID_group2_Result byid = new sp_viewbyAccountID_group2_Result();
                    byid = bank.sp_viewbyAccountID_group2(accid).FirstOrDefault();
                    return View(byid);
                }
                else
                {
                    return Redirect("../login/login");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           






        }
        [HttpPost]
        public ActionResult deleteaccount(decimal? balamt, int? acctype, long? accid,long? cusid)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (balamt != 0)
                {

                    TempData["cannotdelete"] = "You cannot delete active customer";
                    return View("listofaccounts");
                        
                
                }
                else
                {
                    int res = bank.sp_deleteAccount_group2(accid, cusid, balamt);
                    this.bank.SaveChanges();
                    TempData["Messageofdeleteaccount"] = "Account id of" + accid + " has been inactivated successfully ";
                    return RedirectToAction("listofaccounts");
                   
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }

}