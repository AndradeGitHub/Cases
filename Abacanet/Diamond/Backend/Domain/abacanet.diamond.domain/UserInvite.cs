using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure;
using abacanet.diamond.infrastructure.common;
using abacanet.diamond.infrastructure.mail;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.domain
{
    public class UserInvite : Operation<UserInviteDomainModel>
    {        
        private static dynamic _repositoryFactory;

        public UserInvite(IUnitOfWork unitOfWork)
        {            
            _repositoryFactory = RepositoryFactory.CreateRepository<UserInviteDomainModel, UserInviteRepository>(unitOfWork);
        }

        public override void AddInviteUser(UserInviteDomainModel userInviteDomainModel)
        {            
            userInviteDomainModel.RequestDate = DateTime.Now;

            _repositoryFactory.Add(userInviteDomainModel);            
        }

        public override bool EmailInviteUser(UserInviteDomainModel userInviteDomainModel)
        {
            bool toEmailValidade = Mail.MailValide(userInviteDomainModel.Email);

            string url = string.Concat(userInviteDomainModel.Url, userInviteDomainModel.Id.ToString());

            if (toEmailValidade)
                Mail.SendEmail(userInviteDomainModel.Email, "Test Subject", url);

            return toEmailValidade;
        }
    }
}
