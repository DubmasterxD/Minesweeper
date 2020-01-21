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
        [SerializeField] InputField musicVolumeInput = null;
        [SerializeField] InputField sfxVolumeInput = null;
        [SerializeField] Slider musicVolumeSlider = null;
        [SerializeField] Slider sfxVolumeSlider = null;
        [SerializeField] Toggle darkModeToggle = null;
        [SerializeField] ColorPalette[] colorSets = null;
        [SerializeField] int maxRows = 20;
        [SerializeField] int maxColumns = 40;
        public int MaxRows { get => maxRows; }
        public int MaxColumns { get => maxColumns; }

        public int rowsNumber { get; private set; } = 10;
        public int columnsNumber { get; private set; } = 10;
        public int minesNumber { get; private set; } = 10;
        public int musicVolume { get; private set; } = 100;
        public int sfxVolume { get; private set; } = 100;
        bool isDarkMode = false;
        ColorPalette currentColors = null;

        ColorChanger colorChanger;
        GameManager game;
        Music music;
        Audio sfx;

        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
            colorChanger = FindObjectOfType<ColorChanger>();
            music = FindObjectOfType<Music>();
            sfx = FindObjectOfType<Audio>();
        }

        private void Start()
        {
            ChangeColors(0);
            if (game != null)
            {
                game.onGameStart += GameStart;
            }
            LoadPrefs();
        }

        public void GameStart()
        {
            gameObject.SetActive(false);
            SavePrefs();
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
            isDarkMode = turnOn;
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

        public void ChangeMusicVolume(float newVolume)
        {
            music.ChangeVolume((int)newVolume);
            musicVolume = (int)newVolume;
            musicVolumeInput.text = newVolume.ToString();
        }

        public void ChangeSFXVolume(float newVolume)
        {
            sfx.ChangeVolume((int)newVolume);
            sfxVolume = (int)newVolume;
            sfxVolumeInput.text = newVolume.ToString();
        }

        public void ToggleOptions()
        {
            if (gameObject.activeInHierarchy)
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

        private void SavePrefs()
        {
            PlayerPrefs.SetInt("music", musicVolume);
            PlayerPrefs.SetInt("sfx", sfxVolume);
            if (isDarkMode)
            {
                PlayerPrefs.SetInt("darkMode", 1);
            }
            else
            {
                PlayerPrefs.SetInt("darkMode", 0);
            }
        }

        private void LoadPrefs()
        {
            if (PlayerPrefs.HasKey("music"))
            {
                UpdateMusicVolumeSlider(PlayerPrefs.GetInt("music"));
            }
            if (PlayerPrefs.HasKey("sfx"))
            {
                UpdateSFXVolumeSlider(PlayerPrefs.GetInt("sfx"));
            }
            if (PlayerPrefs.HasKey("darkMode"))
            {
                UpdateDarkModeToggle(PlayerPrefs.GetInt("darkMode"));
            }
        }

        private void UpdateMusicVolumeSlider(int newValue)
        {
            musicVolumeSlider.value = newValue;
        }

        private void UpdateSFXVolumeSlider(int newValue)
        {
            sfxVolumeSlider.value = newValue;
        }

        private void UpdateDarkModeToggle(int isDarkMode)
        {
            if (isDarkMode == 0)
            {
                darkModeToggle.isOn = false;
            }
            else
            {
                darkModeToggle.isOn = true;
            }
        }
    }
}