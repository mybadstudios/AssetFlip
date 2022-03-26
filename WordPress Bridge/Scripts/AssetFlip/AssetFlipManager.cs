using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MBS
{
    public class AssetFlipManager : MonoBehaviour
    {
        void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene _, LoadSceneMode mode) 
        { 
            //Init SaveGameSpots
            var go = FindObjectOfType<WUBSaveGameSpot>();
            if (null != go) go.SetupMarkerIds();
        }
    }
}