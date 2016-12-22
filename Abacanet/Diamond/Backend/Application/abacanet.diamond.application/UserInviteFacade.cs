using System;
using System.Collections.Generic;

using AutoMapper;

using abacanet.diamond.domain;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.interfaces;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.log;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.application
{
    public class UserInviteFacade
    {
        private static IUnitOfWork _unitOfWork;

        private static dynamic _domainFactory;
        private static dynamic _repositoryFactory;

        public UserInviteFacade()
        {
            _unitOfWork = new UnitOfWork();

            _domainFactory = DomainFactory.CreateDomain<UserInviteDomainModel, UserInvite>(_unitOfWork);
            _repositoryFactory = RepositoryFactory.CreateRepository<UserInviteDomainModel, UserInviteRepository>(_unitOfWork);
        }

        public UserInviteFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _domainFactory = DomainFactory.CreateDomain<UserInviteDomainModel, UserInvite>(_unitOfWork);
            _repositoryFactory = RepositoryFactory.CreateRepository<UserInviteDomainModel, UserInviteRepository>(_unitOfWork);
        }

        public T AddUserInvite<T>(T userInviteView)
        {
            try
            {
                //Convert ViewModel to DomainModel                   
                Mapper.Initialize(cfg => cfg.CreateMap<T, UserInviteDomainModel>());
                var userInviteDomain = Mapper.Map<UserInviteDomainModel>(userInviteView);

                _domainFactory.AddInviteUser(userInviteDomain);
                                
                _domainFactory.EmailInviteUser(userInviteDomain);

                _unitOfWork.Commit();

                //Convert DomainModel to ViewModel                   
                Mapper.Initialize(cfg => cfg.CreateMap<UserInviteDomainModel, T>());
                var retUserInviteView = Mapper.Map<T>(userInviteDomain);

                return retUserInviteView;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }

        public T GetUserInvite<T>(int id)
        {
            try
            {
                var resultUserDomain = _repositoryFactory.GetById(id);

                //Convert DomainModel to ViewModel                                   
                Mapper.Initialize(cfg => cfg.CreateMap<UserInviteDomainModel, T>());
                var userView = Mapper.Map<UserInviteDomainModel, T>(resultUserDomain);

                return userView;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }
    }
}
