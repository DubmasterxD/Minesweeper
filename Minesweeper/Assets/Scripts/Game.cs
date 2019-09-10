using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class Game : MonoBehaviour
    {
        [SerializeField] Text gameEndInfoText = null;

        Board board = null;
        Info info = null;
        Options options = null;
        Flag flag = null;

        private void Awake()
        {
            board = GetComponentInChildren<Board>();
            info = GetComponentInChildren<Info>();
            options = GetComponentInChildren<Options>();
            flag = GetComponentInChildren<Flag>();
            board.options = options;
            info.options = options;
            flag.options = options;
        }

        private void Start()
        {
            SetWindowSize(10, 10);
        }

        private void OnEnable()
        {
            Field.onMineClick += GameOver;
            Field.onCheckGameStatus += CheckGameStatus;
        }

        private void OnDisable()
        {
            Field.onMineClick -= GameOver;
            Field.onCheckGameStatus -= CheckGameStatus;
        }

        public void StartGame()
        {
            gameEndInfoText.gameObject.SetActive(false);
            options.gameObject.SetActive(false);
            flag.gameObject.SetActive(true);
            info.DisableMenu();
            board.Clear();
            info.Restart();
            SetWindowSize(options.rowsNumber, options.columnsNumber);
            board.CreateFields(options.rowsNumber, options.columnsNumber);
            board.SetMines(options.minesNumber);
            options.ResetFlags();
            flag.UnsetFlagginMode();
            info.StartTimer();
        }

        private void CheckGameStatus()
        {
            if(board.CountFieldsNotRevealed()==options.minesNumber)
            {
                Win();
            }
        }

        private void Win()
        {
            gameEndInfoText.text = "Victory!";
            GameEnd();
        }

        private void GameOver()
        {
            gameEndInfoText.text = "Game Over :(";
            GameEnd();
            board.RevealAllMines();
        }

        private void GameEnd()
        {
            info.StopTimer();
            info.EnableMenu();
            gameEndInfoText.gameObject.SetActive(true);
            flag.gameObject.SetActive(false);
        }

        private void SetWindowSize(int rowsNumber, int columnsNumber)
        {
            int height = 46 + rowsNumber * 33 - 3;
            int width = columnsNumber * 33 - 3;
            Screen.SetResolution(width, height, false);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }
}
