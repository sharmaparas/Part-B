using Microsoft.EntityFrameworkCore;
using Part_B.Database;
using Part_B.Database.DbModels;
using Part_B.Model;
using Part_B.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_B.Service
{
    public class GlossaryService : IGlossaryService
    {
        protected readonly PartBDbContext _dbcontext;
        public GlossaryService(PartBDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<int> DeleteGlossaryAsync(int id)
        {
            var item = await _dbcontext.Glossary.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                _dbcontext.Glossary.Remove(item);
            }
           return await _dbcontext.SaveChangesAsync();
        }

        public async Task<GlossaryView> GetGlossaryAsync(int id)
        {
            return await _dbcontext.Glossary
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new GlossaryView
                {
                    Id = x.Id,
                    Definition = x.Definition,
                    Term = x.Term
                }).FirstOrDefaultAsync();
        }

        public async Task<PagedList<GlossaryView>> GetPagedGlossaryListAsync(GlossaryFilter filter)
        {
            var query = _dbcontext.Glossary.AsNoTracking()
                .OrderBy(x => x.Term)
                .Select(x => new GlossaryView
                {
                    Definition = x.Definition,
                    Id = x.Id,
                    Term = x.Term
                });

            return await ToPagedListAsync(query, filter.Page, filter.ItemsPerPage);

        }

        public async Task<PagedList<T>> ToPagedListAsync<T>(IQueryable<T> query, int? page, int? itemsPerPage)
        {
            var currentPage = page.HasValue ? page.Value : 1;
            int limitValue = itemsPerPage > 0 ? itemsPerPage.Value : 10; // Default can come from config
            int skip = (currentPage - 1) * limitValue;

            var instance = new PagedList<T>
            {
                Total = await query.CountAsync(),
                Items = await query.Skip(skip).Take(limitValue).ToListAsync()
            };
            return instance;
        }

        public async Task<int> UpSertAsync(GlossaryView item)
        {
            if (item.Id.HasValue && item.Id > 0)
            {
                var glossary = await _dbcontext.Glossary.FirstOrDefaultAsync(x => x.Id == item.Id.Value);
                if (glossary != null)
                {
                    glossary.Term = item.Term;
                    glossary.Definition = item.Definition;
                }
            }
            else
            {
                var glossary = new Glossary
                {
                    Term = item.Term,
                    Definition = item.Definition
                };
                _dbcontext.Glossary.Add(glossary);
            }
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
