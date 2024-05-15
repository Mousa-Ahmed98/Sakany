using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.DTOS
{
    public class MessageDTO
    {

        [Required(ErrorMessage = "Please enter your full name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your message.")]
        public string MessageText { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression("^01[0125]\\d{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }
    }
}
