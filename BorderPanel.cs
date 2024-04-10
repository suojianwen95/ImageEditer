using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditer
{
    internal class BorderPanel : Panel
    {
        private Color _BoarderColor = Color.Black;

        [Browsable(true), Description("边框颜色"), Category("自定义分组")] 
        public Color BoarderColor 
        {
            get
            {
                return _BoarderColor;
            }
            set
            {
                _BoarderColor = value;
            }
        }

        private int _BoarderSize = 2;//初始边框粗细

        [Browsable(true), Description("边框粗细"), Category("自定义分组")]//功能如上
        public int BoarderSize//边框粗细
        {
            get
            {
                return _BoarderSize;
            }
            set
            {
                _BoarderSize = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.WriteLine("----------BorderPanel");
            if (_BoarderSize > 0) {
                Pen p = new Pen(_BoarderColor, _BoarderSize);
                p.DashStyle = DashStyle.Dot;
                e.Graphics.DrawRectangle(p, this.ClientRectangle);
            }
           
            base.OnPaint(e);
        }

    }
}
