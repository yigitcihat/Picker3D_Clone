using UnityEngine.Events;

namespace _Picker3D_.Scripts.Managers
{
    public static class EventManager
    {
        public static UnityEvent OnLevelWin = new UnityEvent();
        public static UnityEvent OnLevelFail = new UnityEvent();
    }
}
