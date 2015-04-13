using System;
using System.Drawing;

namespace ClientManagerNotifier.Models
{
    /// <summary>
    /// The icon processor class.
    /// </summary>
    public class IconCreator
    {
        private readonly Graphics _processedIconGraphic;
        private readonly Bitmap _drawBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="IconCreator"/> class.
        /// </summary>
        /// <param name="icon">Icon which be used in background.</param>
        /// <param name="numberColor">Color of drawn number.</param>
        public IconCreator(Icon icon, Color numberColor)
        {
            if (icon == null) throw new ArgumentNullException("icon");

            IconSource = icon;
            IconNumberColor = new SolidBrush(numberColor);
            ProcessedIcon = icon;

            _drawBuffer = new Bitmap(16, 16);
            _processedIconGraphic = Graphics.FromImage(_drawBuffer);
        }

        /// <summary>
        /// Modified icon.
        /// </summary>
        public Icon ProcessedIcon { get; private set; }

        /// <summary>
        /// Icon source.
        /// </summary>
        public Icon IconSource { get; private set; }

        /// <summary>
        /// Icon source.
        /// </summary>
        public int DrawedNumber { get; private set; }

        /// <summary>
        /// Icon number color.
        /// </summary>
        public Brush IconNumberColor { get; private set; }

        /// <summary>
        /// Draw number on image.
        /// </summary>
        /// <param name="numberForDrawing">Number that should be draw on image.</param>
        /// <returns>False if count not changed since last call.</returns>
        public bool ProcessImage(int numberForDrawing)
        {
            if (numberForDrawing == DrawedNumber) return false;
            
            DrawedNumber = numberForDrawing;
            if (numberForDrawing == 0)
            {
                ProcessedIcon = IconSource;
            }
            else
            {
                _processedIconGraphic.DrawIcon(IconSource, new Rectangle(0, 0, 16, 16));
                _processedIconGraphic.DrawString(DrawedNumber.ToString(), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                             IconNumberColor, numberForDrawing < 10 ? 2 : 0, 0);
                ProcessedIcon = Icon.FromHandle(_drawBuffer.GetHicon());
            }

            return true;
        }
    }
}
