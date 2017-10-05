using System;
using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GSTInvoiceData.Repository
{
    public class UserRepository
    {
        static GSTInvoiceDBContext dbContext = new GSTInvoiceDBContext();
        public static void RegisterUser(RegisterViewModel registerUser, string baseUrl)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.EmailId = registerUser.EmailId;
            userInfo.FirstName = registerUser.FirstName;
            userInfo.LastName = registerUser.LastName;
            userInfo.Password = registerUser.Password;
            userInfo.UserId = Guid.NewGuid();
            userInfo.RequestTokenNo = Guid.NewGuid().ToString().Replace("-", "");
            userInfo.UserType = 0;
            userInfo.RequestDateTime = DateTime.Now;
            dbContext.userInfo.Add(userInfo);
            dbContext.SaveChanges();

            StringBuilder MessageBody = new StringBuilder("Welcome ");
            MessageBody.Append(userInfo.FirstName + " " + userInfo.LastName);
            MessageBody.Append("!<br/><br/>You can confirm your account email within 24 hrs through the link below:");
            string linkConfirmEmail = baseUrl + "?Account_Confirmation_Token=" + userInfo.RequestTokenNo;
            MessageBody.Append("<br/><br/><a href='" + linkConfirmEmail + "'>" + linkConfirmEmail + "</a>");
            MessageBody.Append("<br/><br/>If you need any assistance, please contact us at <a href='info@peaceinfotech.com'>info@peaceinfotech.com</a>");
            MailModel mailToSend = new MailModel();
            mailToSend.To = userInfo.EmailId;
            mailToSend.Subject = "Email Account Confirmation";
            mailToSend.Body = MessageBody.ToString();
            mailToSend.SendMail(mailToSend);
        }

        public static string SendForgetPasswordLink(string Email,string baseUrl)
        {
            string message = "";
            UserInfo currentUser = dbContext.userInfo.FirstOrDefault(user => user.EmailId.Equals(Email));
            if (currentUser != null)
            {
                if (currentUser.IsEmailVerified)
                {
                    currentUser.RequestTokenNo = Guid.NewGuid().ToString().Replace("-", "");
                    dbContext.Entry(currentUser).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    StringBuilder MessageBody = new StringBuilder("Hello");
                    MessageBody.Append(currentUser.FirstName + " " + currentUser.LastName);
                    MessageBody.Append("!<br/><br/>Someone has requested a link to change your password. You can do this through the link below.");
                    string linkConfirmEmail = baseUrl + "?forget_password_Token=" + currentUser.RequestTokenNo;
                    MessageBody.Append("<br/><br/><a href='" + linkConfirmEmail + "'>" + linkConfirmEmail + "</a>");
                    MessageBody.Append("<br/><br/>If you didn't request this, please ignore this email. Your password won't change until you access the link above and create a new one.");
                    MessageBody.Append("<br/><br/>If you need any assistance, please contact us at <a href='info@peaceinfotech.com'>info@peaceinfotech.com</a>");
                    MailModel mailToSend = new MailModel();
                    mailToSend.To = currentUser.EmailId;
                    mailToSend.Subject = "Password Reset Confirmation";
                    mailToSend.Body = MessageBody.ToString();
                    mailToSend.SendMail(mailToSend);
                    message = "Email with link has been sent to your email for password reset";
                }
                else
                {
                    message = "Please varify your Email";
                }
                
            }
            else
                message= "No User Found";
            return message;
        }

        public static bool IsUserExist(string emailId)
        {
            return dbContext.userInfo.Where(a => a.EmailId == emailId).Any();
        }

        public static UserInfo GetUserByEmail(ForGotPasswordViewModel forgotPassword)
        {
            return dbContext.userInfo
                    .FirstOrDefault(User => User.EmailId.Equals(forgotPassword.EmailId));
        }

        public static UserInfo GetUserByEmailOrMobile(LoginViewModel login)
        {
            return dbContext.userInfo
                    .FirstOrDefault(User => (User.EmailId.Equals(login.EmailId) || User.ContactNumber.Equals(login.EmailId)) &&
                           User.Password.Equals(login.Password));
        }

        public static UserInfo GetUserRequestToken(string requestToken)
        {
            return dbContext.userInfo
                    .FirstOrDefault(User => User.RequestTokenNo.Equals(requestToken));
        }

        public static bool VerifyEmail(UserInfo userInfo)
        {
            bool isVerified = false;
            try
            {
                userInfo.IsEmailVerified = true;
                dbContext.Entry(userInfo).State = EntityState.Modified;
                dbContext.SaveChanges();
                isVerified = true;
            }
            catch
            {

            }
            return isVerified;
        }

        public static void ResetOrChangePassword(UserInfo userInfo)
        {
            dbContext.Entry(userInfo).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
