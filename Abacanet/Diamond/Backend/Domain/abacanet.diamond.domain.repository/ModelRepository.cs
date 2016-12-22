//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;

//using abacanet.diamond.domain.model;

//namespace abacanet.diamond.domain.repository
//{
//    public class ModelRepository : DbContext
//    {
//        public ModelRepository() : base("ModelRepository")
//        {
//            //var adapter = (IObjectContextAdapter)this;
//            //var objectContext = adapter.ObjectContext;
//        }

//        public DbSet<UserDomainModel> User { get; set; }
//        public DbSet<UserInviteDomainModel> UserInvite { get; set; }
//        public DbSet<OrderDomainModel> Order { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            #region User
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Id).HasColumnName("CD_USER").IsRequired();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Login).HasColumnName("NM_LOGIN").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Password).HasColumnName("TX_PASSWORD").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.FirstName).HasColumnName("NM_FIRSTNAME").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.LastName).HasColumnName("NM_LASTNAME").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Company).HasColumnName("NM_COMPANY").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Email).HasColumnName("TX_EMAIL").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Notes).HasColumnName("TX_NOTES").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.RequestDate).HasColumnName("DT_REQUESTDATE").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Address).HasColumnName("TX_ADDRESS").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.City).HasColumnName("NM_CITY").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.State).HasColumnName("NM_STATE").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.ZipCode).HasColumnName("TX_ZIPCODE").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().Property(dp => dp.Status).HasColumnName("CD_STATUS").IsOptional();
//            modelBuilder.Entity<UserDomainModel>().ToTable("USER");
//            #endregion 

//            #region UserInvite
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.Id).HasColumnName("CD_USER_INVITE").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.FirstName).HasColumnName("NM_FIRSTNAME").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.LastName).HasColumnName("NM_LASTNAME").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.Company).HasColumnName("NM_COMPANY").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.Email).HasColumnName("TX_EMAIL").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.Notes).HasColumnName("TX_NOTES").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.RequestDate).HasColumnName("DT_REQUESTDATE").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.Address).HasColumnName("TX_ADDRESS").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.City).HasColumnName("NM_CITY").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.State).HasColumnName("NM_STATE").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().Property(dp => dp.ZipCode).HasColumnName("TX_ZIPCODE").IsOptional();
//            modelBuilder.Entity<UserInviteDomainModel>().ToTable("USERINVITE");
//            #endregion 

//            #region Order
//            modelBuilder.Entity<OrderDomainModel>().Property(dp => dp.Id).HasColumnName("CD_ORDER").IsOptional();
//            modelBuilder.Entity<OrderDomainModel>().Property(dp => dp.OrderName).HasColumnName("NM_ORDER").IsOptional();
//            modelBuilder.Entity<OrderDomainModel>().ToTable("ORDER");
//            #endregion         
//        }
//    }
//}