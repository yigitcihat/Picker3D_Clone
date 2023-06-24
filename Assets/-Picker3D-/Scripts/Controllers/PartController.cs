using System.Collections.Generic;
using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    public class PartController : Singleton<PartController>
    {
        [SerializeField] internal Renderer rightWall, leftWall, partGround, containerGround;

        [SerializeField] private List<Renderer> containerWalls = new List<Renderer>();

        [SerializeField] private Transform containerPart;
        [SerializeField] internal GameObject finalLine;
        [SerializeField] internal GameObject Success_Fx;
        internal Container container;
        private float _containerPartInitPosY;
        
        public override void Awake()
        {
            container = GetComponentInChildren<Container>();
            var transform1 = containerPart.transform;
            var localPosition = transform1.localPosition;
            _containerPartInitPosY = localPosition.y;
            localPosition -= new Vector3(0, 2, 0);
            transform1.localPosition = localPosition;
           
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
        internal void PlayFX()
        {
            foreach (var fx in Success_Fx.GetComponentsInChildren<ParticleSystem>())
            {
                Debug.Log("asdasd");
                fx.Play();
            }
        }

    }
}