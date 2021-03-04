using UnityEngine;


namespace Space_Adventures.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Canvas missionCanvas = null;
        [SerializeField] private Canvas optionsCanvas = null;

        private void Start()
        {
            ActivateMissions();
        }

        public void ActivateOptions()
        {
            missionCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(true);
        }

        public void ActivateMissions()
        {
            missionCanvas.gameObject.SetActive(true);
            optionsCanvas.gameObject.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
