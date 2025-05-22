using Microsoft.AspNetCore.Mvc;
using AracKiralamaAPI.models;
using AracKiralamaAPI.business.repository;
namespace AracKiralamaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        public KiralamaController(IKiralamaRepository kiralamaRepository)
        {
            _kiralamaRepository = kiralamaRepository;
        }
        [HttpGet]
        public IActionResult GetKiralamaKayitlari()
        {
            var kayitlar = _kiralamaRepository.GetKiralamaKayitlari();
            return Ok(kayitlar);
        }

        [HttpPost]
        public IActionResult InsertKiralamaKayit(Kiralama kayit)
        {
            _kiralamaRepository.InsertKiralamaKayit(kayit);
            return Ok();
        }
    }
}
