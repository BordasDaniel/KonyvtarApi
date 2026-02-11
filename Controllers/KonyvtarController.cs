using KonyvtarApi.DTOs;
using KonyvtarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KonyvtarController : ControllerBase
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

        [HttpGet("GetById/{id}")]
        public IActionResult GetByIdKonyvtarak(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    Konyvtarak? keresett = context.Konyvtaraks.FirstOrDefault(k => k.Id == id);
                    if (keresett != null)
                    {
                        return Ok(keresett);
                    }
                    if (keresett == null)
                    {
                        return NotFound($"Nincs ilyen id: {id}");
                    }
                    return BadRequest();

                } catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("Telepules/{telepules}")]
        public IActionResult GetByTelepules(string telepules)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    var keresett = context.Telepuleseks.Include(t => t.Konyvtaraks).Where(x => x.TelepNev == telepules).Select(t => t.Konyvtaraks).ToList();

                    if(keresett.Count > 0)
                    {
                        return Ok(keresett);
                    }
                    if (keresett.Count < 0)
                    {
                        return NotFound($"Nincs ilyen település: {telepules}");
                    }

                    return BadRequest();

                } catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("Megye/{megye}")]
        public IActionResult GetByMegye(string megye)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    List<MegyeDTO> megyedtoList = context.Konyvtaraks.Select(l => new MegyeDTO()
                    {
                        Id = l.Id,
                        TelepulesNev = l.IrszNavigation.TelepNev,
                        KonyvtarNev = l.KonyvtarNev
                    }).ToList();


                    if (megyedtoList.Count > 0)
                    {
                        return Ok(megyedtoList);
                    }

                    if (megyedtoList.Count < 0)
                    {
                        return NotFound($"Nincs ilyen megye: {megye}");
                    }
                    return BadRequest();


                } catch (Exception ex)
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

        [HttpDelete("Torol/{id}")]
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
