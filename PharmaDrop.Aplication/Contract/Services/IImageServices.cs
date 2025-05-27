using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Contract.Services
{
    public interface IImageServices
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files , string src);
        void DeleteImageAsync(string src);
    }
}
