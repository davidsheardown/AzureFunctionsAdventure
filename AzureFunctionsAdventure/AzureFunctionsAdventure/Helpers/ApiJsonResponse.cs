using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsAdventure.Helpers
{
    public class ApiJsonResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = null;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public Object Entity { get; set; }
    }
}
