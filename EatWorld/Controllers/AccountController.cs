using EatWorld.Data;
using EatWorld.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("User");
        var userDb = _context.Users.FirstOrDefault(u => u.UserName == user);
        ViewData["UserDb"] = userDb;
        IEnumerable<User> UserList = _context.Users;
        return View(UserList);
    }

    public IActionResult Edit(int? Id)
    {
        var User = _context.Users.Find(Id);
        if (User == null)
        {
            return NotFound();
        }
        return View(User);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Edit(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            TempData["SuccessMsg"] = user.UserName + " isimli kullanıcının verileri değiştirildi!";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            TempData["SuccessMsg"] = user.UserName + " isimli kullanıcı eklendi!";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        User? user = _context.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        User? obj = _context.Users.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Users.Remove(obj);
            _context.SaveChanges();
            TempData["SuccessMsg"] = obj.UserName + " isimli kullanıcı silindi!";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UserRegister(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        else
        {
            TempData["RegisterMsg"] = "Lütfen Bilgileri Eksiksiz ve Doğru Girin!";
        }
        return View("Register");
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UserLogin(User user)
    {
        var loginUser = _context.Users.FirstOrDefault(u => u.EMail == user.EMail && u.Password == user.Password);

        if (loginUser != null)
        {
            HttpContext.Session.SetString("User", loginUser.UserName);
            return RedirectToAction("Index", "Yemek");
        }
        else
        {
            TempData["LoginMsg"] = "Hatalı E-posta veya Şifre!";
        }

        return View("Login");
    }
}