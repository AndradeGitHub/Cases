using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure;
using abacanet.diamond.infrastructure.common;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.domain
{
    public class User : Operation<UserDomainModel>
    {        
        private static dynamic _repositoryFactory;

        public User(IUnitOfWork unitOfWork)
        {            
            _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(unitOfWork);            
        }

        public override void AddUser(UserDomainModel userDomainModel)
        {
            userDomainModel.Status = (int)UserStatusEnum.ACTIVE;
            userDomainModel.RequestDate = DateTime.Now;            

            _repositoryFactory.Add(userDomainModel);            
        }
    }
}
