using Classes;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IOTPService
    {
        double ExpiryInDays();
        Task<string> GenerateOTP();
    }

    public class OTPService: IOTPService
    {
        private readonly OTPSettings _settings;

        public OTPService(IOptions<OTPSettings> settings)
        {
            _settings = settings.Value;            
        }

        public double ExpiryInDays()
        {
            return Convert.ToDouble(_settings.ExpiryInDays);
        }

        public async Task<string> GenerateOTP()
        {
            int Length = _settings.Length;

            return await Task.Run(() =>
            {
                char[] OTPChars = new char[Length];

                Random rand = new Random();

                for (int i = 0; i < OTPChars.Length; i++)
                {
                    OTPChars[i] = _settings.CharPool[rand.Next(_settings.CharPool.Length)];
                }

                return new String(OTPChars);
            });
        }
    }
}
