using System;

namespace Classes
{
    public class OTPSettings
    {
        private readonly string _defaultPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        private string _charPool;

        public int Length { get; set; } = 6;
        public double ExpiryInDays { get; set; } = 1;
        
        public string CharPool 
        {
            get { return _charPool; } 
           
            set 
            {
                if (String.IsNullOrEmpty(value))
                    _charPool = _defaultPool;
                else
                    _charPool = value;
            } 
        }
    }
}