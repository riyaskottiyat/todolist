using AppBoard.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBoard.DAL
{
    /// <summary>
    /// Class UserRepository.
    /// Implements the <see cref="AppBoard.DAL.BaseRepository{AppBoard.Entity.ToDo}" />
    /// Implements the <see cref="AppBoard.DAL.IUserRepository" />
    /// </summary>
    /// <seealso cref="AppBoard.DAL.BaseRepository{AppBoard.Entity.ToDo}" />
    /// <seealso cref="AppBoard.DAL.IUserRepository" />
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        // Custom DB operations 
    }
}
