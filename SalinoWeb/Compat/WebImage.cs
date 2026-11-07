using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace SalinoMvc5.Compat
{
    /// <summary>
    /// Replacement for System.Web.Helpers.WebImage (only the members the admin upload code uses).
    /// Uses System.Drawing, which is Windows-only on .NET 6+ (the site is hosted on Windows/IIS).
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class WebImage
    {
        /// <summary>Physical wwwroot path, set at startup.</summary>
        public static string WebRootPath { get; set; }

        private readonly byte[] _original;
        private Bitmap _resized;

        public WebImage(Stream content)
        {
            using var ms = new MemoryStream();
            content.CopyTo(ms);
            _original = ms.ToArray();
            using var probe = Image.FromStream(new MemoryStream(_original));
            Width = probe.Width;
            Height = probe.Height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>Same semantics as WebImage.Resize: fit inside width x height, keeping the aspect ratio.</summary>
        public WebImage Resize(int width, int height, bool preserveAspectRatio = true, bool preventEnlarge = false)
        {
            if (preserveAspectRatio)
            {
                double heightRatio = height * 100.0 / Height;
                double widthRatio = width * 100.0 / Width;
                if (heightRatio > widthRatio) height = (int)Math.Round(widthRatio * Height / 100);
                else if (heightRatio < widthRatio) width = (int)Math.Round(heightRatio * Width / 100);
            }
            if (preventEnlarge && (width > Width || height > Height)) return this;

            using var source = Image.FromStream(new MemoryStream(_original));
            var target = new Bitmap(Math.Max(1, width), Math.Max(1, height));
            using (var g = Graphics.FromImage(target))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(source, 0, 0, target.Width, target.Height);
            }
            _resized?.Dispose();
            _resized = target;
            Width = target.Width;
            Height = target.Height;
            return this;
        }

        /// <summary>Saves to a "~/..." path under wwwroot (or an absolute path).</summary>
        public WebImage Save(string filePath)
        {
            var physical = filePath.StartsWith("~") ? MapPath(filePath) : filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(physical)!);

            if (_resized == null)
            {
                File.WriteAllBytes(physical, _original);
            }
            else
            {
                _resized.Save(physical, FormatFromExtension(Path.GetExtension(physical)));
            }
            return this;
        }

        private static string MapPath(string virtualPath)
        {
            var root = Path.GetFullPath(WebRootPath ?? throw new InvalidOperationException("WebImage.WebRootPath is not set."));
            var full = Path.GetFullPath(Path.Combine(root, virtualPath.TrimStart('~').TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar)));
            if (!full.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Path escapes the web root: " + virtualPath);
            return full;
        }

        private static ImageFormat FormatFromExtension(string ext) => ext.ToLowerInvariant() switch
        {
            ".png" => ImageFormat.Png,
            ".gif" => ImageFormat.Gif,
            ".bmp" => ImageFormat.Bmp,
            ".tif" or ".tiff" => ImageFormat.Tiff,
            ".ico" => ImageFormat.Icon,
            _ => ImageFormat.Jpeg,
        };
    }
}
