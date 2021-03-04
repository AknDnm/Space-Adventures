using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Space_Adventures.UI
{
    public class CoolDown : MonoBehaviour
    {
        private Image timer;
        private float time = 10f;
        private bool setTimer = false;

        private void Start()
        {
            timer = GetComponent<Image>();
        }

        void Update()
        {
            if(setTimer)
            {
                timer.fillAmount -= 1.0f / time * Time.deltaTime;

                if(timer.fillAmount <= 0f)
                {
                    setTimer = false;
                }
            }
        }

        public void SetCoolDown(float time)
        {
            this.time = time;
            setTimer = true;
            timer.fillAmount = 1f;
        }

        public bool TimerIsWorking()
        {
            return setTimer;
        }
    }
}
