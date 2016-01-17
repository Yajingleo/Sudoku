using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        private Random Rand { get; set; }
        private List<char[][]> Solutions;
        private Button[,] Buttons { get; set; }
        private char[][] Board;
        private Tuple<int, int> position { get; set; }
        private bool game_started { get; set; }
        private SolutionSudoku Generator { get; set; }
        private bool finished_board { get; set; }

        public Form1()
        {
            InitializeComponent();

            Buttons = new Button[9,9];
            if (Buttons == null) MessageBox.Show("First, the Buttons are empty");
            for (int i = 0; i < 9; i++){
                for (int j=0; j<9; j++)
                {
                     Button newButton = new Button();
                     newButton.Width = 52;
                     newButton.Height = 52;
                     newButton.Left = 50 * i;
                     newButton.Top = 50 * j;
                     Buttons[i, j] = new Button ();
                     Buttons[i, j] = newButton;
                     this.Controls.Add(Buttons[i,j]);
                }
            }
            if (Buttons == null) MessageBox.Show("Buttons are empty");
            Generator= new SolutionSudoku();
            Solutions = new List<char[][]>();
            Solutions = Generator.solveSudoku();
            Rand= new Random();
            position = Tuple.Create(-1, -1);
            game_started = false;
            finished_board = true;
        }

        

       
/// <summary>
/// Events
/// <param name="sender"></param>
/// <param name="e"></param>
/// 
        private void button1_Click(object sender, EventArgs e)
        {
           

            if (comboBox1.SelectedItem == null)
                MessageBox.Show("Choose a difficulty level!");
            else
            {
                game_started = true;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Buttons[i, j].Enabled = true;
                    }
                }
                int Sol_Num = Rand.Next(1, 1000);
                Board = new char[9][];
                Board = Solutions[Sol_Num];

                string Level = "";
                Level = comboBox1.SelectedItem.ToString();

                int Num=0;

                if (Level == "Easy")
                    Num = Rand.Next(6,12);
                if (Level == "Medium")
                    Num = Rand.Next(15, 17);
                if (Level == "Hard")
                    Num = Rand.Next(25, 35);
                
                HashSet<Tuple<int,int>> Positions = new HashSet<Tuple<int,int>>();
                Positions = RandomPositions(Num);
                
                for (int i=0; i<9; i++)
                {
                    for (int j=0; j<9; j++)
                    {
                        var Q =Tuple.Create( i, j );
                        if (!Positions.Contains(Q))
                        {
                            Buttons[i, j].Text = Board[i][j].ToString();
                            Buttons[i, j].Enabled = false;
                        }
                        else
                        {
                            Buttons[i, j].Text = "";
                            Board[i][j] = '*';
                            Buttons[i, j].Click += new EventHandler(button_Click);  

                        }
                    }
                }
              
            }
        }

        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // identify which button was clicked and perform necessary actions
            MessageBox.Show("Choose a number from 1 to 9 using the lower right box.");
            comboBox2.DroppedDown = true;
            position = Tuple.Create(button.Left/50, button.Top/50);

        }


        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose the level fromt the difficulties menu below.");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!game_started)
            {
                MessageBox.Show("Start a new game.");
                return;
            }
            else
            {
                if (position.Item1 == -1)
                {
                    MessageBox.Show("Click an empty cell.");
                }
                else
                {
                    //MessageBox.Show("Change the number at row " + (position.Item2+1).ToString() + " and column " + (position.Item1+1).ToString()+" to the selected number.");
                    Board[position.Item1][position.Item2] = Convert.ToChar('1' + comboBox2.SelectedIndex);
                    Buttons[position.Item1, position.Item2].Text = comboBox2.SelectedItem.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!game_started)
            {
                MessageBox.Show("Start a game!");
                return;
            }

            bool succeeded= checkSolution(ref Board);
            if (succeeded)
            {
                MessageBox.Show("Congratulations! You succeed!");
            }
            else
            {
                if (!finished_board)
                    MessageBox.Show("There are empty cells in the board.");
                else
                {
                    MessageBox.Show("Sorry! The answer is wrong. Keep tring!");
                }
            }
        }

