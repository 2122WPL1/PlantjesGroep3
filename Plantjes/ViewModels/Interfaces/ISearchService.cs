using Plantjes.Models;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Plantjes.ViewModels.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<TEntity> GetList<TEntity>(bool distinct = false) where TEntity : class;

        IEnumerable<TEntity> GetListWhere<TEntity>(Func<TEntity, bool> predicate, bool distinct = false) where TEntity : class;

        IEnumerable<Plant> GetListPlants(string? naam, string? grondsoort, string? habitat, string? habitus, string? sociabiliteit, string? bezonning);
    }
}
