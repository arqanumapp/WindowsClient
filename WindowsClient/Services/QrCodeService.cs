using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace WindowsClient.Services
{
    public interface IQrCodeService
    {
        BitmapImage GenerateQrCode(string text);
    }
    public class QrCodeService : IQrCodeService
    {
        public BitmapImage GenerateQrCode(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.H);

            using var qrCode = new QRCode(qrData);
            using var logo = BitmapFromResource(); 


            using var qrBitmap = qrCode.GetGraphic(
                pixelsPerModule: 25,
                darkColor: Color.Black,     
                lightColor: Color.White,                    
                drawQuietZones: true,                       
                icon: logo,                                 
                iconSizePercent: 20,
                iconBorderWidth : 15,                           
                iconBackgroundColor: Color.White            
            );

            using var memory = new MemoryStream();
            qrBitmap.Save(memory, ImageFormat.Png);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private System.Drawing.Bitmap BitmapFromResource()
        {
            var uri = new Uri("pack://application:,,,/Resources/Img/Logo/logo.png", UriKind.Absolute);
            var bitmapImage = new BitmapImage(uri);

            using var memoryStream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(memoryStream);
            memoryStream.Position = 0;

            return new System.Drawing.Bitmap(memoryStream);
        }



    }
}
