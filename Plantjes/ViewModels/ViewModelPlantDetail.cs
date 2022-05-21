using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.ViewModels
{
    //Written by Renzo
    public class ViewModelPlantDetail : ViewModelBase
    {
        private List<Grid> _eigenschappen;

        public ViewModelPlantDetail(Plant plant)
        {
            ExportCommand = new RelayCommand(ExportCsv);

            Plant = plant;
            _eigenschappen = new List<Grid>();
            if (plant.Fenotypes.Count > 0 || plant.FenotypeMultis.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<Fenotype, FenotypeMulti>(plant.Fenotypes, plant.FenotypeMultis));
            if (plant.Abiotieks.Count > 0 || plant.AbiotiekMultis.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<Abiotiek, AbiotiekMulti>(plant.Abiotieks, plant.AbiotiekMultis));
            if (plant.Commensalismes.Count > 0 || plant.CommensalismeMultis.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<Commensalisme, CommensalismeMulti>(plant.Commensalismes, plant.CommensalismeMultis));
            if (plant.BeheerMaands.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<BeheerMaand, object>(plant.BeheerMaands));
            if (plant.ExtraEigenschaps.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<ExtraEigenschap, object>(plant.ExtraEigenschaps));
            if (plant.Fotos.Count > 0)
                _eigenschappen.Add(new PlantEigenschap<Foto, object>(plant.Fotos));
        }

        public void ExportCsv()
        {
            CsvHelper.ExportPlantDetailsToCsv(Plant);
        }
        public string PlantNaam { get => Plant.GetPlantName(); }

        public Plant Plant { get; private set; }
        
        public BitmapImage PlantImage { get => Plant.GetPlantImage(); }

        public List<Grid> Eigenschappen { get => _eigenschappen; }

        public RelayCommand ExportCommand { get; set; }
    }
}
