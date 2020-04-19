using AutoMapper;
using ContactManagement.Models;
using ContactMangement.Data;
using ContactMangement.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ContactManagement.Controllers
{
    /// <summary>
    /// Contacts API
    /// </summary>
    [Authorize]
    [System.Web.Mvc.HandleError]
    public class ContactsAPIController : ApiController
    {
        private IContactRepository _repository;

        public ContactsAPIController(IContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get Contacts List
        /// </summary>
        /// <returns>contact list</returns>
        [ActionName("GetContactsList")]
        [Route("api/ContactsAPI/GetContactsList")]
        [HttpGet]
        public IEnumerable<ContactsModel> GetContactsList()
        {
            List<ContactsModel> contactsList = new List<ContactsModel>();
            try
            {
                List<Contact> contactListEntity = _repository.GetContactsList();
                var iMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Contact, ContactsModel>();
                }).CreateMapper();

                contactsList = iMapper.Map<List<Contact>, List<ContactsModel>>(contactListEntity);
                return contactsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        /// <param name="contactModel">contact view model</param>
        [ActionName("SaveContact")]
        [Route("api/ContactsAPI/SaveContact")]
        [HttpPost]
        public void SaveContact(ContactsModel contactModel)
        {
            try
            {
                Contact newContact = new Contact();
                var iMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ContactsModel, Contact>();
                }).CreateMapper();

                newContact = iMapper.Map<ContactsModel, Contact>(contactModel);

                _repository.SaveContact(newContact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get Contact Details
        /// </summary>
        /// <param name="id">contact Id</param>
        /// <returns>contact details</returns>
        [ActionName("GetContactDetails")]
        [Route("api/ContactsAPI/GetContactDetails/{id}")]
        [HttpGet]
        public ContactsModel GetContactDetails(int id)
        {
            ContactsModel contactModel = new ContactsModel();
            try
            {
                Contact contactEntity = _repository.GetContactDetails(id);
                var iMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Contact, ContactsModel>();
                }).CreateMapper();

                contactModel = iMapper.Map<Contact, ContactsModel>(contactEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contactModel;
        }


        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id">contact Id</param>
        [ActionName("DeleteContact")]
        [Route("api/ContactsAPI/DeleteContact/{id}")]
        [HttpGet]
        public void DeleteContact(int id)
        {
            try
            {
                _repository.DeleteContact(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}