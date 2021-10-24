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
    /// Interaction logic for CameraPage.xaml
    /// </summary>
    public partial class CameraPage : UserControl
    {
        private int lessonNum;
        private bool isProblem;

        public CameraPage(int lessonNum, bool isProblem)
        {
            InitializeComponent();
            this.lessonNum = lessonNum;
            this.isProblem = isProblem;
        }

        public delegate void CameraBackClick(int lessonNum, bool isProblem);
        public event CameraBackClick cameraBackClickEvent;

        public delegate void SubmitButtonClick(int lessonNum, bool isProblem);
        public event SubmitButtonClick submitButtonClickEvent;

        private void CameraBackButton_Click(object sender, RoutedEventArgs e)
        {
            cameraBackClickEvent(lessonNum, isProblem);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            submitButtonClickEvent(lessonNum, isProblem);
        }
    }
}
