﻿using System;
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

        //Written by Ian Dumalin on 28/04
        //edited by Warre on 04/05
        public IEnumerable<Plant> GetListPlants(string? naam, string? grondsoort, string? habitat, string? habitus, string? sociabiliteit, string? bezonning)
        {
            var plantList = DaoPlant.GetPlants();
            // if search values are null => return every plant in DB.
            if (naam == null && grondsoort == null && habitat == null && habitus == null && sociabiliteit == null && bezonning == null)
            {
                return plantList;
            }

            // Search plants on input values.
            if (!string.IsNullOrWhiteSpace(naam))
                plantList = plantList.Where(p =>
                (!string.IsNullOrEmpty(p.Variant) && p.Variant.ToLower().Contains(naam.ToLower())) ||
                (!string.IsNullOrEmpty(p.Soort) && p.Soort.ToLower().Contains(naam.ToLower())) ||
                (!string.IsNullOrEmpty(p.Geslacht) && p.Geslacht.ToLower().Contains(naam.ToLower())));
            if (!string.IsNullOrWhiteSpace(grondsoort))
                plantList = plantList.Where(p => p.Abiotieks.Any(a => !string.IsNullOrEmpty(a.Grondsoort) && a.Grondsoort.ToLower() == grondsoort.ToLower()));
            if (!string.IsNullOrWhiteSpace(habitus))
                plantList = plantList.Where(p => p.Fenotypes.Any(f => !string.IsNullOrEmpty(f.Habitus) && f.Habitus.ToLower() == habitus.ToLower()));
            if (!string.IsNullOrWhiteSpace(bezonning))
                plantList = plantList.Where(p => p.Abiotieks.Any(a => !string.IsNullOrEmpty(a.Bezonning) && a.Bezonning.ToLower() == bezonning.ToLower()));
            if (!string.IsNullOrWhiteSpace(habitat)) 
                plantList = plantList.Where(p => p.AbiotiekMultis.Any(a => a.Eigenschap.ToLower() == "habitat" && a.Waarde.ToLower() == habitat.ToLower()));
            if (!string.IsNullOrWhiteSpace(sociabiliteit)) 
                plantList = plantList.Where(p => p.FenotypeMultis.Any(f => f.Eigenschap.ToLower() == "sociabiliteit" && f.Waarde.ToLower() == sociabiliteit.ToLower()));

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