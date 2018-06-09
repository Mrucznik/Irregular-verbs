using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for LearnView.xaml
    /// </summary>
    public partial class LearnView
    {
        private Verb[] _verbs;
        private int _activeWord;
        private int _verbsDone;
        private int _verbsGood;

        public LearnView()
        {
            InitializeComponent();

            var db = new VerbsDbContext();
            _verbs = db.Verbs.ToArray();
            
            if (_verbs.Length == 0)
            {
                MessageTextBlock.Text = "Empty verbs database.";
                return;
            }

            Shuffle();
            _activeWord = 0;
            GetNextVerb();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (_verbs.Length == 0)
                return;
            CheckVerb();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_verbs.Length == 0)
                return;
            GetNextVerb();
        }

        private void Shuffle()
        {
            Random random = new Random();
            _verbs = _verbs.OrderBy(x => random.Next()).ToArray();
        }

        private void GetNextVerb()
        {
            if (_activeWord == _verbs.Length)
            {
                Shuffle();
                MessageTextBlock.Text = "All verbs done, but not all correct. Now only incorrect verbs.";
                _activeWord = 0;
            }

            int good = 0;
            while (_verbs[_activeWord].Good)
            {
                if (good == _verbs.Length)
                {
                    MessageTextBlock.Text = "All verbs done, all verbs correct! Congratulations!";
                    break;
                }
                good++;
                _activeWord++;
                if (_activeWord == _verbs.Length)
                    _activeWord = 0;
            }

            VerbTextBlock.Text = _verbs[_activeWord].BaseForm;
            _activeWord++;

            VerbForm1CheckBox.IsChecked = VerbForm2CheckBox.IsChecked = false;
            CorrectVerbForm1TextBlock.Text = CorrectVerbForm2TextBlock.Text = VerbForm1TextBox.Text = VerbForm2TextBox.Text = "";
            NextButton.IsEnabled = false;
        }

        private void CheckVerb()
        {
            _activeWord--;
            string[] words1 = _verbs[_activeWord].PastSimple.Split('/');
            string[] words2 = _verbs[_activeWord].PastParticiple.Split('/');
            int verbsGoodOld = _verbsGood;
            if ( words1.Length == 2 && (words1[0] == VerbForm1TextBox.Text || words1[1] == VerbForm1TextBox.Text) || words1[0] == VerbForm1TextBox.Text)
            {
                VerbForm1CheckBox.IsChecked = true;
                CorrectVerbForm1TextBlock.Foreground = new SolidColorBrush(Colors.Green);
                _verbsGood++;
            }
            else
            {
                VerbForm1CheckBox.IsChecked = false;
                CorrectVerbForm1TextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (words2.Length == 2 && (words2[0] == VerbForm2TextBox.Text || words2[1] == VerbForm2TextBox.Text) || words2[0] == VerbForm2TextBox.Text)
            {
                VerbForm2CheckBox.IsChecked = true;
                CorrectVerbForm2TextBlock.Foreground = new SolidColorBrush(Colors.Green);
                _verbsGood++;
            }
            else
            {
                VerbForm2CheckBox.IsChecked = false;
                CorrectVerbForm2TextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (verbsGoodOld + 2 == _verbsGood)
                _verbs[_activeWord].Good = true;

            CorrectVerbForm1TextBlock.Text = _verbs[_activeWord].PastSimple;
            CorrectVerbForm2TextBlock.Text = _verbs[_activeWord].PastParticiple;
            _activeWord++;
            _verbsDone+=2;
            SummaryTextBlock.Text = $"{_verbsGood}/{_verbsDone}";
            NextButton.IsEnabled = true;
        }
    }
}
