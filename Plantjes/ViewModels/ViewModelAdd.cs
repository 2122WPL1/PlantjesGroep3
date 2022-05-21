using GalaSoft.MvvmLight.Ioc;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Plantjes.ViewModels.Converters;

namespace Plantjes.ViewModels
{
    //written by Warre
    internal class ViewModelAdd : ViewModelBase
    {
        private readonly ISearchService _searchService;

        private int _selectedTab;
        private TfgsvType _selectedType;
        private TfgsvFamilie _selectedFamilie;
        private TfgsvGeslacht _selectedGeslacht;
        private TfgsvSoort _selectedSoort;
        private string _selectedFenotypeMaand;

        private readonly ObservableCollection<Beheersdaad> _beheersdaden;
        private readonly ObservableCollection<FenotypeMonth> _fenotypeMonths;
        
        private byte[] _bloeiImage = Array.Empty<byte>();
        private byte[] _habitusImage = Array.Empty<byte>();
        private byte[] _bladImage = Array.Empty<byte>();

        public ViewModelAdd(ISearchService searchService)
        {
            _searchService = searchService;
            _selectedTab = 0;

            _beheersdaden = new ObservableCollection<Beheersdaad>() { new() };
            _fenotypeMonths = new ObservableCollection<FenotypeMonth>() { new() };

            CmbTypes = searchService.GetList<TfgsvType>().OrderBy(t => t.Planttypenaam);
            CmbBladgrootte = searchService.GetList<FenoBladgrootte>().Select(f => f.Bladgrootte);
            CmbBladvorm = searchService.GetList<FenoBladvorm>().Select(f => f.Vorm);
            CmbBloeiwijze = searchService.GetList<FenoBloeiwijze>().Select(f => new StackLabelImage(f.Naam, false));
            CmbHabitus = searchService.GetList<FenoHabitu>().Select(f => new StackLabelImage(f.Naam, true));
            CmbMaand = Helper.GetMonthsList();
            CmbSpruitfenologie = searchService.GetList<FenoSpruitfenologie>().Select(f => f.Fenologie);
            MBladkleur = Helper.MakeColorMenuItemList().ToList();
            MBloeikleur = Helper.MakeColorMenuItemList().ToList();
            MBezonning = Helper.MakeMenuItemList<AbioBezonning>(a => a.Naam).ToList();
            MGrondsoort = Helper.MakeMenuItemList<AbioGrondsoort>(a => a.Grondsoort).ToList();
            CmbVochtbehoefte = searchService.GetList<AbioVochtbehoefte>().Select(v => v.Vochtbehoefte);
            MVoedingsbehoefte = Helper.MakeMenuItemList<AbioVoedingsbehoefte>(a => a.Voedingsbehoefte);
            MHabitat = Helper.MakeMenuItemList<AbioHabitat>(a => a.Afkorting).ToList();
            CmbOntwikkelingssnelheid = searchService.GetList<CommOntwikkelsnelheid>().Select(o => o.Snelheid);
            MStrategie = Helper.MakeMenuItemList<CommStrategie>(s => s.Strategie).ToList();
            MConcurrentiekracht = Helper.MakeMenuItemList<CommLevensvorm>(a => a.Levensvorm).ToList();
            CbPollen = searchService.GetList<ExtraPollenwaarde>().Select(o => o.Waarde);
            CbNectar = searchService.GetList<ExtraNectarwaarde>().Select(o => o.Waarde);

            BeheersdaadCommand = new Command(AddBeheersdaadItem);
            FenotypeMonthCommand = new Command(AddFenotypeMonth);
            AddPlantCommand = new Command<object>(AddPlant);
            FotoCommand = new Command<string>(AddFoto);
        }

