using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppBoard.DAL
{
    /// <summary>
    /// Generic Repository for DB Operation
    /// Implements the <see cref="AppBoard.DAL.IRepository{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="AppBoard.DAL.IGenericRepository{T}" />
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        private AppBoardDBContext _context = null;
        /// <summary>
        /// The table
        /// </summary>
        private DbSet<T> table = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        public BaseRepository()
        {
            this._context = new AppBoardDBContext();
            table = _context.Set<T>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="_context">The context.</param>
        public BaseRepository(AppBoardDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public T GetById(object id)
        {
            return table.Find(id);
        }
        /// <summary>
        /// Inserts the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
