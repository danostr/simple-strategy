using UnityEngine;

public class RuntimeProvinceState
{
    // Read-only pointer back to the immutable configuration blueprint
    public ProvinceData SourceAsset { get; private set; }

    // Mutable proxy parameters that will fluctuate during the simulation game loop
    public int CurrentPopulation { get; set; }
    public int CurrentEconomy { get; set; }

    /// <summary>
    /// Constructor: Dependency injection payload cloning a static asset configuration template.
    /// </summary>
    public RuntimeProvinceState(ProvinceData source)
    {
        if (source == null)
        {
            Debug.LogError("[Runtime State] Cannot instantiate state wrapper with a null data source template!");
            return;
        }

        SourceAsset = source;
        
        // Clone mutable baseline values straight into live runtime memory tracking slots
        CurrentPopulation = source.population;
        CurrentEconomy = source.economy;
    }
}