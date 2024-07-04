namespace _2048WinFormsApp
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
        }

        private void ResultsForm_Load(object sender, EventArgs e)
        {
            var users = UserManager.GetAll();

            foreach (var user in users)
            {
                resultsDataGridView.Rows.Add(user.Name, user.Score);
            }
        }
    }
}
