using System.Drawing;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace Chess_Adative_AI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //private void pegBoard_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    {
        //        using (SolidBrush brush = new SolidBrush(Color.DarkGray))
        //        {
        //            for (int i = 0; i < 50; i++)
        //            {
        //                brush.Color = GetPegColour(Data[i]);

        //                int col = i % 5;
        //                int row = i / 5;
        //                int x = 22 + (col * 30);
        //                int y = 393 - (row * 40);
        //                g.FillEllipse(brush, x, y, 15, 15);
        //            }
        //        }
        //    }
        //}


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        
        #endregion

        private Button start_button;
    }
}

