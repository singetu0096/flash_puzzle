using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool flag = true;

        private System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();

        public int click_counter = 0;

        private Button[] button;

        private bool[] state = new bool[25];

        private char[] Congratulations = {'C','o','n','g','r','a','t','u','l','a','t','i','o','n','s','!','!','!','!','!','!','!','!','!','!'};

        public Form1()
        {
            InitializeComponent();
            button = new Button[25]
            {
                this.button1, this.button2, this.button3, this.button4, this.button5, 
                this.button6, this.button7, this.button8, this.button9, this.button10,
                this.button11, this.button12, this.button13, this.button14, this.button15, 
                this.button16, this.button17, this.button18, this.button19, this.button20,
                this.button21, this.button22, this.button23, this.button24, this.button25
            };

            for (int i = 0; i < state.Length; i++)
            {
                state[i] = true;
            }


            state[6] = state[8] = state[12] = state[16] = state[18] = false;

            update();

        }

        private void update()
        {
            for (int i = 0; i < button.Length; i++)
            {
                if (state[i])
                {
                    button[i].BackColor = Color.FromArgb(64, 64, 128);
                }
                else
                {
                    button[i].BackColor = Color.FromArgb(100, 180, 120);
                }
            }
        }


        private void button_Click(object sender, EventArgs e)
        {
            click_counter++;

            if (flag)
            {
                time.Start();
                flag = false;
            }
            
            int click_point = 0;
            for (int i = 0; i < 25; i++)
            {
                if (sender == button[i])
                {
                    click_point = i;
                }
            }
            push(click_point);
            update();
            check(state.Length - 1);
        }
        
        private void push(int click_point)
        {
            int row = click_point % 5;
            int line = click_point / 5;
            int[] changerow_array = { 0, 1, -1, 0, 0 };
            int[] changeline_array2 = { 0, 0, 0, 1, -1 };
            for (int i = 0; i < 5; i++)
            {
                int change_row = row + changerow_array[i];
                int change_line = line + changeline_array2[i];

                if (0 <= change_row && change_row < 5 && 0 <= change_line && change_line < 5)
                {
                    state[change_line * 5 + change_row] = !state[change_line * 5 + change_row];
                }
            }
        }

        private void check(int i)
        {
            if (i != 0 && !state[i])
            {
                check(i - 1);
            }
            else if(i == 0 && !state[i])
            {
                time.Stop();

                count();
            }
        }

        public async void count()
        {
            for (int i = 0; i < button.Length; i++)
            {
                button[i].Font = new Font("MS UI Gothic", 27.75f, FontStyle.Bold, GraphicsUnit.Point, 128);
                button[i].Text = Congratulations[i].ToString();

                await Task.Delay(50);

                if (i == button.Length - 1)
                {
                    await Task.Delay(500);

                    score();
                }
            }

        }

        public async void score()
        {
            TimeSpan msec = time.Elapsed;
            string result;
            int score = (int)Math.Round(msec.TotalMilliseconds);
            score = score * click_counter;
            result = score.ToString().PadLeft(20, '0');

            var timescore = result.ToCharArray();
            char[] score_array = { 'S', 'c', 'o', 'r', 'e' };

            for(int i = 0; i < button.Length - 20; i++)
            {
                button[i].Text = score_array[i].ToString();

                await Task.Delay(50);

                if(i == button.Length - 21)
                {
                    await Task.Delay(500);

                    for (int j = 5; j < button.Length; j++)
                    {
                        button[j].Font = new Font("MS UI Gothic", 27.75f, FontStyle.Bold, GraphicsUnit.Point, 128);
                        button[j].Text = timescore[j - 5].ToString();

                        await Task.Delay(75);
                    }
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

    }
}
