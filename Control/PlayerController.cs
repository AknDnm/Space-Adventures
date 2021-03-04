using UnityEngine;
using Space_Adventures.Combat;
using Space_Adventures.Resources;
using Space_Adventures.Core;
using UnityEngine.Events;

namespace Space_Adventures.Control
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour, IAction, ILastAction
    {
        [SerializeField] private GameObject turbine = null;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float xpadding = 1f;
        [SerializeField] private float ypadding = 1f;
        [SerializeField] private UnityEvent onCollision = null;

        private PlayerShooter playerShooter;
        private Ammunition ammunition;
        private bool touched = false;

        private float startPosX;
        private float startPosY;
        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;


        private void Awake()
        {
            playerShooter = GetComponent<PlayerShooter>();
            ammunition = GetComponent<Ammunition>();
        }

        private void Start()
        {
            SetUpMoveBoundaries();
            Fire();
        }

        private void OnEnable()
        {
            SpaceAdventuresEvents.rocketButtonPressed.AddListener(LaunchRocket);
            SpaceAdventuresEvents.shieldButtonPressed.AddListener(ActivateShield);
        }

        private void Update()
        {
            Move();
        }

        // To test on computer.
        private void KeyboardControl()
        {
            if (Input.GetButtonDown("Fire2")) { LaunchRocket(); }
            if (Input.GetButtonDown("Fire3")) { ActivateShield(); }

            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); //With using Mathf.Clamp moveable area is limited.
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax); //With using Mathf.Clamp moveable area is limited.

            transform.position = new Vector2(newXPos, newYPos);
        }

        private void Move()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                switch(touch.phase){ 
                    case TouchPhase.Began:
                        // Calculating the offset between touch position and gameobject position
                        startPosX = touchPos.x - transform.position.x;
                        startPosY = touchPos.y - transform.position.y;

                        // I discovered a bug that occurs when the player touches the screen with two fingers. When the player removes one
                        // finger then the character immediately moves to the position of the players remaining second finger. I created a bool
                        // to detect the first touch. This will be set to false whenever this instance of screen touch ends and it will stay false 
                        // until a new touch is detected to control the player.
                        touched = true;
                        break;
                    case TouchPhase.Moved:
                        if (touched)
                        {
                            Vector2 newPosition = new Vector3(touchPos.x - startPosX, touchPos.y - startPosY);
                            var newPos = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);

                            // Checks the new position to not exceed borders
                            newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
                            newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);

                            transform.position = newPos;
                        }
                        break;
                    case TouchPhase.Ended:
                        touched = false;
                        break;
                }
            }
        }

        private void Fire()
        {
            StartCoroutine(playerShooter.FireContinuosly(0)); //First gun will be fired
        }

        private void LaunchRocket()
        {
            if (ammunition.RocketIsReady())
            {
                playerShooter.Fire(1); //Second gun will be fired
                ammunition.RocketHasBeenLaunched();
            }
        }

        private void ActivateShield()
        {
            if(ammunition.ShieldIsReady())
            {
                ammunition.ActivateShield();
            }
        }

        private void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xpadding;  //Convert min. camera.x position to world position
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xpadding;  //Convert max. camera.x position to world position

            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + ypadding;  //Convert min. camera.y position to world position
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - ypadding;  //Convert max. camera.y position to world position
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision.Invoke();
            GetComponent<PlayerHealth>().Die();
        }

        public void Cancel()
        {
            Destroy(turbine);
            Destroy(this);
        }

        private void OnDestroy()
        {
            SpaceAdventuresEvents.rocketButtonPressed.RemoveListener(LaunchRocket);
            SpaceAdventuresEvents.shieldButtonPressed.RemoveListener(ActivateShield);
        }

        public void InvokeLastAction()
        {
            Destroy(this);
        }
    }
}