/// <summary>
/// Internal Methods 
/// </summary>
/// <param name="board"></param>
/// <returns></returns>
/// 
        private HashSet<Tuple<int, int>> RandomPositions(int Num)
        {

            HashSet<Tuple<int, int>> used = new HashSet<Tuple<int, int>>();
            while (used.Count < Num)
            {
                int i = Rand.Next(0, 8), j = Rand.Next(0, 8);
                var P = Tuple.Create(i, j);
                used.Add(P);
            }
            return used;
        }

        public bool checkSolution(ref char[][] board)
        {
            if (!game_started) return false;
            finished_board = true;
            for (int i = 0; i < 9; i++)
            {
                
                bool[] row = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '*')
                    {
                       // MessageBox.Show("Choose a number at row " + (j + 1).ToString() + " and column " + (i + 1).ToString() + ".");
                        finished_board = false;
                        return false;
                    }
                    if (row[board[i][j] - '1']) return false;
                    else row[board[i][j] - '1'] = true;
                }
            }

            for (int j = 0; j < 9; j++)
            {
                bool[] row = new bool[9];
                for (int i = 0; i < 9; i++)
                {
                    if (row[board[i][j] - '1']) return false;
                    else row[board[i][j] - '1'] = true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool[] box = new bool[9];
                    for (int i1 = 3 * i; i1 < 3 * i + 3; i1++)
                    {
                        for (int j1 = 3 * j; j1 < 3 * j + 3; j1++)
                        {
                            if (box[board[i1][j1] - '1']) return false;
                            else box[board[i1][j1] - '1'] = true;
                        }
                    }
                }
            }
            
            return true;
        }
    }



    class SolutionSudoku
    {
        

        public List<char[][]> solveSudoku()
        {
            List<char[][]> result = new List<char[][]>();
            char[][] board = new char[][]
                        {
                             "123456789".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray(),
                             ".........".ToCharArray()
                        };

            dfs(ref board, 0, 0, ref result);
            return result;
        }

        public void dfs(ref char[][] board, int i, int j, ref List<char[][]> result)
        {
            if (result.Count > 1000)
            {
                return;
            }

            if (i == 9)
            {
                char[][] copy = new char[9][];
                for (int k = 0; k < 9; k++)
                {
                    copy[k] = new char[9];
                    for (int h = 0; h < 9; h++)
                    {
                        copy[k][h] = board[k][h];
                    }
                }
                result.Add(copy);
                return;
            }

            int i1, j1;
            if (j == 8)
            {
                i1 = i + 1;
                j1 = 0;
            }
            else
            {
                i1 = i;
                j1 = j + 1;
            }

            if (board[i][j] != '.') dfs(ref board, i1, j1, ref result);

            else
            {
                for (int k = 0; k < 9; k++)
                {
                    board[i][j] = Convert.ToChar('1' + k);
                    if (checkat(ref board, i, j)) dfs(ref board, i1, j1, ref result);
                    board[i][j] = '.';
                }
            }
        }

        public bool checkat(ref char[][] board, int i, int j)
        {
            for (int x = 0; x < 9; x++)
            {
                if (i != x && board[i][j] == board[x][j]) return false;
            }

            for (int y = 0; y < 9; y++)
            {
                if (j != y && board[i][j] == board[i][y]) return false;
            }

            for (int x = 3 * (i / 3); x < 3 * (i / 3) + 3; x++)
            {
                for (int y = 3 * (j / 3); y < 3 * (j / 3) + 3; y++)
                {
                    if ((i != x || j != y) && board[i][j] == board[x][y]) return false;
                }
            }

            return true;
        }

        
    }

}
