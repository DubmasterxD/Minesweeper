using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class Field : MonoBehaviour
    {
        [SerializeField] Text numberOfMinesNearbyText = null;

        public static event Action onMineClick;
        public static event Action onCheckGameStatus;

        public bool isMine { get; set; } = false;
        public bool isFlagged { get; set; } = false;
        public Options options { get; set; } = null;

        public bool isRevealed { get; private set; } = false;
        public List<Field> nearbyFields { get; private set; } = new List<Field>();

        private int numberOfMinesNearby = 0;
        Flag flag = null;

        private void Awake()
        {
            flag = FindObjectOfType<Flag>();
        }

        private void Start()
        {
            ChangeBackgroundColor(options.currentColors.basicColor);
        }

        public void CheckField()
        {
            if (!flag.gameObject.activeInHierarchy)
            {
                return;
            }
            if (flag.isFlaggingMode)
            {
                ToggleFlagField();
            }
            else if (!isFlagged)
            {
                if (isMine)
                {
                    onMineClick();
                    return;
                }
                else
                {
                    RevealField();
                }
            }
            onCheckGameStatus();
        }

        public void AssignNearbyField(Field field)
        {
            nearbyFields.Add(field);
        }

        public void IncreaseNumberOfMinesNearby()
        {
            numberOfMinesNearby++;
        }

        public void RevealMine()
        {
            ChangeBackgroundColor(options.currentColors.mineColor);
            DisableButton();
        }

        private void ToggleFlagField()
        {
            if (isFlagged)
            {
                isFlagged = false;
                ChangeBackgroundColor(options.currentColors.basicColor);
                options.ReturnFlag();
            }
            else
            {
                if (options.flagsLeft > 0)
                {
                    isFlagged = true;
                    ChangeBackgroundColor(options.currentColors.flagColor);
                    options.UseFlag();
                }
            }
        }

        private void RevealField()
        {
            isRevealed = true;
            ShowNumberOfMines();
            if(numberOfMinesNearby==0)
            {
                RevealNearby();
            }
            ChangeBackgroundColor(options.currentColors.correctColor);
            DisableButton();
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
            numberOfMinesNearbyText.color = options.currentColors.numberColors[numberOfMinesNearby - 1];
        }

        private void ChangeBackgroundColor(Color newColor)
        {
            GetComponent<Image>().color = newColor;
        }

        private void DisableButton()
        {
            GetComponent<Button>().enabled = false;
        }
    }
}
