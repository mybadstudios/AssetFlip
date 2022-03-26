using UnityEngine;

public class WUBMobEntityExample : WUBMobEntity, IWUBMobEntity
{
    override public void Init(params object[] args)
    {
        base.Init(args);

        //do any "returned to the terrain" init here
        //in this case we say that this creature should auto destroy
        //after a few seconds so we can test the mob spawner functionality
        //In normal cases you would delete this line and the Destroy function
        //and manually call OnKilled() when your creature dies
        Invoke("Destroy", Random.Range(3, 5f));
    }

    /// <summary>
    /// After a random number of seconds, call OnKilled
    /// </summary>
    void Destroy() => OnKilled();
}
