﻿namespace AccountingPolessUp.Models
{
    public static class TokenManager
    {
        private static string _accessToken;

        public static string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }
    }
}
