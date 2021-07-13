using System.Collections.Generic;
using mimiAPI.Models;
using mimiAPI.Utils.Types;

namespace mimiAPI.Interfaces
{
    public interface IPalavraRepository
    {
        PaginationList<Palavra> ObterPalavras(PalavraUrlQuery query);
        Palavra Obter(int id);
        void Cadastrar(Palavra palavra);
        void Atualizar(Palavra palavra);
        void Deletar(int id);
        
        
    }
}