using UnityEngine.Events;

namespace Space_Adventures.Core
{
    public static class SpaceAdventuresEvents
    {
        public static UnityEvent rocketButtonPressed = new UnityEvent();
        public static UnityEvent shieldButtonPressed = new UnityEvent();
        public static UnityEvent playerDied = new UnityEvent();
        public static TakeDamageEvent damageTaken = new TakeDamageEvent();
        public static SetPlayerHealthBar setPlayerHealthBar = new SetPlayerHealthBar();
        public static UpdatePlayerHealthBar updatePlayerHealthBar = new UpdatePlayerHealthBar();
        public static SetRocketButton setRocketButton = new SetRocketButton();
        public static UpdateRocketCount updateRocketCount = new UpdateRocketCount();
        public static SetShieldButton setShieldButton = new SetShieldButton();
        public static UpdateShieldCount updateShieldCount = new UpdateShieldCount();
        public static ActivateLevelClearedUI activateLevelClearedUI = new ActivateLevelClearedUI();
        public static ProcessLevelClearedUI processLevelClearedUI = new ProcessLevelClearedUI();
        public static UnityEvent activateGameOverUI = new UnityEvent();
    }
    public class TakeDamageEvent : UnityEvent<float> { }
    public class SetPlayerHealthBar : UnityEvent<float> { }
    public class UpdatePlayerHealthBar : UnityEvent<float> { }
    public class SetRocketButton : UnityEvent<int, float> { }
    public class UpdateRocketCount : UnityEvent<int> { }
    public class SetShieldButton : UnityEvent<int, float> { }
    public class UpdateShieldCount : UnityEvent<int> { }
    public class ActivateLevelClearedUI : UnityEvent<int, int> { }
    public class ProcessLevelClearedUI : UnityEvent<int, int> { }
}
