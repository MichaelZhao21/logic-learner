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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int completedLessons = 0;
        private List<int> completedProblems = new List<int>();
        private int totalLessons = 0;
        private int totalProblems = 0;

        public MainWindow()
        {
            InitializeComponent();
            Welcome welcome = new Welcome();
            this.Content = welcome.Content;
            welcome.welcomePageClickEvent += new LogicLearner.Pages.Welcome.WelcomePageClick(openDashboard);
        }

        private void openDashboard()
        {
            Dashboard dashboard = new Dashboard(completedLessons, completedProblems.Count, totalLessons, totalProblems);
            this.Content = dashboard.Content;
            dashboard.nextLessonClickEvent += new LogicLearner.Pages.Dashboard.NextLessonClick(openLesson);
            dashboard.dashboardNavToLessonsEvent += new LogicLearner.Pages.Dashboard.DashboardNavToLessons(openLessonsList);
            dashboard.dashboardNavToProblemsEvent += new LogicLearner.Pages.Dashboard.DashboardNavToProblems(openProblemsList);
        }

        private void openLessonsList()
        {
            LessonList lessonList = new LessonList();
            this.Content = lessonList.Content;
            lessonList.lessonsNavToDashboardEvent += new LogicLearner.Pages.LessonList.LessonsNavToDashboard(openDashboard);
            lessonList.lessonsNavToProblemsEvent += new LogicLearner.Pages.LessonList.LessonsNavToProblems(openProblemsList);
            lessonList.startLessonEvent += new LogicLearner.Pages.LessonList.StartLesson(openLesson);
        }

        // Also same as problem
        private void openLesson(int lessonNum, bool isProblem)
        {
            LessonPage lessonPage = new LessonPage(lessonNum, isProblem);
            this.Content = lessonPage.Content;
            lessonPage.lessonBackClickEvent += new LogicLearner.Pages.LessonPage.LessonBackClick(openLessonsList);
            lessonPage.sketchButtonClickEvent += new LogicLearner.Pages.LessonPage.SketchButtonClick(openCamera);
        }

        private void openProblemsList()
        {
            ProblemList problemList = new ProblemList();
            this.Content = problemList.Content;
            problemList.problemsNavToDashboardEvent += new LogicLearner.Pages.ProblemList.ProblemsNavToDashboard(openDashboard);
            problemList.problemsNavToLessonsEvent += new LogicLearner.Pages.ProblemList.ProblemsNavToLessons(openLessonsList);
        }

        private void openCamera(int lessonNum, bool isProblem)
        {
            CameraPage cameraPage = new CameraPage(lessonNum, isProblem);
            this.Content = cameraPage.Content;
            cameraPage.cameraBackClickEvent += new LogicLearner.Pages.CameraPage.CameraBackClick(openLesson);
            cameraPage.submitButtonClickEvent += new LogicLearner.Pages.CameraPage.SubmitButtonClick(openSubmit);
        }

        private void openSubmit(int lessonNum, bool isProblem)
        {
            Submission submit = new Submission(lessonNum, isProblem);
            this.Content = submit.Content;
            submit.submitNextLessonButtonClickEvent += new LogicLearner.Pages.Submission.SubmitNextLessonButtonClick(openLesson);
        }
    }
}
