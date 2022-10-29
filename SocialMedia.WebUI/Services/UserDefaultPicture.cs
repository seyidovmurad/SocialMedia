using SocialMedia.Entities.Abstract;
using SocialMedia.Entities.Concrete;

namespace SocialMedia.WebUI.Services
{
    public static class UserDefaultPicture
    {
        private const string DefaultMalePictureUrl = "default_profile_male.jpg";
        private const string DefaultFemalePictureUrl = "default_profile_female.jpg";
        private const string DefaultPictureUrl = "default_profile.png";


        public static string GetProfilePic(string? pic, UserGender gender)
        {
            return pic ?? (gender == UserGender.Male ?
                             DefaultMalePictureUrl : gender == UserGender.Female ?
                             DefaultFemalePictureUrl : DefaultPictureUrl);
        }
    }
}
