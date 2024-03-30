namespace ImageEditer
{
    partial class ImageEdit
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.imageList = new System.Windows.Forms.Panel();
            this.imageContainer = new System.Windows.Forms.Panel();
            this.optPanel = new System.Windows.Forms.Panel();
            this.reset = new System.Windows.Forms.Button();
            this.rota180 = new System.Windows.Forms.Button();
            this.rotaLeft = new System.Windows.Forms.Button();
            this.rota90 = new System.Windows.Forms.Button();
            this.roatRight = new System.Windows.Forms.Button();
            this.vertical = new System.Windows.Forms.Button();
            this.clip = new System.Windows.Forms.Button();
            this.mirror = new System.Windows.Forms.Button();
            this.ImageBox = new System.Windows.Forms.Panel();
            this.clipPanel = new ImageEditer.ExtendedPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageArray = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.imageContainer.SuspendLayout();
            this.optPanel.SuspendLayout();
            this.ImageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.imageList);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.imageContainer);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(961, 480);
            this.splitContainer.SplitterDistance = 236;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageList.Location = new System.Drawing.Point(0, 0);
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(232, 476);
            this.imageList.TabIndex = 1;
            // 
            // imageContainer
            // 
            this.imageContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageContainer.AutoScroll = true;
            this.imageContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.imageContainer.Controls.Add(this.optPanel);
            this.imageContainer.Controls.Add(this.ImageBox);
            this.imageContainer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.imageContainer.Location = new System.Drawing.Point(0, 0);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(713, 476);
            this.imageContainer.TabIndex = 0;
            // 
            // optPanel
            // 
            this.optPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optPanel.BackColor = System.Drawing.Color.Gray;
            this.optPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.optPanel.Controls.Add(this.reset);
            this.optPanel.Controls.Add(this.rota180);
            this.optPanel.Controls.Add(this.rotaLeft);
            this.optPanel.Controls.Add(this.rota90);
            this.optPanel.Controls.Add(this.roatRight);
            this.optPanel.Controls.Add(this.vertical);
            this.optPanel.Controls.Add(this.clip);
            this.optPanel.Controls.Add(this.mirror);
            this.optPanel.Location = new System.Drawing.Point(0, 0);
            this.optPanel.Name = "optPanel";
            this.optPanel.Size = new System.Drawing.Size(713, 26);
            this.optPanel.TabIndex = 2;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(1, 0);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(60, 23);
            this.reset.TabIndex = 3;
            this.reset.Text = "复位";
            this.reset.UseVisualStyleBackColor = true;
            // 
            // rota180
            // 
            this.rota180.Location = new System.Drawing.Point(412, 0);
            this.rota180.Name = "rota180";
            this.rota180.Size = new System.Drawing.Size(72, 23);
            this.rota180.TabIndex = 9;
            this.rota180.Text = "旋转180°";
            this.rota180.UseVisualStyleBackColor = true;
            // 
            // rotaLeft
            // 
            this.rotaLeft.Location = new System.Drawing.Point(64, 0);
            this.rotaLeft.Name = "rotaLeft";
            this.rotaLeft.Size = new System.Drawing.Size(60, 23);
            this.rotaLeft.TabIndex = 1;
            this.rotaLeft.Text = "左旋";
            this.rotaLeft.UseVisualStyleBackColor = true;
            // 
            // rota90
            // 
            this.rota90.Location = new System.Drawing.Point(346, 0);
            this.rota90.Name = "rota90";
            this.rota90.Size = new System.Drawing.Size(63, 23);
            this.rota90.TabIndex = 8;
            this.rota90.Text = "旋转90°";
            this.rota90.UseVisualStyleBackColor = true;
            // 
            // roatRight
            // 
            this.roatRight.Location = new System.Drawing.Point(127, 0);
            this.roatRight.Name = "roatRight";
            this.roatRight.Size = new System.Drawing.Size(60, 23);
            this.roatRight.TabIndex = 2;
            this.roatRight.Text = "右旋";
            this.roatRight.UseVisualStyleBackColor = true;
            // 
            // vertical
            // 
            this.vertical.Location = new System.Drawing.Point(268, 0);
            this.vertical.Name = "vertical";
            this.vertical.Size = new System.Drawing.Size(75, 23);
            this.vertical.TabIndex = 7;
            this.vertical.Text = "垂直翻转";
            this.vertical.UseVisualStyleBackColor = true;
            // 
            // clip
            // 
            this.clip.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.clip.Location = new System.Drawing.Point(487, 0);
            this.clip.Name = "clip";
            this.clip.Size = new System.Drawing.Size(68, 23);
            this.clip.TabIndex = 4;
            this.clip.Text = "裁切图像";
            this.clip.UseVisualStyleBackColor = true;
            // 
            // mirror
            // 
            this.mirror.Location = new System.Drawing.Point(190, 0);
            this.mirror.Name = "mirror";
            this.mirror.Size = new System.Drawing.Size(75, 23);
            this.mirror.TabIndex = 6;
            this.mirror.Text = "水平翻转";
            this.mirror.UseVisualStyleBackColor = true;
            // 
            // ImageBox
            // 
            this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBox.AutoScroll = true;
            this.ImageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ImageBox.Controls.Add(this.clipPanel);
            this.ImageBox.Controls.Add(this.pictureBox);
            this.ImageBox.Location = new System.Drawing.Point(0, 23);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(713, 455);
            this.ImageBox.TabIndex = 5;
            // 
            // clipPanel
            // 
            this.clipPanel.BackColor = System.Drawing.Color.Transparent;
            this.clipPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.clipPanel.Location = new System.Drawing.Point(80, 9);
            this.clipPanel.Name = "clipPanel";
            this.clipPanel.Size = new System.Drawing.Size(45, 41);
            this.clipPanel.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(3, 6);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(59, 49);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // imageArray
            // 
            this.imageArray.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageArray.ImageSize = new System.Drawing.Size(16, 16);
            this.imageArray.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ImageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 480);
            this.Controls.Add(this.splitContainer);
            this.Name = "ImageEdit";
            this.Text = "图像";
            this.SizeChanged += new System.EventHandler(this.ImageEdit_ResizeBegin);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.imageContainer.ResumeLayout(false);
            this.optPanel.ResumeLayout(false);
            this.ImageBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel imageContainer;
        private System.Windows.Forms.Button rotaLeft;
        private System.Windows.Forms.Button roatRight;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button clip;
        private System.Windows.Forms.Panel ImageBox;
        private System.Windows.Forms.Panel imageList;
        private System.Windows.Forms.PictureBox pictureBox;
        private ExtendedPanel clipPanel;
        private System.Windows.Forms.Button vertical;
        private System.Windows.Forms.Button mirror;
        private System.Windows.Forms.Button rota90;
        private System.Windows.Forms.Button rota180;
        private System.Windows.Forms.Panel optPanel;
        private System.Windows.Forms.ImageList imageArray;
    }
}

