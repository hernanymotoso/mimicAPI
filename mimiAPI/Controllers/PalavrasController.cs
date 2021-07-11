using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
//using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mimiAPI.Models;

namespace mimiAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly MimicContext _banco;

        public PalavrasController(MimicContext banco)
        {
            _banco = banco;
        }

        [Route("")]
        [HttpGet]
        public IActionResult ObterTodas(DateTime? date, int? pagNumero, int? pagRegistroPag)
        {
            var item = _banco.Palavras.AsQueryable();
            if (date.HasValue)
            {
                item = item.Where(p => p.Criado > date.Value || p.Atualizado > date.Value);
            }

            if (pagNumero.HasValue)
            {
               if(pagRegistroPag.HasValue)
                    item = item.Skip((pagNumero.Value - 1) * pagRegistroPag.Value).Take(pagRegistroPag.Value);
            }

            return new JsonResult(item);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Obter(int id)
        {
            var obj = _banco.Palavras.Find(id);

            if (obj == null)
            {
                return NotFound();
            }


            return new JsonResult(_banco.Palavras.Find(id));
        }

        [Route("")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();

            // return CreatedResult($"/api/palavras/{palavra.Id}", palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
         
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            var obj = _banco.Palavras.AsNoTrackingWithIdentityResolution().FirstOrDefault(p => p.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            
            
            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.Find(id);

            if (palavra == null)
            {
                return NotFound();
            }

            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

            return NoContent();
        }
    }
}