using Microsoft.AspNetCore.Mvc;
using SaatApiCore.DTOs;
using SaatApiCore.Entities;
using SaatApiCore.Interfaces;

namespace SaatApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaatController : ControllerBase
{
    private readonly ISaatService _saatService;

    public SaatController(ISaatService saatService)
    {
        _saatService = saatService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var saatler = await _saatService.GetAllAsync();
        return Ok(saatler);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var saat = await _saatService.GetByIdAsync(id);

        if (saat == null)
            return NotFound(new { message = "Saat bulunamadı." });

        return Ok(saat);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaatItem item)
    {
        await _saatService.AddAsync(item);
        return Ok(new { message = "Saat başarıyla eklendi." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] SaatItem item)
    {
        try
        {
            await _saatService.UpdateAsync(id, item);
            return Ok(new { message = "Saat başarıyla güncellendi." });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Güncellenecek saat bulunamadı." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var mevcutSaat = await _saatService.GetByIdAsync(id);

        if (mevcutSaat == null)
            return NotFound(new { message = "Silinecek saat bulunamadı." });

        await _saatService.DeleteAsync(id);
        return Ok(new { message = "Saat başarıyla silindi." });
    }
}