using MvvmHelpers.Commands;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels
{
    public class ViewModelSearch : ViewModelBase
    {
        private readonly ISearchService searchService;

        private IEnumerable<Plant> plants;

        public ViewModelSearch(ISearchService searchService)
        {
            this.searchService = searchService;

            SearchCommand = new Command(new Action(Search));
        }

        // GetListPlants(string? type, string? familie, string? geslacht, string? grondsoort, string? habitat, string? habitus, string? sociabiliteit, string? bezonning)
        private void Search()
        {
            plants = searchService.GetListPlants(null, null, null, null, null, null, null, null);
            OnPropertyChanged("Plants");
        }

        public Command SearchCommand { get; set; }

        public IEnumerable<PlantItem> Plants
        {
            get
            {
                return plants?.Select(p => new PlantItem(p)) ?? Enumerable.Empty<PlantItem>();
            }
        }
    }
}
