using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using RegistrationAndLogin7.Models;

namespace RegistrationAndLogin7.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register(Account account)
        {
            using (var connection = new SqlConnection("Server=localhost;User=test;Password=1234;Database=test;"))
            {
                string str_query = "INSERT INTO Account(Id, Name, Email, Password) VALUES (@Id, @Name, @Email, @Password)";
                connection.Execute(str_query, account);
            }          
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}