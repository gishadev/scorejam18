using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Gisha.Optimisation
{
    public static class PoolManager
    {
        private static Dictionary<PoolObject, List<GameObject>> objectsByPoolObject;
        private static Dictionary<PoolObject, Transform> parentByPoolObject;

        private static List<PoolObject> _poolObjects;

        public static void Init(List<PoolObject> _poolObjects)
        {
            objectsByPoolObject = new Dictionary<PoolObject, List<GameObject>>();
            parentByPoolObject = new Dictionary<PoolObject, Transform>();

            PoolManager._poolObjects = _poolObjects;
            PoolManager.InitializePools(PoolManager._poolObjects);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            PoolObject po = GetOrCreatePoolObject(prefab);

            List<GameObject> sceneObjectsList;
            if (objectsByPoolObject.TryGetValue(po, out sceneObjectsList))
            {
                if (sceneObjectsList.Any(x => !x.activeInHierarchy))
                    return ActivateAvailableObject(position, rotation, sceneObjectsList);
            }

            else
            {
                objectsByPoolObject.Add(po, new List<GameObject>());
                CreateParent(po);
            }

            return InstantiateNewObject(prefab, position, rotation, po);
        }

        private static void InitializePools(List<PoolObject> _poolObjects)
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                PoolObject po = _poolObjects[i];

                objectsByPoolObject.Add(po, new List<GameObject>());
                CreateParent(po);

                for (int j = 0; j < po.OnInitCount; j++)
                    InstantiateNewObject(po.Prefab, Vector3.zero, Quaternion.identity, po).SetActive(false);
            }
        }

        #region Object Instantiating
        private static GameObject InstantiateNewObject(GameObject prefab, Vector3 position, Quaternion rotation, PoolObject po)
        {
            Transform parent = parentByPoolObject[po];

            GameObject createdObject = Object.Instantiate(prefab, position, rotation, parent);
            objectsByPoolObject[po].Add(createdObject);

            return createdObject;
        }

        private static GameObject ActivateAvailableObject(Vector3 position, Quaternion rotation, List<GameObject> sceneObjectsList)
        {
            GameObject objectToActivate;
            if (sceneObjectsList.Count > 1)
            {
                List<GameObject> unactiveObjects = sceneObjectsList.Where(x => !x.activeInHierarchy).ToList();
                objectToActivate = unactiveObjects.ElementAtOrDefault(new Random().Next() % unactiveObjects.Count());
            }
            else 
                objectToActivate = sceneObjectsList.FirstOrDefault(x => !x.activeInHierarchy);

            objectToActivate.transform.position = position;
            objectToActivate.transform.rotation = rotation;

            objectToActivate.SetActive(true);

            return objectToActivate;
        }
        #endregion

        private static PoolObject GetOrCreatePoolObject(GameObject prefab)
        {
            int prefabId = prefab.GetInstanceID();
            int index = _poolObjects.FindIndex(x => x.InstanceIds.Contains(prefabId));

            if (index == -1)
            {
                PoolObject newPO = new PoolObject(prefab);
                PoolSetup.Instance.poolObjectsCollection.Add(newPO);
                index = _poolObjects.Count - 1;

                Debug.LogFormat("New Pool Object was created with name {0}.", newPO.Name);
            }

            return _poolObjects[index];
        }

        private static void CreateParent(PoolObject poKey)
        {
            string name = string.Format("pool_{0}", poKey.Name);
            GameObject parent = new GameObject(name);
            parent.transform.SetParent(PoolSetup.Instance.transform);

            parentByPoolObject.Add(poKey, parent.transform);
        }

    }
}