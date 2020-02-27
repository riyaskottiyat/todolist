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
    public class TODORepository : BaseRepository<ToDo>, ITODORepository
    {
        // Custom DB operations 

        /// <summary>
        /// Gets my todos.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <param name="today">The today.</param>
        /// <returns>IEnumerable&lt;ToDo&gt;.</returns>
        public IEnumerable<ToDo> GetMyTODOS(int userId, bool? status, DateTime? today)
        {
            return _context.ToDoes.Where(t => t.UserId == userId
            && (!status.HasValue || (status.HasValue && t.Status.Equals(status.Value)))
            && (!today.HasValue || (today.HasValue && t.CreatedDateTime.Value.ToString("yyyymmdd").Equals(today.Value.ToString("yyyymmdd")))));
        }
    }
}
