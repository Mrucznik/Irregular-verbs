using System.Windows;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView
    {
        private readonly VerbsDbContext _db;

        public AddView()
        {
            _db = new VerbsDbContext();
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _db.Verbs.Add(new Verb
            {
                BaseForm = VerbFrom1.Text,
                PastSimple = VerbForm2.Text,
                PastParticiple = VerbForm3.Text
            });
            _db.SaveChanges();

            VerbFrom1.Clear();
            VerbForm2.Clear();
            VerbForm3.Clear();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
