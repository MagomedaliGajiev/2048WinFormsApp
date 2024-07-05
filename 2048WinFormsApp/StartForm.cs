namespace _2048WinFormsApp
{
    public partial class StartForm : Form
    {
        public List<RadioButton> RadioButtons { get; set; }
        public StartForm()
        {
            InitializeComponent();

            RadioButtons = new List<RadioButton>
            {
                radioButton1, radioButton2, radioButton3, radioButton4
            };
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
