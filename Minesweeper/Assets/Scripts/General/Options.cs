using Minesweeper.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.General
{
    public class Options : MonoBehaviour
    {
        [SerializeField] InputField rowsNumberInput = null;
        [SerializeField] InputField columnsNumberInput = null;
        [SerializeField] InputField minesNumberInput = null;
        [SerializeField] ColorPalette[] colorSets = null;
        [SerializeField] int maxRows = 20;
        [SerializeField] int maxColumns = 40;
        public int MaxRows { get => maxRows; }
        public int MaxColumns { get => maxColumns; }

        public int rowsNumber { get; private set; } = 10;
        public int columnsNumber { get; private set; } = 10;
        public int minesNumber { get; private set; } = 10;
        ColorPalette currentColors = null;

        ColorChanger colorChanger;
        GameManager game;

        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
            colorChanger = FindObjectOfType<ColorChanger>();
        }

        private void Start()
        {
            ChangeColors(0);
            if (game != null)
            {
                game.onGameStart += GameStart;
            }
        }

        public void GameStart()
        {
            gameObject.SetActive(false);
            game.SetWindowSize(rowsNumber, columnsNumber);
        }

        public ColorPalette GetCurrentColors()
        {
            if (currentColors != null)
            {
                return currentColors;
            }
            else
            {
                return new ColorPalette();
            }
        }

        public void ToggleNightMode(bool turnOn)
        {
            if (turnOn)
            {
                ChangeColors(1);
            }
            else
            {
                ChangeColors(0);
            }
        }

        public void ChangeMinesNumber(float newNumber)
        {
            minesNumber = (int)newNumber;
            minesNumberInput.text = newNumber.ToString();
        }

        public void ChangeRowsNumber(float newNumber)
        {
            rowsNumber = (int)newNumber;
            rowsNumberInput.text = newNumber.ToString();
        }

        public void ChangeColumnsNumber(float newNumber)
        {
            columnsNumber = (int)newNumber;
            columnsNumberInput.text = newNumber.ToString();
        }

        public void ToggleOptions()
        {
            if(gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        private void ChangeColors(int index)
        {
            currentColors = colorSets[index];
            colorChanger.ChangeColors(currentColors);
        }
    }
}