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
    }
}
