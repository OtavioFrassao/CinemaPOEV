using CinemaDomain.Base;
using CinemaRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaRepository.Base
{
    public class BaseRepository<TypeEntity> : IBaseRepository<TypeEntity> where TypeEntity : BaseEntity
    {
        protected readonly MyDBContext _myDbContext;

        public BaseRepository(MyDBContext myDbContext)
        {
            _myDbContext = myDbContext;
            _myDbContext.Set<TypeEntity>();
        }
        public void Create(TypeEntity entity)
        {
            _myDbContext.Add(entity);
            _myDbContext.SaveChanges();
        }
        public TypeEntity ReadById(int id)
        {
            var dbContext = _myDbContext.Set<TypeEntity>().AsQueryable();
            return dbContext.ToList().Find(x => x.Id == id);
        }

        public IList<TypeEntity> ReadAll()
        {
            var dbContext = _myDbContext.Set<TypeEntity>().AsQueryable();
            return dbContext.ToList();
        }

        public void Update(TypeEntity entity)
        {
            _myDbContext.Entry(entity).State = EntityState.Modified;
            _myDbContext.SaveChanges(true);
        }
        public void Delete(int id)
        {
            _myDbContext.Remove(ReadById(id));
            _myDbContext.SaveChanges();
        }
    }
}
