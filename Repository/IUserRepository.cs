using System;
using CandyApi.DTO;

namespace CandyApi.Repository;

public interface IUserRespository
{

    public Task<bool> IsValidUserCredentials(LoginDTO loginDTO);

}