        private bool IsRequiredFilled()
        {
            if (new List<string>() { SelectedType?.Planttypenaam, TextFamilie, TextGeslacht }.Any(s => string.IsNullOrEmpty(s)))
            {
                MessageBox.Show("Zorg dat je de verplichte velden ingevuld hebt!");
                _selectedTab = 0;
                OnPropertyChanged();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds a <see cref="Beheersdaad"/> to <see cref="_beheersdaden"/>.
        /// </summary>
        private void AddBeheersdaadItem()
        {
            _beheersdaden.Add(new Beheersdaad());
        }

        /// <summary>
        /// Adds a <see cref="FenotypeMonth"/> to <see cref="_fenotypeMonths"/>.
        /// </summary>
        private void AddFenotypeMonth()
        {
            
            _fenotypeMonths.Add(new FenotypeMonth());
        }

        private void AddFoto(string imageName)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Image|*.jpeg;*.png;*.jpg",
                InitialDirectory = SHGetKnownFolderPath(new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0),
            };
            
            if (!(ofd.ShowDialog() ?? false))
                return;

            var field = GetType().GetField(imageName, BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(this, File.ReadAllBytes(ofd.FileName));
            GetType().GetProperty(field.Name[1..].FirstToUpper())?.SetValue(this, ofd.FileName);
        }

        [DllImport("shell32", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        private static extern string SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, nint hToken = 0);

        /// <summary>
        /// Adds a new <see cref="Plant"/> and all its eigenschappen to the database.
        /// </summary>
        /// <param name="parameters">The values from the <see cref="CommonMultiValueConverter"/>.</param>
        /// <exception cref="ArgumentException">Throws exception if the required parameters are not entered.</exception>
        private void AddPlant(object parameters)
        {
            List<object> items = parameters as List<object>;

            // Checks if the required fields are filled in
            if (!IsRequiredFilled())
                return;

            TfgsvFamilie familie = DaoTfgsv.GetList<TfgsvFamilie>().FirstOrDefault(f => f.Familienaam == TextFamilie);
            if (familie == null)
                familie = DaoTfgsv.AddFamilie(SelectedType, TextFamilie);
            TfgsvGeslacht geslacht = DaoTfgsv.GetList<TfgsvGeslacht>().FirstOrDefault(g => g.Geslachtnaam == TextGeslacht);
            if (geslacht == null)
                geslacht = DaoTfgsv.AddGeslacht(familie, TextGeslacht);
            TfgsvSoort soort = DaoTfgsv.GetList<TfgsvSoort>().FirstOrDefault(s => s.Soortnaam == TextSoort);
            if (soort == null)
                soort = DaoTfgsv.AddSoort(geslacht, TextSoort);
            TfgsvVariant variant = DaoTfgsv.GetList<TfgsvVariant>().FirstOrDefault(v => v.Variantnaam == TextVariant);
            if (variant == null)
                variant = DaoTfgsv.AddVariant(soort, TextVariant);

            // Adds the base plant to the DB
            Plant plant = DaoPlant.AddPlant(SelectedType.Planttypenaam, TextFamilie, TextGeslacht, 
                string.IsNullOrEmpty(TextSoort) ? null : TextSoort,
                string.IsNullOrEmpty(TextVariant) ? null : TextVariant);

            if (_bloeiImage.Length > 0)
                DaoFoto.AddFoto(plant, "bloei", string.Empty, _bloeiImage);
            if (_bladImage.Length > 0)
                DaoFoto.AddFoto(plant, "blad", string.Empty, _bladImage);
            if (_habitusImage.Length > 0)
                DaoFoto.AddFoto(plant, "habitus", string.Empty, _habitusImage);

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
            if (MHabitat.Any(mi => mi.IsChecked))
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
            int socIndex = 65;
            foreach (bool? check in items.GetRange(2, 5))
            {
                if (check ?? false)
                    DaoCommensalisme.AddCommensalismeMulti(plant, "sociabiliteit", ((char)socIndex).ToString());
                socIndex++;
            }

            if (MConcurrentiekracht.Any(mi => mi.IsChecked))
                foreach (MenuItem item in MConcurrentiekracht)
                {
                    if (item.IsChecked)
                        DaoCommensalisme.AddCommensalismeMulti(plant, "concurrentiekracht", item.Header as string);
                }

            // checks if any param are filled, adds fenotype to DB
            if (new List<object>() { items[7], items[8], items[9], items[10], items[11], items[12] }.Any(s => !string.IsNullOrEmpty(s as string)))
            {
                DaoFenotype.AddFenotype(plant,
                    string.IsNullOrEmpty(items[7] as string) ? null : int.Parse(items[7] as string),
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
            if (MBladkleur.Any(mi => mi.IsChecked))
                foreach (MenuItem item in MBladkleur)
                {
                    if (item.IsChecked)
                        DaoFenotype.AddFenotypeMulti(plant, "bladkleur", item.InputGestureText as string);
                }
            if (MBloeikleur.Any(mi => mi.IsChecked))
                foreach (MenuItem item in MBloeikleur)
                {
                    if (item.IsChecked)
                        DaoFenotype.AddFenotypeMulti(plant, "bloeikleur", item.InputGestureText as string);
                }

            if (!string.IsNullOrEmpty(items[13] as string) || !string.IsNullOrEmpty(items[14] as string) || items.GetRange(15, 10).Any(b => b as bool? ?? false))
            {
                IList<bool?> booleans = items.GetRange(15, 10).Select(o => o as bool?).ToList();
                DaoExtraeigenschap.AddExtraEigenschap(plant,
                    string.IsNullOrEmpty(items[13] as string) ? null : items[13] as string,
                    string.IsNullOrEmpty(items[14] as string) ? null : items[14] as string,
                    Helper.RadioButtonToBool(booleans[0], booleans[1]),
                    Helper.RadioButtonToBool(booleans[2], booleans[3]),
                    Helper.RadioButtonToBool(booleans[4], booleans[5]),
                    Helper.RadioButtonToBool(booleans[6], booleans[7]),
                    Helper.RadioButtonToBool(booleans[8], booleans[9]));
            }

            Helper.SwitchTabAndReset("VIEWDETAIL", 
                () => new ViewModelPlantDetail(plant), 
                () => new ViewModelAdd(SimpleIoc.Default.GetInstance<ISearchService>()));
        }

        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (IsRequiredFilled())
                {
                    _selectedTab = value;
                
                }
            }
        }

        public Command<object> AddPlantCommand { get; set; }

        #region Algemene Info
        public IEnumerable<TfgsvType> CmbTypes { get; }

        public IEnumerable<TfgsvFamilie> CmbFamilies =>
            SelectedType == null ?
                Enumerable.Empty<TfgsvFamilie>() :
                _searchService.GetListWhere<TfgsvFamilie>(f => f.TypeTypeid == SelectedType.Planttypeid).OrderBy(f => f.Familienaam);

        public IEnumerable<TfgsvGeslacht> CmbGeslacht =>
            SelectedFamilie == null ?
                Enumerable.Empty<TfgsvGeslacht>() :
                _searchService.GetListWhere<TfgsvGeslacht>(g => g.FamilieFamileId == SelectedFamilie.FamileId).OrderBy(g => g.Geslachtnaam);

        public IEnumerable<TfgsvSoort> CmbSoort =>
            SelectedGeslacht == null ?
                Enumerable.Empty<TfgsvSoort>() :
                _searchService.GetListWhere<TfgsvSoort>(s => s.GeslachtGeslachtId == SelectedGeslacht.GeslachtId).OrderBy(s => s.Soortnaam);

        public IEnumerable<TfgsvVariant> CmbVariant =>
            SelectedSoort == null ?
                Enumerable.Empty<TfgsvVariant>() :
                _searchService.GetListWhere<TfgsvVariant>(v => v.SoortSoortid == SelectedSoort.Soortid).OrderBy(v => v.Variantnaam);

        public TfgsvType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged("CmbFamilies");
            }
        }
        public string TextFamilie { get; set; }
        public TfgsvFamilie SelectedFamilie
        {
            get => _selectedFamilie;
            set
            {
                _selectedFamilie = value;
                OnPropertyChanged("CmbGeslacht");
            }
        }
        public string TextGeslacht { get; set; }
        public TfgsvGeslacht SelectedGeslacht
        {
            get => _selectedGeslacht;
            set
            {
                _selectedGeslacht = value;
                OnPropertyChanged("CmbSoort");
            }
        }
        public string TextSoort { get; set; }
        public TfgsvSoort SelectedSoort
        {
            get => _selectedSoort;
            set
            {
                _selectedSoort = value;
                OnPropertyChanged("CmbVariant");
            }
        }
        public string TextVariant { get; set; }
        public TfgsvVariant SelectedVariant { get; set; }

