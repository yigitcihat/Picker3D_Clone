using System.Collections;
using System.Collections.Generic;
using _Picker3D_.Scripts.Controllers;
using _Picker3D_.Scripts.Data;
using _Picker3D_.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    public class Container : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform containerPart;
        [SerializeField] private Transform[] barrierParts;
        
        [SerializeField] private GameObject barrierFx;
        [SerializeField]private PartController _partController;
        // [SerializeField] private FXData _objectsParticleData;
        [SerializeField] internal int requireObjectCount;

        private List<Collectable> _objectsInContainer;
        private List<Collectable> ObjectsInContainer =>
            _objectsInContainer ?? (_objectsInContainer = new List<Collectable>());

        private TextMeshPro _requireObjectCountText;

        private TextMeshPro RequireObjectCountText => _requireObjectCountText == null
            ? _requireObjectCountText = GetComponentInChildren<TextMeshPro>()
            : _requireObjectCountText;

        private Material _containerPartMaterial;
        
        private Material PlatformPartMaterial => _containerPartMaterial == null
            ? _containerPartMaterial = containerPart.GetComponent<MeshRenderer>().material
            : _containerPartMaterial;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            containerPart.DOLocalMoveY(-1.5f, 0);
        }

        private void Start()
        {
            UpdateText();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collectable = collision.gameObject.GetComponent<Collectable>();

            if (collectable != null && !ObjectsInContainer.Contains(collectable) && GameManager.Instance.isGameStarted)
                AddToStorage(collectable);
        }

        #endregion

        #region Other Methods

        // Public Methods

        public int GetRequireObjectCount()
        {
            return requireObjectCount;
        }

        // Private Methods

        private void AddToStorage(Collectable target)
        {
            ObjectsInContainer.Add(target);
            UpdateText();
            StartCoroutine(ExplodeObjectsWithDelay());
        }

        private void UpdateText()
        {
            RequireObjectCountText.text = ObjectsInContainer.Count + "/" + requireObjectCount;
        }

        private IEnumerator ExplodeObjectsWithDelay()
        {
            if (ObjectsInContainer.Count != requireObjectCount) yield break;
            PlayerController.Instance.canMove = true;
            for (var index = 0; index < ObjectsInContainer.Count; index++)
            {
                var t = ObjectsInContainer[index];
                yield return new WaitForSeconds(.05f);
                var currentObject = t.gameObject;
                // _objectsParticleData.myFxPool.GetObjFromPool(currentObject.transform.position);
                Destroy(currentObject);
            }

            containerPart.DOLocalMoveY(1.77f, 1.5f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                OpenBarrier();
                var color = _partController.partGround.material.color;
                PlatformPartMaterial.DOColor(color, 1f);
            });
        }

        private void OpenBarrier()
        {
            for (var i = 0; i < barrierParts.Length; i++)
                barrierParts[i].DOLocalRotate(barrierParts[i].transform.forward * (-60 * (-i == 0 ? 1 : -1)), 1f)
                    .OnComplete(() =>
                    {
                        barrierFx.SetActive(true);
                        PlayerController.Instance.SetWaitingState(PlayerStates.PlayerState.Moving);
                        containerPart.SetParent(null);
                        Destroy(this);
                    });
        }

        #endregion
    }
}