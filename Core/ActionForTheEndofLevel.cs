using UnityEngine;

namespace Space_Adventures.Core
{
    // At the end of the level enemies or player will stop shooting and if player is alive it will stop moving also
    public class ActionForTheEndofLevel : MonoBehaviour
    {
        public void InvokeTheEndGameAction()
        {
            foreach(ILastAction component in GetComponents<ILastAction>())
            {
                component.InvokeLastAction();
            }
        }
    }
}
