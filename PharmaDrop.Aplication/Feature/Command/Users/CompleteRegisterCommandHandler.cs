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
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;
        public CompleteRegisterCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
        }

        public async Task<HttpResponse<string>> Handle(CompleteRegisterCommand request, CancellationToken cancellationToken)
        {
            var otp = await _otpRepository.FirstOrDefaultAsync(o=> o.otp == request.userOtp.otp);
            if (otp == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,"Plaese Enter True OTP");

            if (otp.IsExpier)
                return new HttpResponse<string>(HttpStatusCode.BadRequest, "Expier OTP");

            if (otp.IsUsed)
                return new HttpResponse<string>(HttpStatusCode.BadRequest, "OTP Already Used");

            var user = await _userRepository.FirstOrDefaultAsync(u=> u.Email == otp.UserEmail);
            user.IsActive = true;
            

            await _userRepository.UpdateAsync(user);
            await _otpRepository.DeleteAsync(otp);

            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss Complete Register Plaese Go To Login !",$"Mr: {user.UserName}");
        }
    }
}




