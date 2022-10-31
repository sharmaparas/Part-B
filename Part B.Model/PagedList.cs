using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_B.Model
{
    public interface IPagedList<T>
    {
        int Total { get; set; }
        int? PageSize { get; set; }
        IList<T> Items { get; set; }
    }

    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {

        }

        public int Total
        {
            get;
            set;
        }

        public IList<T> Items
        {
            get;
            set;
        }
        public int? PageSize { get; set; }
    }

}
