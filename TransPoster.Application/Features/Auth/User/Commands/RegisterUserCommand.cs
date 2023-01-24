using MediatR;
using TransPorter.Shared.Enums;
using TransPorter.Shared.Wrapper;

namespace TransPoster.Application.Features.Auth.RegisterUser.Command
{
    public class RegisterUserCommand : IRequest<IResult<Guid>>
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Othernames { get; set; }
        public string Surname { get; set; } = null!;
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
