using UnityEngine;

namespace Space_Adventures.Core
{
    // Stop player and enemy when they die also stop projectile when it hits.
    public class ActionScheduler : MonoBehaviour
    {
        public void Disable()
        {
            foreach(IAction component in GetComponents<IAction>())
            {
                component.Cancel();
            }
        }
    }
}
