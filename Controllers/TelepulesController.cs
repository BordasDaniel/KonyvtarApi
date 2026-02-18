using KonyvtarApi.DTOs;
using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TelepulesController : ControllerBase
    {
        [HttpPost("InsertTelpules")]
        public async Task<IActionResult> InsertTelpules(Telepulesek telepules)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    await context.AddAsync(telepules);
                    context.SaveChanges();
                    return Ok("Sikeres rögzítés");

                } catch (Exception ex)
                {
                    return BadRequest("Sikertelen felvitel!");
                }
            }
        }


        [HttpPut("ModositTelepules")]
        public async Task<IActionResult> ModifyTelepules(Telepulesek telepules)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    if (context.Telepuleseks.Any(u => u.Id == telepules.Id))
                    {
                        await context.AddAsync(telepules);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen");
                    }
                } catch (Exception ex)
                {
                    return BadRequest("Sikertelen módosítás");
                }
            }
        }

        [HttpDelete("TorolTelepules/{id}")]
        public IActionResult DeleteTelepules(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    Telepulesek? keresett = context.Telepuleseks.FirstOrDefault(k => k.Id == id);
                    if (keresett != null)
                    {
                        context.Remove(keresett);
                        context.SaveChanges();
                        return Ok();
                    }
                    return NotFound("Nincs ilyen.");
                } catch(Exception ex) {
                    return BadRequest("Sikertelen törlés");
                }
            }
        }

        [HttpGet("Resznev/{reszlet}")]
        public IActionResult GetByReszlet(string reszlet)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    var keresettek = context.Telepuleseks.Include(m => m.Megye).Where(n => n.TelepNev.Contains(reszlet)).Select(d => new TelepMegyeDTO()
                    {
                        TelepNev = d.TelepNev,
                        Megyenev = d.Megye.MegyeNev,
                    }).ToList();

                    if (keresettek.Count > 0)
                    {
                        return Ok(keresettek);
                    }
                    else
                    {
                        return NotFound("Nincs találat");
                    }

                } catch(Exception ex)
                {
                    return BadRequest("Sikertelen lekérdezés");
                }
            }
        }
    }
}
