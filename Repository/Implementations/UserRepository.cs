
using CandyApi.Data;
using Microsoft.EntityFrameworkCore;

using CandyApi.DTO;
using CandyApi.Repository.Interfaces;

namespace CandyApi.Repository.Implementations;

public class UserRepository : IUserRespository
{
    private readonly ApplicationDBContext _db;

    public UserRepository(ApplicationDBContext db)
    {
        _db = db;
    }
    
    public  Task<bool> IsValidUserCredentials(LoginDTO loginDTO)
    {
        var username = loginDTO.Username;
        var password = loginDTO.Password;          
      var query = _db.CatUsuarios
            .FromSqlInterpolated($"SELECT * FROM ControlLogin.dbo.cat_Usuarios WHERE Login = {username} AND Password = {password} and Estatus='ACTIVO' ")
            .AsNoTracking();
        return query.AnyAsync();
    
    }
}
