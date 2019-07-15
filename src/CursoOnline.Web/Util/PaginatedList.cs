using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Web.Util
{
    public class PaginatedList <T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(IEnumerable<T> itens, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(itens);
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

        public static PaginatedList<T> Create(IEnumerable<T> souce, HttpRequest request)
        {
            const int pageSize = 10;
            int.TryParse(request.Query["page"], out int pageIndex);
            pageIndex = pageIndex > 0 ? pageIndex : 1;

            var enuerable = souce as List<T> ?? souce.ToList();
            var count = enuerable.Count();
            var itens = enuerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(itens, count, pageIndex, pageSize);
        }
    }
}
