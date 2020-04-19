using System.Collections.Generic;

namespace ContactMangement.Data.Abstract
{
    //Abstract Interface for Contact management
    public interface IContactRepository
    {
        List<Contact> GetContactsList();
        void SaveContact(Contact newContact);
        Contact GetContactDetails(int id);
        void DeleteContact(int id);
    }
}
