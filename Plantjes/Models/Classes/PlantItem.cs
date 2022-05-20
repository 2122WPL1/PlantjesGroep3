using GalaSoft.MvvmLight.Ioc;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using Plantjes.ViewModels;
using Plantjes.ViewModels.HelpClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Plantjes.Models.Classes
{
    public class PlantItem : Grid
    {
        private readonly Plant _plant;

        public PlantItem(bool isEmptyPlant = false)
        {
            Margin = new Thickness(0, 50, 0, 0);
            if (isEmptyPlant)
                Children.Add(new Label() { Content = "Geen planten gevonden!", HorizontalAlignment = HorizontalAlignment.Center, FontSize = 24, Foreground = Brushes.Gray });
        }

        public PlantItem(Plant plant)
        {
            _plant = plant;

            Children.Add(new Border() { BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4239")), BorderThickness = new Thickness(1) });
            Children.Add(new Rectangle() { Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9F9F9")), Margin = new Thickness(1) });
            DockPanel panel = new DockPanel();
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(10);
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9F9F9"));
            MouseDown += OnClick;

            Image image = new Image() { Source = plant.GetPlantImage(), Margin = new Thickness(1) };
            DockPanel.SetDock(image, Dock.Top);
            panel.Children.Add(image);

            Label nameLabel = new Label()
            {
                Content = plant.GetPlantName(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            DockPanel.SetDock(nameLabel, Dock.Bottom);
            panel.Children.Add(nameLabel);
            Children.Add(panel);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Helper.SwitchTab("VIEWDETAIL", () => new ViewModelPlantDetail(Plant));
        }

        public Plant Plant { get { return _plant; } }
    }
}
