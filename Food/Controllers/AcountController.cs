using Microsoft.AspNetCore.Mvc;
using Food.Data;
using Food.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Food.Controllers
{
    public class AcountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         
        //public JsonResult IsEmailExist(string Email )
        //{
        //    return Json(data:!_dbcontext.Customers.Any(x=> x.Email == Email));
        //}
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IWebHostEnvironment _Environment;
        public AcountController(ApplicationDbcontext context ,IWebHostEnvironment environment)
        {
            _dbcontext = context;
            _Environment = environment;
        }
       
        [HttpGet]
        public IActionResult Regester()
        {
            return View();
        }

        [HttpPost]
        //public JsonResult CheckEmail(string Email)
        //{
            
        //}

        [HttpPost]
        public IActionResult Regester(Customer user, IFormFile ImgFile )
        {
                string path = Path.Combine(_Environment.WebRootPath, "UserPhoto");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                if (ImgFile != null)
                {
                    path = Path.Combine(path, ImgFile.FileName); // for example : /Img/Photoname.png
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImgFile.CopyToAsync(stream);
                        //ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                        user.uesrpicture = ImgFile.FileName;
                    }


                }
                else
                {
                    user.uesrpicture = "default.jpg"; // to save the default image path in database.
                }

            try
            {

                if (ModelState.IsValid)
                {
                    //var Checkuser = _dbcontext.Customers.FirstOrDefault(u => u.Email == user.Email);
                    //if (Checkuser == null)
                    //{

                    user.FullName =
                  user.FirstName
                  + " " + user.LastName;
                    _dbcontext.Add(user);

                    _dbcontext.SaveChanges();
                    return RedirectToAction("login", "Acount");
                    //}
                }
            }catch (Exception ex) { ViewBag.exc = ex.Message; }


            return View(User);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User_login Model)
        {
            if (ModelState.IsValid)
            {
                //var user = _dbcontext.Customers.FirstOrDefault(u => u.Email == Model.Email);
                var Check  = _dbcontext.Customers.Where(x => x.Email.Equals(Model.Email)).ToList();
                foreach (var item in Check)
                {

                    if (item != null && item.Password == Model.Password && item.Status == "Oner")
                    {

                        HttpContext.Session.SetString("Name", item.FirstName);
                        HttpContext.Session.SetInt32("ID", item.CustomerId);
                        HttpContext.Session.SetString("statous", item.Status);
                        return RedirectToAction("Index", "Customer");

                    }
                    else if (item != null && item.Password == Model.Password && item.Status == "Admin")
                    {

                        HttpContext.Session.SetString("Name", item.FirstName);
                        HttpContext.Session.SetInt32("ID", item.CustomerId);
                        HttpContext.Session.SetString("statous", item.Status);

                        return RedirectToAction("Index", "Item");


                    }
                    else
                    {
                        if (item != null && item.Password == Model.Password)
                        {
                            HttpContext.Session.SetString("Name", item.FirstName);
                            HttpContext.Session.SetInt32("ID", item.CustomerId);

                            return RedirectToAction("Index", "Home");
                        }

                    }


                }
            }
            return View(Model);
        }
        public IActionResult MyAccount()
        {
            int Id = Convert.ToInt32(HttpContext.Session.GetInt32("ID"));
            //int UserId =Convert.ToInt32(_dbcontext.Customers.FirstOrDefault(x => x.CustomerId == Id));

            return View();

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("stayous");
            HttpContext.Session.Remove("Name");
            

            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToAction(int? v)
        {
            throw new NotImplementedException();
        }
    }
}
