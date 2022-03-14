using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;
using Plantjes.Dao;
using Plantjes.ViewModels.Interfaces;
using System.Globalization;
using Plantjes.ViewModels.HelpClasses;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using Plantjes.Models.Extensions;
using Plantjes.Models.Classes;

namespace Plantjes.ViewModels
{
    //written by Warre
    internal class ViewModelAdd : ViewModelBase
    {
        private readonly DAOLogic dao;
        private readonly ISearchService searchService;

        private TfgsvType selectedType;
        private TfgsvFamilie selectedFamilie;
        private TfgsvGeslacht selectedGeslacht;
        private TfgsvSoort selectedSoort;
        private TfgsvVariant selectedVariant;

        private string selectedFenotypeMaand;

        private List<StackPanel> beheersdaden;

        public ViewModelAdd(ISearchService searchService)
        {
            dao = DAOLogic.Instance();
            this.searchService = searchService;
            AddBeheersdaadCommand = new Command<List<StackPanel>>(new Action<List<StackPanel>>(addBeheersdaadItem));
            ToevoegenCommand = new Command<object>(new Action<object>(AddPlant));
        }

        private IEnumerable<MenuItem> makeColorMenuItemList()
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

        private IEnumerable<MenuItem> makeMenuItemList<TEntity>(Func<TEntity, string> selector) where TEntity : class
        {
            foreach (TEntity item in searchService.GetList<TEntity>())
            {
                yield return new MenuItem()
                {
                    Width = double.NaN,
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = selector(item).FirstToUpper(),
                };
            }
        }

        private void addBeheersdaadItem(List<StackPanel> panels)
        {
            panels.Insert(panels.Count - 1, new Beheersdaad());
        }

        private void AddPlant(object parameters)
        {
            
        }

        public Command<List<StackPanel>> AddBeheersdaadCommand { get; set; }
        public Command<object> ToevoegenCommand { get; set; }

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
        public TfgsvFamilie SelectedFamilie
        {
            get { return selectedFamilie; }
            set
            {
                selectedFamilie = value;
                OnPropertyChanged("CmbGeslacht");
            }
        }
        public TfgsvGeslacht SelectedGeslacht
        {
            get { return selectedGeslacht; }
            set
            {
                selectedGeslacht = value;
                OnPropertyChanged("CmbSoort");
            }
        }
        public TfgsvSoort SelectedSoort
        {
            get { return selectedSoort; }
            set
            {
                selectedSoort = value;
                OnPropertyChanged("CmbVariant");
            }
        }
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
        public IEnumerable<string> CmbBladgrootte
        {
            get => searchService.GetList<FenoBladgrootte>().Select(f => f.Bladgrootte);
        }
        public IEnumerable<string> CmbBladvorm
        {
            get => searchService.GetList<FenoBladvorm>().Select(f => f.Vorm);
        }
        public IEnumerable<string> CmbRatio
        {
            get => searchService.GetList<FenoSpruitfenologie>().Select(f => f.Fenologie);
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
            get => CultureInfo.GetCultureInfo("nl-BE").DateTimeFormat.MonthNames;
        }
        public IEnumerable<MenuItem> CmbBladkleur
        {
            get => makeColorMenuItemList();
        }
        public IEnumerable<MenuItem> CmbBloeikleur
        {
            get => makeColorMenuItemList();
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
        public IEnumerable<MenuItem> CmbBezonning
        {
            get => makeMenuItemList<AbioBezonning>(a => a.Naam);
        }
        public IEnumerable<MenuItem> CmbGrondsoort
        {
            get => makeMenuItemList<AbioGrondsoort>(a => a.Grondsoort);
        }
        public IEnumerable<MenuItem> CmbVochtbehoefte
        {
            get => makeMenuItemList<AbioVochtbehoefte>(a => a.Vochtbehoefte);
        }
        public IEnumerable<MenuItem> CmbVoedingsbehoefte
        {
            get => makeMenuItemList<AbioVoedingsbehoefte>(a => a.Voedingsbehoefte);
        }
        public IEnumerable<MenuItem> CmbHabitat
        {
            get => makeMenuItemList<AbioHabitat>(a => a.Waarde);
        }
        #endregion

        #region Beheersdaden
        public List<StackPanel> IctrlBeheersdaad
        {
            get => beheersdaden;
        }
        #endregion

        #region Commensalisme
        public IEnumerable<MenuItem> CmbOntwikkelingssnelheid
        {
            get => makeMenuItemList<AbioBezonning>(a => a.Naam);
        }
        public IEnumerable<MenuItem> CmbConcurrentiekracht
        {
            get => makeMenuItemList<AbioGrondsoort>(a => a.Grondsoort);
        }
        public IEnumerable<MenuItem> CmbSociabiliteit
        {
            get => makeMenuItemList<AbioVochtbehoefte>(a => a.Vochtbehoefte);
        }
        #endregion
    }
}
