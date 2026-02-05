using System;
using CandyApi.DTO;

namespace CandyApi.Repository.Interfaces;

public interface IUserRespository
{

    public Task<bool> IsValidUserCredentials(LoginDTO loginDTO);

}
