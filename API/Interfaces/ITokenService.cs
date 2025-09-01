using API.Entities;
using System;

namespace API.Interfaces;

public interface ITokenService
{
    public string CreateToken(AppUser user);
}
