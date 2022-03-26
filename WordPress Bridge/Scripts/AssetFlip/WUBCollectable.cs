using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBS
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class WUBCollectable : MonoBehaviour
    {
        [SerializeField] string InventoryName;
        [SerializeField] long Qty = 1;
        [SerializeField] bool DestroyOnTriggerEnter = true;
        [SerializeField] string[] MetaFields;
        [SerializeField] string[] MetaValues;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            CMLData data = null;
            if (MetaFields.Length > 0 && MetaValues.Length > 0)
            {
                int max = MetaFields.Length;
                if (MetaValues.Length < max) max = MetaValues.Length;
                if (max > 0)
                {
                    data = new CMLData();
                    for (int i = 0; i < max; i++)
                    {
                        if (MetaValues[i].Trim() == string.Empty || MetaFields[i].Trim().ToLower() == "id")
                            continue;
                        data.Set(MetaFields[i].Trim(), MetaValues[i].Trim());
                    }
                }
                if (data.defined.Count == 0) data = null;
            }
            WUBInventory.AddItems(InventoryName, Qty, data);
            if (DestroyOnTriggerEnter) Destroy(gameObject);
        }
    }
}