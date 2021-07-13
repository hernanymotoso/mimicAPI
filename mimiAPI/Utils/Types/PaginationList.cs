using System.Collections.Generic;

namespace mimiAPI.Utils.Types
{
    public class PaginationList<T> : List<T>
    {
        public Paginacao Paginacao { get; set; }
        
    }
}