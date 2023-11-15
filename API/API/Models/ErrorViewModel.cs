using System;

namespace API.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
        public string Path { get; set; }
        public string StackTrace { get; set; }
    }
}
