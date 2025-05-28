using PharmaDrop.Application.DTOs;
using QRCoder;
using SkiaSharp;

namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public class QRcodeServices : IQRcodeServices
    {
        public byte[] GenerateQRcode(ProductDetailsQRcode details)
        {
            var Details = $"Product Name: {details.Name}\n" +
                          $"Product Price: {details.Price}\n" +
                          $"Product Manufacturer: {details.Manufacturer}\n" +
                          $"Product Expier: {details.ExpiryDate}";

            using (var generator = new QRCodeGenerator())
            using (var data = generator.CreateQrCode(Details, QRCodeGenerator.ECCLevel.Q))
            {
                var png = new PngByteQRCode(data);
                return png.GetGraphic(20);
            }
        }
        }
    }

