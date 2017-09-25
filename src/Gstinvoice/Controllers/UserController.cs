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
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                GSTInvoiceData.Repository.UserRepository.RegisterUser(userInfo, Url.Action("ConfirmEmail", "User", null, "http"));
                return RedirectToAction("Index", "User");
            }
            return View(userInfo);
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
            ViewBag.TokenError = "Invalid Request to Confirm Email";
            var currentUser = GSTInvoiceData.Repository.UserRepository.GetUserRequestToken(Account_Confirmation_Token);
            if (currentUser != null)
            {
                GSTInvoiceData.Repository.UserRepository.VerifyEmail(currentUser);
                ViewBag.TokenError = "Your Email Verified Successfully";
            }
            return RedirectToAction("RegisterConfirmation", "User");
        }

        #region Forgot Password
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(UserForGotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GSTInvoiceData.Repository.UserRepository.GetUserByEmail(forgotPassword);
                if (currentUser != null)
                {
                    var resetLink = "<a href='" + Url.Action("ResetPassword", "Home", new { currentUser.EmailId, currentUser.UserId }, "http") + "'>Reset Password</a>";
                    MailModel mailToSend = new MailModel();
                    mailToSend.To = currentUser.EmailId;
                    mailToSend.Subject = "reset password";
                    mailToSend.Body = "please click this below link to reset your password<br/><br/><br/> " + resetLink;
                    mailToSend.SendMail(mailToSend);
                }
            }
            return View();
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
                var currentUser = GSTInvoiceData.Repository.UserRepository.GetUserByEmailOrMobile(login);
                if (currentUser != null)
                {
                    Session["LoggedUserId"] = currentUser.UserId.ToString();
                    return RedirectToAction("AfterLogin", "User");
                }
                else
                {
                    ViewBag.Message = "Invalid EmailId/Password";
                }
            }
            return View(login);
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

        public ActionResult RegisterConfirmation()
        {
            GSTInvoiceDBContext db = new GSTInvoiceDBContext();
            bool emailVarifybit = db.userInfo.Select(user => user.IsEmailVerified).FirstOrDefault() ;
            int bit = emailVarifybit ? 1 : 0;
            if(bit==1)
            {
                ViewBag.Success = "Thank you for your submission";
            }
            else
            {
                ViewBag.Error= "Invalid Request to Confirm Email";
            }
            return View();
        }
    }
}