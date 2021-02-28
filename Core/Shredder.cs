using UnityEngine;

namespace Space_Adventures.Core
{
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
