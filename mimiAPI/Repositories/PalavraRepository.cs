using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using mimiAPI.Interfaces;
using mimiAPI.Models;
using mimiAPI.Utils.Types;
using Newtonsoft.Json;

namespace mimiAPI.Repositories
{
    public class PalavraRepository : IPalavraRepository
    {
        private readonly MimicContext _dbContext;

        public PalavraRepository(MimicContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        public PaginationList<Palavra> ObterPalavras(PalavraUrlQuery query)
        {
            var lista = new PaginationList<Palavra>();
            var item = _dbContext.Palavras.AsNoTrackingWithIdentityResolution().AsQueryable();
            if (query.Date.HasValue)
            {
                item = item.Where(p => p.Criado > query.Date.Value || p.Atualizado > query.Date.Value);
            }

            if (query.PagNumero.HasValue && query.PagRegistro.HasValue)
            {
                var quantidadeTotalRegistros = item.Count();
                
                // if(pagRegistro.HasValue)
                /*Calculo da paginação, onde pula e pega os itens*/
                item = item.Skip((query.PagNumero.Value - 1) * query.PagRegistro.Value).Take(query.PagRegistro.Value);

                var paginacao = new Paginacao
                {
                    NumeroPagina = query.PagNumero.Value,
                    RegistroPorPagina = query.PagRegistro.Value,
                    TotalRegistros = quantidadeTotalRegistros,
                    TotalPaginas = (int) Math.Ceiling((double) quantidadeTotalRegistros / query.PagRegistro.Value)
                    
                };

                lista.Paginacao = paginacao;
            }

            lista.AddRange(item.ToList());
            
            return lista;
            
            
            }

        public Palavra Obter(int id)
        {
            return _dbContext.Palavras.AsNoTrackingWithIdentityResolution().FirstOrDefault(p => p.Id == id);
        }

        public void Cadastrar(Palavra palavra)
        {
            _dbContext.Palavras.Add(palavra);
            _dbContext.SaveChanges();
        }

        public void Atualizar(Palavra palavra)
        {
            _dbContext.Palavras.Update(palavra);
            _dbContext.SaveChanges();
            
        }

        public void Deletar(int id)
        {
            var palavra = Obter(id);
            palavra.Ativo = false;
            _dbContext.Palavras.Update(palavra);
            _dbContext.SaveChanges();
            
        }
    }
}