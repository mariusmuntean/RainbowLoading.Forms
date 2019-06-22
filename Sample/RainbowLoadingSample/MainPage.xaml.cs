using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace RainbowLoadingSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private readonly Random rand = new Random();


        public MainPage()
        {
            InitializeComponent();

            AddRainbowLoadingVariations();
        }

        private void AddRainbowLoadingVariations()
        {
            var rl1 = new RainbowLoading.RainbowLoading();
            var l1 = new Label { Text = "Defaults" };
            var sl1 = new StackLayout();
            sl1.Children.Add(rl1);
            sl1.Children.Add(l1);
            MainLayout.Children.Add(sl1);

            var rl2 = new RainbowLoading.RainbowLoading
            {
                ProgressDuration = TimeSpan.FromMilliseconds(4000)
            };
            var l2 = new Label { Text = "Progress Duration = 4000 millis" };
            var sl2 = new StackLayout();
            sl2.Children.Add(rl2);
            sl2.Children.Add(l2);
            MainLayout.Children.Add(sl2);

            var rl3 = new RainbowLoading.RainbowLoading
            {
                RotationDuration = TimeSpan.FromMilliseconds(1000)
            };
            var l3 = new Label { Text = "Rotation Duration = 1000 millis" };
            var sl3 = new StackLayout();
            sl3.Children.Add(rl3);
            sl3.Children.Add(l3);
            MainLayout.Children.Add(sl3);


            var rl4 = new RainbowLoading.RainbowLoading
            {
                ProgressColors = GetListOfRandomPastelColors(28)
            };
            var l4 = new Label { Text = "Pastel Colours" };
            var sl4 = new StackLayout();
            sl4.Children.Add(rl4);
            sl4.Children.Add(l4);
            MainLayout.Children.Add(sl4);

            var rl5 = new RainbowLoading.RainbowLoading
            {
                BackgroundColor = GetRandomPastelColor().WithLuminosity(0.5)
            };
            var l5 = new Label { Text = "Custom Background Color" };
            var sl5 = new StackLayout();
            sl5.Children.Add(rl5);
            sl5.Children.Add(l5);
            MainLayout.Children.Add(sl5);
        }

        private List<Color> GetListOfRandomPastelColors(int count)
        {
            return Enumerable.Range(0, count).Select(i => GetRandomPastelColor()).ToList();
        }

        private Color GetRandomPastelColor()
        {
            // Thanks Jason: https://youtu.be/sxjOqNZFhKU?t=1104
            return new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()).WithLuminosity(0.8).WithSaturation(0.8);
        }
    }
}
