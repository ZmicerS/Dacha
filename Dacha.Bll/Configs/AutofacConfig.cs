using Autofac;
using System.Data.Entity;
using Dacha.Dal.Interfaces;
using Dacha.Dal.Repositories;
using Dacha.Bll.Services;
using Dacha.Bll.Interfaces;
using Dacha.Dal.EF;

namespace Dacha.Bll.Configs
{
   public static class AutofacConfig
    {
        public static void Create(string connectionStringName, ref ContainerBuilder builder)
        {       
            builder.RegisterType<ApplicationContext>().As<DbContext>().WithParameter(new TypedParameter(typeof(string), connectionStringName));        
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter(new TypedParameter(typeof(string), connectionStringName));
            builder.RegisterType<CompanionshipService>().As<ICompanionshipService>().WithParameter(new TypedParameter(typeof(string), connectionStringName));
            builder.RegisterType<MemberService>().As<IMemberService>().WithParameter(new TypedParameter(typeof(string), connectionStringName));            
            builder.RegisterType<MemberDocService>().As<IMemberDocService>().WithParameter(new TypedParameter(typeof(string), connectionStringName));
            builder.RegisterType<AccountService>().As<IAccountService>().WithParameter(new TypedParameter(typeof(string), connectionStringName));     
         }      
    }
}
