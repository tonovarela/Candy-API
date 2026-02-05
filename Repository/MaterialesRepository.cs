using System;
using CandyApi.Data;
using CandyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandyApi.Repository;

public class MaterialesRepository:IMaterialesRepository
{

    private readonly ApplicationDBContext _db;

    public MaterialesRepository(ApplicationDBContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<TipoMaterial>> obtener(string id, string descripcion, string catalogo)
    {
        //  var tablasPermitidas = new[] { "TiposMateriales", "Catalogos", "OtraTabla" };
        // if (!tablasPermitidas.Contains(catalogo))
        //     throw new ArgumentException($"Catálogo no permitido: {catalogo}");

        // // Lista blanca de columnas permitidas
        // var columnasPermitidas = new[] { "Id", "Nombre", "Descripcion", "Codigo" };
        // if (!columnasPermitidas.Contains(id) || !columnasPermitidas.Contains(descripcion))
        //     throw new ArgumentException("Columna no permitida");
                    
        string sql=$"select {id} Id, {descripcion} Nombre from {catalogo}";
        Console.WriteLine($"Ejecutando consulta SQL: {sql}");
        var result = await _db.TiposMateriales
                .FromSqlRaw(sql)
            .AsNoTracking().ToListAsync();
        return result;
    }
}
