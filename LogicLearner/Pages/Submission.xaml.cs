using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicLearner.Pages
{
    /// <summary>
    /// Interaction logic for Submission.xaml
    /// </summary>
    public partial class Submission : UserControl
    {
        private int lessonNum;
        private bool isProblem;

        public Submission(int lessonNum, bool isProblem, bool solution)
        {
            Debug.WriteLine(solution);
            InitializeComponent();
            this.lessonNum = lessonNum;
            this.isProblem = isProblem;
        }

        public delegate void SubmitNextLessonButtonClick(int lessonNum, bool isProblem);
        public event SubmitNextLessonButtonClick submitNextLessonButtonClickEvent;

        private void SubmitNextLesson_Click(object sender, RoutedEventArgs e)
        {
            submitNextLessonButtonClickEvent(lessonNum + 1, isProblem);
        }
    }
}
