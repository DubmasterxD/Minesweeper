using Minesweeper.General;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Board
{
    public class Info : MonoBehaviour
    {
        [SerializeField] Button menuButton = null;
        [SerializeField] Text timeElapsedText = null;
        [SerializeField] Text flagsLeftText = null;

        public int fieldsNotRevealed { get; private set; } = 0;
        public int flagsLeft { get; private set; } = 10;
        float timer = 0;
        bool isTimerActive = false;

        GameManager game;
        Options options;

        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
            options = FindObjectOfType<Options>();
        }

        private void Start()
        {
            if (game != null)
            {
                game.onGameStart += GameStart;
                game.onGameEnd += GameEnd;
            }
        }

        private void Update()
        {
            UpdateTimer();
            UpdateFlagsLeft();
        }

        public void UseFlag()
        {
            flagsLeft--;
        }

        public void ReturnFlag()
        {
            flagsLeft++;
        }

        public void ResetFlags()
        {
            flagsLeft = options.minesNumber;
        }

        private void GameStart()
        {
            fieldsNotRevealed = options.columnsNumber * options.rowsNumber;
            ResetInfo();
            ToggleMenu(false);
            ToggleTimer(true);
        }

        private void GameEnd()
        {
            ToggleMenu(true);
            ToggleTimer(false);
        }

        private void ResetInfo()
        {
            timer = 0;
            flagsLeftText.text = options.minesNumber.ToString();
            ResetFlags();
        }
        public void ReduceFieldsNotRevealed()
        {
            fieldsNotRevealed--;
        }

        private void ToggleMenu(bool shouldActivate)
        {
            menuButton.gameObject.SetActive(shouldActivate);
        }

        private void ToggleTimer(bool shouldActivate)
        {
            isTimerActive = shouldActivate;
        }

        private void UpdateTimer()
        {
            if (isTimerActive)
            {
                timer += Time.deltaTime;
                timeElapsedText.text = string.Format("{0:0}s", timer);
            }
        }

        private void UpdateFlagsLeft()
        {
            flagsLeftText.text = string.Format("{0}", flagsLeft);
        }
    }
}