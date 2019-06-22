using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace RainbowLoading
{
    public partial class RainbowLoading
    {

        public static readonly BindableProperty ProgressDurationProperty = BindableProperty.Create(nameof(ProgressDuration),
            typeof(TimeSpan),
            typeof(RainbowLoading),
            TimeSpan.FromMilliseconds(1400));

        public TimeSpan ProgressDuration
        {
            get => (TimeSpan)GetValue(ProgressDurationProperty);
            set => SetValue(ProgressDurationProperty, value);
        }

        public static readonly BindableProperty RotationDurationProperty = BindableProperty.Create(nameof(RotationDuration),
            typeof(TimeSpan),
            typeof(RainbowLoading),
            TimeSpan.FromMilliseconds(2000));

        public TimeSpan RotationDuration
        {
            get => (TimeSpan)GetValue(ProgressDurationProperty);
            set => SetValue(ProgressDurationProperty, value);
        }

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor),
            typeof(Color),
            typeof(RainbowLoading),
            Color.White);

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty ProgressColorsProperty = BindableProperty.Create(nameof(ProgressColors),
            typeof(List<Color>),
            typeof(RainbowLoading),
            new List<Color>
            {
                new Color(66/255.0f,133/255.0f,244/255.0f),
                new Color(219/255.0f,68/255.0f,55/255.0f),
                new Color(244/255.0f,160/255.0f,0/255.0f),
                new Color(15/255.0f,157/255.0f,88/255.0f)

            });

        public List<Color> ProgressColors
        {
            get => (List<Color>)GetValue(ProgressColorsProperty);
            set => SetValue(ProgressColorsProperty, value);
        }
    }
}

