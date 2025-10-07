using OfficePlanner.Api.Data.Models;

namespace OfficePlanner.Api.Data.Interfaces;

public interface IAdminAccess : IGenericAccess<Admin, int>
{
    // Empty for the moment, inherited basic CRUD
}