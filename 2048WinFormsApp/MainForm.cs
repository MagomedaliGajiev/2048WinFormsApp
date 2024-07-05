namespace _2048WinFormsApp
{
    public partial class MainForm : Form
    {
        private const int _labelSize = 150;
        private const int _padding = 6;
        private const int _startX = 10;
        private const int _startY = 150;

        private int _mapSize = 4;
        private Label[,] _labelsMap;
        private int _score = 0;
        private int _bestScore = 0;
        private string userName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var startForm = new StartForm();
            startForm.ShowDialog();
            userName = startForm.userNameTextBox.Text;

            SetMapSize(startForm.RadioButtons);

            InitMap();
            GenerateNumber();
            ShowScore();
            CalculateBestScore();
        }

        private void SetMapSize(List<RadioButton> radioButtons)
        {
            foreach (var button in radioButtons)
            {
                if (button.Checked)
                {
                    _mapSize = Convert.ToInt32(button.Text[0].ToString());
                    break;
                }
            }
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
            ClientSize = new Size(_startX + (_labelSize + _padding) * _mapSize, _startY + (_labelSize + _padding) * _mapSize);

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
                var randomNumberLabel = random.Next(_mapSize * _mapSize);
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
            label.Size = new Size(_labelSize, _labelSize);
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = _startX + indexColumn * (_labelSize + _padding);
            int y = _startY + indexRow * (_labelSize + _padding);
            label.Location = new Point(x, y);

            label.TextChanged += Label_TextChanged;
            return label;
        }

        private void Label_TextChanged(object? sender, EventArgs e)
        {
            var label = (Label)sender;
            switch (label.Text)
            {
                case "": label.BackColor = SystemColors.ButtonShadow; break;
                case "2": label.BackColor = Color.FromArgb(238, 228, 218); break;
                case "4": label.BackColor = Color.FromArgb(237, 224, 200); break;
                case "8": label.BackColor = Color.FromArgb(242, 177, 121); break;
                case "16": label.BackColor = Color.FromArgb(245, 149, 99); break;
                case "32": label.BackColor = Color.FromArgb(246, 124, 95); break;
                case "64": label.BackColor = Color.FromArgb(246, 94, 59); break;
                case "128": label.BackColor = Color.FromArgb(249, 246, 242); break;
                case "256": label.BackColor = Color.FromArgb(237, 204, 97); break;
                case "512": label.BackColor = Color.FromArgb(249, 246, 242); break;
                case "1024": label.BackColor = SystemColors.ButtonShadow; break;
                case "2048": label.BackColor = SystemColors.ButtonShadow; break;

            }
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
                UserManager.Add(new User() { Name = userName, Score = _score });
                MessageBox.Show("���! �� ��������");
                return;
            }

            if (EndGame())
            {
                UserManager.Add(new User() { Name = userName, Score = _score });
                MessageBox.Show("� ��������� �� ���������!");
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

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("������� ����!");
        }

        private void ������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultsForm = new ResultsForm();
            resultsForm.ShowDialog();
        }
    }
}
