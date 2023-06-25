using UnityEngine.Events;

namespace _Picker3D_.Scripts.Managers
{
    public static class EventManager
    {
        public static UnityEvent OnPartSuccess = new UnityEvent();
        public static UnityEvent OnLevelRestart= new UnityEvent();
    }
}
