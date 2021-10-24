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
    /// Interaction logic for Problems.xaml
    /// </summary>
    public partial class ProblemList : UserControl
    {
        public ProblemList()
        {
            InitializeComponent();
        }

        public delegate void ProblemsNavToDashboard();
        public event ProblemsNavToDashboard problemsNavToDashboardEvent;

        public delegate void ProblemsNavToLessons();
        public event ProblemsNavToLessons problemsNavToLessonsEvent;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            problemsNavToLessonsEvent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            problemsNavToDashboardEvent();
        }


    }
}
