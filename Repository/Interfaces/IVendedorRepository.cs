

using CandyApi.Entities;

namespace CandyApi.Repository.Interfaces;

public interface IVendedorRepository
{

  public Task<IEnumerable<Vendedor>> listar();

}
