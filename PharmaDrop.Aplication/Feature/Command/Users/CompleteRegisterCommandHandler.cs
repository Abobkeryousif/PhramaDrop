using MediatR;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Feature.Command.Users
{
    public record CompleteRegisterCommand(UserOtpDto userOtp) : IRequest<HttpResponse<string>>;
    public class CompleteRegisterCommandHandler : IRequestHandler<CompleteRegisterCommand, HttpResponse<string>>
    {
        private readonly IUnitofWork _unitofWork;
        public CompleteRegisterCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<HttpResponse<string>> Handle(CompleteRegisterCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitofWork.otpRepository.FirstOrDefaultAsync(o=> o.otp == request.userOtp.otp);
            if (otp == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,"Plaese Enter True OTP");

            if (otp.IsExpier)
                return new HttpResponse<string>(HttpStatusCode.BadRequest, "Expier OTP");

            if (otp.IsUsed)
                return new HttpResponse<string>(HttpStatusCode.BadRequest, "OTP Already Used");

            var user = await _unitofWork.UserRepository.FirstOrDefaultAsync(u=> u.Email == otp.UserEmail);
            user.IsActive = true;
            

            await _unitofWork.UserRepository.UpdateAsync(user);
            await _unitofWork.otpRepository.DeleteAsync(otp);

            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss Complete Register Plaese Go To Login !",$"Mr: {user.UserName}");
        }
    }
}




