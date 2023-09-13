using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Mammals collection.
/// </summary>
public class Mammals : IMammals
{
    #region IMammals Implementation

    /// <inheritdoc/>
    public List<IDog> Dogs { get; set; }

    public List<IAfricanElephant> AfricanElephants { get; set; }
    public List<IPolarBear> PolarBears { get; set; }
    public List<IGrizzlyBear> GrizzlyBears { get; set; }   
    #endregion // IMammals Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public Mammals()
    {
        Dogs = new List<IDog>();
        PolarBears = new List<IPolarBear>();
        AfricanElephants = new List<IAfricanElephant>();
        GrizzlyBears = new List<IGrizzlyBear>();
    }

    #endregion // Ctors
}
