using ContactManagement.APIHelpers;
using ContactManagement.Attributes;
using ContactManagement.Core;
using ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ContactManagement.Controllers
{
    [Token] // API TOKEN
    [HandleError] // ERROR HANDLER
    public class ManageContactsController : Controller
    {
        #region Public action methods
        /// <summary>
        /// Index
        /// </summary>
        /// <returns>Contact List</returns>
        public ActionResult Index()
        {
            List<ContactsModel> contactList = new List<ContactsModel>();
            try
            {
                string url = string.Empty;
                string token = string.Empty;
                GetAPIDetails(ref url, ref token);
                contactList = APIHelper.GetContactsList(token, url);
                return View("Index", contactList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <returns></returns>
        // GET: ManageContacts/Create
        public ActionResult Create()
        {
            ContactsModel cModel = new ContactsModel();
            return View("Create", cModel);
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <param name="contactsModel"></param>
        /// <returns></returns>
        // POST: ManageContacts/Create,
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Status")] ContactsModel contactsModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string url = string.Empty;
                    string token = string.Empty;
                    GetAPIDetails(ref url, ref token);
                    APIHelper.CreateContact(contactsModel, url, token);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(contactsModel);
        }

        /// <summary>
        /// Edit contact
        /// </summary>
        /// <param name="id">contact Id</param>
        /// <returns></returns>
        // GET: ManageContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactsModel contactsModel = new Models.ContactsModel();
            try
            {
                string url = string.Empty;
                string token = string.Empty;
                GetAPIDetails(ref url, ref token);
                contactsModel = APIHelper.GetContactDetails(id.Value, url, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (contactsModel == null)
            {
                return HttpNotFound();
            }
            return View(contactsModel);
        }

        /// <summary>
        /// Save contact on Edit
        /// </summary>
        /// <param name="contactsModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Status")] ContactsModel contactsModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string url = string.Empty;
                    string token = string.Empty;
                    GetAPIDetails(ref url, ref token);
                    APIHelper.CreateContact(contactsModel, url, token);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");
            }
            return View(contactsModel);
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ManageContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactsModel contactsModel = new Models.ContactsModel();
            try
            {
                string url = string.Empty;
                string token = string.Empty;
                GetAPIDetails(ref url, ref token);
                contactsModel = APIHelper.GetContactDetails(id.Value, url, token);
                if (contactsModel == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(contactsModel);
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="id">contact Id</param>
        /// <returns></returns>
        // POST: ManageContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                string url = string.Empty;
                string token = string.Empty;
                GetAPIDetails(ref url, ref token);
                APIHelper.DeleteContact(id, url, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get API Details
        /// </summary>
        /// <param name="url">base url</param>
        /// <param name="token">api token</param>
        private void GetAPIDetails(ref string url, ref string token)
        {
            url = Request.Url.AbsoluteUri;
            url = url.Replace(Request.Url.AbsolutePath, "/");
            token = Convert.ToString(Session[SessionKeys.APIToken]);
        }
        #endregion
    }
}
