using _Picker3D_.Scripts.Managers;
using UnityEngine;

namespace _Picker3D_.Scripts.Others
{
    public class Particules : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.OnStageSuccess.AddListener(LevelSuccessParticules);

        }

        private void LevelSuccessParticules()
        {
            foreach (var particle in transform.GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play();
            }
        }
    }
}
