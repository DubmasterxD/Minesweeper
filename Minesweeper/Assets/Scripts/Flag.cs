using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class Flag : MonoBehaviour
    {
        public Options options { get; set; } = null;
        public bool isFlaggingMode { get; private set; } = false;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlag();
            }
        }

        public void ToggleFlag()
        {
            if (isFlaggingMode)
            {
                UnsetFlagginMode();
            }
            else
            {
                SetFlaggingMode();
            }
        }

        public void SetFlaggingMode()
        {
            isFlaggingMode = true;
            GetComponent<Image>().color = options.currentColors.flagColor;
        }

        public void UnsetFlagginMode()
        {
            isFlaggingMode = false;
            GetComponent<Image>().color = options.currentColors.correctColor;
        }
    }
}
