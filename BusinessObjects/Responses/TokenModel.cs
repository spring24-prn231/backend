﻿namespace BusinessObjects.Responses
{
    public class TokenModel
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
