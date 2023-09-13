using System.Runtime.CompilerServices;

namespace SampleHierarchies.Interfaces.Data.Mammals;

public interface IAfricanElephant : IMammal
{
    #region Interface Members
    /// <summary>
    /// Characteristics of Elephant
    /// </summary>
    /// 
    float Height { get; set; }

    float Weight { get; set; }

    float TuskLength { get; set; }

    int Lifespan { get; set; }

    string SocialBehavior { get; set; }

    #endregion // Interface Members
}
