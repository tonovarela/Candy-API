using System;
using CandyApi.Entities;

namespace CandyApi.Repository;

public interface IMaterialesRepository
{
    
    public Task<IEnumerable<TipoMaterial>> obtener(string id,string descripcion,string catalogo);
    


}
