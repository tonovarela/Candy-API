using System;
using System.Data;
using CandyApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CandyApi.Repository;

public class UserRepository : IUserRespository
{
    private readonly ApplicationDBContext _db;

    public UserRepository(ApplicationDBContext db)
    {
        _db = db;
    }
    
    public bool IsValidUserCredentials(string username, string password)
    {      
      var query = _db.CatUsuarios
            .FromSqlInterpolated($"SELECT * FROM ControlLogin.dbo.cat_Usuarios WHERE Login = {username} AND Password = {password} and Estatus='ACTIVO' ")
            .AsNoTracking();

        return query.Any();
    }
}
