using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Optimisation
{
    public class PoolSetup : MonoBehaviour
    {
        #region Singleton
        public static PoolSetup Instance { get; private set; }
        #endregion

        public PoolObjectsCollection poolObjectsCollection;

        private void Awake()
        {
            Instance = this;
            PoolManager.Init(poolObjectsCollection.PoolObjects);
        }
    }
}