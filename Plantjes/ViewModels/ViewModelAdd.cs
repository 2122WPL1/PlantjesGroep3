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
using Plantjes.ViewModels.HelpClasses;

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
            beheersdaden = new ObservableCollection<GroupBox>() { new Beheersdaad() };
            AddBeheersdaadCommand = new Command(new Action(AddBeheersdaadItem));
            AddPlantCommand = new Command<object>(new Action<object>(AddPlant));
        }

        /// <summary>
        /// Makes an MenuItem list with all colors in the database.
        /// </summary>
        /// <returns>Returns a menuitem list with all color previews and names.</returns>
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

        /// <summary>
        /// Makes a MenuItem list with preset settings.
        /// </summary>
        /// <typeparam name="TEntity">The entity to be used for selector.</typeparam>
        /// <param name="selector">The name of the TEntity.</param>
        /// <returns>Returns a list of menu items with the name of TEntity.</returns>
        private IEnumerable<MenuItem> MakeMenuItemList<TEntity>(Func<TEntity, string> selector) where TEntity : class
        {
            foreach (TEntity item in searchService.GetList<TEntity>())
            {
                if (selector(item).Contains('-') ||
                    (item is AbioGrondsoort && selector(item).Length > 1) ||
                    (item is CommStrategie && selector(item).Length > 1))
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

        /// <summary>
        /// Adds a <see cref="Beheersdaad"/> to <see cref="beheersdaden"/>.
        /// </summary>
        private void AddBeheersdaadItem()
        {
            beheersdaden.Add(new Beheersdaad());
        }

        /// <summary>
        /// Adds a new <see cref="Plant"/> to the database and all its eigenschappen.
        /// </summary>
        /// <param name="parameters">The values from the <see cref="PlantParameterConverter"/>.</param>
        /// <exception cref="ArgumentException">Throws exception if the required parameters are not entered.</exception>
        private void AddPlant(object parameters)
        {
            List<object> items = parameters as List<object>;
            
            if (new List<string>() { SelectedType.Planttypenaam, TextFamilie, TextGeslacht }.Any(s => string.IsNullOrEmpty(s)))
                throw new ArgumentException("Zorg dat je alle algemene info ingevuld hebt!");

            Plant plant = DaoPlant.AddPlant(SelectedType.Planttypenaam, TextFamilie, TextGeslacht, 
                string.IsNullOrEmpty(TextFamilie) ? null : TextFamilie,
                string.IsNullOrEmpty(TextVariant) ? null : TextVariant);

            string bezonning = null, grondsoort = null, voedingsBehoefte = null;
            if (MBezonning.Any(mi => mi.IsChecked))
            {
                bezonning = string.Empty;
                foreach (MenuItem item in MBezonning)
                {
                    bezonning += item.Header + " - ";
                }
                bezonning = bezonning[..^3];
            }
            if (MGrondsoort.Any(mi => mi.IsChecked))
            {
                grondsoort = string.Empty;
                foreach (MenuItem item in MGrondsoort)
                {
                    grondsoort += item.Header;
                }
            }
            if (MVoedingsbehoefte.Any(mi => mi.IsChecked))
            {
                voedingsBehoefte = string.Empty;
                foreach (MenuItem item in MVoedingsbehoefte)
                {
                    voedingsBehoefte += item.Header + " ";
                }
                voedingsBehoefte = voedingsBehoefte[..^1];
            }
            DaoAbiotiek.AddAbiotiek(plant, bezonning, grondsoort, string.IsNullOrEmpty(items[0] as string) ? null : items[0] as string, voedingsBehoefte);


            foreach (MenuItem item in MHabitat)
            {
                if (item.IsChecked)
                    DaoAbiotiek.AddAbiotiekMulti(plant, "habitat", item.Header as string);
            }

            foreach (Beheersdaad beheersdaad in IctrlBeheersdaad)
            {
                if (!string.IsNullOrEmpty(beheersdaad.BeheersdaadText))
                    DaoBeheersdaden.AddBeheersdaden(plant, beheersdaad.BeheersdaadText, beheersdaad.Months.Select(mi => mi.IsChecked).ToList());
            }

            string strategie = null;
            if (MStrategie.Any(mi => mi.IsChecked))
            {
                strategie = string.Empty;
                foreach (MenuItem item in MStrategie)
                {
                    strategie += item.Header;
                }
            }
            DaoCommensalisme.AddCommensalisme(plant, string.IsNullOrEmpty(items[1] as string) ? null : items[1] as string, strategie);

            int socIndex = 49;
            foreach (bool check in items.GetRange(2, 5))
            {
                if (check)
                    DaoCommensalisme.AddCommensalismeMulti(plant, "sociabiliteit", ((char)socIndex).ToString());
                socIndex++;
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
        public IEnumerable<MenuItem> MBladkleur
        {
            get => MakeColorMenuItemList();
        }
        public IEnumerable<MenuItem> MBloeikleur
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
        public IEnumerable<string> CmbVochtbehoefte
        {
            get => searchService.GetList<AbioVochtbehoefte>().Select(v => v.Vochtbehoefte);
        }
        public IEnumerable<MenuItem> MVoedingsbehoefte
        {
            get => MakeMenuItemList<AbioVoedingsbehoefte>(a => a.Voedingsbehoefte);
        }
        public IEnumerable<MenuItem> MHabitat
        {
            get => MakeMenuItemList<AbioHabitat>(a => a.Afkorting);
        }
        #endregion

        #region Beheersdaden
        public ObservableCollection<GroupBox> IctrlBeheersdaad
        {
            get => beheersdaden;
        }
        #endregion

        #region Commensalisme
        public IEnumerable<string> CmbOntwikkelingssnelheid
        {
            get => searchService.GetList<CommOntwikkelsnelheid>().Select(o => o.Snelheid);
        }
        public IEnumerable<MenuItem> MStrategie
        {
            get => MakeMenuItemList<CommStrategie>(s => s.Strategie);
        }
        public IEnumerable<MenuItem> MConcurrentiekracht
        {
            get => MakeMenuItemList<CommLevensvorm>(a => a.Levensvorm);
        }
        #endregion

        #region Extra
        // Written by Ian Dumalin on 18/03
        public IEnumerable<string> CbPollen
        {
            get => searchService.GetList<ExtraPollenwaarde>().Select(o => o.Waarde);
        }

        public IEnumerable<string> CbNectar
        {
            get => searchService.GetList<ExtraNectarwaarde>().Select(o => o.Waarde);
        }
        #endregion
    }
}
