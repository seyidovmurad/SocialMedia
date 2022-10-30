using System.ComponentModel.DataAnnotations;

namespace SocialMedia.WebUI.Models
{
    public class AccountDetailViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? ProfilePic { get; set; }

        public IFormFile? ImageFile { get; set; }


        //[DataType(DataType.PhoneNumber)]
        //public string? Phone { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Address { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Country { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Town { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[DataType(DataType.PostalCode)]
        //public string PostalCode { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //[MaxLength(256,ErrorMessage = "You are exiting max 256 character.")]
        //public string Description { get; set; }
    }
}
