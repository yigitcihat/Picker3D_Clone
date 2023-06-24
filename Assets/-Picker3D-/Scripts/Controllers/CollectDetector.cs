﻿using System.Collections.Generic;
using _Picker3D_.Scripts.Data;
using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    public class CollectDetector : Singleton<CollectDetector>
    {
        #region Variables

        [SerializeField] private float pushForce;

        private List<Rigidbody> _myObjects;
        private List<Rigidbody> MyObjects => _myObjects ?? (_myObjects = new List<Rigidbody>());

        private MeshCollider _meshCollider;
        private MeshCollider MeshCollider => _meshCollider == null ? _meshCollider = GetComponent<MeshCollider>() : _meshCollider;

        #endregion

        #region MonoBehaviour Callbacks

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.GetComponent<Rigidbody>();
            var collectable = other.GetComponent<Collectable>();
            if (collectable != null && !MyObjects.Contains(obj))
                AddToList(obj);
        }

        private void OnTriggerExit(Collider other)
        {
            var obj = other.GetComponent<Rigidbody>();
            var collectable = other.GetComponent<Collectable>();
            if (collectable != null && MyObjects.Contains(obj))
                RemoveFromList(obj);
        }

        #endregion

        #region Other Methods

        // Public Methods

        public void PushAllObjects()
        {
            MeshCollider.enabled = false;
            foreach (var t in MyObjects)
            {
                t.gameObject.layer = 9;
                t.AddForce(Vector3.forward * pushForce);
            }
        }

        public void ClearList()
        {
            MyObjects.Clear();
            MeshCollider.enabled = true;
        }

        public bool CheckHaveEnoughObjects(int count)
        {
            return MyObjects.Count >= count;
        }

        // Private Methods

        private void AddToList(Rigidbody obj)
        {
            MyObjects.Add(obj);
        }

        private void RemoveFromList(Rigidbody obj)
        {
            MyObjects.Remove(obj);
        }

        #endregion
    }
}