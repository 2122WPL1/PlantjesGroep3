using Microsoft.Win32;
using MvvmHelpers.Commands;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using System.Drawing;

namespace Plantjes.ViewModels
{
    //written by Warre
    internal class ViewModelAdd : ViewModelBase
    {
        private readonly ISearchService searchService;

        private int selectedTab;
        private TfgsvType selectedType;
        private TfgsvFamilie selectedFamilie;
        private TfgsvGeslacht selectedGeslacht;
        private TfgsvSoort selectedSoort;
        private TfgsvVariant selectedVariant;
        private string selectedFenotypeMaand;

        private ObservableCollection<Beheersdaad> beheersdaden;
        private ObservableCollection<FenotypeMonth> fenotypeMonths;

        private string typeColor = "Black";
        private string familieColor = "Black";
        private string geslachtColor = "Black";

        public ViewModelAdd(ISearchService searchService)
        {
            this.searchService = searchService;
            selectedTab = 0;

            beheersdaden = new ObservableCollection<Beheersdaad>() { new Beheersdaad() };
            fenotypeMonths = new ObservableCollection<FenotypeMonth>() { new FenotypeMonth() };
            AddBeheersdaadCommand = new Command(new Action(AddBeheersdaadItem));
            AddFenotypeMonthCommand = new Command(new Action(AddFenotypeMonth));
            AddPlantCommand = new Command<object>(new Action<object>(AddPlant));

            BloeiFotoCommand = new Command(new Action(AddFoto));
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
                    Header = new System.Windows.Shapes.Rectangle()
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

    private bool IsRequiredFilled()
    {
        if (new List<string>() { SelectedType?.Planttypenaam, TextFamilie, TextGeslacht }.Any(s => string.IsNullOrEmpty(s)))
        {
            MessageBox.Show("Zorg dat je de verplichte velden ingevuld hebt!");
            selectedTab = 0;
            OnPropertyChanged();
            return false;
        }
        return true;
    }

        /// <summary>
        /// Adds a <see cref="Beheersdaad"/> to <see cref="beheersdaden"/>.
        /// </summary>
        private void AddBeheersdaadItem()
        {
            beheersdaden.Add(new Beheersdaad());
        }

        /// <summary>
        /// Adds a <see cref="FenotypeMonth"/> to <see cref="fenotypeMonths"/>.
        /// </summary>
        private void AddFenotypeMonth()
        {
            fenotypeMonths.Add(new FenotypeMonth());
        }

        private void AddFoto()
        {
            string path = "";
            string savepath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG files|*.jpeg|PNG files|*.png";

            if (ofd.ShowDialog() == true) 
            {
                path = ofd.FileName;
            }
            else
            {
                return;
            }

            BitmapImage inputImage = new BitmapImage(new Uri(path, UriKind.Absolute));

        }

        /// <summary>
        /// Adds a new <see cref="Plant"/> and all its eigenschappen to the database.
        /// </summary>
        /// <param name="parameters">The values from the <see cref="PlantParameterConverter"/>.</param>
        /// <exception cref="ArgumentException">Throws exception if the required parameters are not entered.</exception>
        private void AddPlant(object parameters)
        {
            List<object> items = parameters as List<object>;

            // Checks if the required fields are filled in
            IsRequiredFilled();

            // Adds the base plant to the DB
            Plant plant = DaoPlant.AddPlant(SelectedType.Planttypenaam, TextFamilie, TextGeslacht, 
                string.IsNullOrEmpty(TextFamilie) ? null : TextFamilie,
                string.IsNullOrEmpty(TextVariant) ? null : TextVariant);

            // checks if any fields for abiotiek are filled in
            if (MBezonning.Any(mi => mi.IsChecked) || MGrondsoort.Any(mi => mi.IsChecked) || MVoedingsbehoefte.Any(mi => mi.IsChecked))
            {
                // formats the bezonning, grondsoort and voedingsbehoefte to the uniform info in DB
                string bezonning = null, grondsoort = null, voedingsBehoefte = null;
                if (MBezonning.Any(mi => mi.IsChecked))
                {
                    bezonning = string.Empty;
                    foreach (MenuItem item in MBezonning)
                    {
                        if (item.IsChecked)
                            bezonning += item.Header + " - ";
                    }
                    bezonning = bezonning[..^3];
                }
                if (MGrondsoort.Any(mi => mi.IsChecked))
                {
                    grondsoort = string.Empty;
                    foreach (MenuItem item in MGrondsoort)
                    {
                        if (item.IsChecked)
                            grondsoort += item.Header;
                    }
                }
                if (MVoedingsbehoefte.Any(mi => mi.IsChecked))
                {
                    voedingsBehoefte = string.Empty;
                    foreach (MenuItem item in MVoedingsbehoefte)
                    {
                        if (item.IsChecked)
                            voedingsBehoefte += item.Header + " ";
                    }
                    voedingsBehoefte = voedingsBehoefte[..^1];
                }
                // adds the data to above plant
                DaoAbiotiek.AddAbiotiek(plant, bezonning, grondsoort, string.IsNullOrEmpty(items[0] as string) ? null : items[0] as string, voedingsBehoefte);
            }

            // checks each habitat if filled in and adds to db
            foreach (MenuItem item in MHabitat)
            {
                if (item.IsChecked)
                    DaoAbiotiek.AddAbiotiekMulti(plant, "habitat", item.Header as string);
            }

            // checks each beheersdaad and adds to db if not empty
            foreach (Beheersdaad beheersdaad in IctrlBeheersdaad)
            {
                if (!string.IsNullOrEmpty(beheersdaad.BeheersdaadText))
                    DaoBeheersdaden.AddBeheersdaden(plant, beheersdaad.BeheersdaadText, beheersdaad.Months.Select(mi => mi.IsChecked).ToList());
            }

            // checks if any strategie is checked
            if (MStrategie.Any(mi => mi.IsChecked))
            {
                // formats and adds the strategie to the uniform form in DB
                string strategie = string.Empty;
                foreach (MenuItem item in MStrategie)
                {
                    if (item.IsChecked)
                        strategie += item.Header;
                }
                DaoCommensalisme.AddCommensalisme(plant, string.IsNullOrEmpty(items[1] as string) ? null : items[1] as string, strategie);
            }

            // converts number to its asci and adds sociabiliteit to DB if checked
            int socIndex = 49;
            foreach (bool? check in items.GetRange(2, 5))
            {
                if (check ?? false)
                    DaoCommensalisme.AddCommensalismeMulti(plant, "sociabiliteit", ((char)socIndex).ToString());
                socIndex++;
            }

            // checks if any param are filled, adds fenotype to DB
            if (new List<object>() { items[7], items[8], items[9], items[10], items[11], items[12], items[13] }.Any(s => !string.IsNullOrEmpty(s as string)))
            {
                int bladGrootte;
                if (!int.TryParse(items[7] as string, out bladGrootte))
                {
                    bladGrootte = 0;
                }
                DaoFenotype.AddFenotype(plant,
                    bladGrootte <= 0 ? null : bladGrootte,
                    string.IsNullOrEmpty(items[8] as string) ? null : items[8] as string,
                    string.IsNullOrEmpty(items[9] as string) ? null : items[9] as string,
                    string.IsNullOrEmpty(items[10] as string) ? null : items[10] as string,
                    string.IsNullOrEmpty(items[11] as string) ? null : items[11] as string,
                    string.IsNullOrEmpty(items[12] as string) ? null : items[12] as string);
            }

            if (IctrlFenotypeMonth.Any(fm => !string.IsNullOrEmpty(fm.SelectedMonth)))
            {
                foreach (FenotypeMonth fenotype in IctrlFenotypeMonth)
                {
                    if (!string.IsNullOrEmpty(fenotype.Bladgrootte))
                        DaoFenotype.AddFenotypeMulti(plant, "bladgrootte", fenotype.Bladgrootte, fenotype.SelectedMonth);
                    if (!string.IsNullOrEmpty(fenotype.Bladhoogte))
                        DaoFenotype.AddFenotypeMulti(plant, "bladhoogte", fenotype.Bladhoogte, fenotype.SelectedMonth);
                }
            }
            foreach (MenuItem item in MBladkleur)
            {
                if (item.IsChecked)
                    DaoFenotype.AddFenotypeMulti(plant, "bladkleur", item.Header as string);
            }
            foreach (MenuItem item in MBloeikleur)
            {
                if (item.IsChecked)
                    DaoFenotype.AddFenotypeMulti(plant, "bloeikleur", item.Header as string);
            }
        }

        public int SelectedTab
        {
            get { return selectedTab; }
            set
            {

                if (IsRequiredFilled())
                {
                    selectedTab = value;
                
                }
            }
        }

        public string TypeColor
        {
            get { return typeColor; }
            set
            {
                typeColor = value;
                OnPropertyChanged("TypeColor");
            }
        }

        public string FamilieColor
        {
            get { return familieColor; }
            set
            {
                familieColor = value;
                OnPropertyChanged("FamilieColor");
            }
        }

        public string GeslachtColor
        {
            get { return geslachtColor; }
            set
            {
                geslachtColor = value;
                OnPropertyChanged("GeslachtColor");
            }
        }

        public Command<object> AddPlantCommand { get; set; }

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

        public Command BloeiFotoCommand { get; set; }
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
            get => Helper.GetMonthsList();
        }
        public IEnumerable<string> CmbSpruitfenologie
        {
            get => searchService.GetList<FenoSpruitfenologie>().Select(f => f.Fenologie);
        }
        public IEnumerable<MenuItem> MBladkleur
        {
            get => Helper.MakeColorMenuItemList();
        }
        public IEnumerable<MenuItem> MBloeikleur
        {
            get => Helper.MakeColorMenuItemList();
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

        public ObservableCollection<FenotypeMonth> IctrlFenotypeMonth
        {
            get => fenotypeMonths;
        }

        public Command AddFenotypeMonthCommand { get; set; }
        #endregion

        #region Abiotische Factoren
        public IEnumerable<MenuItem> MBezonning
        {
            get => Helper.MakeMenuItemList<AbioBezonning>(a => a.Naam);
        }
        public IEnumerable<MenuItem> MGrondsoort
        {
            get => Helper.MakeMenuItemList<AbioGrondsoort>(a => a.Grondsoort);
        }
        public IEnumerable<string> CmbVochtbehoefte
        {
            get => searchService.GetList<AbioVochtbehoefte>().Select(v => v.Vochtbehoefte);
        }
        public IEnumerable<MenuItem> MVoedingsbehoefte
        {
            get => Helper.MakeMenuItemList<AbioVoedingsbehoefte>(a => a.Voedingsbehoefte);
        }
        public IEnumerable<MenuItem> MHabitat
        {
            get => Helper.MakeMenuItemList<AbioHabitat>(a => a.Afkorting);
        }
        #endregion

        #region Beheersdaden
        public ObservableCollection<Beheersdaad> IctrlBeheersdaad
        {
            get => beheersdaden;
        }
        public Command AddBeheersdaadCommand { get; set; }
        #endregion

        #region Commensalisme
        public IEnumerable<string> CmbOntwikkelingssnelheid
        {
            get => searchService.GetList<CommOntwikkelsnelheid>().Select(o => o.Snelheid);
        }
        public IEnumerable<MenuItem> MStrategie
        {
            get => Helper.MakeMenuItemList<CommStrategie>(s => s.Strategie);
        }
        public IEnumerable<MenuItem> MConcurrentiekracht
        {
            get => Helper.MakeMenuItemList<CommLevensvorm>(a => a.Levensvorm);
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
