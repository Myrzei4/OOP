using SampleHierarchies.Interfaces.Data.Mammals;
using System.Xml.Linq;

namespace SampleHierarchies.Data.Mammals;

  public class AfricanElephant  : MammalBase, IAfricanElephant
{ 
    
        
       #region Inteface implimentetion
      public float Height { get; set; }

      public float Weight { get; set; }

      public float TuskLength { get; set; }

      public int Lifespan { get; set; }

      public string SocialBehavior { get; set; }

    #endregion Inteface implimentetion
    public override void Display()
    {
        Console.WriteLine("My name is: {0} and I am: {1} years old. My height is {2} meters, my weight is {3} kilograms, my tusk is {4} meters long, I can live {5} years, in social I'm {6}", Name, Age, Height, Weight,TuskLength, Lifespan, SocialBehavior);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="age"></param>
    /// <param name="height"></param>
    /// <param name="weight"></param>
    /// <param name="tuskLenght"></param>
    /// <param name="lifespan"></param>
    /// <param name="socialBehavior"></param>
    public AfricanElephant(string name, int age ,float height, float weight, float tuskLenght, int lifespan, string socialBehavior) : base(name, age, MammalSpecies.AfricanElephant) 
    {
        Height = height;
        Weight = weight;
        TuskLength = tuskLenght;
        Lifespan = lifespan;
        SocialBehavior = socialBehavior;
    }
  
    }

