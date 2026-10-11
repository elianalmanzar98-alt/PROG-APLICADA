

using System.Linq.Expressions;

namespace GestionLibros.Services;

public interface IService<T>where T : class
{
    Task<bool> Guardar(T entidad);
    Task<T?> Buscar(int id);
    Task<bool> Eliminar(int id);
    Task<List<T>>  Listar(Expression<Func<T, bool>> criterio);
}