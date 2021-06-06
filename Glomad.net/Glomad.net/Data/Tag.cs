using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.net.Data
{
    public class Tag
    {
        public string Title { get; set; }
        public string Style { get; init; }

        public static Tag Covid = new("Covid", "danger");
        public static Tag PCR = new("PCR", "warning");
        public static Tag Slow = new("Slow", "warning");
        public static Tag SimpleVisa = new("Simple visa", "success");
        public static Tag FullYear = new("Full Year", "success");
        public static Tag Evisa = new("E-visa", null);
        public static Tag days180 = new("180 days", "success");
        public static Tag days90 = new("90 days", "success");
        public Tag(string title, string style)
        {
            Title = title;
            Style = string.IsNullOrEmpty(style) ? "primary" : style;
        }
    }
}
