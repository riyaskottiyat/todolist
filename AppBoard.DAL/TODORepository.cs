using AppBoard.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBoard.DAL
{
    /// <summary>
    /// Class TODORepository.
    /// Implements the <see cref="AppBoard.DAL.BaseRepository{AppBoard.Entity.ToDo}" />
    /// </summary>
    /// <seealso cref="AppBoard.DAL.BaseRepository{AppBoard.Entity.ToDo}" />
    public class TODORepository: BaseRepository<ToDo>, ITODORepository
    {
        // Custom DB operations 

        /// <summary>
        /// Gets my todos.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable&lt;ToDo&gt;.</returns>
        public IEnumerable<ToDo> GetMyTODOS(int userId)
        {
            return _context.ToDoes.Where(t => t.UserId == userId);
        }
    }
}
