using System;
using GSTInvoiceData.Models;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GSTInvoiceData.Repository
{
    public class UserRepository
    {
        static GSTInvoiceDBContext dbContext = new GSTInvoiceDBContext();
        public static void RegisterUser(UserInfo userInfo, string baseUrl)
        {
            userInfo.UserId = Guid.NewGuid();
            userInfo.RequestTokenNo = Guid.NewGuid().ToString().Replace("-", "");
            userInfo.UserType = 0;
            userInfo.RequestDateTime = DateTime.Now;
            dbContext.userInfo.Add(userInfo);
            dbContext.SaveChanges();

            StringBuilder MessageBody = new StringBuilder("Welcome ");
            MessageBody.Append(userInfo.FirstName + " " + userInfo.LastName);
            MessageBody.Append("!<br/><br/>You can confirm your account email through the link below:");
            string linkConfirmEmail = baseUrl + "?Account_Confirmation_Token=" + userInfo.RequestTokenNo;
            MessageBody.Append("<br/><br/><a href='" + linkConfirmEmail + "'>" + linkConfirmEmail + "</a>");
            MessageBody.Append("<br/><br/>If you need any assistance, please contact us at info@peaceinfotech.com.");
            MailModel mailToSend = new MailModel();
            mailToSend.To = userInfo.EmailId;
            mailToSend.Subject = "Email Account Confirmation";
            mailToSend.Body = MessageBody.ToString();
            mailToSend.SendMail(mailToSend);
        }

        public static bool IsUserExist(string emailId)
        {
            return dbContext.userInfo.Where(a => a.EmailId == emailId).Any();
        }

        public static UserInfo GetUserByEmail(UserForGotPassword login)
        {
            return dbContext.userInfo
                    .FirstOrDefault(User => User.EmailId.Equals(login.EmailId));
           
        }

        public static UserInfo GetUserByEmailOrMobile(UserLogin login)
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

        public static void VerifyEmail(UserInfo userInfo)
        {
            userInfo.IsEmailVerified = true;
            dbContext.Entry(userInfo).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
