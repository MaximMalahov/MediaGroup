using System.IO;
using System.Windows.Media.Imaging;

namespace Малахов.Models
{
    internal class ImageManager
    {
        internal static byte[] CroppedToBytes(BitmapImage image)
        {
            var encode = new JpegBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(image));
            byte[] result;
            using (var ms = new MemoryStream())
            {
                encode.Save(ms);
                result = ms.ToArray();
            }
            return result;
        }

        internal static BitmapImage CroppedToBitmapImage(string path)
        {
            var imageToBytes = File.ReadAllBytes(path);
            var fs = new MemoryStream(imageToBytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = fs;
            image.DecodePixelWidth = 400;
            image.EndInit();
            return image;
        }
	}
}
