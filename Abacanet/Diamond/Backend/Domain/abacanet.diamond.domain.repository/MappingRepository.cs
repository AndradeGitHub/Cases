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
    public class MappingRepository : Repository<MappingDomainModel>
    {
        private readonly IUnitOfWork _db;

        public MappingRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public override List<MappingDomainModel> GetAll()
        {
            return _db.Mapping.OrderBy(p => p.Project).ToList<MappingDomainModel>();
        }
    }
}