using Core.CrossCuttingConcerns.Exceptions;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCannotBeDublicatedWhenRegistered(string email)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email);
        if (user != null)
        {
            throw new BusinessException("Mail already exists.");
        }
    }
}