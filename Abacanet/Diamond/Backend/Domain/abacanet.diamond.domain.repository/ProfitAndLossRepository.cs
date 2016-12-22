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
    public class ProfitAndLossRepository : Repository<ProfitAndLossModel>
    {
        private readonly IUnitOfWork _db;

        public ProfitAndLossRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public override void Add(ProfitAndLossModel profitAndLoss)
        {
            _db.ProfitsAndLosses.Add(profitAndLoss);
        }

        public override List<ProfitAndLossModel> GetAll()
        {
            return _db.ProfitsAndLosses
                .Include(p => p.Children).ToList()
                .Where(p => p.Parent == null).ToList();
        }
    }
}