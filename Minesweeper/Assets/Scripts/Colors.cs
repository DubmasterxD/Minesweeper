using UnityEngine;

namespace Minesweeper
{
    [CreateAssetMenu(fileName ="New Color Palette",menuName ="Minesweeper/Colors",order =0)]
    public class Colors : ScriptableObject
    {
        public Color basicColor = default;
        public Color textsColor = default;
        public Color secondaryColor = default;
        public Color correctColor = default;
        public Color mineColor = default;
        public Color flagColor = default;
        public Color[] numberColors = null;
    }
}
