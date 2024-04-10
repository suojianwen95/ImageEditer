using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditer
{
    internal class ExtendedPanel : Panel
    {

        private const int WS_EX_TRANSPARENT = 0x20;

        public ExtendedPanel()
        {
            SetStyle(ControlStyles.Opaque, true);
        }

        private int opacity = 50;
        [DefaultValue(50)]
        public int Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("value must be between 0 and 100");
                this.opacity = value;
            }
        }

        private string mode = "default";
        [DefaultValue("default")]
        public string Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
            {
                Pen p = new Pen(Color.Red, 2);
                if (this.mode == "text") {
                    p.DashStyle = DashStyle.Dot;
                }

                e.Graphics.FillRectangle(brush, this.ClientRectangle);
                e.Graphics.DrawRectangle(p, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }
}
