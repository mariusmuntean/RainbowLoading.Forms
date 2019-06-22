using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace RainbowLoading
{
    public partial class RainbowLoading : ContentView
    {
        private string ProgressAnimationName = "ProgressAnimation";
        private string RotationAnimationName = "RotationAnimation";

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

        protected override void OnParentSet()
        {
            base.OnParentSet();

            _currentColor = ProgressColors[_currentColorIndex].ToSKColor();
            progressPaint.Color = _currentColor;

            _canvas = new SKCanvasView();
            _canvas.PaintSurface += PaintRainbow;
            Content = _canvas;

            RunProgressAnimation();
            RunRotationAnimation();
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
                    _currentColorIndex = _currentColorIndex % ProgressColors.Count;
                    _currentColor = ProgressColors[_currentColorIndex].ToSKColor();
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

            // Clear the canvas and move the canvas center point to the viewport center
            canvas.Clear();
            canvas.Translate(canvasCenter);

            // Draw background as a white disc
            backgroundPaint.Color = BackgroundColor.ToSKColor();
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
