using SampleHierarchies.Interfaces.Data.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data.Mammals;

public class GrizzlyBear : MammalBase, IGrizzlyBear 
{
    public int Hibernation { get; set; }
    public string Diet { get; set; }
    public float Size { get; set; }
    public string ClawsUsage { get; set; }
    public string SenseOfSmell { get; set; }
    public override void Display()
    {
        Console.WriteLine("My name is: {0} and I am: {1} years old. My hibernation is {2} month, I eat {3}, my size is {4} meters, I use my claws for {5}, I have {6} sense of smell", Name, Age, Hibernation, Diet, Size, ClawsUsage, SenseOfSmell); 
    }
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="age"></param>
    /// <param name="hibernation"></param>
    /// <param name="diet"></param>
    /// <param name="size"></param>
    /// <param name="clawsUsage"></param>
    /// <param name="senseOfSmell"></param>
    public GrizzlyBear(string name, int age, int hibernation, string diet, float size, string clawsUsage, string senseOfSmell): base(name, age, MammalSpecies.GrizzlyBear) 
    {
        Hibernation = hibernation;
        Diet = diet;
        Size = size;
        ClawsUsage = clawsUsage;
        SenseOfSmell = senseOfSmell;
    }
}
