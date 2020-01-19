using UnityEngine;

namespace Minesweeper.Colors
{
    [CreateAssetMenu(fileName ="New Color Palette",menuName ="Minesweeper/Colors",order =0)]
    public class ColorPalette : ScriptableObject
    {
        public enum Colors
        {
            Primary,
            Secondary,
            Text,
            Correct,
            Mine,
            Flag
        }

        [SerializeField] Color primary = default;
        [SerializeField] Color texts = default;
        [SerializeField] Color secondary = default;
        [SerializeField] Color correct = default;
        [SerializeField] Color mine = default;
        [SerializeField] Color flag = default;
        [SerializeField] Color[] numbers = new Color[8];

        public Color GetNumberColor(int number)
        {
            if (number > 0 && number <= numbers.Length)
            {
                return numbers[number - 1];
            }
            else
            {
                return new Color(0, 0, 0);
            }
        }

        public Color GetColor(Colors color)
        {
            switch (color)
            {
                case Colors.Primary:
                    return primary;
                case Colors.Secondary:
                    return secondary;
                case Colors.Text:
                    return texts;
                case Colors.Correct:
                    return correct;
                case Colors.Mine:
                    return mine;
                case Colors.Flag:
                    return flag;
                default:
                    return new Color(0,0,0);
            }
        }
    }
}
