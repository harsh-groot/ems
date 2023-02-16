using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace EMS.ViewModel
{
    public class EMSRequest
    {
        [Required]
        [BindRequired]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}