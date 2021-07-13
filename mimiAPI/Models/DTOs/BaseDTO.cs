using System.Collections.Generic;

namespace mimiAPI.Models.DTOs
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; }
    }
}