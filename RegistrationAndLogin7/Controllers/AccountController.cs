using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dapper;
using RegistrationAndLogin7.Models;

namespace RegistrationAndLogin7.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                string registerQuery = @"INSERT INTO [dbo].[Account] ([UserId], [Email], [Password]) VALUES (@UserId, @Email, @Password)";
                //var result = db.Execute(registerQuery, account);
                db.Execute(registerQuery, account);
            }
            return RedirectToAction("Login", "Account");
        }

        /*[HttpPost]
        public ActionResult Register(Account account)
        {
            using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                if (ModelState.IsValid)
                {
                    db.Open();
                    if ((db.State & System.Data.ConnectionState.Open) > 0)
                    {
                        SqlCommand cmd = new SqlCommand("UserRegistration", db);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserId", account.UserId));
                        cmd.Parameters.Add(new SqlParameter("@Email", account.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", account.Password));
                        int results = cmd.ExecuteNonQuery();
                    }
                    db.Close();
                    return View();
                }
                else
                {
                    return View();
                }

            }
        }*/

        /*[HttpPost]
        public JsonResult CheckUserId(string UserId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                bool isValid = !db.Users.ToList().Exists(p => p.UserId.Equals(UserId, StringComparison.CurrentCultureIgnoreCase));
                return Json(isValid);
            }
        }*/

        public ActionResult Login()
        {
            return View();
        }

        /*public JsonResult CheckLogin(string UserId, string Password)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                //var dataItem = db.Where(x => x.UserId == UserId && x.Password == Password).SingleOrDefault();
                //string loginQuery = @"SELECT * FROM [dbo].[Account] WHERE UserID = @UserId AND Password = @Password ";
                var dataItem = db.QuerySingleOrDefault()
                bool isLogged = true;

                if (dataItem != null)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.UserId, true);
                    var mdl = System.Web.HttpContext.Current.User.Identity.Name;
                    isLogged = true;
                }
                else
                {
                    isLogged = false;
                }
                return Json(isLogged, JsonRequestBehavior.AllowGet);
            }
        }*/


        /*[HttpPost]
        public JsonResult ValidateAccount(string UserId, string Password, bool RememberMe)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                LoginStatus status = new LoginStatus();
                if (Membership.ValidateUser(UserId, Password))
                {
                    FormsAuthentication.SetAuthCookie(UserId, RememberMe);
                    status.Success = true;
                    status.TargetURL = FormsAuthentication.GetRedirectUrl(UserId, RememberMe);
                    if (string.IsNullOrEmpty(status.TargetURL))
                    {
                        status.TargetURL = FormsAuthentication.DefaultUrl;
                    }
                    status.Message = "로그인이 성공적으로 완료되었습니다.";
                }
                else
                {
                    status.Success = false;
                    status.Message = "유효하지 않은 아이디 또는 비밀번호 입니다.";
                    status.TargetURL = FormsAuthentication.LoginUrl;
                }
                return Json(status);
            }
        }*/

        /*[HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                using (IDbConnection db = new SqlConnection("Server=localhost;User=test;Password=1234;Database=test;"))
                {
                    var userLoggedIn = db.Execute();
                    if (userLoggedIn != null)
                    {
                        return RedirectToAction("Index", "Home", new { UserId = account.UserId });
                    }
                    else
                    {
                        return View();
                    }
                }
               return RedirectToAction("Index", "Home");
            }
            else
            {

            }
            return View();
        }*/

        // 로그아웃
        public ActionResult Logout()
        {
            /*Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");*/
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Login", "Account");
        }

        // 회원삭제
        public void Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                string deleteQuery = "DELETE Account WHERE Id = @Id";
                db.Execute(deleteQuery, new { Id = Id });
            }
        }
    }
}