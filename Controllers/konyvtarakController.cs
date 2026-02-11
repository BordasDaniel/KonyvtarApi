using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonyvtarApi.Controllers
{
    [Route("Konyvtar/[controller]")]
    [ApiController]
    public class konyvtarakController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAllKonyvtarak()
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    List<Konyvtarak> konyvtarak = [.. context.Konyvtaraks];
                    return Ok(konyvtarak);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }    
        }

        [HttpPost("Uj")]
        public IActionResult NewKonyvtarak(Konyvtarak konyvtar)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    context.Add(konyvtar);
                    context.SaveChanges();
                    return Created();
                } catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("Modosit")]
        public IActionResult ModifyKonyvtarak(Konyvtarak konyvtar)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    if (context.Konyvtaraks.Contains(konyvtar))
                    {
                        context.Update(konyvtar);
                        context.SaveChanges();
                        return Ok();
                    }
                    return NotFound("Nem található..");

                } catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }

        [HttpDelete("Torol")]
        public IActionResult DeleteKonyvtarak(int id)
        {
            using (var context =new KonyvtarakContext())
            {
                try
                {
                    Konyvtarak? keresett = context.Konyvtaraks.FirstOrDefault(k => k.Id == id);
                    if (keresett != null)
                    {
                        context.Remove(keresett);
                        context.SaveChanges();
                        return Ok();
                    }
                    return NotFound("Nincs ilyen");

                } catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

    }
}
