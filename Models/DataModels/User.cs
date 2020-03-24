using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(100, ErrorMessage ="Nom, maximum 100 char")]
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(maximumLength:11, MinimumLength =11, ErrorMessage ="n° de registre national non valide")]
        public string NationalNumber { get; set; }

        [Required]
        [StringLength(maximumLength:10, MinimumLength = 9, ErrorMessage ="n° de telephone non valide")]
        public string Phonenumber { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z0-9\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage ="Email non valide")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Password { get; set; } = "secret";

        public string Sex { get; set; }

        [Required]
        public int CompanyStatus { get; set; }

        public Address PrivateAddress { get; set; } = new Address();
    }
}
