namespace OfficePlanner.Api.Data.Interfaces;

// TModel is the data model class like User or Events
// TKey is the type of the primary key or id in the db, like string or int
public interface IGenericAccess<TModel, TKey> where TModel : class
{
    Task<TModel?> GetByIdAsync(TKey id);
    Task<List<TModel>?> GetAllAsync();
    Task DeleteAsync(TKey id);
    Task InsertAsync(TModel item);
    Task UpdateAsync(TModel item);
}
