using System.Collections.Generic;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    public class PartController : MonoBehaviour
    {
        [SerializeField] internal Renderer rightWall, leftWall, partGround, containerGround;

        [SerializeField] private List<Renderer> containerWalls = new List<Renderer>();

        [SerializeField] private Transform containerPart;
        [SerializeField] internal Container container;
        private float _containerPartInitPosY;

        private void Start()
        {
            var transform1 = containerPart.transform;
            var localPosition = transform1.localPosition;
            _containerPartInitPosY = localPosition.y;
            localPosition -= new Vector3(0, 2, 0);
            transform1.localPosition = localPosition;
            container = GetComponentInChildren<Container>();
        }

        internal void SetPartConfig(Color partWallColor, Color partGroundColor, Color containerWallColor,
            Color containerGroundColor)
        {
            rightWall.material.color = partWallColor;
            leftWall.material.color = partWallColor;
            partGround.material.color = partGroundColor;
            containerGround.material.color = containerGroundColor;
            foreach (var wall in containerWalls)
            {
                wall.material.color = containerWallColor;
            }

        }
    }
}