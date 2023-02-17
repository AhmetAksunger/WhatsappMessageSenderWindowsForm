using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WindowsInput;
using static System.Net.Mime.MediaTypeNames;

namespace WhatsappMessageSenderWindowsForm
{
    public partial class Form1 : Form
    {
        bool check = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Amount.Text == "")
                {
                    MessageBox.Show("Please enter a message amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (richtxt_Message.Text == "")
                {
                    MessageBox.Show("Please enter a message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                else if (txt_SleepTime.Text == "")
                {
                    MessageBox.Show("Please enter a sleep time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                else
                {
                    List<string> messageList = new List<string>();
                    messageList.AddRange(richtxt_Message.Lines);
                    MessageBox.Show("Message sender will start in 5 seconds\nPlease open WhatsappWeb","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Thread.Sleep(5000);
                    StartSending(messageList);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occured. Please try again","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void StartSending(List<string> list)
        {
            try
            {
                int i = 0;
                string formattedSleepTimeText = txt_SleepTime.Text.Replace('.', ',');
                double sleepTime = Convert.ToDouble(formattedSleepTimeText) * 1000;


                int messageAmount = int.Parse(txt_Amount.Text);
                int ma = messageAmount;
                while (check && messageAmount != 0)
                {
                    InputSimulator inputSimulator = new InputSimulator();
                    inputSimulator.Keyboard.TextEntry(list[i]);
                    Thread.Sleep(Convert.ToInt32(sleepTime));
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                    messageAmount--;

                    if (i < list.Count - 1)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }

                }
                MessageBox.Show("Successfully sent " + ma.ToString() + " messages");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
     
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            check = false;
        }
    }
}
