using UnityEngine;
using Space_Adventures.Core;
using UnityEngine.UI;

namespace Space_Adventures.UI
{
    public class ShieldButton : MonoBehaviour
    {
        [SerializeField] private CoolDown coolDown = null;
        [SerializeField] private Text numberText = null;

        private void OnEnable()
        {
            SpaceAdventuresEvents.setShieldButton.AddListener(ButtonPressed);
            SpaceAdventuresEvents.updateShieldCount.AddListener(UpdateText);
        }

        public void UpdateText(int number)
        {
            numberText.text = number.ToString();
        }

        public void ButtonPressed(int count, float time)
        {
            coolDown.SetCoolDown(time);
            numberText.text = count.ToString();
        }

        private void OnDisable()
        {
            SpaceAdventuresEvents.setShieldButton.RemoveListener(ButtonPressed);
            SpaceAdventuresEvents.updateShieldCount.RemoveListener(UpdateText);
        }
    }
}

