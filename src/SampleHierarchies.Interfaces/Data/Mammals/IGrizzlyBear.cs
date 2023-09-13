using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals;

public interface IGrizzlyBear : IMammal
{
    int Hibernation { get; set; }
    string Diet { get; set; }
    float Size { get; set; }
    string ClawsUsage { get; set; }
    string SenseOfSmell { get; set; }
}
