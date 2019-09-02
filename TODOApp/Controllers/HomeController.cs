using AppBoard.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TODOApp.Security;

namespace TODOApp.Controllers
{
    /// <summary>
    /// Home Controller
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        #region Private Properies

        /// <summary>
        /// Gets or sets the apiurl.
        /// </summary>
        /// <value>The apiurl.</value>
        private string APIURL { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            APIURL = ConfigUtility.ReadString("APIURL");
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    // TODO: Authenticate from database
                    if (user.Name.Equals("admin1") && user.Password.Equals("Passw@rd123"))
                    {
                        // Setting User id on session
                        HttpContext.Session["UserId"] = 1;
                        HttpContext.Session["UserName"] = "Admin 1";

                        return RedirectToAction("Index");
                    }
                    else if (user.Name.Equals("admin2") && user.Password.Equals("Passw@rd123"))
                    {
                        // Setting User id on session
                        HttpContext.Session["UserId"] = 2;
                        HttpContext.Session["UserName"] = "Admin 2";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(new User() { UserId = -1 });
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// Logouts from the App.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Authenticate]
        public ActionResult Logout()
        {
            HttpContext.Session["UserId"] = HttpContext.Session["UserName"] = null;
            return View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        [Authenticate]
        public async Task<ActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Concat(APIURL, "/TODO/User/", HttpContext.Session["UserId"].ToString()));
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<ToDo> todoList = response.Content.ReadAsAsync<IEnumerable<ToDo>>().Result;
                    return View(todoList);
                }
            }

            return View();
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [Authenticate]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Add the specified TODO item.
        /// </summary>
        /// <param name="todoItem">The todo item.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [Authenticate]
        public async Task<ActionResult> Add(ToDo todoItem)
        {
            if (ModelState.IsValid)
            {
                // TODO: Current User
                todoItem.UserId = HttpContext.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Session["UserId"]) : 0;
                todoItem.CreatedDateTime = todoItem.UpdatedDateTime = DateTime.Now;
                todoItem.Status = false;

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Concat(APIURL, "/TODO"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic QWRtaW46UGFzc3dAcmQyMDE5");
                    string jsonString = JsonConvert.SerializeObject(todoItem);
                    var jsoncontent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(string.Concat(APIURL, "/TODO"), jsoncontent);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        [Authenticate]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Concat(APIURL, "/TODO/", id));
                if (response.IsSuccessStatusCode)
                {
                    ToDo todoItem = response.Content.ReadAsAsync<ToDo>().Result;
                    return View(todoItem);
                }
            }

            return View();
        }

        /// <summary>
        /// Edits the specified todo item.
        /// </summary>
        /// <param name="todoItem">The todo item.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [Authenticate]
        public async Task<ActionResult> Edit(ToDo todoItem)
        {
            if (ModelState.IsValid)
            {
                // Updated Date & Time
                todoItem.UpdatedDateTime = DateTime.Now;
                todoItem.UserId = HttpContext.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Session["UserId"]) : 0;

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(string.Concat(APIURL, "/TODO"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic QWRtaW46UGFzc3dAcmQyMDE5");
                    string jsonString = JsonConvert.SerializeObject(todoItem);
                    var jsoncontent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(string.Concat(APIURL, "/TODO"), jsoncontent);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [Authenticate]
        public async Task<ActionResult> UpdateStatus(ToDo objects)
        {
            int id = Convert.ToInt32(objects.ToDoId);
            bool status = Convert.ToBoolean(objects.Status);
            if (id < 1)
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Concat(APIURL, "/TODO/", id));
                if (response.IsSuccessStatusCode)
                {
                    // Get the existing item 
                    ToDo todoItem = response.Content.ReadAsAsync<ToDo>().Result;

                    // Update the item
                    todoItem.Status = status;
                    todoItem.UpdatedDateTime = DateTime.Now;
                    todoItem.UserId = HttpContext.Session["UserId"] != null ? Convert.ToInt32(HttpContext.Session["UserId"]) : 0;

                    string jsonString = JsonConvert.SerializeObject(todoItem);
                    var jsoncontent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    // Update the entity
                    HttpResponseMessage updateResponse = await client.PutAsync(string.Concat(APIURL, "/TODO"), jsoncontent);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Deletes the specified TOTO item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        [Authenticate]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Concat(APIURL, "/TODO"));
                client.DefaultRequestHeaders.Accept.Clear();
                // client.DefaultRequestHeaders.Add("Authorization", "Basic QWRtaW46UGFzc3dAcmQyMDE5");
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(APIURL, "/TODO/", id.Value));
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}