using UnityEngine;

public class WUBMobEntity : MonoBehaviour, IWUBMobEntity
{
    WUBMobSpawner OwningSpawner;

    /// <summary>
    /// what should happen once this object is placed back into the world?
    /// </summary>
    /// <param name="args">First param must be the owning Mob Spawner</param>
    virtual public void Init(params object[] args)
    {
        OwningSpawner = args[0] as WUBMobSpawner;

        //do any "returned to the world" init here
   }

    virtual public void OnKilled(params object[] args)
    {
        OwningSpawner.OnCreatureDied(transform);
    }
}
