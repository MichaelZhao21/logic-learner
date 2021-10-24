using Emgu.CV.CvEnum;
using Emgu.CV.Stitching;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using Wireform.Circuitry.Data.Bits;
using Wireform.Circuitry.Data;
using Wireform.Circuitry.Gates;
using Wireform.Circuitry;
using System.Windows.Interop;
using System.IO;
using Emgu.CV.Structure;

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
            ComponentDispatcher.ThreadIdle += LoadFrame;
            //MainWindow.setImage = (image) => ImageBox.Source =;
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



        //wireform
        bool captureWireform = true;

        const float distCutoff = 10000f;
bool clicked = false;
        private void LoadFrame(object sender, EventArgs e)
        {
////load a frame from the camera
//using Mat frame = capture.QueryFrame();
//ImageBox.Source = Utils.BitmapToImageSource(frame.ToBitmap());

            using Mat frame = MainWindow.capture.QueryFrame();

            CvInvoke.Rotate(frame, frame, RotateFlags.Rotate90Clockwise);
            if (frame == null)
            {
                MainWindow.capture.Stop();
                MainWindow.capture.Start();
                return;
            }

            MainWindow.sketcher.ProcessFrame(frame, captureWireform);
if (captureWireform)
{
captureWireform = false;
}
            else if (MainWindow.sketcher.f_transformationG != null)
            {
                var input = Mouse.GetPosition(ImageBox);
                PointF[] points = CvInvoke.PerspectiveTransform(new PointF[] { new PointF((float)input.X * 2*16/9f, (float)input.Y * 2*16/9f) }, MainWindow.sketcher.f_transformationG);
//CvInvoke.DrawMarker(frame, new System.Drawing.Point((int)input.X, (int)input.Y), new MCvScalar(), MarkerTypes.Cross, thickness:3);
//CvInvoke.DrawMarker(frame, new System.Drawing.Point((int)points[0].X, (int)points[0].Y), new MCvScalar(), MarkerTypes.Star, thickness: 3);

                BitSource minGate = null;
                float minDist = float.MaxValue;
                foreach (var gate in MainWindow.sketcher.boardStack.CurrentState.Gates.Where(x => x is BitSource))
                {
                    float dist = MathF.Pow(gate.StartPoint.X - points[0].X, 2) + MathF.Pow(gate.StartPoint.Y - points[0].Y, 2);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minGate = (BitSource)gate;
                    }
                    //CvInvoke.DrawMarker(frame, new System.Drawing.Point((int)gate.StartPoint.X, (int)gate.StartPoint.Y), new MCvScalar(255, 0, 255), MarkerTypes.Diamond, thickness: 3);
                }
                if (Mouse.LeftButton == MouseButtonState.Pressed && !clicked)
                {
                    clicked = true;
if (minGate is null || minDist > distCutoff)
                    {
                        captureWireform = true;
                        var text = GetConnections(MainWindow.sketcher.boardStack.CurrentState);
                        File.WriteAllLines(@"C:\Users\rhala\Code\Calhacks\circuit.txt", text);
                    }
                    else
                    {
                        if (minGate.Value == BitValue.One)
                        {
                            minGate.Value = BitValue.Zero;
                        }
else
{
                            minGate.Value = BitValue.One;
                        }
                        MainWindow.sketcher.boardStack.CurrentState.Propogate();
                    }
                }
                else if (Mouse.LeftButton == MouseButtonState.Released)
                {
                    clicked = false;
                }
            }
            bitmap = frame.ToBitmap();
            ImageBox.Source = Utils.BitmapToImageSource(bitmap);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static System.Drawing.Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new System.Drawing.Point(w32Mouse.X, w32Mouse.Y);
        }

        public static Bitmap bitmap;

        

        public bool CheckEquivalent(List<string> first, List<string> other)
        {
            first.Sort();
            other.Sort();
            return first.Zip(other, string.Equals).All(x => x);
        }


        ///// <summary>
        ///// Checks if two circuits are 'equivalent' (as in, they have the same pathways)
        ///// </summary>
        //public bool IsEquivalent(BoardState state, BoardState other)
        //{
        //    List<Gate> stateSources = state.Gates.Where(x => x is BitSource).ToList();
        //    List<Gate> otherSources = other.Gates.Where(x => x is BitSource).ToList();
        //    foreach(var ssource in stateSources)
        //    {
        //        bool eq = false;
        //        for(int i = 0; i < otherSources.Count; i++)
        //        {
        //            if(sourceEq(ssource, otherSources[i], new HashSet<Gate>()))
        //            {
        //                eq = true;
        //                otherSources.RemoveAt(i);
        //                break;
        //            }
        //        }
        //        if (!eq) return false;
        //    }
        //    return true;

        //    static bool sourceEq(Gate ssource, Gate osource, HashSet<Gate> visited)
        //    {
        //        visited.Add(ssource);
        //        visited.Add(osource);

        //        HashSet<Gate> soutputs = new HashSet<Gate>();
        //        HashSet<Gate> ooutputs = new HashSet<Gate>();

        //        HashSet<WireLine> visitedW = new HashSet<WireLine>();
        //    }
        //}

        /// <summary>
        /// Gets all the connections of a circuit. Essentially a text representation of a circuit's connections
        /// </summary>
        /// <returns>set of (gate name, gate name) pairs. (output pin's gate name, input pin's gate name). List is sorted.
        /// Prepended to that is a list of all gate names, also sorted.</returns>
        public List<string> GetConnections(BoardState state)
        {
            List<string> pairs = new List<string>();
            HashSet<DrawableObject> visited = new HashSet<DrawableObject>();
            foreach (var gate in state.Gates)
            {
                foreach (var output in gate.Outputs)
                {
                    if (!state.Connections.ContainsKey(output.StartPoint)) continue;
                    visited.Clear();

                    var connects = state.Connections[output.StartPoint].ToList();


                    for (int i = 0; i < connects.Count; i++)
                    {
                        if (visited.Contains(connects[i])) continue;
                        visited.Add(connects[i]);
                        if (connects[i] is GatePin pin && pin.Parent != gate)
                        {
                            pairs.Add($"({gate.GetType().Name}, {pin.Parent.GetType().Name})");
                        }
                        else if (connects[i] is WireLine wire)
                        {
                            //this is slow, fix it
                            if (state.Connections.ContainsKey(wire.StartPoint))
                            {
                                connects.AddRange(state.Connections[wire.StartPoint]);
                            }

                            if (state.Connections.ContainsKey(wire.EndPoint))
                            {
                                connects.AddRange(state.Connections[wire.EndPoint]);
                            }
                        }
                    }
                }
            }
            pairs.Sort();

            List<string> gates = new List<string>();
            gates.AddRange(state.Gates.Select(x => x.GetType().Name));
            gates.Sort();
            gates.AddRange(pairs);
            return gates;
        }
    }
}
