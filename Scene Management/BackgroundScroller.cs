using UnityEngine;

namespace Space_Adventures.SceneManagement
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.5f;

        private Material myMaterial;
        Vector2 offSet;

        private void Start()
        {
            myMaterial = GetComponent<Renderer>().material;
            offSet = new Vector2(scrollSpeed, 0f);
        }

        private void Update()
        {
            myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
    }
}
