using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class ScreenDefinition
    {
        public List<ScreenLineEntry> LineEntries = new List<ScreenLineEntry> 
        {
            new ScreenLineEntry(ConsoleColor.Green,
                ConsoleColor.White,
                "ScreenEnrty")
        };
    }
}
