using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;
using abacanet.diamond.domain.model;

namespace abacanet.diamond.domain.repository
{
    public class UserRepository : Repository<UserDomainModel>
    {
        private readonly IUnitOfWork _db;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public override void Add(UserDomainModel user)
        {
            _db.Users.Add(user);            
        }

        public override List<UserDomainModel> GetAll()
        {
            return _db.Users.OrderBy(p => p.FirstName).Where(p => p.Status == 1).ToList<UserDomainModel>();                        
        }

        public override UserDomainModel GetById(int id)
        {
            return _db.Users.Where(p => p.Id == id).FirstOrDefault();            
        }

        public override UserDomainModel Get(UserDomainModel user)
        {
            var ret = new UserDomainModel();

            if (!string.IsNullOrEmpty(user.Email))
                ret = _db.Users.Where(p => p.Email == user.Email).FirstOrDefault();
            else if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                ret = _db.Users.Where(p => p.Login == user.Login && p.Password == user.Password && p.Status == 1).FirstOrDefault();

            return ret;
        }
    }
}