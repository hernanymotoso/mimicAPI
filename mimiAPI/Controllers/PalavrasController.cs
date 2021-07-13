using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
//using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mimiAPI.Interfaces;
using mimiAPI.Models;
using mimiAPI.Models.DTOs;
using mimiAPI.Repositories;
using mimiAPI.Utils.Types;
using Newtonsoft.Json;

namespace mimiAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly IPalavraRepository _palavraRepository;
        private readonly IMapper _mapper;

        public PalavrasController(IPalavraRepository palavraRepository, IMapper mapper)
        {
            _palavraRepository = palavraRepository;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public IActionResult ObterTodas([FromQuery]PalavraUrlQuery query)
        {
            var item = _palavraRepository.ObterPalavras(query);
            
            if (query.PagNumero > item.Paginacao.TotalPaginas)
            {
                return NotFound();
            }
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

            return new JsonResult(item.ToList());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Obter(int id)
        {
            var obj = _palavraRepository.Obter(id);
            
            if (obj == null)
            {
                return NotFound();
            }

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(obj);
            palavraDTO.Links = new List<LinkDTO>();

            palavraDTO.Links.Add(
                new LinkDTO("self", $"https://localhost:5001/api/palavras/{palavraDTO.Id}", "GET")    
            );
           
       
            return new JsonResult(palavraDTO);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _palavraRepository.Cadastrar(palavra);

            // return CreatedResult($"/api/palavras/{palavra.Id}", palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
         
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            var obj = _palavraRepository.Obter(id);

            if (obj == null)
            {
                return NotFound();
            }
            
            
            palavra.Id = id;
            _palavraRepository.Atualizar(palavra);
            
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            var palavra = _palavraRepository.Obter(id);
            
            if (palavra == null)
            {
                return NotFound();
            }
           
            _palavraRepository.Deletar(id);
            
            return NoContent();
        }
    }
}
