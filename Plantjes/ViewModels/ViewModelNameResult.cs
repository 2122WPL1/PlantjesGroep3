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

            //ICommands
            ////These will be used to bind our buttons in the xaml and to give them functionality
            SearchCommand = new RelayCommand(ApplyFilterClick);
            ResetCommand = new RelayCommand(ResetClick);
        }

        //written by kenny (region)
        #region tussenFunctie voor knoppen met parameters
        public void ResetClick()
        {
            FilteredPlantResults = Enumerable.Empty<Plant>();


            SelectedNederlandseNaam = null;
        }

        public void ApplyFilterClick()
        {
            FilteredPlantResults = this._searchService.GetFilteredPlants(SelectedType, SelectedFamilie, SelectedGeslacht,
                SelectedSoort, SelectedVariant, SelectedNederlandseNaam, SelectedRatioBloeiBlad);

            OnPropertyChanged("FilteredPlantResults");
        }
        #endregion

        //written by Warre
        public IEnumerable<TfgsvType> CmbTypes
        {
            get => _searchService.GetList<TfgsvType>().OrderBy(t => t.Planttypenaam);
        }
        public IEnumerable<TfgsvFamilie> CmbFamilies
        {
            get => SelectedType == null ?
                Enumerable.Empty<TfgsvFamilie>() :
                _searchService.GetListWhere<TfgsvFamilie>(f => f.TypeTypeid == SelectedType.Planttypeid).OrderBy(f => f.Familienaam);
        }
        public IEnumerable<TfgsvGeslacht> CmbGeslacht
        {
            get => SelectedFamilie == null ?
                Enumerable.Empty<TfgsvGeslacht>() :
                _searchService.GetListWhere<TfgsvGeslacht>(g => g.FamilieFamileId == SelectedFamilie.FamileId).OrderBy(g => g.Geslachtnaam);
        }
        public IEnumerable<TfgsvSoort> CmbSoort
        {
            get => SelectedGeslacht == null ?
                Enumerable.Empty<TfgsvSoort>() :
                _searchService.GetListWhere<TfgsvSoort>(s => s.GeslachtGeslachtId == SelectedGeslacht.GeslachtId).OrderBy(s => s.Soortnaam);
        }
        public IEnumerable<TfgsvVariant> CmbVariant
        {
            get => SelectedSoort == null ?
                Enumerable.Empty<TfgsvVariant>() :
                _searchService.GetListWhere<TfgsvVariant>(v => v.SoortSoortid == SelectedSoort.Soortid).OrderBy(v => v.Variantnaam);
        }
        public IEnumerable<Fenotype> CmbRatioBladBloei
        {
            get => _searchService.GetList<Fenotype>().OrderBy(f => f.RatioBloeiBlad);
        }

        ////Bind to ListBoxes
        public IEnumerable<Plant> FilteredPlantResults { get; set; } = Enumerable.Empty<Plant>();

        public IEnumerable<string> DetailsSelectedPlant { get; set; } = Enumerable.Empty<string>();

        #region RelayCommands
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
            }
        }

        private string _selectedRatioBloeiBlad;

        public string SelectedRatioBloeiBlad
        {
            get { return _selectedRatioBloeiBlad; }
            set
            {
                _selectedRatioBloeiBlad = value;
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
                DetailsSelectedPlant = _searchService.GetDetailPlantResult(value);
                OnPropertyChanged("DetailsSelectedPlant");
            }
        }
        #endregion

        //geschreven door owen
        private void FillAllImages()
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
            





