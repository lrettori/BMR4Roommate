using System.Drawing;
using System.Windows.Forms;




namespace MotorTaskAcquisition
{
    public static class ColorManagement
    {
        public static Color primaryColor = ColorTranslator.FromHtml("#8A95D6");
        //public static Color secondaryColor = ColorTranslator.FromHtml("#89DFEB");
        public static Color secondaryColor = ColorTranslator.FromHtml("#4DD0E1");
        public static Color dangerColor = ColorTranslator.FromHtml("#FF4C4C");
        public static Color successColor = ColorTranslator.FromHtml("#28A745");
        public static Color primaryHoverColor = ColorTranslator.FromHtml("#3F51B5");
        public static Color secondaryHoverColor = ColorTranslator.FromHtml("#4DD0E1");
        public static Color dangerHoverColor = ColorTranslator.FromHtml("#D43F3F");
        public static Color successHoverColor = ColorTranslator.FromHtml("#218838");



        public static void StyleButton(System.Windows.Forms.Button btn, Color backColor, Color textColor, Color hoverColor, bool hoverEnabled)
        {
            btn.BackColor = backColor;
            btn.ForeColor = textColor;
            //btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = new Font("Arial", 9, FontStyle.Bold);

            if (hoverEnabled)
            {
                // Hover event
                btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; };
                btn.MouseLeave += (s, e) => { btn.BackColor = backColor; };
            }
        }
    }
}