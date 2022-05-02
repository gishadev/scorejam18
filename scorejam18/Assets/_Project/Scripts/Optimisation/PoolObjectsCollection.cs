using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Optimisation
{
    [CreateAssetMenu(fileName = "PoolObjectsCollection", menuName = "Scriptable Objects/Game/Create Pool Objects Collection", order = 3)]
    public class PoolObjectsCollection : ScriptableObject
    {
        [SerializeField] private List<PoolObject> poolObjects = new List<PoolObject>();
        public List<PoolObject> PoolObjects => poolObjects; 

        public void Add(PoolObject item)
        {
            poolObjects.Add(item);
        }

        public void Reset()
        {
            poolObjects.Clear();
        }
    }
}