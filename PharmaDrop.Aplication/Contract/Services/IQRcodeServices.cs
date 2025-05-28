using PharmaDrop.Application.DTOs;


namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public interface IQRcodeServices
    {
        byte[] GenerateQRcode(ProductDetailsQRcode details);
    }
}
