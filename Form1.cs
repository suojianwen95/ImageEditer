using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ImageEditer
{
    public partial class ImageEdit : Form
    {
        private static int rotaNum = 0; // 旋转角度
        private static float zoomNum = 1.0f; // 缩放比例 未使用
        private static int pointRadis = 12; // 裁切点大小
        private static Boolean clipMode = false; // 是否开启裁切

        private static Pen rotaPen = new Pen(Color.Blue, 10); // 旋转颜色 未使用
        private static Pen clipPen = new Pen(Color.Red, 10); // 裁切颜色

        private static string imagePath = null; // 图像路径
        private static Image sourceImage = null; // 初始输入图像源
        private static Image outputImage = null; // 初始输出图像源 未使用

        private static PictureBox currentPictureBox = null; // 当前图像容器
        private static Panel currentImageBox = null; // 当前图像panel
        private static Panel currentClipPanel = null; // 裁切平面

        private static Point one = new Point(0, 0);  // 裁切调整坐标1
        private static Point two = new Point(0, 0);  // 裁切调整坐标2
        private static Point three = new Point(0, 0);  // 裁切调整坐标3
        private static Point four = new Point(0, 0);  // 裁切调整坐标4

        private static PictureBox OneButton = new PictureBox(); // 裁切调整点1
        private static PictureBox TwoButton = new PictureBox(); // 裁切调整点2
        private static PictureBox ThreeButton = new PictureBox(); // 裁切调整点3
        private static PictureBox FourButton = new PictureBox(); // 裁切调整点4

        private static System.Drawing.Rectangle clipRect = new System.Drawing.Rectangle(0, 0, 0, 0); // 裁切矩形信息

        public Point mouseDownPoint; // 记录鼠标点击坐标
        public bool isSelected = false; // 是否点击图像
        public bool isClicked = false; // 是否点击裁切矩形
        public bool isClickedOne = false; // 是否点解裁切矩形调整点1
        public bool isClickedTwo = false; // 是否点解裁切矩形调整点2
        public bool isClickedThree = false; // 是否点解裁切矩形调整点3
        public bool isClickedFour = false; // 是否点解裁切矩形调整点4

        public ImageEdit(){
           InitializeComponent();
           imagePath = @"D:\1.jpg";
           this.init(this.ImageBox, this.pictureBox, imagePath);
        }
        /**
         * 初始化组件及事件
         */
        public void init(Panel currentImage, PictureBox pictureBox, string path) {
            
            imagePath = path.Trim();
            sourceImage = Image.FromFile(path);
            currentImageBox = currentImage;

            /************************按钮事件**************************/
            this.reset.Click += new System.EventHandler(this.reset_Click);
            this.rotaLeft.Click += new System.EventHandler(this.roatleft_Click);
            this.roatRight.Click += new System.EventHandler(this.roatRight_Click);
            this.rota90.Click += new System.EventHandler(this.roat90_Click);
            this.rota180.Click += new System.EventHandler(this.roat180_Click);
            this.mirror.Click += new System.EventHandler(this.mirror_Click);
            this.vertical.Click += new System.EventHandler(this.vertical_Click);
            this.clip.Click += new System.EventHandler(this.clip_Click);

            currentPictureBox = pictureBox;
            currentPictureBox.Location = new Point(0, 0);
            currentPictureBox.Width = 0;
            currentPictureBox.Height = 0;
            currentPictureBox.Image = sourceImage;
            currentPictureBox.Width = sourceImage.Width;
            currentPictureBox.Height = sourceImage.Height;
            currentPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.SizeChanged += new EventHandler(this.ImageEdit_ResizeBegin);
            currentPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
            currentPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            currentPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            currentPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);

            /************************裁切框组件**************************/
           
            OneButton.Size = new Size(pointRadis, pointRadis);
            TwoButton.Size = new Size(pointRadis, pointRadis);
            ThreeButton.Size = new Size(pointRadis, pointRadis);
            FourButton.Size = new Size(pointRadis, pointRadis);

            OneButton.BackColor = Color.Red;
            TwoButton.BackColor = Color.Red;
            ThreeButton.BackColor = Color.Red;
            FourButton.BackColor = Color.Red;
            OneButton.Visible = clipMode;
            TwoButton.Visible = clipMode;
            ThreeButton.Visible = clipMode;
            FourButton.Visible = clipMode;
            FourButton.TabIndex = 10000;

            currentImageBox.Controls.Add(OneButton);
            currentImageBox.Controls.Add(TwoButton);
            currentImageBox.Controls.Add(ThreeButton);
            currentImageBox.Controls.Add(FourButton);
            currentImageBox.Controls.Add(pictureBox);

            currentClipPanel = this.clipPanel;
            currentClipPanel.BackColor = Color.Transparent;
            currentClipPanel.Visible = false;
            currentClipPanel.Location = new Point(0, 0);
            currentClipPanel.Width = 0;
            currentClipPanel.Height = 0;

            currentClipPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clip_MouseDown);
            currentClipPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.clip_MouseMove);
            currentClipPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clip_MouseUp);

            OneButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.One_MouseDown);
            OneButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.One_MouseMove);
            OneButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.One_MouseUp);

            TwoButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Two_MouseDown);
            TwoButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Two_MouseMove);
            TwoButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Two_MouseUp);

            ThreeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Three_MouseDown);
            ThreeButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Three_MouseMove);
            ThreeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Three_MouseUp);

            FourButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Four_MouseDown);
            FourButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Four_MouseMove);
            FourButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Four_MouseUp);

            clipRect.X = 0;
            clipRect.Y = 0;
            clipRect.Width = currentPictureBox.Image.Width;
            clipRect.Height = currentPictureBox.Image.Height;

            resetImageToPanel();

        }
        /************************窗口事件**************************/
        /**
         * 窗口变化时候
         */
        private void ImageEdit_ResizeBegin(object sender, EventArgs e)
        {
            resetImageToPanel();
        }
        /************************居中显示**************************/
        /**
         * 缩放图片到panel 居中
         */
        private static void resetImageToPanel()
        {
            int windowWidth = currentImageBox.Width;
            int windowHeight = currentImageBox.Height;

            int imageWidth = clipRect.Width;
            int imageHeight = clipRect.Height;

            if (imageWidth >= imageHeight)
            {
                if (imageWidth > windowWidth)
                {
                    double scaleHeight = (double)windowWidth / imageWidth * imageHeight;
                    currentPictureBox.Width = windowWidth;
                    currentPictureBox.Height = (int)scaleHeight;
                }
                else {
                    currentPictureBox.Width = (int)windowWidth;
                    currentPictureBox.Height = (int)windowHeight;
                }
            }
            else
            {
                if (imageHeight > windowHeight)
                {
                    double scaleWidth = (double)windowHeight / imageHeight * imageWidth;
                    currentPictureBox.Height = windowHeight;
                    currentPictureBox.Width = (int)scaleWidth;
                }
                else {
                    currentPictureBox.Width = (int)windowWidth;
                    currentPictureBox.Height = (int)windowHeight;
                }

            }

            int X = (windowWidth - currentPictureBox.Width) / 2;
            int Y = (windowHeight - currentPictureBox.Height) / 2;
            currentPictureBox.Location = new Point(X, Y);
        }
        /************************按钮事件**************************/
        // 复位
        private void reset_Click(object sender, EventArgs e)
        {
            rotaNum = 0;
            resetImageToPanel();
        }
        // 左旋
        private void roatleft_Click(object sender, EventArgs e)
        {
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum -= 5);
        }
        // 右旋
        private void roatRight_Click(object sender, EventArgs e)
        {
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum += 5);
        }
        // 旋转90度
        private void roat90_Click(object sender, EventArgs e)
        {
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum -= 90);
        }
        // 旋转180度
        private void roat180_Click(object sender, EventArgs e)
        {
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum -= 180);
        }
        // 水平镜像
        private void mirror_Click(object sender, EventArgs e)
        {
            //currentPictureBox.Image = RotateImage(sourceImage, rotaNum);
            currentPictureBox.Image = turnOverImage(currentPictureBox.Image, "mirror");
        }
        // 垂直镜像
        private void vertical_Click(object sender, EventArgs e)
        {
            //currentPictureBox.Image = RotateImage(sourceImage, rotaNum);
            currentPictureBox.Image = turnOverImage(currentPictureBox.Image, "vertical");
        }
        // 裁切
        private void clip_Click(object sender, EventArgs e)
        {
            clipMode = !clipMode;

            if (clipMode)
            {
                this.clip.BackColor = Color.Blue;
                this.clip.ForeColor = Color.White;
                this.beginPainting();

            }
            else 
            {
                this.clip.BackColor = Color.Transparent;
                this.clip.ForeColor = Color.Black;
                this.overPainting();
            }

        }
        /************************图像鼠标事件**************************/
        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (currentPictureBox.ClientRectangle.Contains(e.Location))
            {
                ((HandledMouseEventArgs)e).Handled = true;
                currentPictureBox.Focus();

                float zoomChange = e.Delta > 0 ? 2f : 0.5f;

                // Debug.WriteLine(e.Delta);

                // 计算缩放后图片的大小 
                int newWidth = (int)(currentPictureBox.Width * zoomChange);
                int newHeight = (int)(currentPictureBox.Height * zoomChange);

                int newX = 0;
                int newY = 0;

                if (e.Delta > 0)
                {
                    newX = (int)(currentPictureBox.Left - e.X);
                    newY = (int)(currentPictureBox.Top - e.Y);
                }
                else
                {
                    newX = (int)(currentPictureBox.Left + e.X / 2);
                    newY = (int)(currentPictureBox.Top + e.Y / 2);
                }

                currentPictureBox.Size = new Size(newWidth, newHeight);
                currentPictureBox.Location = new Point(newX, newY);

            }

        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected)//确定已经激发MouseDown事件，和鼠标在picturebox的范围内
            {
                currentPictureBox.Left = currentPictureBox.Left + (Cursor.Position.X - mouseDownPoint.X);
                currentPictureBox.Top = currentPictureBox.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                Cursor.Current = Cursors.SizeAll;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
        }
        /*************************裁切平面鼠标事件*************************/
        private void clip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClicked = true;
            }
        }
        private void clip_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                int dotaX = mouseDownPoint.X - Cursor.Position.X;
                int dotaY = mouseDownPoint.Y - Cursor.Position.Y;

                currentClipPanel.Left = currentClipPanel.Left - dotaX;
                currentClipPanel.Top = currentClipPanel.Top - dotaY;

                // 刷新第一、二、三、四个点位置
                int newOneX = currentClipPanel.Location.X + currentClipPanel.Width / 2 - pointRadis / 2;
                int newTwoY = currentClipPanel.Location.Y + currentClipPanel.Height / 2 - pointRadis / 2;

                OneButton.Location = new Point(newOneX, currentClipPanel.Location.Y - pointRadis / 2);
                ThreeButton.Location = new Point(newOneX, currentClipPanel.Location.Y + currentClipPanel.Height - pointRadis / 2);

                TwoButton.Location = new Point(currentClipPanel.Location.X + currentClipPanel.Width - pointRadis / 2, newTwoY);
                FourButton.Location = new Point(currentClipPanel.Location.X - pointRadis / 2, newTwoY);

                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }
        }
        private void clip_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
        }
        /*************************第一个点鼠标事件*************************/
        private void One_MouseDown(object sender, MouseEventArgs e) {
            Cursor.Current = Cursors.SizeNS;
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClickedOne = true;
            }
        }
        private void One_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClickedOne) {
                Cursor.Current = Cursors.SizeNS;
                int dotaY = mouseDownPoint.Y - Cursor.Position.Y;

                if (ThreeButton.Location.Y - OneButton.Location.Y  > 50 || dotaY > 0) {

                    // 计算高度和位置
                    currentClipPanel.Height = currentClipPanel.Height + dotaY;
                    int newY = currentClipPanel.Location.Y - dotaY;
                    currentClipPanel.Location = new Point(currentClipPanel.Location.X, newY);
                    int newPointY = OneButton.Location.Y - dotaY;
                    OneButton.Location = new Point(OneButton.Location.X, newPointY);
                    // 刷新第二、四个点位置
                    int newTwoY = currentClipPanel.Location.Y + currentClipPanel.Height / 2 - pointRadis / 2;
                    TwoButton.Location = new Point(TwoButton.Location.X, newTwoY);
                    FourButton.Location = new Point(FourButton.Location.X, newTwoY);

                    mouseDownPoint.X = Cursor.Position.X;
                    mouseDownPoint.Y = Cursor.Position.Y;

                }
            }
        }
        private void One_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isClickedOne = false;
        }
        /*************************第二个点鼠标事件*************************/
        private void Two_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClickedTwo = true;
            }

        }
        private void Two_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;

            if (isClickedTwo)
            {
                int dotaX = mouseDownPoint.X - Cursor.Position.X;
                if (TwoButton.Location.X - FourButton.Location.X > 50 || dotaX < 0) {
                    // 计算宽度和位置  
                    currentClipPanel.Width -= dotaX;

                    int newPointX = TwoButton.Location.X - dotaX;
                    TwoButton.Location = new Point(newPointX, TwoButton.Location.Y);
                    // 刷新第一、三个点位置
                    int newOneX = currentClipPanel.Location.X + currentClipPanel.Width / 2 - pointRadis / 2;
                    OneButton.Location = new Point(newOneX, OneButton.Location.Y);
                    ThreeButton.Location = new Point(newOneX, ThreeButton.Location.Y);

                    mouseDownPoint.X = Cursor.Position.X;
                    mouseDownPoint.Y = Cursor.Position.Y;
                }
            }
        }
        private void Two_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isClickedTwo = false;
        }
        /*************************第三个点鼠标事件*************************/
        private void Three_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClickedThree = true;
            }
        }
        private void Three_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeNS;
            if (isClickedThree)
            {
                int dotaY = mouseDownPoint.Y - Cursor.Position.Y;
                if (ThreeButton.Location.Y - OneButton.Location.Y > 50 || dotaY < 0)
                {
                    // 计算高度和位置
                    currentClipPanel.Height = currentClipPanel.Height - dotaY;
                    int newPointY = ThreeButton.Location.Y - dotaY;
                    ThreeButton.Location = new Point(ThreeButton.Location.X, newPointY);
                    // 刷新第二、四个点位置
                    int newTwoY = currentClipPanel.Location.Y + currentClipPanel.Height / 2 - pointRadis / 2;
                    TwoButton.Location = new Point(TwoButton.Location.X, newTwoY);
                    FourButton.Location = new Point(FourButton.Location.X, newTwoY);

                    mouseDownPoint.X = Cursor.Position.X;
                    mouseDownPoint.Y = Cursor.Position.Y;
                }
            }
        }
        private void Three_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isClickedThree = false;
        }
        /*************************第四个点鼠标事件*************************/
        private void Four_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isClickedFour = true;
            }
        }
        private void Four_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeWE;
            if (isClickedFour)
            {
                // 计算宽度和位置
                int dotaX = mouseDownPoint.X - Cursor.Position.X;
                if (TwoButton.Location.X - FourButton.Location.X > 50 || dotaX > 0)
                {
                    currentClipPanel.Width += dotaX;
                    int newX = currentClipPanel.Location.X - dotaX;
                    currentClipPanel.Location = new Point(newX, currentClipPanel.Location.Y);
                    int newPointX = FourButton.Location.X - dotaX;
                    FourButton.Location = new Point(newPointX, FourButton.Location.Y);
                    // 刷新第一、三个点位置
                    int newOneX = currentClipPanel.Location.X + currentClipPanel.Width / 2 - pointRadis / 2;
                    OneButton.Location = new Point(newOneX, OneButton.Location.Y);
                    ThreeButton.Location = new Point(newOneX, ThreeButton.Location.Y);

                    mouseDownPoint.X = Cursor.Position.X;
                    mouseDownPoint.Y = Cursor.Position.Y;
                }
            }
        }
        private void Four_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isClickedFour = false;
        }
        /***************************功能方法***********************/
        /**
         * 绘制裁切框
         */
        private void beginPainting()
        {
            Debug.WriteLine("====beginPainting====");
            OneButton.Visible = clipMode;
            TwoButton.Visible = clipMode;
            ThreeButton.Visible = clipMode;
            FourButton.Visible = clipMode;
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum);
            currentPictureBox.Refresh();

            // 初始化位置
            setDefaultClipButtonPosition();
            currentClipPanel.Visible = clipMode;
        }
        /**
         * 结束绘制
         */
        private void overPainting()
        {
            Debug.WriteLine("====overPainting====");
            OneButton.Visible = clipMode;
            TwoButton.Visible = clipMode;
            ThreeButton.Visible = clipMode;
            FourButton.Visible = clipMode;
            currentPictureBox.Image = RotateImage(sourceImage, rotaNum);
            currentImageBox.Refresh();
           
            // 裁切
            clipBitmap();
            currentClipPanel.Visible = clipMode;
        }
        /**
         * 初始化裁切框调整按钮到图像尺寸
         */
        private static void setDefaultClipButtonPosition()
        {

            int halfButton = pointRadis / 2 ;
            // 设置拖拉按钮
            one.X = currentPictureBox.Location.X + currentPictureBox.Width / 2 - halfButton;
            one.Y = currentPictureBox.Location.Y - halfButton;
            OneButton.Location = one;

            two.X = currentPictureBox.Location.X + currentPictureBox.Width - halfButton;
            two.Y = currentPictureBox.Location.Y + currentPictureBox.Height / 2 - halfButton;
            TwoButton.Location = two;

            three.X = one.X;
            three.Y = currentPictureBox.Location.Y + currentPictureBox.Height - halfButton;
            ThreeButton.Location = three;

            four.X = currentPictureBox.Location.X - halfButton;
            four.Y = two.Y;
            FourButton.Location = four;

            currentClipPanel.Location = currentPictureBox.Location;
            currentClipPanel.Width = currentPictureBox.Width;
            currentClipPanel.Height = currentPictureBox.Height;

            Graphics graphicsClip = OneButton.CreateGraphics();
            graphicsClip.DrawRectangle(clipPen, clipRect);
            graphicsClip.Dispose();
        }
        /**
         * 裁切图片
         */
        private static void clipBitmap() {

            Image srcImage = currentPictureBox.Image;
            int X = currentClipPanel.Location.X - currentPictureBox.Location.X;
            int Y = currentClipPanel.Location.Y - currentPictureBox.Location.Y;
            X = X < 0 ? 0 : X * srcImage.Width / currentPictureBox.Width;
            Y = Y < 0 ? 0 : Y * srcImage.Height / currentPictureBox.Height;
            int Width = currentClipPanel.Width * srcImage.Width / currentPictureBox.Width;
            int Height = currentClipPanel.Height * srcImage.Height / currentPictureBox.Height;

            // 创建新图位图
            Bitmap bitmap = new Bitmap(Width, Height);
            bitmap.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);
            // 创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            // 截取原图相应区域写入作图区
            Rectangle cRect = new Rectangle(X,Y, Width, Height);
            graphic.DrawImage(srcImage, 0, 0, cRect, GraphicsUnit.Pixel);
            graphic.Dispose();

            Debug.WriteLine("SourceWidth===========>" + sourceImage.Width);
            Debug.WriteLine("SourceHeight===========>" + sourceImage.Height);
            Debug.WriteLine("Width===========>" + bitmap.Width);
            Debug.WriteLine("Height===========>" + bitmap.Height);

            //从作图区生成新图
            // currentPictureBox.Image = Image.FromHbitmap( bitmap.GetHbitmap() );
            /********刷新图像*********/
            currentPictureBox.Image = bitmap;
            sourceImage = bitmap;
            rotaNum = 0;
            currentImageBox.Refresh();
            clipRect.X = 0;
            clipRect.Y = 0;
            clipRect.Width = Width;
            clipRect.Height = Height;
            /********刷新图像*********/
            resetImageToPanel();
        }
        /**
         * 旋转图像
         */
        private static Bitmap RotateImage(Image image, float angle)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            // 获取中心点  
            float halfWidth = image.Width / 2.0f;
            float halfHeight = image.Height / 2.0f;

            // 弧度转换
            angle = angle % 360;
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            int width = image.Width;
            int height = image.Height;

            // 计算旋转后的图像容器长宽
            int resultWidth = (int)(Math.Max(Math.Abs(width * cos - height * sin), Math.Abs(width * cos + height * sin)));
            int resultHeight = (int)(Math.Max(Math.Abs(width * sin - height * cos), Math.Abs(width * sin + height * cos)));

            float centerX = resultWidth / 2;
            float centerY = resultHeight / 2;

            // 设置新的图像容器
            Bitmap newImage = new Bitmap(resultWidth, resultHeight);
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics graphics = Graphics.FromImage(newImage);

            // 平移图像到中心点
            graphics.TranslateTransform(centerX, centerY);
            // 旋转
            graphics.RotateTransform(angle);
            // 再次平移回来
            graphics.TranslateTransform(-centerX, -centerY);

            // 将图片绘制到中心点上
            float resultX = centerX - halfWidth;
            float resultY = centerY - halfHeight;
            graphics.DrawImage(image, new PointF(resultX, resultY));
            graphics.Dispose();

            clipRect.X = 0;
            clipRect.Y = 0;
            clipRect.Width = resultWidth;
            clipRect.Height = resultHeight;

            resetImageToPanel();

            // 如果是裁切将显示裁切框
            /*if (clipMode)
            {
                Graphics graphicsClip = Graphics.FromImage(newImage);
                graphicsClip.DrawRectangle(rotaPen, clipRect);
                graphicsClip.Dispose();
            }*/

            /*Graphics graphicsClip = Graphics.FromImage(newImage);
            graphicsClip.DrawRectangle(rotaPen, clipRect);
            graphicsClip.Dispose();*/

            return newImage;
        }
        /**
         * 水平镜像
         */
        private static Image turnOverImage(Image image, string mode) {
            Graphics g = Graphics.FromImage(image);
            Rectangle rect = new Rectangle(0, 0, currentPictureBox.Image.Width, currentPictureBox.Image.Height);
            switch (mode) {
                case "mirror":
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case "vertical":
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
            }
            g.DrawImage(image, rect);
            g.Dispose();
            return image;
        }
    }

}
