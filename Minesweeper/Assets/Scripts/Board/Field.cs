using Minesweeper.General;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Board
{
    public class Field : MonoBehaviour
    {
        [SerializeField] Text numberOfMinesNearbyText = null;

        public bool isMine { get; set; } = false;
        bool isFlagged = false;
        bool isRevealed = true;
        int numberOfMinesNearby = 0;
        List<Field> nearbyFields = new List<Field>();

        Button button;
        Image image;
        GameManager game;
        Options options;
        Info info;

        private void Awake()
        {
            button = GetComponent<Button>();
            image = GetComponent<Image>();
            game = FindObjectOfType<GameManager>();
            options = FindObjectOfType<Options>();
            info = FindObjectOfType<Info>();
        }

        public void ResetField()
        {
            if (options != null)
            {
                ChangeBackgroundColor(options.GetCurrentColors().GetColor(Colors.ColorPalette.Colors.Primary));
            }
            isMine = false;
            isFlagged = false;
            isRevealed = false;
            numberOfMinesNearby = 0;
            ToggleButton(true);
            numberOfMinesNearbyText.gameObject.SetActive(false);
        }

        public void CheckField()
        {
            if (game.isPlaying)
            {
                Flag flag = FindObjectOfType<Flag>();
                if (flag.isFlaggingMode)
                {
                    ToggleFlagField();
                }
                else if (!isFlagged)
                {
                    if (isMine)
                    {
                        game.GameOver(false);
                        return;
                    }
                    else
                    {
                        RevealField();
                    }
                }
                CheckGameStatus();
            }
        }

        private void CheckGameStatus()
        {
            if (info.fieldsNotRevealed == options.minesNumber)
            {
                game.GameOver(true);
            }
        }

        public void AssignNearbyField(Field field)
        {
            nearbyFields.Add(field);
        }

        public List<Field> GetNearbyFields()
        {
            return nearbyFields;
        }

        public void IncreaseNumberOfMinesNearby()
        {
            numberOfMinesNearby++;
        }

        public void RevealMine()
        {
            ChangeBackgroundColor(options.GetCurrentColors().GetColor(Colors.ColorPalette.Colors.Mine));
            ToggleButton(false);
        }

        private void ToggleFlagField()
        {
            if (isFlagged)
            {
                isFlagged = false;
                ChangeBackgroundColor(options.GetCurrentColors().GetColor(Colors.ColorPalette.Colors.Primary));
                info.ReturnFlag();
            }
            else
            {
                if (info.flagsLeft > 0)
                {
                    isFlagged = true;
                    ChangeBackgroundColor(options.GetCurrentColors().GetColor(Colors.ColorPalette.Colors.Flag));
                    info.UseFlag();
                }
            }
        }

        private void RevealField()
        {
            if (!isFlagged)
            {
                isRevealed = true;
                info.ReduceFieldsNotRevealed();
                ShowNumberOfMines();
                if (numberOfMinesNearby == 0)
                {
                    RevealNearby();
                }
                ChangeBackgroundColor(options.GetCurrentColors().GetColor(Colors.ColorPalette.Colors.Correct));
                ToggleButton(false);
            }
        }

        private void RevealNearby()
        {
            foreach(Field field in nearbyFields)
            {
                if(!field.isRevealed)
                {
                    field.RevealField();
                }
            }
        }

        private void ShowNumberOfMines()
        {
            if (numberOfMinesNearby == 0)
            {
                return;
            }
            ChangeTextColor();
            numberOfMinesNearbyText.text = numberOfMinesNearby.ToString();
            numberOfMinesNearbyText.gameObject.SetActive(true);
        }

        private void ChangeTextColor()
        {
            numberOfMinesNearbyText.color = options.GetCurrentColors().GetNumberColor(numberOfMinesNearby);
        }

        private void ChangeBackgroundColor(Color newColor)
        {
            image.color = newColor;
        }

        private void ToggleButton(bool shouldEnable)
        {
            button.enabled = shouldEnable;
        }
    }
}
