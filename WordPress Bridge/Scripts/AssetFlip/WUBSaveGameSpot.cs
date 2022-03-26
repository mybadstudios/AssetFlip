using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MBS
{
    /// <summary>
    /// Place an object somewhere in your scene and add this to it to create a save game spot
    /// on your map. This will save the player's entire inventory to disk (or online if you have
    /// the Data extension installed) so it will save everything the player has done and collected
    /// up to the point where they trigger this. 
    /// 
    /// Triggering the same save game spot has no effect but returning to it again after reaching
    /// another save game spot will allow this spot to be triggered again.
    /// 
    /// Ideally, place all your save game spots in the scene in advance but dynamic spawning is supported
    /// 
    /// Once a SaveGameSpot is triggered an event is fired for you to trigger any custom code
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(Collider))]
    public class WUBSaveGameSpot : MBSEntity<WUBSaveGameSpot>
    {
        long MarkerID = 0;
        long LastMarkerReached = 0;

        /// <summary>
        /// This should only run if you spawned a SaveGameMarker after the scene has loaded
        /// </summary>
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            if (MarkerID == 0)
                SetupMarkerIds();
        }

        public void SetupMarkerIds() 
        {
            if (null == Entities) return;
            var LastID = LastMarkerReached = 0;

            //Set each SaveGameSpot's MarkerID to match it's index in the Entities List
            foreach (var entity in Entities)
            {
                if (entity is WUBSaveGameSpot sgs)
                    sgs.MarkerID = LastID++;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || MarkerID == LastMarkerReached) return;

            LastMarkerReached = MarkerID;
            WUBInventory.SetItemQuantity($"LastMarkerReached_{SceneManager.GetActiveScene().buildIndex}", LastMarkerReached);

#if WUDATA
            WUBInventory.SaveInventoryOnline();
#else
            WUBInventory.SaveInventory();
#endif
            this.TriggerEvent(EAFEvents.OnSaveGameMarkerReached, new MBSEvent((int)MarkerID));
        }
    }
}