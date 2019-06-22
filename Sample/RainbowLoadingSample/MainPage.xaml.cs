using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RainbowLoadingSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            AddRainbowLoadingVariations();
        }

        private void AddRainbowLoadingVariations()
        {
            var rl1 = new RainbowLoading.RainbowLoading();
            var l1 = new Label() { Text = "Defaults"};
            var sl1 = new StackLayout();
            sl1.Children.Add(rl1);
            sl1.Children.Add(l1);
            MainLayout.Children.Add(sl1);

            var rl2 = new RainbowLoading.RainbowLoading
            {
                ProgressDuration = TimeSpan.FromMilliseconds(4000)
            };
            var l2 = new Label() { Text = "Progress Duration = 4000 millis" };
            var sl2 = new StackLayout();
            sl2.Children.Add(rl2);
            sl2.Children.Add(l2);
            MainLayout.Children.Add(sl2);

            var rl3 = new RainbowLoading.RainbowLoading
            {
                RotationDuration = TimeSpan.FromMilliseconds(1000)
            };
            var l3 = new Label() { Text = "Rotation Duration = 1000 millis" };
            var sl3 = new StackLayout();
            sl3.Children.Add(rl3);
            sl3.Children.Add(l3);
            MainLayout.Children.Add(sl3);
        }
    }
}
