using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageEditer
{
    internal class ExtendedTextPanel : Panel
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private bool isClicked = false;
        public Point mouseDownPoint; // 记录鼠标点击坐标

        public ExtendedTextPanel()
        {
            SetStyle(ControlStyles.Opaque, true);

            this.extendedPanel.BoarderColor = Color.Red;
            this.extendedPanel.BoarderSize = 2;
            this.extendedPanel.Location = new Point(0, 0);
            this.extendedPanel.Size = new Size(this.Width, this.Height);
            this.Controls.Add(this.extendedPanel);
            this.Resize += ExtendedTextPanel_Resize;
        }

        private void ExtendedTextPanel_Resize(object sender, EventArgs e)
        {
            this.extendedPanel.Location = new Point(0, 0);
            this.extendedPanel.Size = new Size(this.Width, this.Height);
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

        private TransTextBox transTextBox = new TransTextBox();
        public TransTextBox TransTextBox
        {
            get
            {
                return this.transTextBox;
            }
            set
            {
                this.transTextBox = value;
            }
        }

        private BorderPanel extendedPanel = new BorderPanel();
        public BorderPanel ExtendedPanel {
            get
            {
                return this.extendedPanel;
            }
            set
            {
                this.extendedPanel = value;
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
        /**
         * 重绘
         */ 
        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.WriteLine("=====ExtendedTextPanel");
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
            {
                Pen p = new Pen(Color.Red, 2);
                if (this.mode == "text")
                {
                    p.DashStyle = DashStyle.Dot;
                }
                
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
                e.Graphics.Dispose();
            }
            base.OnPaint(e);
        }

        /**
         * 显示输入框
         */
        public void showTransTextBox() {

            this.transTextBox.Location = new Point(2, 2);
            this.transTextBox.AutoSize = true;
            this.transTextBox.Multiline = true;
            this.transTextBox.BorderStyle = BorderStyle.None;
            this.transTextBox.Size = new Size(this.Width -4, this.Height-4);
            this.extendedPanel.Controls.Add(this.transTextBox);

            this.transTextBox.LostFocus += new EventHandler(txt_LostFocus);
            this.transTextBox.GotFocus += new EventHandler(txt_GotFocus);

            this.transTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDown);
            this.transTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txt_MouseMove);
            this.transTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txt_MouseUp);

        }

        private void txt_LostFocus(object sender, EventArgs e)
        {
            Debug.WriteLine("txt_LostFocus");
            this.clearBoder();
        }

        private void txt_GotFocus(object sender, EventArgs e)
        {
            Debug.WriteLine("txt_GotFocus");
            this.showBoder();
        }

        public void clearBoder() {
            this.extendedPanel.BoarderSize = 0;
            this.extendedPanel.Refresh();
        }

        public void showBoder()
        {
            this.extendedPanel.BoarderSize = 2;
            this.extendedPanel.Refresh();
        }


        private void txt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.SizeAll;
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClicked = true;
            }

        }
        private void txt_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isClicked)
            {
                this.Left = this.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.Top = this.Top + (Cursor.Position.Y - mouseDownPoint.Y);

                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }
        }
        private void txt_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
        }

    }
}
