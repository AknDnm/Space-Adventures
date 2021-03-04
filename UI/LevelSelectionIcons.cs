using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Space_Adventures.Progression
{
    public class LevelSelectionIcons : MonoBehaviour
    {
        [SerializeField] private List<Button> levelButtons = null;
        [SerializeField] private List<Sprite> images = null; // 0)Locked 1)Active 2)None Start 3)1 Star 4)2 Stars 5)3 Stars

        private GameProgression gameProgression;

        private void Awake()
        {
            gameProgression = FindObjectOfType<GameProgression>();
        }

        private void Start()
        {
            ConfigureLevelSelectionIcons();
        }

        public void ConfigureLevelSelectionIcons()
        {
            int levelIndex = 0;
            
            foreach(Button levelButton in levelButtons)
            {
                int rating = gameProgression.GetTheLevelRating(levelIndex);

                if (levelIndex == 0 && rating == 0)
                {
                    levelButton.GetComponent<Image>().sprite = images[1];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                else if(rating == 0)
                {
                    levelButton.GetComponent<Image>().sprite = images[0];
                    levelButton.GetComponent<Button>().interactable = false;
                }
                else if(rating == 1)
                {
                    levelButton.GetComponent<Image>().sprite = images[1];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                else if (rating == 2)
                {
                    levelButton.GetComponent<Image>().sprite = images[2];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                else if (rating == 3)
                {
                    levelButton.GetComponent<Image>().sprite = images[3];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                else if (rating == 4)
                {
                    levelButton.GetComponent<Image>().sprite = images[4];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                else if (rating == 5)
                {
                    levelButton.GetComponent<Image>().sprite = images[5];
                    levelButton.GetComponent<Button>().interactable = true;
                }
                levelIndex++;
            }
        }
    }
}
