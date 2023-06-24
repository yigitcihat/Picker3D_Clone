using DG.Tweening;
using UnityEngine;

namespace _Picker3D_.Scripts.Others
{
    public class Lock : MonoBehaviour
    {
        public float rotationDuration = 1f; 

        public void Start()
        {
            transform.DOLocalRotate(new Vector3(0f, 0f, 360f), rotationDuration, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart) 
                .SetEase(Ease.Linear); 

        }
    }
}
