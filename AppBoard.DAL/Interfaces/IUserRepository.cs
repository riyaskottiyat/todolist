using AppBoard.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBoard.DAL
{
    /// <summary>
    /// Interface UserRepository
    /// Implements the <see cref="AppBoard.DAL.IRepository{AppBoard.Entity.User}" />
    /// </summary>
    /// <seealso cref="AppBoard.DAL.IRepository{AppBoard.Entity.User}" />
    public interface IUserRepository : IRepository<User>
    {
    }
}
