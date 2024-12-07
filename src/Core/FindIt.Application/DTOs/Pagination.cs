using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Application.DTOs
{
    public record Pagination<T>
    (
        int PageIndex, 
        int PageSize, 
        int Count, 
        IReadOnlyList<T> Data
    );
}
