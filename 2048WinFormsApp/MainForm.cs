namespace _2048WinFormsApp
{
    public partial class MainForm : Form
    {
        private int _mapSize = 4;
        private Label[,] _labelsMap;
        private static Random _random = new Random();
        private int _score = 0;
        private int _bestScore = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitMap();
            GenerateNumber();
            ShowScore();
            CalculateBestScore();
        }

        private void CalculateBestScore()
        {
            var users = UserManager.GetAll();
            if (users.Count == 0)
            {
                return;
            }

            _bestScore= users[0].Score;
            foreach (var user in users)
            {
                if (user.Score > _bestScore)
                {
                    _bestScore = user.Score;
                }
            }

            ShowBestScore();
        }

        private void InitMap()
        {
            _labelsMap = new Label[_mapSize, _mapSize];

            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    _labelsMap[i, j] = newLabel;
                }
            }
        }

        private void ShowScore()
        {
            scoreLabel.Text = _score.ToString();
        }

        private void ShowBestScore()
        {
            if (_score > _bestScore)
            {
                _bestScore = _score;
            }
            bestScoreLabel.Text = _bestScore.ToString();
        }

        private void GenerateNumber()
        {
            var random = new Random();
            while (true)
            {
                var randomNumberLabel = _random.Next(_mapSize * _mapSize);
                var indexRow = randomNumberLabel / _mapSize;
                var indexColumn = randomNumberLabel % _mapSize;
                if (_labelsMap[indexRow, indexColumn].Text == string.Empty)
                {
                    var randomNumber = random.Next(1, 101);
                    if (randomNumber <= 75)
                    {
                        _labelsMap[indexRow, indexColumn].Text = "2";
                    }
                    else
                    {
                        _labelsMap[indexRow, indexColumn].Text = "4";
                    }
                    _labelsMap[indexRow, indexColumn].Text = "2";
                }
                break;
            }
        }
        private Label CreateLabel(int indexRow, int indexColumn)
        {
            var label = new Label();
            label.BackColor = SystemColors.ButtonShadow;
            label.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label.Size = new Size(150, 150);
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = 10 + indexColumn * (150 + 6);
            int y = 150 + indexRow * (150 + 6);
            label.Location = new Point(x, y);

            return label;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();

            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }

            GenerateNumber();
            ShowScore();
            ShowBestScore();

            if (Win())
            {
                UserManager.Add(new User() { Name = "test" + _score, Score = _score });
                MessageBox.Show("”‡! ¬˚ ÔÓ·Â‰ËÎË");
                return;
            }

            if (EndGame())
            {
                UserManager.Add(new User() { Name = "test" + _score, Score = _score });
                MessageBox.Show("  ÒÓÊ‡ÎÂÌË˛ ‚˚ ÔÓË„‡ÎË!");
                return;
            }
        }

        private bool EndGame()
        {
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (_labelsMap[i, j].Text == "")
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < _mapSize - 1; i++)
            {
                for (int j = 0; j < _mapSize  - 1; j++)
                {
                    if (_labelsMap[i, j].Text == _labelsMap[i, j + 1].Text || _labelsMap[i, j].Text == _labelsMap[i + 1, j].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool Win()
        {
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (_labelsMap[i, j].Text == "2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void MoveDown()
        {
            for (int j = 0; j < _mapSize; j++)
            {
                for (int i = _mapSize - 1; i >= 0; i--)
                {

                    if (_labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (_labelsMap[k, j].Text != string.Empty)
                            {
                                if (_labelsMap[i, j].Text == _labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(_labelsMap[i, j].Text);
                                    _score += number * 2;
                                    _labelsMap[i, j].Text = (number * 2).ToString();
                                    _labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            for (int j = 0; j < _mapSize; j++)
            {
                for (int i = _mapSize - 1; i >= 0; i--)
                {
                    if (_labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (_labelsMap[k, j].Text != string.Empty)
                            {
                                _labelsMap[i, j].Text = _labelsMap[k, j].Text;
                                _labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveUp()
        {
            for (int j = 0; j < _mapSize; j++)
            {
                for (int i = 0; i < _mapSize; i++)
                {

                    if (_labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i + 1; k < _mapSize; k++)
                        {
                            if (_labelsMap[k, j].Text != string.Empty)
                            {
                                if (_labelsMap[i, j].Text == _labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(_labelsMap[i, j].Text);
                                    _score += number * 2;
                                    _labelsMap[i, j].Text = (number * 2).ToString();
                                    _labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            for (int j = 0; j < _mapSize; j++)
            {
                for (int i = 0; i < _mapSize; i++)
                {
                    if (_labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i + 1; k < _mapSize; k++)
                        {
                            if (_labelsMap[k, j].Text != string.Empty)
                            {
                                _labelsMap[i, j].Text = _labelsMap[k, j].Text;
                                _labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveLeft()
        {
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (_labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j + 1; k < _mapSize; k++)
                        {
                            if (_labelsMap[i, k].Text != string.Empty)
                            {
                                if (_labelsMap[i, j].Text == _labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(_labelsMap[i, j].Text);
                                    _score += number * 2;
                                    _labelsMap[i, j].Text = (number * 2).ToString();
                                    _labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = 0; j < _mapSize; j++)
                {
                    if (_labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j + 1; k < _mapSize; k++)
                        {
                            if (_labelsMap[i, k].Text != string.Empty)
                            {
                                _labelsMap[i, j].Text = _labelsMap[i, k].Text;
                                _labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveRight()
        {
            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = _mapSize - 1; j >= 0; j--)
                {
                    if (_labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (_labelsMap[i, k].Text != string.Empty)
                            {
                                if (_labelsMap[i, j].Text == _labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(_labelsMap[i, j].Text);
                                    _score += number * 2;
                                    _labelsMap[i, j].Text = (number * 2).ToString();
                                    _labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < _mapSize; i++)
            {
                for (int j = _mapSize - 1; j >= 0; j--)
                {
                    if (_labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (_labelsMap[i, k].Text != string.Empty)
                            {
                                _labelsMap[i, j].Text = _labelsMap[i, k].Text;
                                _labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void ÂÒÚ‡ÚToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ‚˚ıÓ‰ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Ô‡‚ËÎ‡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("œ‡‚ËÎ‡ Ë„˚!");
        }

        private void ÔÓÍ‡Á‡Ú¸–ÂÁÛÎ¸Ú‡Ú˚ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultsForm = new ResultsForm();
            resultsForm.ShowDialog();
        }
    }
}
