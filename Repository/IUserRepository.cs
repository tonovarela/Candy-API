using System;

namespace CandyApi.Repository;

public interface IUserRespository
{

    public bool IsValidUserCredentials(string username, string password);

}
