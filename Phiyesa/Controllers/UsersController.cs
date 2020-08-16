using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Phiyesa;

namespace Phiyesa.Controllers
{
    public class UsersController : Controller
    {
        private PHIYESAEntities1 db = new PHIYESAEntities1();

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());

            //string usertype = Session["USER_TYPE"].ToString();
            //if (usertype == "1")   //-- Admin user ... 
            //{
            //    return View(db.Users.ToList());
            //}
            //else { return RedirectToAction("UnauthorizedAccess", "Users"); }
        } //-- 

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Position,Email,MobileNo,Username,Password,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = sha256_hash(user.Password);
                user.Password = hashPassword;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        } //--

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } //--

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        } //--

        [HttpPost]
        public ActionResult Login(User userInfo, string ReturnUrl)
        {
            string userID = userInfo.Username.ToString();
            string userPass = userInfo.Password.ToString();
            string hashPassword = "";

            User user_entity = db.Users.FirstOrDefault(usr => usr.Username == userID);
            if (user_entity != null)
            {
                string userEntityPwd = user_entity.Password.ToString();
                //string userEntitySaltcode = user_entity.SaltCode.ToString();

                hashPassword = sha256_hash(userPass);
                //hashPassword = userPass;

                if (userEntityPwd == hashPassword)
                {
                    Session["USER_ID"] = user_entity.Id;
                    Session["USER_NAME"] = userID;
                    Session["USER_TYPE"] = user_entity.UserType;
                    Session["USER_EMAIL"] = user_entity.Email;

                    FormsAuthentication.SetAuthCookie(userID, false);
                    //utHelper.InsertLogAudit(Session["USER_NAME"].ToString());

                    return Redirect(ReturnUrl);
                }
                else
                {   ViewBag.errorLogin = "Incorrect Password! Please try again.";
                    return View();
                }
            }
            else
            {   ViewBag.errorLogin = "Invalid User Id! Please try again.";
                return View();
            }


        } //..**

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        } //--

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session["USER_ID"] = null;
            Session["USER_NAME"] = null;
            Session["USER_TYPE"] = null;
            Session["USER_EMAIL"] = null;

            return RedirectToAction("Index", "Home");
        }

        

    }
}
