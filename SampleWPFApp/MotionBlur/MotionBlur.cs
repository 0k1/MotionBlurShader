using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SampleWPFApp.MotionBlur
{
    internal static class Global
    {
        public static Uri MakePackUri(string relativeFile)
        {
            string uriString = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
            return new Uri(uriString);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    Assembly a = typeof(Global).Assembly;

                    // Pull out the short name.
                    _assemblyShortName = a.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }

        private static string _assemblyShortName;
    }
    public class MotionBlurEffect : ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader();

        static MotionBlurEffect()
        {
            _pixelShader.UriSource = Global.MakePackUri("MotionBlur.ps");
        }

        public MotionBlurEffect()
        {
            this.PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(BlurAngleProperty);
            UpdateShaderValue(BlurMagnitudeProperty);
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(MotionBlurEffect), 0);
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty BlurAngleProperty = DependencyProperty.Register("BlurAngle", typeof(double), typeof(MotionBlurEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));
        public double BlurAngle
        {
            get { return (double)GetValue(BlurAngleProperty); }
            set { SetValue(BlurAngleProperty, value); }
        }

        public static readonly DependencyProperty BlurMagnitudeProperty = DependencyProperty.Register("BlurMagnitude", typeof(double), typeof(MotionBlurEffect), new UIPropertyMetadata(0.001d, PixelShaderConstantCallback(1)));
        public double BlurMagnitude
        {
            get { return (double)GetValue(BlurMagnitudeProperty); }
            set { SetValue(BlurMagnitudeProperty, value); }
        }
    }

}
