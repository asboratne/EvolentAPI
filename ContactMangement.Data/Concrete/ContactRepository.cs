using ContactMangement.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactMangement.Data.Concrete
{
    /// <summary>
    /// Concrete Repository
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        ContactManagementDBEntities entity = new ContactManagementDBEntities(); // edmx entity

        /// <summary>
        /// Get Contacts List
        /// </summary>
        /// <returns>List of saved contacts</returns>
        public List<Contact> GetContactsList()
        {
            List<Contact> contactList = new List<Data.Contact>();
            try
            {
                contactList = entity.Contacts.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return contactList;
        }

        /// <summary>
        /// Save Contact (both new and edit contact)
        /// </summary>
        /// <param name="newContact">contact entity</param>
        public void SaveContact(Contact newContact)
        {
            try
            {
                if (newContact.Id == 0) //new contact
                {
                    entity.Contacts.Add(newContact);
                }
                else //edit contact
                {
                    entity.Entry(newContact).State = System.Data.Entity.EntityState.Modified;
                }
                entity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Contact details
        /// </summary>
        /// <param name="id">contact id</param>
        /// <returns>contact details for the given id</returns>
        public Contact GetContactDetails(int id)
        {
            Contact contact = new Contact();
            try
            {
                contact = entity.Contacts.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return contact;
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id">contact Id</param>
        public void DeleteContact(int id)
        {
            try
            {
                Contact contact = entity.Contacts.Find(id);
                entity.Contacts.Remove(contact);
                entity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
