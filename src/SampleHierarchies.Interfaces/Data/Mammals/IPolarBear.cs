using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals;

public interface IPolarBear : IMammal
{
    string FurCoat { get; set; }
    int SizePaws { get; set; }
    string CarnivorousDiet { get; set; }
    string SemiAquatic { get; set; }
    string SenseOfSmell { get; set; }
}
