using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;
using Plantjes.Dao;
using Plantjes.ViewModels.Interfaces;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using Plantjes.Models.Extensions;
using Plantjes.Models.Classes;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

namespace Plantjes.ViewModels
{
    //written by Warre
    internal class ViewModelAdd : ViewModelBase
    {
        private readonly ISearchService searchService;

        private TfgsvType selectedType;
        private TfgsvFamilie selectedFamilie;
        private TfgsvGeslacht selectedGeslacht;
        private TfgsvSoort selectedSoort;
        private TfgsvVariant selectedVariant;

        private string selectedFenotypeMaand;

        private ObservableCollection<GroupBox> beheersdaden;

        public ViewModelAdd(ISearchService searchService)
        {
            this.searchService = searchService;
            beheersdaden = new ObservableCollection<GroupBox>();
            AddBeheersdaadItem();
            AddBeheersdaadCommand = new Command(new Action(AddBeheersdaadItem));
            AddPlantCommand = new Command<object>(new Action<object>(AddPlant));
        }

        private IEnumerable<MenuItem> MakeColorMenuItemList()
        {
            foreach (FenoKleur item in searchService.GetList<FenoKleur>())
            {
                yield return new MenuItem()
                {
                    Width = double.NaN,
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = new Rectangle()
                    {
                        Width = 20,
                        Height = 20,
                        Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#" + Convert.ToHexString(item.HexWaarde)),
                    },
                    InputGestureText = item.NaamKleur.FirstToUpper(),
                };
            }
        }

        private IEnumerable<MenuItem> MakeMenuItemList<TEntity>(Func<TEntity, string> selector) where TEntity : class
        {
            foreach (TEntity item in searchService.GetList<TEntity>())
            {
                if (selector(item).Contains('-'))
                    continue;
                yield return new MenuItem()
                {
                    Width = double.NaN,
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = selector(item).FirstToUpper(),
                };
            }
        }

        private void AddBeheersdaadItem()
        {
            beheersdaden.Add(new Beheersdaad());
        }

        private void AddPlant(object parameters)
        {
            foreach (Beheersdaad beheersdaad in beheersdaden)
            {
                var test = beheersdaad.Area;
            }
        }

        public Command AddBeheersdaadCommand { get; set; }
        public Command<object> AddPlantCommand { get; set; }

        #region Tabcontrol
        //Written by Ian Dumalin on 18/03
        //Code for Q4
        #endregion

        #region Algemene Info
        public IEnumerable<TfgsvType> CmbTypes
        {
            get => searchService.GetList<TfgsvType>().OrderBy(t => t.Planttypenaam);
        }
        public IEnumerable<TfgsvFamilie> CmbFamilies
        {
            get => SelectedType == null ?
                Enumerable.Empty<TfgsvFamilie>() :
                searchService.GetListWhere<TfgsvFamilie>(f => f.TypeTypeid == SelectedType.Planttypeid).OrderBy(f => f.Familienaam);
        }
        public IEnumerable<TfgsvGeslacht> CmbGeslacht
        {
            get => SelectedFamilie == null ?
                Enumerable.Empty<TfgsvGeslacht>() :
                searchService.GetListWhere<TfgsvGeslacht>(g => g.FamilieFamileId == SelectedFamilie.FamileId).OrderBy(g => g.Geslachtnaam);
        }
        public IEnumerable<TfgsvSoort> CmbSoort
        {
            get => SelectedGeslacht == null ?
                Enumerable.Empty<TfgsvSoort>() :
                searchService.GetListWhere<TfgsvSoort>(s => s.GeslachtGeslachtId == SelectedGeslacht.GeslachtId).OrderBy(s => s.Soortnaam);
        }
        public IEnumerable<TfgsvVariant> CmbVariant
        {
            get => SelectedSoort == null ?
                Enumerable.Empty<TfgsvVariant>() :
                searchService.GetListWhere<TfgsvVariant>(v => v.SoortSoortid == SelectedSoort.Soortid).OrderBy(v => v.Variantnaam);
        }

