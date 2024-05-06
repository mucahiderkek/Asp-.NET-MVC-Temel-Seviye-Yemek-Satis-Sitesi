using EatWorld.Data;
using EatWorld.Models;
using Microsoft.AspNetCore.Mvc;

namespace EatWorld.Controllers
{
    public class YemekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YemekController(ApplicationDbContext context)
        {
           _context = context;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("User");
            var userDb = _context.Users.FirstOrDefault(u => u.UserName == user);
            ViewData["UserDb"] = userDb;
            IEnumerable<Yemek> kitaps = _context.Yemeks;
            return View(kitaps);
        }


		public IActionResult Görüntüle(int? id)
		{
			var yemek = _context.Yemeks.Find(id);
			if (yemek == null)
			{
				return NotFound();
			}
			return View(yemek);
		}

		public IActionResult Düzenle(int? id)
		{
			var yemek = _context.Yemeks.Find(id);
			if (yemek == null)
			{
				return NotFound();
			}
			return View(yemek);
		}


		//Ekleme

		public IActionResult Ekle()
        {
            var user = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(user))
            {
                var UserDb = _context.Users.FirstOrDefault(u => u.UserName == user);
                if (UserDb.Permission == "Yetkili")
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Ekle(Yemek kitap)
        {
            if (ModelState.IsValid)
            {

                _context.Yemeks.Add(kitap);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
       

        //Güncelleme

        public IActionResult Duzenle(int? id)
		{
			var ktp = _context.Yemeks.Find(id);
			if (ktp == null)
			{
				return NotFound();
			}
			return View(ktp);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Duzenle(Yemek krs)
		{
			if (ModelState.IsValid)
			{
				_context.Yemeks.Update(krs);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(krs);
		}

		//Silme

		public IActionResult Sil(int? id)
        {
            var ktp = _context.Yemeks.Find(id);
            if (ktp == null)
            {
                return NotFound();
            }
            return View(ktp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SilKitap(int? id)
        {
            var ktp = _context.Yemeks.Find(id);
            if (ktp == null)
            {
                return NotFound();
            }
            _context.Yemeks.Remove(ktp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult StinAl()
        {
            // Eğer bu eylem sadece bir sayfaya yönlendirme yapıyorsa, burada gerekli işlemleri gerçekleştirmeniz gerekmez
            return View(); // İlgili sayfayı döndürür
        }


    }
}

