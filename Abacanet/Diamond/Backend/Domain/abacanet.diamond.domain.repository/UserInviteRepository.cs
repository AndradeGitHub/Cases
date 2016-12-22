using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using abacanet.diamond.infrastructure.persistence.interfaces;
using abacanet.diamond.domain.model;

namespace abacanet.diamond.domain.repository
{
    public class UserInviteRepository : Repository<UserInviteDomainModel>
    {
        private readonly IUnitOfWork _db;

        public UserInviteRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public override void Add(UserInviteDomainModel userInvite)
        {
            _db.UserInvite.Add(userInvite);                               
        }

        public override UserInviteDomainModel GetById(int id)
        {
            return _db.UserInvite.FirstOrDefault(p => p.Id == id);
        }
    }
}
