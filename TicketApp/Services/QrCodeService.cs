using QRCoder;
using Microsoft.Maui.Controls;

namespace TicketApp.Services
{
    public class QrCodeService
    {
        public ImageSource GenerateQrCode(string content, int size = 400)
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            try
            {
                var generator = new QRCodeGenerator();
                var data = generator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);

                // PNG-Bytearray erzeugen (keine Bitmaps mehr!)
                var pngQr = new PngByteQRCode(data);
                byte[] pngBytes = pngQr.GetGraphic(20);  // 20 = Pixel pro QR-Modul

                // Bytearray direkt in MAUI ImageSource umwandeln
                return ImageSource.FromStream(() => new MemoryStream(pngBytes));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"QR-Code Fehler: {ex.Message}");
                return null;
            }
        }
    }
}