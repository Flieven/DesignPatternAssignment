using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class ObjectPool : IPool<GameObject>
    {
        private readonly GameObject poolPrefab;
        private readonly uint poolExpansion;
        private readonly Transform poolParent;

        readonly Stack<GameObject> objStack = new Stack<GameObject>();

        public ObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
            poolExpansion = (uint)Mathf.Max(1, expandBy);
            poolPrefab = prefab;
            poolParent = parent;
            poolPrefab.SetActive(false);
        }

        private void Expand(uint amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject tempObj = Object.Instantiate(poolPrefab, poolParent);
                EmitOnDisable emitOnDisable = tempObj.AddComponent<EmitOnDisable>();
                emitOnDisable.OnDisableGameObject += UnRent;
                objStack.Push(tempObj);
            }
        }

        public void UnRent(GameObject obj)
        {
            objStack.Push(obj);
        }

        public GameObject Rent(bool returnActive)
        {
            if(objStack.Count == 0) { Expand(poolExpansion); }
            GameObject tempObj = objStack.Pop();
            tempObj.SetActive(returnActive);
            return tempObj;
        }

        public int stackSize {  get { return objStack.Count; } }

    }
}