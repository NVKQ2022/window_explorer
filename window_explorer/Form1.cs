using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace window_explorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string path;
        string[] imageFiles;
        bool be_opened=false;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                    textBox1.Text = path;
                    imageFiles =null;
                    if(be_opened)
                    {
                        listBox1.Items.Clear();
                    }
                }
                imageFiles = Directory.GetFiles(path)
                        .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                be_opened = true;
                foreach (var file in imageFiles)
                {
                  
                    listBox1.Items.Add(Path.GetFileName(file));
                }
            }

            /*   
                pictureBox1.Image=System.Drawing.Image.FromFile(Path);
               int imgWidth = pictureBox1.Image.Width;
               int imgHeight = pictureBox1.Image.Height;
               int boxWidth = pictureBox1.Width;
               int boxHeight = pictureBox1.Height;

               // Calculate the aspect ratios of the image and the PictureBox
               float imgAspectRatio = (float)imgWidth / imgHeight;
               float boxAspectRatio = (float)boxWidth / boxHeight;


               float scaleFactor;
               if (imgAspectRatio > boxAspectRatio)
               {

                   scaleFactor = (float)imgWidth / boxWidth;
               }
               else
               {

                   scaleFactor = (float)imgHeight / boxHeight;
               }
               pictureBox1.Width = (int)(imgWidth / scaleFactor);
               pictureBox1.Height = (int)(imgHeight / scaleFactor);

               // Set the image size mode to stretch the image properly
               pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Width= (int)(pictureBox1.Width*1.1);
            pictureBox1.Height = (int)(pictureBox1.Height * 1.1);
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int newX = (formWidth - pictureBox1.Width) / 2;
            int newY = (formHeight - pictureBox1.Height) / 2;

            // Set the new location of the PictureBox
           // pictureBox1.Location = new Point(newX, newY);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = (int)(pictureBox1.Width * 0.9);
            pictureBox1.Height = (int)(pictureBox1.Height * 0.9);
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int newX = (formWidth - pictureBox1.Width) / 2 ;
            int newY = (formHeight - pictureBox1.Height) / 2;

            // Set the new location of the PictureBox
            //pictureBox1.Location = new Point(newX, newY);
        }

        private void picture_Enter(object sender, EventArgs e)
        {

        }
        private Image LoadResizedImage(string imagePath, int maxWidth, int maxHeight)
        {
            using (Image originalImage = Image.FromFile(imagePath))
            {
                // Calculate new size while maintaining the aspect ratio
                double ratioX = (double)maxWidth / originalImage.Width;
                double ratioY = (double)maxHeight / originalImage.Height;
                double ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(originalImage.Width * ratio);
                int newHeight = (int)(originalImage.Height * ratio);

                // Create a new bitmap with the new size
                Bitmap resizedImage = new Bitmap(newWidth, newHeight);

                // Draw the original image into the new bitmap, scaling it down
                using (Graphics graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }

                return resizedImage;
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                // Get the full file path of the selected image
                string selectedImagePath = imageFiles[selectedIndex];

                // Load and display the selected image in the PictureBox
                using (Image img = Image.FromFile(selectedImagePath))
                {
                    // Optional: Resize PictureBox to fit the image or adjust the PictureBox's SizeMode
                    pictureBox1 .Image = LoadResizedImage(selectedImagePath, 800, 600);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Or other modes like Zoom, AutoSize, etc.

                }
            }

        }
    }
}
