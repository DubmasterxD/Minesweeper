using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] Text[] texts = null;
        [SerializeField] Image[] primaryImages = null;
        [SerializeField] Image[] secondaryImages = null;

        public void ChangeColors(Colors colors)
        {
            ChangeTextsColor(colors.textsColor);
            ChangePrimaryColor(colors.basicColor);
            ChangeSecondaryColor(colors.secondaryColor);
        }

        private void ChangeTextsColor(Color color)
        {
            foreach (Text text in texts)
            {
                text.color = color;
            }
        }

        private void ChangePrimaryColor(Color color)
        {

            foreach (Image image in primaryImages)
            {
                image.color = color;
            }
        }

        private void ChangeSecondaryColor(Color color)
        {

            foreach (Image image in secondaryImages)
            {
                image.color = color;
            }
        }
    }
}
