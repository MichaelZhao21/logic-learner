using System;
using System.Collections.Generic;
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
    /// Interaction logic for Lessons.xaml
    /// </summary>
    public partial class LessonList : UserControl
    {
        public LessonList()
        {
            InitializeComponent();
        }

        public delegate void LessonsNavToDashboard();
        public event LessonsNavToDashboard lessonsNavToDashboardEvent;

        public delegate void LessonsNavToProblems();
        public event LessonsNavToProblems lessonsNavToProblemsEvent;

        public delegate void StartLesson(int num, bool problem);
        public event StartLesson startLessonEvent;

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lessonsNavToDashboardEvent();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            lessonsNavToProblemsEvent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startLessonEvent(0, false); // Starts lesson 0 (1)
        }
    }
}
