using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using demo_azure_openid_connect.Models;

namespace demo_azure_openid_connect.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        public IActionResult SingIn()
        {
            return View();
        }

        public IActionResult SingOut()
        {
            return View();
        }
    }
}