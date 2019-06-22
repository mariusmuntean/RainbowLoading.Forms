using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace RainbowLoading
{
    public class RainbowLoading : ContentView
    {
        private string ProgressAnimationName = "ProgressAnimation";
        private string RotationAnimationName = "RotationAnimation";

        private readonly List<SKColor> progressColors = new List<SKColor>
        {
            new SKColor(66,133,244),
            new SKColor(219,68,55),
            new SKColor(244,160,0),
            new SKColor(15,157,88)

        };

        SKPaint backgroundPaint = new SKPaint()
        {
            Color = SKColors.White,
            IsStroke = false,
            IsAntialias = true
        };

        SKPaint progressPaint = new SKPaint()
        {
            Color = SKColors.White,
            IsStroke = true,
            StrokeWidth = 10,
            IsAntialias = true
        };

        private SKColor _currentColor;
        private double _progress = 0.0;
        private double _rotation = 0.0;
        private int _currentColorIndex = 0;
        private float _progressArcDiameterProportion = 0.65f;

        SKCanvasView _canvas;

        public RainbowLoading()
        {
            _currentColor = progressColors[_currentColorIndex];
            progressPaint.Color = _currentColor;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            _canvas = new SKCanvasView();
            _canvas.PaintSurface += PaintRainbow;
            Content = _canvas;

            RunProgressAnimation();
            RunRotationAnimation();
        }

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

        private void RunRotationAnimation()
        {
            var rotationAnimation = new Animation(interpolatedValue =>
            {
                _rotation = interpolatedValue;
                _canvas.InvalidateSurface();
            });

            rotationAnimation.Commit(this, RotationAnimationName,
                length: (uint)RotationDuration.TotalMilliseconds,
                easing: Easing.Linear,
                repeat: () => true);
        }

        private void RunProgressAnimation()
        {
            var progressAnimation = new Animation(interpolated =>
            {
                _progress = interpolated;
                _canvas.InvalidateSurface();
            });

            progressAnimation.Commit(this, ProgressAnimationName,
                length: (uint)ProgressDuration.TotalMilliseconds,
                easing: Easing.CubicInOut,
                finished: (d, b) =>
                {
                    _currentColorIndex++;
                    _currentColorIndex = _currentColorIndex % progressColors.Count;
                    _currentColor = progressColors[_currentColorIndex];
                    _canvas.InvalidateSurface();
                },
                repeat: () => true);
        }

        private void PaintRainbow(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var size = info.Size;
            var surface = args.Surface;
            var canvas = args.Surface.Canvas;

            var canvasCenter = new SKPoint(size.Width / 2, size.Height / 2);

            canvas.Clear();
            canvas.Translate(canvasCenter);

            // Draw background as a white disc
            canvas.DrawCircle(0, 0, size.Width / 2, backgroundPaint);

            // Rotate the canvas
            canvas.RotateDegrees((float)(_rotation * 360.0f));

            // Draw the progress arc
            var progressArcBoundingRect = new SKRect(
                -size.Width * _progressArcDiameterProportion * 0.5f,
                -size.Height * _progressArcDiameterProportion * 0.5f,
                size.Width * _progressArcDiameterProportion * 0.5f,
                size.Height * _progressArcDiameterProportion * 0.5f);
            progressPaint.StrokeWidth = size.Width * 0.08f;
            progressPaint.Color = _currentColor;
            using (var arcPath = new SKPath())
            {
                float startAngle = 0.0f;
                float sweepAngle = 0.0f;
                if (_progress <= 0.5f)
                {
                    sweepAngle = (float)(2 * _progress * 360.0f);
                    startAngle = -90.0f;
                }

                if (_progress > 0.5d)
                {
                    startAngle = ((float)_progress - 0.5f) // shift the range to (0, 0.5]
                        * 2 // map the range to (0, 1.0]
                        * 360.0f // map the range to (0, 360]
                        - 90.0f; // shift the range to (-90.0, 270]
                    sweepAngle = 270.0f - startAngle;
                }

                arcPath.AddArc(progressArcBoundingRect, startAngle, sweepAngle);
                canvas.DrawPath(arcPath, progressPaint);
            }
        }
    }
}
