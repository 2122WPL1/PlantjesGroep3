using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Helpers;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Services
{

    /*written by kenny and robin from an example of Roy and some help of Killian*/
    public class SearchService : ISearchService
    {
        //written by Warre
        #region Get methods
        public IEnumerable<TEntity> GetList<TEntity>(bool distinct = false) where TEntity : class
        {
            return DaoBase.GetList<TEntity>(distinct);
        }

        public IEnumerable<TEntity> GetListWhere<TEntity>(Func<TEntity, bool> predicate, bool distinct = false) where TEntity : class
        {
            return DaoBase.GetListWhere<TEntity>(predicate, distinct);
        }
        #endregion

        #region Plant Search Help

        //Written by Ian Dumalin on 28/04
        public IEnumerable<Plant> GetListPlants(string? type, string? familie, string? geslacht, string? grondsoort,
            string? habitat, string? habitus, string? sociabiliteit, string? bezonning)
        {
            var plantList = DaoPlant.GetList<Plant>();
            // if search values are null => return every plant in DB.
            if (type == null && familie == null && geslacht == null && grondsoort == null && habitat == null && habitus == null &&
                sociabiliteit == null && bezonning == null)
            {
                return plantList;
            }
            // Search plants on input values.

            //List<Plant> searchedPlants =
            //    (List<Plant>)plantList.Where(p =>
            //        p.Type.Contains(type) &&
            //        p.Familie.Contains(familie) &&
            //        p.Geslacht.Contains(geslacht) &&
            //        p.Abiotieks.First().Grondsoort == grondsoort &&
            //        p.Fenotypes.First().Habitus == habitus &&
            //        p.Abiotieks.First().Bezonning == bezonning &&
            //        p.AbiotiekMultis.Any(a => a.Eigenschap == "habitat" && a.Waarde == habitat) &&
            //        p.FenotypeMultis.Any(f => f.Eigenschap == "sociabiliteit" && f.Waarde == sociabiliteit));

            if (type != null) plantList = plantList.Where(p => p.Type.Contains(type));
            if (familie != null) plantList = plantList.Where(p => p.Familie.Contains(type));
            if (geslacht != null) plantList = plantList.Where(p => p.Geslacht.Contains(geslacht));
            if (grondsoort != null) plantList = plantList.Where(p => p.Abiotieks.First().Grondsoort == grondsoort);
            if (habitus != null) plantList = plantList.Where(p => p.Fenotypes.First().Habitus == habitus);
            if (bezonning != null) plantList = plantList.Where(p => p.Abiotieks.First().Bezonning == bezonning);
            if (habitat != null) plantList = plantList.Where(p => p.AbiotiekMultis.Any(a => a.Eigenschap == "habitat" && a.Waarde == habitat));
            if (sociabiliteit != null) plantList = plantList.Where(p => p.FenotypeMultis.Any(f => f.Eigenschap == "sociabiliteit" && f.Waarde == sociabiliteit));

            return plantList;
        }

        #endregion

        #region OLD CODE
        /// <summary>
        /// Uses <see cref="GetListWhere{TEntity}(Func{TEntity, bool}, bool)"/> to filter to the filled in params.
        /// </summary>
        /// <param name="SelectedtType">The selected type.</param>
        /// <param name="SelectedFamilie">The selected familie.</param>
        /// <param name="SelectedGeslacht">The selected geslacht.</param>
        /// <param name="SelectedSoort">The selected soort.</param>
        /// <param name="SelectedVariant">The selected variant.</param>
        /// <param name="SelectedNederlandseNaam">The filled in naam.</param>
        /// <param name="SelectedRatioBloeiBlad">The filled in ratio.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of filtered plants.</returns>
        public IEnumerable<Plant> GetFilteredPlants(TfgsvType SelectedtType, TfgsvFamilie SelectedFamilie, TfgsvGeslacht SelectedGeslacht, TfgsvSoort SelectedSoort, TfgsvVariant SelectedVariant, string SelectedNederlandseNaam, string SelectedRatioBloeiBlad)
        {
            IEnumerable<Plant> plants = DaoBase.GetList<Plant>().ToList();

            if (SelectedtType != null)
                plants = plants.Where(p => p.TypeId == SelectedtType.Planttypeid);

            if (SelectedFamilie != null)
                plants = plants.Where(p => p.FamilieId == SelectedFamilie.FamileId);

            if (SelectedGeslacht != null)
                plants = plants.Where(p => p.GeslachtId == SelectedGeslacht.GeslachtId);

            if (SelectedSoort != null)
                plants = plants.Where(p => p.SoortId == SelectedSoort.Soortid);

            if (SelectedVariant != null)
                plants = plants.Where(p => p.VariantId == SelectedVariant.VariantId);

            if (SelectedNederlandseNaam != null)
                plants = plants.Where(p => p.NederlandsNaam.ToLower().Contains(SelectedNederlandseNaam.ToLower().Trim()));

            return plants;
        }
        #endregion

        #region Fill plant details in detain screen
        /// <summary>
        /// Plant detail listbox methods, geschreven door Robin, omgezet voor de service door kenny
        /// </summary>
        /// <param name="detailsSelectedPlant"></param>
        /// <param name="selectedPlant"></param>
        public IEnumerable<string> GetDetailPlantResult(Plant selectedPlant)
        {
            List<string> list = new List<string>();
            //Every property of the selected plant will be added to the OC
            //Now when I bind it to the list in the xaml, they will be displayed
            if (selectedPlant != null)
            {
                //Add every available plant property to the OC
                ////start with the properties consisting of a single value              
                FillSingleValuePlantDetails(list, selectedPlant);
                //Tables linked to Plant by PlantId
                ////Abiotiek
                FillDetailsPlantAbiotiek(list, selectedPlant);
                ////Abiotiek_Multi
                FillDetailsPlantAbiotiekMulti(list, selectedPlant);
                ////Beheermaand
                FillDetailsPlantBeheermaand(list, selectedPlant);
                ////Commensalisme
                FillDetailsPlantCommensalisme(list, selectedPlant);
                ////CommensalismeMulti
                FillDetailsPlantCommensalismeMulti(list, selectedPlant);
                ////ExtraEigenschap
                FillExtraEigenschap(list, selectedPlant);
                ////FenoType
                FillFenotype(list, selectedPlant);

                ////Foto
                ////UpdatePlant
            }
            return list;
        }
        public void FillSingleValuePlantDetails(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            //These are single value properties and can be added to the details screen immediatly
            detailsSelectedPlant.Add("Plant Id: " + SelectedPlantInResult.PlantId);
            detailsSelectedPlant.Add("Nederlandse naam: " + SelectedPlantInResult.NederlandsNaam);
            detailsSelectedPlant.Add("Wetenschappelijke naam: " + SelectedPlantInResult.Fgsv);
            detailsSelectedPlant.Add("Type: " + SelectedPlantInResult.Type);
            detailsSelectedPlant.Add("Familie: " + SelectedPlantInResult.Familie);
            detailsSelectedPlant.Add("Geslacht: " + SelectedPlantInResult.Geslacht);
            detailsSelectedPlant.Add("Soort: " + SelectedPlantInResult.Soort);
            detailsSelectedPlant.Add("Variant: " + SelectedPlantInResult.Variant);
            detailsSelectedPlant.Add("Minimale plantdichtheid: " + SelectedPlantInResult.PlantdichtheidMin);
            detailsSelectedPlant.Add("Maximale plantdichtheid: " + SelectedPlantInResult.PlantdichtheidMax);
            detailsSelectedPlant.Add("status: " + SelectedPlantInResult.Status);
        }

        public void FillDetailsPlantAbiotiek(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Abiotiek list, then we'll need to filter that list
            ////by checking if the Abiotiek.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Abiotiek types in the detailSelectedPlant Observable Collection
            var abioList = DaoBase.GetList<Abiotiek>();

            foreach (var itemAbio in abioList)
            {
                foreach (var plantItem in SelectedPlantInResult.Abiotieks)
                {
                    if (itemAbio.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Antagonische omgeving: " + itemAbio.AntagonischeOmgeving);
                        detailsSelectedPlant.Add("Bezonning: " + itemAbio.Bezonning);
                        detailsSelectedPlant.Add("Grondsoort: " + itemAbio.Grondsoort);
                        detailsSelectedPlant.Add("Vochtbehoefte: " + itemAbio.Vochtbehoefte);
                        detailsSelectedPlant.Add("Voedingsbehoefte: " + itemAbio.Voedingsbehoefte);
                    }
                }
            }
        }
        public void FillDetailsPlantAbiotiekMulti(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Abiotiek_Multi list, then we'll need to filter that list
            ////by checking if the Abiotiek_Multi.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Abiotiek_Multi types in the detailSelectedPlant Observable Collection
            var abioMultiList = DaoBase.GetList<AbiotiekMulti>();
            bool hasCheckedPlant;

            //bool gebruiken
            foreach (var itemAbioMulti in abioMultiList)
            {
                //A multi table contains the same PlantId multiple times because it can contain multiple properties
                //To refrain the app from showing duplicate data, I use a bool to limit the foreach to 1 run per plantId
                hasCheckedPlant = true;
                foreach (var plantItem in SelectedPlantInResult.AbiotiekMultis)
                {
                    if (hasCheckedPlant == true && itemAbioMulti.PlantId == plantItem.PlantId)
                    {
                        //EVENTUEEL 1 EIGENSCHAP-> VERSCHILLENDE WAARDES MEEGEVEN OP 1 LIJN OF ONDER ELKAAR
                        detailsSelectedPlant.Add("Abio Eigenschap: " + itemAbioMulti.Eigenschap);
                        detailsSelectedPlant.Add("Abio Waarde: " + itemAbioMulti.Waarde);
                    }
                    hasCheckedPlant = false;
                }
            }
        }

        //Table without data
        public void FillDetailsPlantBeheermaand(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an BeheerMaand list consisting of every possible property, then we'll need to filter that list
            ////by checking if the Beheermaand.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Beheermaand types in the detailSelectedPlant Observable Collection

            ////There is currently no data in this table, but the app is prepared for when it's added.
            var beheerMaandList = DaoBase.GetList<BeheerMaand>();

            foreach (var itemBeheerMaand in beheerMaandList)
            {
                foreach (var plantItem in SelectedPlantInResult.BeheerMaands)
                {
                    if (itemBeheerMaand.PlantId == plantItem.PlantId)
                    {
                        //EVENTUEEL 1 EIGENSCHAP-> VERSCHILLENDE WAARDES MEEGEVEN OP 1 LIJN OF ONDER ELKAAR
                        //BOOLS OP EEN ANDERE MANIER GEBRUIKEN?
                        detailsSelectedPlant.Add("Beheerdaad: " + itemBeheerMaand.Beheerdaad);
                        detailsSelectedPlant.Add("Januari: " + itemBeheerMaand.Jan);
                        detailsSelectedPlant.Add("Februari" + itemBeheerMaand.Feb);
                        detailsSelectedPlant.Add("Maart" + itemBeheerMaand.Mrt);
                        detailsSelectedPlant.Add("April" + itemBeheerMaand.Apr);
                        detailsSelectedPlant.Add("Mei" + itemBeheerMaand.Mei);
                        detailsSelectedPlant.Add("Juni" + itemBeheerMaand.Jun);
                        detailsSelectedPlant.Add("Juli" + itemBeheerMaand.Jul);
                        detailsSelectedPlant.Add("Augustus" + itemBeheerMaand.Aug);
                        detailsSelectedPlant.Add("September" + itemBeheerMaand.Sept);
                        detailsSelectedPlant.Add("Oktober" + itemBeheerMaand.Okt);
                        detailsSelectedPlant.Add("November" + itemBeheerMaand.Nov);
                        detailsSelectedPlant.Add("December" + itemBeheerMaand.Dec);
                    }
                }
            }
        }

        //Table without data
        public void FillDetailsPlantCommensalisme(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Commensalisme list consisting of every possible property, then we'll need to filter that list
            ////by checking if the Commensalisme.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Commensalisme types in the detailSelectedPlant Observable Collection

            ////There is currently no data in this table, but the app is prepared for when it's added.
            var commensalismeList = DaoBase.GetList<Commensalisme>();

            foreach (var itemCommensalisme in commensalismeList)
            {
                foreach (var plantItem in SelectedPlantInResult.Commensalismes)
                {
                    if (itemCommensalisme.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Ontwikkelsnelheid: " + itemCommensalisme.Ontwikkelsnelheid);
                        detailsSelectedPlant.Add("Strategie" + itemCommensalisme.Strategie);
                    }
                }
            }
        }

        public void FillDetailsPlantCommensalismeMulti(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an CommensalismeMulti list consisting of every possible property, then we'll need to filter that list
            ////by checking if the CommensalismeMulti.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining CommensalismeMulti types in the detailSelectedPlant Observable Collection

            ////There is currently no data in this table, but the app is prepared for when it's added.
            var commensalismeMultiList = DaoBase.GetList<CommensalismeMulti>();

            foreach (var itemCommensalismeMulti in commensalismeMultiList)
            {    //A multi table contains the same PlantId multiple times because it can contain multiple properties
                //To refrain the app from showing duplicate data, I use a bool to limit the foreach to 1 run per plantId
                foreach (var plantItem in SelectedPlantInResult.Commensalismes)
                {
                    if (itemCommensalismeMulti.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Commensalisme eigenschap: " + itemCommensalismeMulti.Eigenschap);
                        detailsSelectedPlant.Add("Commensalisme waarde: " + itemCommensalismeMulti.Waarde);
                    }
                }
            }
        }

        public void FillExtraEigenschap(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an ExtraEigenschap list, then we'll need to filter that list
            ////by checking if the ExtraEigenschap.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining ExtraEigenschap types in the detailSelectedPlant Observable Collection
            var extraEigenschapList = DaoBase.GetList<ExtraEigenschap>();

            foreach (var itemExtraEigenschap in extraEigenschapList)
            {
                foreach (var plantItem in SelectedPlantInResult.ExtraEigenschaps)
                {
                    if (itemExtraEigenschap.PlantId == plantItem.PlantId)
                    {
                        detailsSelectedPlant.Add("Nectarwaarde: " + itemExtraEigenschap.Nectarwaarde);
                        detailsSelectedPlant.Add("Pollenwaarde: " + itemExtraEigenschap.Pollenwaarde);

                        //Make sure the output is in dutch
                        if (itemExtraEigenschap.Bijvriendelijke == true)
                        {
                            detailsSelectedPlant.Add("Bijvriendelijk: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Bijvriendelijk: Nee");
                        }
                        if (itemExtraEigenschap.Eetbaar == true)
                        {
                            detailsSelectedPlant.Add("Eetbaar: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Eetbaar: Nee");
                        }
                        if (itemExtraEigenschap.Geurend == true)
                        {
                            detailsSelectedPlant.Add("Geurend: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Geurend: Nee");
                        }
                        if (itemExtraEigenschap.Kruidgebruik == true)
                        {
                            detailsSelectedPlant.Add("Kruidgebruik: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Kruidgebruik: Nee");
                        }
                        if (itemExtraEigenschap.Vlindervriendelijk == true)
                        {
                            detailsSelectedPlant.Add("Vlindervriendelijk: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Vlindervriendelijk: Nee");
                        }
                        if (itemExtraEigenschap.Vorstgevoelig == true)
                        {
                            detailsSelectedPlant.Add("Vorstgevoelig: Ja");
                        }
                        else
                        {
                            detailsSelectedPlant.Add("Vorstgevoelig: Nee");
                        }

                    }
                }
            }
        }

        public void FillFenotype(List<string> detailsSelectedPlant, Plant SelectedPlantInResult)
        {
            ////The following property consist of multiple values in a different table
            ////First we need an Fenotype list, then we'll need to filter that list
            ////by checking if the Fenotype.PlantId is the same als the SelectedPlantResult.PlantId.
            ////Once filtered: put the remaining Fenotype types in the detailSelectedPlant Observable Collection
            var fenoTypeList = DaoBase.GetList<Fenotype>();

            foreach (var itemFenotype in fenoTypeList)
            {
                foreach (var item in SelectedPlantInResult.Fenotypes)
                {
                    if (item.PlantId == itemFenotype.PlantId)
                    {
                        //FILTER DE DUBBELE PLANTEN UIT DE DATABASE

                        detailsSelectedPlant.Add("Bladgrootte: " + itemFenotype.Bladgrootte.ToString());
                        detailsSelectedPlant.Add("Bladvorm: " + itemFenotype.Bladvorm);
                        detailsSelectedPlant.Add("BloeiVorm: " + itemFenotype.Bloeiwijze);
                        detailsSelectedPlant.Add("Habitus: " + itemFenotype.Habitus);
                        detailsSelectedPlant.Add("Levensvorm: " + itemFenotype.Levensvorm);
                        detailsSelectedPlant.Add("Spruitfenologie: " + itemFenotype.Spruitfenologie);
                        detailsSelectedPlant.Add("Ratio blad/bloei: " + itemFenotype.RatioBloeiBlad);
                    }
                }
            }
        }
        #endregion

        //geschreven door owen
        //omgezet voor de service door kenny
        public ImageSource GetImageLocation(string ImageCatogrie, Plant SelectedPlantInResult)
        {
            // Request location of the image
            string location = "";
            if (SelectedPlantInResult != null)
            {
                location = DaoFoto.GetImageLocation(SelectedPlantInResult.PlantId, ImageCatogrie);
            }

            if (!string.IsNullOrEmpty(location))
            {
                    //Converting it to a bitmap image. This makes it possible to also have online image.
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(location, UriKind.Absolute);
                    bitmap.EndInit();

                    return bitmap;
            }

            return null;
        }

    }
}