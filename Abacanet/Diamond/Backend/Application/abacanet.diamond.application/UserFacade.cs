using System;
using System.Collections.Generic;

using AutoMapper;

using abacanet.diamond.domain;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.common;
using abacanet.diamond.infrastructure.log;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.application
{
    public class UserFacade
    {
        private static IUnitOfWork _unitOfWork;

        private static dynamic _domainFactory;
        private static dynamic _repositoryFactory;

        public UserFacade()
        {
            _unitOfWork = new UnitOfWork();

            _domainFactory = DomainFactory.CreateDomain<UserDomainModel, User>(_unitOfWork);
            _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(_unitOfWork);
        }

        public UserFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _domainFactory = DomainFactory.CreateDomain<UserDomainModel, User>(_unitOfWork);
            _repositoryFactory = RepositoryFactory.CreateRepository<UserDomainModel, UserRepository>(_unitOfWork);
        }

        public List<T> GelAllUsers<T>()
        {            
            try
            {                
                var resultUserDomain = _repositoryFactory.GetAll();

                //Convert DomainModel to ViewModel   
                Mapper.Initialize(cfg => cfg.CreateMap<UserDomainModel, T>());
                var userView = Mapper.Map<List<UserDomainModel>, List<T>>(resultUserDomain);

                return userView;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }

        public T Login<T>(string login, string password)
        {
            try
            {
                UserDomainModel userDomain = new UserDomainModel();
                userDomain.Login = login;
                userDomain.Password = password;

                var resultUserDomain = _repositoryFactory.Get(userDomain);

                //Convert DomainModel to ViewModel                                   
                Mapper.Initialize(cfg => cfg.CreateMap<UserDomainModel, T>());
                var userView = Mapper.Map<UserDomainModel, T>(resultUserDomain);

                return userView;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }

        public bool AddUser<T>(T userView)
        {
            try
            {
                //Convert ViewModel to DomainModel                   
                Mapper.Initialize(cfg => cfg.CreateMap<T, UserDomainModel>());
                var userDomain = Mapper.Map<UserDomainModel>(userView);

                var user = _repositoryFactory.Get(userDomain);
                if (user == null)
                {
                    _domainFactory.AddUser(userDomain);

                    _unitOfWork.Commit();

                    return true;
                }
                else
                    return false;                
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var user = _repositoryFactory.GetById(id);
                if (user != null)
                {                    
                    user.Status = (int)UserStatusEnum.INACTIVE;
                    user.RequestDate = DateTime.Now;                    
                    
                    _unitOfWork.Commit();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }
    }
}