        public TfgsvType SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("CmbFamilies");
            }
        }
        public string TextFamilie { get; set; }
        public TfgsvFamilie SelectedFamilie
        {
            get { return selectedFamilie; }
            set
            {
                selectedFamilie = value;
                OnPropertyChanged("CmbGeslacht");
            }
        }
        public string TextGeslacht { get; set; }
        public TfgsvGeslacht SelectedGeslacht
        {
            get { return selectedGeslacht; }
            set
            {
                selectedGeslacht = value;
                OnPropertyChanged("CmbSoort");
            }
        }
        public string TextSoort { get; set; }
        public TfgsvSoort SelectedSoort
        {
            get { return selectedSoort; }
            set
            {
                selectedSoort = value;
                OnPropertyChanged("CmbVariant");
            }
        }
        public string TextVariant { get; set; }
        public TfgsvVariant SelectedVariant
        {
            get { return selectedVariant; }
            set
            {
                selectedVariant = value;
            }
        }
        #endregion

        #region Fenotype
        public IEnumerable<string> CmbBladvorm
        {
            get => searchService.GetList<FenoBladvorm>().Select(f => f.Vorm);
        }
        public IEnumerable<string> CmbBloeiwijze
        {
            get => searchService.GetList<FenoBloeiwijze>().Select(f => f.Naam);
        }
        public IEnumerable<string> CmbHabitus
        {
            get => searchService.GetList<FenoHabitu>().Select(f => f.Naam);
        }
        public IEnumerable<string> CmbMaand
        {
            get => CultureInfo.GetCultureInfo("nl-BE").DateTimeFormat.MonthNames[..^1].Select(m => m.FirstToUpper());
        }
        public IEnumerable<MenuItem> CmbBladkleur
        {
            get => MakeColorMenuItemList();
        }
        public IEnumerable<MenuItem> CmbBloeikleur
        {
            get => MakeColorMenuItemList();
        }
        public IEnumerable<string> CmbBladhoogteMax
        {
            get => searchService.GetListWhere<FenotypeMulti>(f => f.Eigenschap.ToLower() == "bladhoogtemax" && f.Maand.ToLower() == SelectedFenotypeMaand.ToLower()).Select(f => f.Waarde);
        }
        public IEnumerable<string> CmbBloeihoogteMin
        {
            get => searchService.GetListWhere<FenotypeMulti>(f => f.Eigenschap.ToLower() == "bloeihoogtemin" && f.Maand.ToLower() == SelectedFenotypeMaand.ToLower()).Select(f => f.Waarde);
        }
        public IEnumerable<string> CmbBloeihoogteMax
        {
            get => searchService.GetListWhere<FenotypeMulti>(f => f.Eigenschap.ToLower() == "bloeihoogtemax" && f.Maand.ToLower() == SelectedFenotypeMaand.ToLower()).Select(f => f.Waarde);
        }

        public string SelectedFenotypeMaand
        {
            get { return selectedFenotypeMaand; }
            set
            {
                selectedFenotypeMaand = value;
                OnPropertyChanged("CmbBladkleur");
                OnPropertyChanged("CmbBloeikleur");
                OnPropertyChanged("CmbBladhoogteMax");
                OnPropertyChanged("CmbBloeihoogteMin");
                OnPropertyChanged("CmbBloeihoogteMax");
            }
        }
        #endregion

        #region Abiotische Factoren
        public IEnumerable<MenuItem> MBezonning
        {
            get => MakeMenuItemList<AbioBezonning>(a => a.Naam);
        }
        public IEnumerable<MenuItem> MGrondsoort
        {
            get => MakeMenuItemList<AbioGrondsoort>(a => a.Grondsoort);
        }
        public IEnumerable<MenuItem> MVochtbehoefte
        {
            get => MakeMenuItemList<AbioVochtbehoefte>(a => a.Vochtbehoefte);
        }
        public IEnumerable<MenuItem> MVoedingsbehoefte
        {
            get => MakeMenuItemList<AbioVoedingsbehoefte>(a => a.Voedingsbehoefte);
        }
        public IEnumerable<MenuItem> MHabitat
        {
            get => MakeMenuItemList<AbioHabitat>(a => a.Waarde);
        }
        #endregion

        #region Beheersdaden
        public ObservableCollection<GroupBox> IctrlBeheersdaad
        {
            get => beheersdaden;
        }
        #endregion

        #region Commensalisme
        public IEnumerable<MenuItem> CmbOntwikkelingssnelheid
        {
            get => MakeMenuItemList<AbioBezonning>(a => a.Naam);
        }
        public IEnumerable<MenuItem> CmbConcurrentiekracht
        {
            get => MakeMenuItemList<AbioGrondsoort>(a => a.Grondsoort);
        }
        #endregion
    }
}
