using UnityEngine;
using System;

namespace Gisha.Effects.VFX
{
    public class VFXManager : ImportTarget
    {
        #region Singleton
        public static VFXManager Instance { private set; get; }
        #endregion

        public EffectData[] effectsCollection;

        void Awake()
        {
            Instance = this;
        }

        #region Emit Functions
        public void Emit(string effectName, Vector3 position, Quaternion rotation)
        {
            EffectData effect = Array.Find(effectsCollection, x => x.name == effectName);

            if (effect == null)
            {
                Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
                return;
            }

            Instantiate(effect.prefab, position, rotation);
        }

        public void Emit(string effectName, Vector3 position)
        {
            EffectData effect = Array.Find(effectsCollection, x => x.name == effectName);
            Quaternion rotation = effect.prefab.transform.rotation;

            if (effect == null)
            {
                Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
                return;
            }

            Instantiate(effect.prefab, position, rotation);
        }

        public void PoolEmit(string effectName, Vector3 position, Quaternion rotation)
        {
            EffectData effect = Array.Find(effectsCollection, x => x.name == effectName);

            if (effect == null)
            {
                Debug.LogErrorFormat("Effect with name {0} wasn't found!", effectName);
                return;
            }

            Optimisation.PoolManager.Instantiate(effect.prefab, position, rotation);
        }
        #endregion

        #region ImportTarget
        public override void Import(string _collection, ResourceData[] _resources)
        {
            EffectData[] coll = new EffectData[_resources.Length];

            for (int i = 0; i < _resources.Length; i++)
            {
                EffectData data = new EffectData();
                data.name = _resources[i].name;
                data.prefab = _resources[i].o as GameObject;

                coll[i] = data;
            }

            GetType().GetField(_collection).SetValue(this, coll);
        }
        #endregion
    }

    [Serializable]
    public class EffectData
    {
        public string name;
        public GameObject prefab;
    }
}