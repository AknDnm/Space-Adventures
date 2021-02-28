using UnityEngine;
using Space_Adventures.Core;

namespace Space_Adventures.Control
{
    [RequireComponent(typeof(ActionScheduler))]
    public class LeftMover : MonoBehaviour, IAction
    {
        [SerializeField] private float moveSpeed = 5f;

        private void Update()
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        public void Cancel()
        {
            Destroy(this);
        }
    }
}
