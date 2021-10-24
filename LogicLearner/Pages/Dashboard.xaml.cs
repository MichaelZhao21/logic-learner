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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private int numLessons;
        private int numProblems;
        private int totalLessons;
        private int totalProblems;

        public Dashboard(int numLessons, int numProblems, int totalLessons, int totalProblems)
        {
            InitializeComponent();
            this.numLessons = numLessons;
            this.numProblems = numProblems;
            this.totalLessons = totalLessons;
            this.totalProblems = totalProblems;
        }

        public delegate void NextLessonClick(int lessonNum, bool isProblem);
        public event NextLessonClick nextLessonClickEvent;

        public delegate void DashboardNavToLessons();
        public event DashboardNavToLessons dashboardNavToLessonsEvent;

        public delegate void DashboardNavToProblems();
        public event DashboardNavToProblems dashboardNavToProblemsEvent;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dashboardNavToProblemsEvent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            nextLessonClickEvent(numLessons, false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dashboardNavToLessonsEvent();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dashboardNavToProblemsEvent();
        }
    }
}
