using UnityEngine;

namespace Minesweeper.Colors
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] Material[] textMaterials = null;
        [SerializeField] Material[] primaryMaterials = null;
        [SerializeField] Material[] secondaryMaterials = null;

        public void ChangeColors(ColorPalette colors)
        {
            ChangeColors(textMaterials, colors.GetColor(ColorPalette.Colors.Text));
            ChangeColors(primaryMaterials, colors.GetColor(ColorPalette.Colors.Primary));
            ChangeColors(secondaryMaterials, colors.GetColor(ColorPalette.Colors.Secondary));
        }

        private void ChangeColors(Material[] materials, Color newColor)
        {
            if (materials != null)
            {
                foreach (Material material in materials)
                {
                    material.color = newColor;
                }
            }
        }
    }
}
