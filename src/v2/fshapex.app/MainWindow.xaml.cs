using cv = OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace fshapex.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var capture = new cv.VideoCapture(0);

            var src = cv.Cv2.CreateFrameSource_Camera(0);
            // Mat src = Cv2.ImRead("lenna.png", ImreadModes.Grayscale);
            var dst = new cv.Mat();

            cv.Cv2..

            //cv.Cv2.Canny(src, dst, 50, 200);
            using (new cv.Window("src image", src.))
            using (new cv.Window("dst image", dst))
            {
                cv.Cv2.WaitKey();
            }
        }
    }
}
