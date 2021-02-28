using UnityEngine;
using Space_Adventures.Core;

namespace Space_Adventures.Control
{
    [RequireComponent(typeof(ActionScheduler))]
    public class RightMover : MonoBehaviour, IAction
    {
        [SerializeField] private float moveSpeed = 5f;

        private void Update()
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        public void Cancel()
        {
            Destroy(this);
        }
    }
}
