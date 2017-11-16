using Dacha.Dal.EF;
using Dacha.Dal.Entities;
using Dacha.Dal.Identity;
using Dacha.Dal.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Dacha.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;       
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        IRepository<RefreshToken> _refreshTokenRepository { set; get; }      

        IRepository<Companionship> _companionshipRepository { set; get; }
        IRepository<Member> _memberRepository { set; get; }
        IRepository<MemberDoc> _memberDocRepository { set; get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context">connectionString fro web config</param>
        public UnitOfWork(string connectionString)
        {
          _db = new ApplicationContext(connectionString);
           //  db.Database.Initialize(force: true);
          _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
          _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
         }

        public ApplicationUserManager UserManager => _userManager;
          
        public ApplicationRoleManager RoleManager => _roleManager;
        
        public IRepository<Companionship> CompanionshipRepository
        {
            get
            {
                if (_companionshipRepository == null)
                    _companionshipRepository = new CompanionshipRepository(_db);
                return _companionshipRepository;
            }
        }

        public IRepository<Member> MemberRepository
        {
            get
            {
                if (_memberRepository == null)
                    _memberRepository = new MemberRepository(_db);
                return _memberRepository;
            }
        }

        public IRepository<MemberDoc> MemberDocRepository
        {
            get
            {
                if (_memberDocRepository == null)
                    _memberDocRepository = new MemberDocRepository(_db);
                return _memberDocRepository;
            }
        }

        public IRepository<RefreshToken> RefreshTokenRepository
        {
            get
            {
                if (_refreshTokenRepository == null)
                    _refreshTokenRepository = new RefreshTokenRepository(_db);
                return _refreshTokenRepository;
            }
        }

        /// <summary>
        /// Saves the changes Dbcontext.
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// Saves the changes Dbcontext.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                  //   userManager.Dispose();
                  //   roleManager.Dispose();                  
                  //  _db.Dispose();
                }
                this.disposed = true;
            }
        }

    }
}
