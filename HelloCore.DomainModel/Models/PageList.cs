using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.DomainModel.Models
{
    public class PageList<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }
        public List<T> Items { get; private set; }

        public PageList(int pageIndex,int pageSize,int pageCount, List<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = pageCount;
            Items = items ?? new List<T>();
        }


    }
}
