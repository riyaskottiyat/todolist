using AppBoard.DAL;
using AppBoard.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppBoard.API
{
    /// <summary>
    /// Class TODOController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TODOController : ApiController
    {
        #region Private Properties

        /// <summary>
        /// The TODO repository
        /// </summary>
        private ITODORepository repository = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TODOController" /> class.
        /// </summary>
        public TODOController()
        {
            this.repository = new TODORepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TODOController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TODOController(ITODORepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public API Methods

        /// <summary>
        /// Gets all instance.
        /// </summary>
        /// <returns>IEnumerable&lt;ToDo&gt;.</returns>
        [HttpGet]
        [Route("TODO")]
        public IEnumerable<ToDo> Get()
        {
            IEnumerable<ToDo> model = repository.GetAll();
            return model;
        }

        /// <summary>
        /// Gets the specified TODO instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpGet]
        [Route("TODO/{id}")]
        public ToDo Get(int id)
        {
            ToDo model = repository.GetById(id);
            return model;
        }

        /// <summary>
        /// Gets my TODOS.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable&lt;ToDo&gt;.</returns>
        [Route("TODO/User/{userId}")]
        public IEnumerable<ToDo> GetMyTODOS(int userId)
        {
            IEnumerable<ToDo> model = repository.GetMyTODOS(userId);
            return model;
        }

        [HttpPost]
        [Route("TODO")]
        public void post([FromBody]ToDo value)
        {
            repository.Insert(value);
            repository.Save();
        }

        [HttpPut]
        [Route("TODO")]
        public void put([FromBody]ToDo value)
        {
            repository.Update(value);
            repository.Save();
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("TODO/{id}")]
        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        #endregion
    }
}
