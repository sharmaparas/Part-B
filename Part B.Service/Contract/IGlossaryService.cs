using Part_B.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_B.Service.Contract
{
    public interface IGlossaryService
    {
        Task<int> DeleteGlossaryAsync(int id);
        Task<GlossaryView> GetGlossaryAsync(int id);
        public Task<PagedList<GlossaryView>> GetPagedGlossaryListAsync(GlossaryFilter filter);
        public Task<int> UpSertAsync(GlossaryView item);
    }
}