        private string _bloeiImagePath;
        public string BloeiImage
        {
            get => _bloeiImagePath;
            set { _bloeiImagePath = value; OnPropertyChanged(); }
        }
        private string _habitusImagePath;
        public string HabitusImage
        {
            get => _habitusImagePath;
            set { _habitusImagePath = value; OnPropertyChanged(); }
        }
        private string _bladImagePath;
        public string BladImage
        {
            get => _bladImagePath;
            set { _bladImagePath = value; OnPropertyChanged(); }
        }

        public Command<string> FotoCommand { get; set; }
        #endregion

        #region Fenotype
        public IEnumerable<string> CmbBladgrootte { get; }

        public IEnumerable<string> CmbBladvorm { get; }

        public IEnumerable<StackPanel> CmbBloeiwijze { get; }

        public IEnumerable<StackPanel> CmbHabitus { get; }

        public IEnumerable<string> CmbMaand { get; }

        public IEnumerable<string> CmbSpruitfenologie { get; }

        public IEnumerable<MenuItem> MBladkleur { get; }

        public IEnumerable<MenuItem> MBloeikleur { get; }

        public string SelectedFenotypeMaand
        {
            get => _selectedFenotypeMaand;
            set
            {
                _selectedFenotypeMaand = value;
                OnPropertyChanged("CmbBladkleur");
                OnPropertyChanged("CmbBloeikleur");
                OnPropertyChanged("CmbBladhoogteMax");
                OnPropertyChanged("CmbBloeihoogteMin");
                OnPropertyChanged("CmbBloeihoogteMax");
            }
        }

        public ObservableCollection<FenotypeMonth> IctrlFenotypeMonth => _fenotypeMonths;

        public Command FenotypeMonthCommand { get; set; }
        #endregion

        #region Abiotische Factoren
        public IEnumerable<MenuItem> MBezonning { get; }

        public IEnumerable<MenuItem> MGrondsoort { get; }

        public IEnumerable<string> CmbVochtbehoefte { get; }

        public IEnumerable<MenuItem> MVoedingsbehoefte { get; }

        public IEnumerable<MenuItem> MHabitat { get; }

        #endregion

        #region Beheersdaden
        public ObservableCollection<Beheersdaad> IctrlBeheersdaad => _beheersdaden;
        public Command BeheersdaadCommand { get; set; }
        #endregion

        #region Commensalisme
        public IEnumerable<string> CmbOntwikkelingssnelheid { get; }

        public IEnumerable<MenuItem> MStrategie { get; }

        public IEnumerable<MenuItem> MConcurrentiekracht { get; }

        #endregion

        #region Extra
        // Written by Ian Dumalin on 18/03
        public IEnumerable<string> CbPollen { get; }

        public IEnumerable<string> CbNectar { get; }

        #endregion
    }
}
