using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class Options : MonoBehaviour
    {
        [SerializeField] InputField rowsNumberInput = null;
        [SerializeField] InputField columnsNumberInput = null;
        [SerializeField] InputField minesNumberInput = null;
        [SerializeField] Colors[] colorSets = null;

        public Colors currentColors { get; private set; } = null;
        public int rowsNumber { get; private set; } = 10;
        public int columnsNumber { get; private set; } = 10;
        public int minesNumber { get; private set; } = 10;
        public int flagsLeft { get; private set; } = 10;

        ColorChanger colorChanger = null;

        private void Awake()
        {
            colorChanger = FindObjectOfType<ColorChanger>();
        }

        private void Start()
        {
            ChangeColors(0);
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
            flagsLeft = minesNumber;
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
