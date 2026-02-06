using CandyApi.Data;
using CandyApi.Entities;
using Microsoft.EntityFrameworkCore;
using CandyApi.Repository.Interfaces;

namespace CandyApi.Repository.Implementations;

public class VendedorRepository : IVendedorRepository
{

    private readonly ApplicationDBContext _db;
    public VendedorRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Vendedor>> listar()
    {

       FormattableString sql = $"SELECT Id_Usuario AS Id, Nombre, Correo, Puesto, Agente FROM ControlLogin.dbo.cat_Usuarios WHERE Departamento='Ventas' AND estatus='ACTIVO'";        
        var result = await _db.Vendedores
                              .FromSqlInterpolated(sql)
                              .AsNoTracking()
                              .ToListAsync();
        
        return result;
        
    }
}
