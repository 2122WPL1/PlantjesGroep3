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
        void fillComboBoxType(ObservableCollection<TfgsvType> cmbTypeCollection);
        void fillComboBoxFamilie(TfgsvType selectedType, ObservableCollection<TfgsvFamilie> cmbFamilieCollection);
        void fillComboBoxGeslacht(TfgsvFamilie selectedFamilie, ObservableCollection<TfgsvGeslacht> cmbGeslachtCollection);
        void fillComboBoxSoort(TfgsvGeslacht selectedGeslacht, ObservableCollection<TfgsvSoort> cmbSoortCollection);
        void fillComboBoxVariant(ObservableCollection<TfgsvVariant> cmbVariantCollection);
        void fillComboBoxRatioBloeiBlad(ObservableCollection<Fenotype> cmbRatioBladBloeiCollection);

        void FillDetailPlantResult(ObservableCollection<string> detailsSelectedPlant, Plant SelectedPlantInResult);

        ImageSource GetImageLocation(string ImageCatogrie, Plant SelectedPlantInResult);


        List<Plant> ApplyFilter(TfgsvType selectedType, TfgsvFamilie selectedFamilie, TfgsvGeslacht selectedGeslacht,
            TfgsvSoort selectedSoort, TfgsvVariant selectedVariant, string selectedNederlandseNaam, string selectedRatioBloeiBlad);

        //void Reset(ObservableCollection<Plant> filteredPlantResults, ObservableCollection<TfgsvType> cmbTypes,
        //    ObservableCollection<TfgsvFamilie> cmbFamilies, ObservableCollection<TfgsvGeslacht> cmbGeslacht,
        //    ObservableCollection<TfgsvSoort> cmbSoort, ObservableCollection<TfgsvVariant> cmbVariant,
        //    ObservableCollection<Fenotype> cmbRatioBladBloei, string selectedNederlandseNaam, TfgsvType selectedType,
        //    TfgsvFamilie selectedFamilie, TfgsvGeslacht selectedGeslacht
        //);


    }
}
