using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.General
{
    public class GameManager : MonoBehaviour
    {
        public event Action onGameStart;
        public event Action onGameEnd;

        [SerializeField] Text gameEndInfo = null;
        [SerializeField] RectTransform infoArea = null;

        public int tileSize { get; set; } = 0;
        public bool isPlaying { get; private set; } = false;
        int spaceBetweenTiles = 3;

        private void Start()
        {
            SetWindowSize(10, 10);
        }

        public void StartGame()
        {
            onGameStart();
            isPlaying = true;
            if (gameEndInfo != null)
            {
                gameEndInfo.gameObject.SetActive(false);
            }
        }

        public void GameOver(bool isWin)
        {
            isPlaying = false;
            if (gameEndInfo != null)
            {
                if (isWin)
                {
                    gameEndInfo.text = "Victory!";
                }
                else
                {
                    gameEndInfo.text = "Game Over :(";
                }
                gameEndInfo.gameObject.SetActive(true);
            }
            onGameEnd();
        }

        public void SetWindowSize(int rowsNumber, int columnsNumber)
        {
            int height = (int)infoArea.rect.height + rowsNumber * (tileSize + spaceBetweenTiles) - spaceBetweenTiles;
            int width = columnsNumber * (tileSize + spaceBetweenTiles) - spaceBetweenTiles;
            Screen.SetResolution(width, height, false);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }
}