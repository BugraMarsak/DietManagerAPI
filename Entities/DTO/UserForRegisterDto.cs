using Core.Entities;

namespace Entities.DTO
{
    public class UserForRegisterDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }

    }
}
