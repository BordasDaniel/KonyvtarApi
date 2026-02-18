using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                    var keresett = context.Telepuleseks.FirstOrDefault(u => u.Id == id);
                    Telepulesek telep = new()
                    {
                        Id = id
                    };

                    if (keresett != null)
                    {
                        context.Remove(telep);
                        return Ok("Sikeres törlés!");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen!");
                    }


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
                    var keresettek = context.Te

                } catch(Exception ex)
                {
                    return BadRequest("Sikertelen lekérdezés");
                }
            }
        }




    }
}
