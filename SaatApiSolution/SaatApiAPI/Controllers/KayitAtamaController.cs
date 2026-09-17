using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaatApi.Business.Services;
using SaatApiCore.DTOs;
using SaatApiCore.Entities;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;

namespace SaatApiAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KayitAtamaController : ControllerBase
    {

        private readonly IKayitService _kayitService;
        public KayitAtamaController(IKayitService kayitService)
        {
            _kayitService = kayitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kayitlar = await _kayitService.GetAllAsync();
            return Ok(kayitlar);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kayit = await _kayitService.GetByIdAsync(id);

            if (kayit == null)
                return NotFound(new { message = "Kayıt bulunamadı." });

            return Ok(kayit);
        }

        [HttpGet("Kapı-{id}")]
        public async Task<IActionResult> GetDoorById(int id)
        {
            var door = await _kayitService.GetDoorByIdAsync(id);

            if (door.Count == 0)
                return NotFound(new { message = "Bu kapıya ait kayıt bulunamadı." });

            return Ok(door);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KayitAtama kayit)
        {
            await _kayitService.AddAsync(kayit);
            return Ok(kayit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] KayitAtama kayit)
        {
            try
            {
                await _kayitService.UpdateAsync(id, kayit);
                return Ok(new { message = "Saat başarıyla güncellendi." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Güncellenecek kayıt bulunamadı." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var mevcutSaat = await _kayitService.GetByIdAsync(id);

            if (mevcutSaat == null)
                return NotFound(new { message = "Silinecek kayıt bulunamadı." });

            await _kayitService.DeleteAsync(id);
            return Ok(new { message = "Saat başarıyla silindi." });
        }
    }
}
