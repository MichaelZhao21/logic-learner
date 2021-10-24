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
    /// Interaction logic for Lesson.xaml
    /// </summary>
    public partial class LessonPage : UserControl
    {
        private int lessonNum;
        private bool isProblem;

        public LessonPage(int lessonNum, bool isProblem)
        {
            InitializeComponent();
            this.lessonNum = lessonNum;
            this.isProblem = isProblem;
        }

        public delegate void LessonBackClick();
        public event LessonBackClick lessonBackClickEvent;

        public delegate void SketchButtonClick(int lessonNum, bool isProblem);
        public event SketchButtonClick sketchButtonClickEvent;

        private void SketchButton_Click(object sender, RoutedEventArgs e)
        {
            sketchButtonClickEvent(lessonNum, isProblem);
        }

        private void NextLessonButton_Click(object sender, RoutedEventArgs e)
        {
            lessonNum++;
            // TODO: Increment lesson by changing content or smth
        }

        private void LessonBack_Click(object sender, RoutedEventArgs e)
        {
            lessonBackClickEvent();
        }
    }
}
