using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Space_Adventures.UI
{
    public class LevelText : MonoBehaviour
    {
        private void Start()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            GetComponent<Text>().text = (index - 1).ToString();
        }
    }
}
