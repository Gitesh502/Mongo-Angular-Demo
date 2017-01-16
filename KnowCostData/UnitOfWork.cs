using KnowCostData.Entity;
using KnowCostData.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData
{
    public class UnitOfWork : IDisposable
    {
        
        private BaseRepository<users> _userRepository;
        private BaseRepository<ConnectedUsers> _connectedUserRepository;
        //private BaseRepository<dynamic> _commonRepository;
        public BaseRepository<users> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new BaseRepository<users>();
                return _userRepository;
            }
        }
        public BaseRepository<ConnectedUsers> ConnectedUserRepository
        {
            get
            {
                if (this._connectedUserRepository == null)
                    this._connectedUserRepository = new BaseRepository<ConnectedUsers>();
                return _connectedUserRepository;
            }
        }
        //public BaseRepository<dynamic> ConnectedUserRepository
        //{
        //    get
        //    {
        //        //if (this._commonRepository == null)
        //        //    this._commonRepository = new BaseRepository<users>();
        //        return _commonRepository;
        //    }
        //}
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
