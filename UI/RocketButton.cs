using UnityEngine;
using Space_Adventures.Core;
using UnityEngine.UI;

namespace Space_Adventures.UI
{
    public class RocketButton : MonoBehaviour
    {
        [SerializeField] private CoolDown coolDown = null;
        [SerializeField] private Text numberText = null;

        private void OnEnable()
        {
            SpaceAdventuresEvents.setRocketButton.AddListener(ButtonPressed);
            SpaceAdventuresEvents.updateRocketCount.AddListener(UpdateText);
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
            SpaceAdventuresEvents.setRocketButton.RemoveListener(ButtonPressed);
            SpaceAdventuresEvents.updateRocketCount.RemoveListener(UpdateText);
        }
    }
}
