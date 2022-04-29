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
        public IEnumerable<Plant> GetListPlants(string? type, string? familie, string? geslacht, string? grondsoort, string? habitat, string? habitus, string? sociabiliteit, string? bezonning)
        {
            var plantList = GetList<Plant>();
            // if search values are null => return every plant in DB.
            if (type == null && familie == null && geslacht == null && grondsoort == null && habitat == null && habitus == null &&
                sociabiliteit == null && bezonning == null)
            {
                return plantList;
            }

            // Search plants on input values.
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