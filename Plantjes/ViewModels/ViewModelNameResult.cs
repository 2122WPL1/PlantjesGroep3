using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models;
using Plantjes.Models.Db;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.ViewModels
{
    public class ViewModelNameResult : ViewModelBase
    {
        //private ServiceProvider _serviceProvider;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();

        public ViewModelNameResult(ISearchService searchService)
        {

            this._searchService = searchService;
            //_searchService = new SearchService();

            //Observable Collections 
            ////Obserbable collections to fill with the necessary objects to show in the comboboxes
            //cmbTypes = new ObservableCollection<TfgsvType>();
            //cmbFamilies = new ObservableCollection<TfgsvFamilie>();
            //cmbGeslacht = new ObservableCollection<TfgsvGeslacht>();
            //cmbSoort = new ObservableCollection<TfgsvSoort>();
            //cmbVariant = new ObservableCollection<TfgsvVariant>();
            //cmbRatioBladBloei = new ObservableCollection<Fenotype>();

            ////Observable Collections to bind to listboxes
            //filteredPlantResults = new ObservableCollection<Plant>();
            //detailsSelectedPlant = new ObservableCollection<string>();

            //ICommands
            ////These will be used to bind our buttons in the xaml and to give them functionality
            SearchCommand = new RelayCommand(ApplyFilterClick);
            ResetCommand = new RelayCommand(ResetClick);

            //These comboboxes will already be filled with data on startup
            fillComboboxes();
        }

        //written by kenny (region)
        #region tussenFunctie voor knoppen met parameters

        public void fillComboboxes()
        {
            //_searchService.fillComboBoxType(cmbTypes);
            //_searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
            //_searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
            //_searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
            //_searchService.fillComboBoxVariant(cmbVariant);
            //_searchService.fillComboBoxRatioBloeiBlad(cmbRatioBladBloei);
        }

        public void ResetClick()
        {

            filteredPlantResults = Enumerable.Empty<Plant>();

            SelectedNederlandseNaam = null;
        }

        public void ApplyFilterClick()
        {
            filteredPlantResults = this._searchService.GetFilteredPlants(SelectedType, SelectedFamilie, SelectedGeslacht,
                SelectedSoort, SelectedVariant, SelectedNederlandseNaam, SelectedRatioBloeiBlad);
        }

        #endregion


        //Observable collections
        ////Bind to comboboxes
        public IEnumerable<TfgsvType> cmbTypes
        {
            get => _searchService.GetList<TfgsvType>().OrderBy(t => t.Planttypenaam);
        }
        public IEnumerable<TfgsvFamilie> cmbFamilies
        {
            get => SelectedType == null ?
                Enumerable.Empty<TfgsvFamilie>() :
                _searchService.GetListWhere<TfgsvFamilie>(f => f.TypeTypeid == SelectedType.Planttypeid).OrderBy(f => f.Familienaam);
        }
        public IEnumerable<TfgsvGeslacht> cmbGeslacht
        {
            get => SelectedFamilie == null ?
                Enumerable.Empty<TfgsvGeslacht>() :
                _searchService.GetListWhere<TfgsvGeslacht>(g => g.FamilieFamileId == SelectedFamilie.FamileId).OrderBy(g => g.Geslachtnaam);
        }
        public IEnumerable<TfgsvSoort> cmbSoort
        {
            get => SelectedGeslacht == null ?
                Enumerable.Empty<TfgsvSoort>() :
                _searchService.GetListWhere<TfgsvSoort>(s => s.GeslachtGeslachtId == SelectedGeslacht.GeslachtId).OrderBy(s => s.Soortnaam);
        }
        public IEnumerable<TfgsvVariant> cmbVariant
        {
            get => SelectedSoort == null ?
                Enumerable.Empty<TfgsvVariant>() :
                _searchService.GetListWhere<TfgsvVariant>(v => v.SoortSoortid == SelectedSoort.Soortid).OrderBy(v => v.Variantnaam);
        }
        public IEnumerable<Fenotype> cmbRatioBladBloei
        {
            get => _searchService.GetList<Fenotype>().OrderBy(f => f.RatioBloeiBlad);
        }

        ////Bind to ListBoxes
        public IEnumerable<Plant> filteredPlantResults { get; set; } = Enumerable.Empty<Plant>();

        public IEnumerable<string> detailsSelectedPlant { get; set; } = Enumerable.Empty<string>();
        ////
        ////

        #region RelayCommands

        //RelayCommands
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

        #endregion

        //geschreven door owen en robin
        #region Selected Item variables for each combobox

        private TfgsvType _selectedType;

        public TfgsvType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;

                //cmbFamilies.Clear();
                //cmbGeslacht.Clear();
                //cmbSoort.Clear();
                //cmbVariant.Clear();

                //_searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
                OnPropertyChanged("cmbFamilies");
            }
        }

        private TfgsvFamilie _selectedFamilie;

        public TfgsvFamilie SelectedFamilie
        {
            get { return _selectedFamilie; }
            set
            {
                _selectedFamilie = value;


                //cmbGeslacht.Clear();
                //cmbSoort.Clear();
                //cmbVariant.Clear();

                //_searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
                OnPropertyChanged("cmbGeslacht");
            }
        }

        private TfgsvGeslacht _selectedGeslacht;

        public TfgsvGeslacht SelectedGeslacht
        {
            get { return _selectedGeslacht; }
            set
            {
                _selectedGeslacht = value;



                //cmbSoort.Clear();
                //cmbVariant.Clear();

                //_searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
                OnPropertyChanged("cmbSoort");
            }
        }

        private TfgsvSoort _selectedSoort;

        public TfgsvSoort SelectedSoort
        {
            get { return _selectedSoort; }
            set
            {
                _selectedSoort = value;

                //cmbVariant.Clear();

                OnPropertyChanged("cmbVariant");
            }
        }

        private TfgsvVariant _selectedVariant;

        public TfgsvVariant SelectedVariant
        {
            get { return _selectedVariant; }
            set
            {
                _selectedVariant = value;
                OnPropertyChanged();
            }
        }

        private string _selectedRatioBloeiBlad;

        public string SelectedRatioBloeiBlad
        {
            get { return _selectedRatioBloeiBlad; }
            set
            {
                _selectedRatioBloeiBlad = value;
                OnPropertyChanged();
            }
        }

        private string _selectedNederlandseNaam;

        public string SelectedNederlandseNaam
        {
            get { return _selectedNederlandseNaam; }
            set
            {
                if (SelectedNederlandseNaam == "")
                {
                    _selectedNederlandseNaam = null;
                }
                else
                {
                    _selectedNederlandseNaam = value;
                }

                OnPropertyChanged();
            }
        }

        //This will update the selected plant in the result listbox
        //This will be used to show the selected plant details
        private Plant _selectedPlantInResult;

        public Plant SelectedPlantInResult
        {
            get { return _selectedPlantInResult; }
            set
            {
                _selectedPlantInResult = value;
                FillAllImages();
                OnPropertyChanged();
                //_searchService.FillDetailPlantResult(detailsSelectedPlant, SelectedPlantInResult);
               
                //Make the currently selected plant in the Result list available in the SearchService
             
            }
        }


        #endregion

        //geschreven door owen
        public void FillAllImages()
        {
            ImageBlad = _searchService.GetImageLocation("blad",SelectedPlantInResult);
            ImageBloei = _searchService.GetImageLocation("bloei", SelectedPlantInResult);
            ImageHabitus = _searchService.GetImageLocation("habitus",SelectedPlantInResult);
        }

      
        //geschreven door owen
        #region binding images

        private ImageSource _imageBloei;

        public ImageSource ImageBloei
        {
            get { return _imageBloei; }
            set
            {
                _imageBloei = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageHabitus;

        public ImageSource ImageHabitus
        {
            get { return _imageHabitus; }
            set
            {
                _imageHabitus = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageBlad;

        public ImageSource ImageBlad
        {
            get { return _imageBlad; }
            set
            {
                _imageBlad = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
            





