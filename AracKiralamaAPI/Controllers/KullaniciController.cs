using Microsoft.AspNetCore.Mvc;
using AracKiralamaAPI.models;
using AracKiralamaAPI.business.repository;

namespace AracKiralamaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;

       
        public KullaniciController(IKullaniciRepository gelirRepository)
        {
            _kullaniciRepository = gelirRepository;
        }

        [HttpGet]
        public IActionResult GetKullaniciKayitlari()
        {
            var kayitlar = _kullaniciRepository.GetKullaniciKayitlari();
            return Ok(kayitlar);
        }

        [HttpPost]
        public IActionResult InsertKullaniciKayit(Kullanici kayit)
        {
            _kullaniciRepository.InsertKullaniciKayit(kayit);
            return Ok();
        }
    }
}
