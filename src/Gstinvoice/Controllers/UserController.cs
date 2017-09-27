using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GSTInvoiceData;
using GSTInvoiceData.Models;
using System.Data.Entity;

namespace Gstinvoice.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {    
            return View();
        }

        #region Register

        [HttpPost]
        public ActionResult Register(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                GSTInvoiceData.Repository.UserRepository.RegisterUser(userInfo, Url.Action("ConfirmEmail", "User", null, "http"));
                TempData["Email"] = userInfo.EmailId;
                return RedirectToAction("RegisterConfirmation", "User");
            }
            return RedirectToAction("Login","User");
        }

        [HttpGet]
        public ActionResult RegisterConfirmation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult IsEmailExist(string emailId)
        {
            bool userExist = GSTInvoiceData.Repository.UserRepository.IsUserExist(emailId);
            return Json(!userExist, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Account_Confirmation_Token)
        {
            TempData["successMsg"] = "Invalid request to confirm Email";
            UserInfo currentUser = GSTInvoiceData.Repository.UserRepository.GetUserRequestToken(Account_Confirmation_Token);
            if (currentUser != null)
            {
                if(GSTInvoiceData.Repository.UserRepository.VerifyEmail(currentUser))
                { 
                    TempData["successMsg"] = "Email Verified Successfully,login to continue..";
                }
            }
            return RedirectToAction("Login", "User");
        }

        #region Forgot Password
        [HttpPost]
        public ActionResult ForgetPassword(UserLogin userLogin)
        {
           TempData["successMsg"] = GSTInvoiceData.Repository.UserRepository.SendForgetPasswordLink(userLogin.EmailId, Url.Action("ResetPassword", "User", null, "http"));
            
            return RedirectToAction("Login", "User");
        }

        public ActionResult ResetPassword(string forget_password_Token)
        {
            GSTInvoiceData.Models.ResetPassword resetPassword = new GSTInvoiceData.Models.ResetPassword();
            resetPassword.RequestToken = forget_password_Token;
            return View(resetPassword);
        }


        [HttpPost]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
                UserInfo currentUser = GSTInvoiceData.Repository.UserRepository.GetUserRequestToken(resetPassword.RequestToken);
                if (currentUser != null)
                {
                currentUser.Password = resetPassword.NewPassword;
                GSTInvoiceData.Repository.UserRepository.ResetOrChangePassword(currentUser);
            }
            return RedirectToAction("Login", "User");
        }
        #endregion
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                UserInfo currentUser = GSTInvoiceData.Repository.UserRepository.GetUserByEmailOrMobile(login);
                if (currentUser != null)
                {
                    if (currentUser.IsEmailVerified)
                    { 
                        Session["LoggedUserId"] = currentUser.UserId.ToString();
                        return RedirectToAction("AfterLogin", "User");
                    }
                    else
                        ViewBag.Message = "Please verify your email for proceeding";
                }
                else
                {
                    ViewBag.Message = "Invalid EmailId/Password";
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult EmailNotExist(string emailId)
        {
            var userExist = GSTInvoiceData.Repository.UserRepository.IsUserExist(emailId);
            return Json(userExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }

        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserInfo userInfo, FormCollection form)
        {
            if (Session["LoggedUserId"] != null)
            {
                using (GSTInvoiceDBContext db = new GSTInvoiceDBContext())
                {
                    string userId = Convert.ToString(Session["LoggedUserId"]);
                    string oldPassword = form["OldPassword"];
                    string password = form["Password"];
                    userInfo = db.userInfo.Where(User => User.Password.Equals(oldPassword) &&
                                        User.UserId.ToString().Equals(userId)).FirstOrDefault();
                    if (userInfo != null)
                    {
                        userInfo.Password = password;
                        db.Entry(userInfo).State = EntityState.Modified;
                        db.SaveChanges();
                        return View(userInfo);
                    }
                }
            }
            return View();
        }
    }
}