using GalaSoft.MvvmLight.Command;
using MvvmHelpers.Commands;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.ViewModels
{
    // Written by Warre and Renzo
    // Export written bij Ian Dumalin on 4/5
    public class ViewModelSearch : ViewModelBase
    {
        private readonly IEnumerable<PlantItem> _emptyPlants = new List<PlantItem>() { new(), new(true) };

        private readonly ISearchService _searchService;

        private IEnumerable<Plant> _plants;

        public Command CollapseCommand { get; set; }

        public ViewModelSearch(ISearchService searchService)
        {
            _searchService = searchService;

            CollapseCommand = new Command(Collapse);
            SearchCommand = new Command<object>(Search);
            ExportCommand = new RelayCommand(ExportCsv);

            CmbHabitus = searchService.GetList<FenoHabitu>().Select(f => f.Naam).Prepend(string.Empty);
            CmbBezonning = searchService.GetList<AbioBezonning>().Select(a => a.Naam).Prepend(string.Empty);
            CmbHabitat = searchService.GetList<AbioHabitat>().Select(a => a.Afkorting).Prepend(string.Empty);
            CmbGrondsoort = searchService.GetList<AbioGrondsoort>().Select(a => a.Grondsoort).Prepend(string.Empty);
            CbBladkleur = searchService.GetList<FenoKleur>().Select(a => new StackLabelRect(a)).Prepend(StackLabelRect.Empty);
            CbBloeikleur = searchService.GetList<FenoKleur>().Select(a => new StackLabelRect(a)).Prepend(StackLabelRect.Empty);
            CmbBladvorm = searchService.GetList<FenoBladvorm>().Select(f => f.Vorm).Prepend(string.Empty);
        }

        private void Search(object parameters)
        {
            List<object> items = parameters as List<object>;

            int socIndex = 65;
            string selectedSoc = string.Empty;
            foreach (bool? check in items.GetRange(4, 5))
            {
                if (check ?? false)
                {
                    selectedSoc = ((char)socIndex).ToString();
                    break;
                }
                socIndex++;
            }

            _plants = _searchService.GetListPlants(
                items[0] as string,
                items[1] as string,
                items[2] as string,
                items[3] as string,
                selectedSoc,
                items[9] as string,
                (items[10] as StackLabelRect)?.NamenKleur ?? null,
                (items[11] as StackLabelRect)?.NamenKleur ?? null,
                items[12] as string);
            OnPropertyChanged("Plants");
        }

        private void Collapse()
        {
            BtnCollapse = BtnCollapse == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged("BtnCollapse");
        }

        private void ExportCsv()
        {
            CsvHelper.ExportPlantsToCsv(_plants);
        }

        public Command SearchCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        public IEnumerable<PlantItem> Plants
        {
            get { return _plants == null ? Enumerable.Empty<PlantItem>() : !_plants.Any() ? _emptyPlants : _plants.Select(p => new PlantItem(p)); }
        }

        public IEnumerable<string> CmbHabitus { get; }

        public IEnumerable<string> CmbBezonning { get; }

        public IEnumerable<string> CmbHabitat { get; }

        public IEnumerable<string> CmbGrondsoort { get; }

        public Visibility BtnCollapse { get; private set; } = Visibility.Collapsed;

        public IEnumerable<StackPanel> CbBladkleur { get; }

        public IEnumerable<StackPanel> CbBloeikleur { get; }

        public IEnumerable<string> CmbBladvorm { get; }
    }
}
