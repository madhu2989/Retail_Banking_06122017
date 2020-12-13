

using Retail_Banking_Group2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retail_Banking_Group2.Controllers
{
    public class CashierController : Controller
    {
        bankEntities bank = new bankEntities();

        public ActionResult cashierView()
        {

            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "2")
                {
                    List<sp_viewCashier_group2_Result> li = new List<sp_viewCashier_group2_Result>();
                    li = bank.sp_viewCashier_group2().ToList();
                    return View(li);
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

        public ActionResult deposit(long? id, decimal? balamt)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "2")
                {
                    cashierViewAccountbyId_group2_Result objAccount = new cashierViewAccountbyId_group2_Result();
                    if (id != null)
                    {

                        objAccount = bank.cashierViewAccountbyId_group2(id).FirstOrDefault();
                        return View(objAccount);
                    }


                    return View();
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

        [HttpPost]
        public ActionResult deposit(addaccount_group2 objTran,long id, decimal balamt)
        {
            try
            {
                ViewBag.username = Session["userName"];
                cashierViewAccountbyId_group2_Result objAccount = new cashierViewAccountbyId_group2_Result();
                if (objTran.DepositAmount <= 0 || objTran.DepositAmount > 100000 || objTran.DepositAmount == null)
                {
                    objAccount.AccountId = objTran.AccountId;
                    objAccount.DepositAmount = objTran.DepositAmount;
                    ViewBag.message = "enter valid amount";
                    return View(objAccount);
                }
                else
                {
                    ObjectParameter objParam = new ObjectParameter("latestbalance", typeof(decimal));
                    int result;
                    if (objTran.AccountType == 1)
                    {
                        result = bank.sp_Deposit_group2(id, "Savings ", "deposit", objTran.DepositAmount, balamt, objParam);
                    }
                    else
                    {
                        result = bank.sp_Deposit_group2(id, "Current ", "deposit", objTran.DepositAmount, balamt, objParam);
                    }
                    if (result > 0)
                    {
                        this.bank.SaveChanges();
                        TempData["Messdep"] = "Successfully Deposited! The new available balance is Rs." + objParam.Value;

                    }
                    return RedirectToAction("cashierView");

                }



            }

            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult withdraw(long? id, decimal? balamt)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "2")
                {
                    cashierViewAccountbyId_group2_Result objaccount = new cashierViewAccountbyId_group2_Result();
                    if (balamt == 0)
                    {
                        TempData["Messagewithdraw"] = "Your balance is RS." + balamt;
                        return RedirectToAction("cashierView");

                    }
                    else
                    {
                        if (id != null)
                        {

                            objaccount = bank.cashierViewAccountbyId_group2(id).FirstOrDefault();
                            int? transctns = bank.sp_Totaltransactions(objaccount.CustomerID, DateTime.Today.Date).FirstOrDefault();
                            int a = transctns.GetValueOrDefault();
                            if (a > 3)
                            {
                                TempData["msg"] = "transactions limit has been reached";
                                return RedirectToAction("cashierView");
                            }
                            return View(objaccount);
                        }
                    }
                    return RedirectToAction("cashierView");

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

        [HttpPost]
        public ActionResult Withdraw(addaccount_group2 withd,long id, decimal? balamt)
        {
            try
            {
                cashierViewAccountbyId_group2_Result objAccount = new cashierViewAccountbyId_group2_Result();
                if (withd.DepositAmount < 0 || withd.DepositAmount > balamt || withd.DepositAmount == 0 || withd.DepositAmount == null)
                {
                    objAccount.AccountId = withd.AccountId;
                    objAccount.DepositAmount = withd.DepositAmount;
                    ViewBag.messageinsuf = "invalid amount";

                    return View();
                }

                else
                {
//                    @AccountId bigint,
//@AccountType varchar(50),
//@TransactionType varchar(50),
//@amount money,
//@beforebalance money,
//@latestbalance money out
                    ObjectParameter objParam = new ObjectParameter("latestbalance", typeof(decimal));
                    int result;
                    if (withd.AccountType == 1)
                    {
                        result = bank.sp_WithdrawAmount_group2(id, "Savings", "withdraw", withd.DepositAmount, balamt, objParam);
                    }
                    else
                    {
                        result = bank.sp_WithdrawAmount_group2(id, "Current", "withdraw", withd.DepositAmount, balamt, objParam);
                    }
                    if (result > 0)
                    {
                        this.bank.SaveChanges();
                        TempData["Messwith"] = "Successfully Withdrawn! The new available balance is Rs." + objParam.Value;
                    }
                    return RedirectToAction("cashierView");
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }




        public ActionResult Transfer(long? id, long? cid, int? acctype, decimal? bal)
        {
            try
            {
                ViewBag.username = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "2")
                {
                    sp_selectCustomerAccountIdforTransfer_group2_Result cusacc = new sp_selectCustomerAccountIdforTransfer_group2_Result();
                    int NoOfacc = Convert.ToInt32(bank.sp_checknoofAccounts_group2(cid).FirstOrDefault());
                    if (NoOfacc == 2)
                    {
                        if (id != null)
                        {

                            cusacc = bank.sp_selectCustomerAccountIdforTransfer_group2(cid, id).FirstOrDefault();
                            int? transctns = bank.sp_Totaltransactions(cusacc.CustomerID, DateTime.Today.Date).FirstOrDefault();
                            int a = transctns.GetValueOrDefault();
//ALTER proc [dbo].[sp_Totaltransactions]
//@CustomerId bigint,
//@date date
//as
//begin
//select count(*) as TransactionsCount from addaccount_group2
//inner join transactiontable_group2 on addaccount_group2.AccountId=transactiontable_group2.AccountId
//where (addaccount_group2.CustomerID=@CustomerId and transactiontable_group2.dateoftransaction=@date)
//end
                            if (a > 3)
                            {
                                TempData["msg"] = "transactions limit has been reached";

                                return RedirectToAction("cashierView");
                            }
                            Session["SAccidsource"] = cusacc.AccountType;
                            Session["SAccId"] = id;
                            Session["SAccBal"] = bal;
                            Session["SAccType"] = acctype;
                          
                            if (cusacc.AccountType == 1)
                            {
                                Session["DAccType"] = "Savings";

                            }
                            else
                                Session["DAccType"] = "Current";
                        }
                        return View(cusacc);
                    }
                    else
                    {
                        TempData["notransfer"] = "This customer has only one account";
                        return RedirectToAction("cashierView");
                    }

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

        [HttpPost]
        public ActionResult Transfer(long id, long cid, addaccount_group2 acctable)
        {
            try
            {
                sp_selectCustomerAccountIdforTransfer_group2_Result cusacc = new sp_selectCustomerAccountIdforTransfer_group2_Result();
               
                if (acctable.DepositAmount > Convert.ToDecimal(Session["SAccBal"]) || acctable.DepositAmount <= 0)
                {

                  
                    TempData["transferfailed"] = "Cannot transfer the entered amount or check your balance!";
                    return RedirectToAction("Transfer");

                }
                else
                {
                    cusacc = bank.sp_selectCustomerAccountIdforTransfer_group2(cid, id).FirstOrDefault();


                    ObjectParameter balsrc = new ObjectParameter("up_balS", typeof(decimal));
                    ObjectParameter baldes = new ObjectParameter("up_balD", typeof(decimal));
                    int result;
                    string type = Convert.ToString(Session["SAccType"]);
                    if (type == "Savings")
                        result = bank.sp_transfer_group2(Convert.ToInt64(Session["SAccId"]),cusacc.AccountId, Convert.ToInt32(Session["SAccType"]), Convert.ToInt32(Session["SAccidsource"]), Convert.ToDecimal(Session["SAccBal"]), cusacc.DepositAmount, acctable.DepositAmount, balsrc, baldes);
                    else
                        result = bank.sp_transfer_group2(Convert.ToInt64(Session["SAccId"]), cusacc.AccountId, Convert.ToInt32(Session["SAccType"]), Convert.ToInt32(Session["SAccidsource"]), Convert.ToDecimal(Session["SAccBal"]), cusacc.DepositAmount, acctable.DepositAmount, balsrc, baldes);
                    if (result > 0)
                    {
                        this.bank.SaveChanges();
                        TempData["Messtransferred"] = "Successfully Transferred! The new available balance is Rs. " + balsrc.Value;

                    }
                    return RedirectToAction("cashierView");
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        public ActionResult PrintStatement(long? accountid, int? NumberofTransactions, DateTime? Startdate, DateTime? EndDate)
        {
            try
            {
                ViewBag.user_name = Session["userName"];
                if (Session["userName"] != null && Session["userroleId"].ToString() == "2")
                {
                    if (accountid == null)
                        return View("print");
                    else if (Startdate == null && EndDate == null)
                    {
                        List<sp_printstatement_group2_Result> probj = new List<sp_printstatement_group2_Result>();
                        probj = bank.sp_printstatement_group2(accountid, NumberofTransactions).ToList();
                        return View(probj);
                    }
                    else
                    {
                        List<sp_statementbyDate_group2_Result> probj = new List<sp_statementbyDate_group2_Result>();
                        probj = bank.sp_statementbyDate_group2(accountid, Startdate, EndDate).ToList();
                        return View("printstatementbyDate", probj);
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



    }
}