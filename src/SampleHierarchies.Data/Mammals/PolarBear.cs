using SampleHierarchies.Interfaces.Data.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleHierarchies.Data.Mammals
{
    public class PolarBear : MammalBase, IPolarBear
    {
        public string FurCoat { get; set; }
        public int SizePaws { get; set; }
        public string CarnivorousDiet { get; set; }
        public string SemiAquatic { get; set; }
        public string SenseOfSmell { get; set; }
        public override void Display()
        {
            Console.WriteLine("My name is: {0} and I am: {1} years old. I have {2} fur, my paws is {3} cm, my diet is {4}, I'm {5} semi-aquatic and I have {6} sense of smell", Name, Age, FurCoat, SizePaws, CarnivorousDiet, SemiAquatic, SenseOfSmell);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="furCoat"></param>
        /// <param name="sizePaws"></param>
        /// <param name="carnivorousDiet"></param>
        /// <param name="semiAquatic"></param>
        /// <param name="senseOfSmell"></param>
        public PolarBear(string name, int age, string furCoat, int sizePaws, string carnivorousDiet, string semiAquatic, string senseOfSmell) : base(name, age, MammalSpecies.PolarBear) 
        {
            FurCoat = furCoat;
            SizePaws = sizePaws;
            CarnivorousDiet = carnivorousDiet;
            SemiAquatic = semiAquatic;
            SenseOfSmell = senseOfSmell;
        }        
        
    }
}
