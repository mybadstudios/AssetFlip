namespace MBS
{
    /// <summary>
    /// Events can be of any type from Object to string to numeric to ... anything. 
    /// I prefer to use enums and such I create this enum to identify and list all events in this asset
    /// </summary>
    enum EAFEvents : byte { 
        OnSaveGameMarkerReached 
    }
}
