
using CandyApi.Entities;

namespace CandyApi.Repository.Interfaces;

public interface IMaterialesRepository
{
    
    public Task<IEnumerable<TipoMaterial>> obtener(string id,string descripcion,string catalogo);
    


}
