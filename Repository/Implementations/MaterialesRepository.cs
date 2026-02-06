using System;
using CandyApi.Data;
using CandyApi.Entities;
using CandyApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandyApi.Repository.Implementations;

public class MaterialesRepository:IMaterialesRepository
{

    private readonly ApplicationDBContext _db;

    public MaterialesRepository(ApplicationDBContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<TipoMaterial>> obtener(string id, string descripcion, string catalogo)
    {                    
        string sql=$"select {id} Id, {descripcion} Nombre from {catalogo}";        
        var result = await _db.TiposMateriales
                .FromSqlRaw(sql)
            .AsNoTracking().ToListAsync();
        return result;
    }
}
