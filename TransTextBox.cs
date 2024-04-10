using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditer
{
    internal class TransTextBox : TextBox
    {

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr LoadLibrary(string lpFileName);

        private const int WS_EX_TRANSPARENT = 0x020;

        public TransTextBox()
        {
            SetStyle(ControlStyles.Opaque, true);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                {
                    prams.ExStyle |= WS_EX_TRANSPARENT;
                    prams.ClassName = "RICHEDIT50W";
                }
                return prams;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            Debug.WriteLine("=====TransTextBox");
        }
    }
}
