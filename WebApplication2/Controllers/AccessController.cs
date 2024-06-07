﻿using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccessController : Controller
    {
        OganiContext db = new OganiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("CusEmail") == null)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            if (HttpContext.Session.GetString("cusEmail") == null)
            {
                var cus = db.Customers.Where(x => x.CusEmail.Equals(customer.CusEmail) && x.CusPassword.Equals(customer.CusPassword)).FirstOrDefault();
                if (cus != null)
                {
                    HttpContext.Session.SetString("cusEmail", cus.CusEmail.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("cusEmail");
            return RedirectToAction("Login", "Access");
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = db.Customers.FirstOrDefault(x => x.CusEmail == customer.CusEmail);
                if (check == null)
                {
                    //customer.CusPassword = GetMD5(customer.CusPassword);
                    //db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CusEmail", "Email already exists");
                    return View();
                }
            }
            return View();
        }
    }
}