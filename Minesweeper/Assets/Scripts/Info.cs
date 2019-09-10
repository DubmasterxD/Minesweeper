using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class Info : MonoBehaviour
    {
        [SerializeField] Button menuButton = null;
        [SerializeField] Text timeElapsedText = null;
        [SerializeField] Text flagsLeftText = null;

        public Options options { get; set; } = null;

        float timer = 0;
        bool isTimerActive = false;

        private void Update()
        {
            UpdateTimer();
            UpdateFlagsLeft();
        }

        public void Restart()
        {
            timer = 0;
            flagsLeftText.text = options.minesNumber.ToString();
        }

        public void StartTimer()
        {
            isTimerActive = true;
        }

        public void StopTimer()
        {
            isTimerActive = false;
        }

        public void DisableMenu()
        {
            menuButton.gameObject.SetActive(false);
        }

        public void EnableMenu()
        {
            menuButton.gameObject.SetActive(true);
        }

        private void UpdateFlagsLeft()
        {
            flagsLeftText.text = options.flagsLeft.ToString();
        }

        private void UpdateTimer()
        {
            if (isTimerActive)
            {
                timer += Time.deltaTime;
                timeElapsedText.text = (int)timer + "s";
            }
        }
    }
}