using Minesweeper.Colors;
using Minesweeper.General;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Board
{
    public class Flag : MonoBehaviour
    {
        [SerializeField] AudioClip toggleFlag = null;
        public bool isFlaggingMode { get; private set; } = false;

        Image image;
        GameManager game;
        Options options;
        Audio audio;
        
        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
            options = FindObjectOfType<Options>();
            image = GetComponent<Image>();
            audio = FindObjectOfType<Audio>();
        }

        private void Start()
        {
            gameObject.SetActive(false);
            if (game != null)
            {
                game.onGameStart += GameStart;
                game.onGameEnd += GameEnd;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlag();
            }
        }

        private void GameStart()
        {
            UnsetFlagginMode();
            gameObject.SetActive(true);
        }

        private void GameEnd()
        {
            gameObject.SetActive(false);
        }

        private void ToggleFlag()
        {
            audio.Play(toggleFlag);
            if (isFlaggingMode)
            {
                UnsetFlagginMode();
            }
            else
            {
                SetFlaggingMode();
            }
        }

        private void SetFlaggingMode()
        {
            isFlaggingMode = true;
            if (image != null && options != null)
            {
                image.color = options.GetCurrentColors().GetColor(ColorPalette.Colors.Flag);
            }
        }

        private void UnsetFlagginMode()
        {
            isFlaggingMode = false;
            if (image != null && options != null)
            {
                image.color = options.GetCurrentColors().GetColor(ColorPalette.Colors.Correct);
            }
        }
    }
}
