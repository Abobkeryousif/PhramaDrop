﻿namespace PharmaDrop.API.Middleware
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
    }
}
