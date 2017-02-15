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

        private BaseRepository<AspNetUsers> _userRepository;
        private BaseRepository<Conversation> _ConversationRepository;
        public BaseRepository<AspNetUsers> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new BaseRepository<AspNetUsers>();
                return _userRepository;
            }
        }
        public BaseRepository<Conversation> ConversationRepository
        {
            get
            {
                if (this._ConversationRepository == null)
                    this._ConversationRepository = new BaseRepository<Conversation>();
                return _ConversationRepository;
            }
        }
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
