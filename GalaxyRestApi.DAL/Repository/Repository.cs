using GalaxyRestApi.DAL.Interfaces;
using GalaxyRestApi.DAL.Models;
using GalaxyRestApi.DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GalaxyRestApi.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly EnterpriseContext EnterpriseContext;

        public Repository(EnterpriseContext enterpriseContext)
        {
            EnterpriseContext = enterpriseContext;
        }

        public async Task<T?> Get(int id)
        {
            var item = await EnterpriseContext.Set<T>().FindAsync(id);
            return item;
        }

        public Task<List<T>> GetAll()
        {
            var items = EnterpriseContext.Set<T>();
            return items.ToListAsync();
        }

        public async Task<T> Create(T model)
        {
            EnterpriseContext.Set<T>().Add(model);
            await EnterpriseContext.SaveChangesAsync();
            return model;
        }

        public async Task<T> Update(T model)
        {
            EnterpriseContext.Entry(model).State = EntityState.Modified;
            await EnterpriseContext.SaveChangesAsync();
            return model;
        }

        public async Task<T?> Delete(int id)
        {
            var entitySet = EnterpriseContext.Set<T>();
            var deleteItem = await entitySet.FindAsync(id);
         
            if (deleteItem == null)
            {
                return deleteItem;
            }

            entitySet.Remove(deleteItem);
            await EnterpriseContext.SaveChangesAsync();

            return deleteItem;
        }
    }
}
