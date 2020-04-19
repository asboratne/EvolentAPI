using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    //Contact Model
    public class ContactsModel
    {
        public ContactsModel()
        {
            Status = true;
        }
        [Key]
        public int Id { get; set; }
        [StringLength(250, ErrorMessage = "Please enter the Contacts First Name"), Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(250, ErrorMessage = "Please enter the Contacts Last Name"), Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Please enter the Contacts Email Address"),Required]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Contacts Phone Number"),Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool Status { get; set; } //status
    }
}