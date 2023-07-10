using SampleHierarchies.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class Settings : ISettings
    {
        #region ISettings Implementation
        public string BackgroundColor { get; set; } = "black"; //BG color
        public string ForegroundColor { get; set; } = "white"; //FG color
        #endregion //ISetings Implementation
    }
}
