using UnityEngine;
using UnityEngine.UI;
using Space_Adventures.Core;

namespace Space_Adventures.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] Slider slider = null;

        private void OnEnable()
        {
            SpaceAdventuresEvents.setPlayerHealthBar.AddListener(SetMaxHealth);
            SpaceAdventuresEvents.updatePlayerHealthBar.AddListener(UpdateTheHealthBar);
        }

        private void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        private void UpdateTheHealthBar(float health)
        {
            slider.value = health;
        }

        private void OnDisable()
        {
            SpaceAdventuresEvents.setPlayerHealthBar.RemoveListener(SetMaxHealth);
            SpaceAdventuresEvents.updatePlayerHealthBar.RemoveListener(UpdateTheHealthBar);
        }
    }
}
