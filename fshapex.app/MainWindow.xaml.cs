using fshapex.app.Domain;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace fshapex.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private BitmapImage BitmapSource { get; set; } = new BitmapImage();
        private BitmapImage BitmapTarget { get; set; } = new BitmapImage();
        private double DPI { get; set; } = 96d;

        public MainWindow()
        {
            InitializeComponent();

            imgSourceSingle.Source = new BitmapImage(new Uri("pack://application:,,,/fshapex.app;component/Assets/Images/user256.png", UriKind.Absolute));
        }

        //https://keybase.io/

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "" +
                    "JPEG Image(*.jpg)|*.jpg|" +
                    "PNG Image(*.png)|*.png|" +
                    "GIF (the first frame) Image(*.gif)|*.gif|" +
                    "BMP Image(*.bmp)|*.bmp"
            };

            bool? result = dialog.ShowDialog(this);

            if (!(bool)result)
                return;

            var file = new FileInfo(dialog.FileName);

            if (file.Length < 1024 || file.Length > ((1024 * 1000) * 4))
                return;

            BitmapSource = new BitmapImage();
            BitmapSource.BeginInit();
            BitmapSource.CacheOption = BitmapCacheOption.None;
            BitmapSource.UriSource = new Uri(dialog.FileName);
            BitmapSource.EndInit();

            if (((BitmapSource.PixelWidth + BitmapSource.PixelHeight) / 2) < 36 || ((BitmapSource.PixelWidth + BitmapSource.PixelHeight) / 2) > 4096)
                return;

            lblImage.Text = dialog.FileName;

            imgSourceSingle.Source = BitmapSource;
            btnClear.IsEnabled = btnStart.IsEnabled = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var file = new Uri("pack://application:,,,/fshapex.app;component/Assets/Images/user256.png", UriKind.Absolute);

            BitmapSource = new BitmapImage();
            BitmapSource.BeginInit();
            BitmapSource.CacheOption = BitmapCacheOption.None;
            BitmapSource.UriSource = file;
            BitmapSource.EndInit();

            lblImage.Text = "Load a image";

            imgSourceSingle.Source = new BitmapImage(file);
            imgGraph.Source = new BitmapImage();
            lblCluster.Text = "0";
            btnClear.IsEnabled = btnStart.IsEnabled = false;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnLoad.IsEnabled = btnClear.IsEnabled = btnStart.IsEnabled = btnProcess.IsEnabled = false;
            Single();
            btnLoad.IsEnabled = btnClear.IsEnabled = btnStart.IsEnabled = btnProcess.IsEnabled = true;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            btnLoad.IsEnabled = btnClear.IsEnabled = btnStart.IsEnabled = btnProcess.IsEnabled = false;
            Multi();
            btnLoad.IsEnabled = btnClear.IsEnabled = btnStart.IsEnabled = btnProcess.IsEnabled = true;
        }

        private void chkDpi_Checked(object sender, RoutedEventArgs e)
        {
            if (sldDpi == null || lblDpi == null)
                return;
            sldDpi.IsEnabled = lblDpi.IsEnabled = true;
        }

        private void chkDpi_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sldDpi == null || lblDpi == null)
                return;
            sldDpi.IsEnabled = lblDpi.IsEnabled = false;
            DPI = 96;
            lblDpi.Content = DPI.ToString();
            sldDpi.Value = DPI;
        }

        private void sldDpi_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblDpi == null)
                return;
            DPI = Convert.ToInt32(Math.Floor(e.NewValue));
            lblDpi.Content = DPI.ToString();
        }

        /*
         * https://github.com/punker76/code-samples#mahappsmetro-themes
        private MetroWindow accentThemeTestWindow;
        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (accentThemeTestWindow != null)
            {
                accentThemeTestWindow.Activate();
                return;
            }

            accentThemeTestWindow = new AccentStyleWindow();
            accentThemeTestWindow.Owner = this;
            accentThemeTestWindow.Closed += (o, args) => accentThemeTestWindow = null;
            accentThemeTestWindow.Left = this.Left + this.ActualWidth / 2.0;
            accentThemeTestWindow.Top = this.Top + this.ActualHeight / 2.0;
            accentThemeTestWindow.Show();
        }
        */

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            grd.Background = new SolidColorBrush(Color.FromRgb(51, 161, 51));
        }

        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            grd.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var directory = Path.Combine(Path.GetTempPath(), "FShapeX");
            try
            {
                if (Directory.Exists(directory))
                    Directory.Delete(directory, true);

                foreach (string graphPath in new string[] { "k4", "k5", "k6", "k7" })
                {
                    if (Directory.Exists("C:/FShapeX/App/Clusters/" + graphPath))
                        Directory.Delete(graphPath, true);
                }
            }
            catch (Exception) { }
        }

        private void btnCopyMail1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("viniciuspicossi@gmail.com");
            FadeOut(lblCopyMail1);
        }

        private void btnCopyMail2_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("paschoal@utfpr.edu.br");
            FadeOut(lblCopyMail2);
        }

        private void btnCopyMail3_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("pbugatti@utfpr.edu.br");
            FadeOut(lblCopyMail3);
        }

        private void btnCopyMail4_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("psaito@utfpr.edu.br");
            FadeOut(lblCopyMail4);
        }



        private void FadeOut(DependencyObject element)
        {
            element.SetValue(UIElement.VisibilityProperty, Visibility.Visible);

            var animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            var storyboard = new Storyboard();

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { element.SetValue(UIElement.VisibilityProperty, Visibility.Hidden); };
            storyboard.Begin();
        }

        private async void Single()
        {
            var pipe = new PipeProcess();

            var bitmap = BitmapSource;
            if (chkDpi.IsChecked.Value)
            {
                bitmap = pipe.Resize(BitmapSource.UriSource.LocalPath);
                imgSourceSingle.Source = bitmap;
            }
            imgGraph.Source = new BitmapImage();
            lblCluster.Text = "0";

            var fileInfo = new FileInfo(bitmap.UriSource.LocalPath);
            var uri = pipe.GenerateURI(fileInfo.FullName, fileInfo.Name);

            var image = new OpenCvSharp.Mat(bitmap.UriSource.LocalPath, OpenCvSharp.ImreadModes.Color);

            var pointsLeft = new List<OpenCvSharp.Point2f>();
            var pointsRight = new List<OpenCvSharp.Point2f>();

            string gender = "";
            string ethnicity = "";
            long age = 0;
            double beautyScore = 0;
            double acne = 0;
            double darkCircle = 0;
            double health = 0;
            double stain = 0;
            string resultsLabels = "";
            string resultsPoints = "";

            //if (rdoCognitiveServices.IsChecked.Value)
            //{
            //    var data0 = await new Services.CognitiveServices.Api().GetDataAsync(uri.AbsoluteUri);

            //    foreach (var face in data0)
            //    {
            //        pointsLeft.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeLeftOuter.X, (float)face.FaceLandmarks.EyeLeftOuter.Y));
            //        pointsLeft.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeLeftTop.X, (float)face.FaceLandmarks.EyeLeftTop.Y));
            //        pointsLeft.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeLeftInner.X, (float)face.FaceLandmarks.EyeLeftInner.Y));
            //        pointsLeft.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeLeftBottom.X, (float)face.FaceLandmarks.EyeLeftBottom.Y));
            //    }

            //    foreach (var face in data0)
            //    {
            //        pointsRight.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeRightOuter.X, (float)face.FaceLandmarks.EyeRightOuter.Y));
            //        pointsRight.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeRightTop.X, (float)face.FaceLandmarks.EyeRightTop.Y));
            //        pointsRight.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeRightInner.X, (float)face.FaceLandmarks.EyeRightInner.Y));
            //        pointsRight.Add(new OpenCvSharp.Point2f((float)face.FaceLandmarks.EyeRightBottom.X, (float)face.FaceLandmarks.EyeRightBottom.Y));
            //    }
            //}
            //else 
            if (rdoFacePlusPlus.IsChecked.Value)
            {
                var data1 = await new Services.FacePlusPlus.Api().GetDataAsync(uri.AbsoluteUri);

                if (data1?.Faces?.Count == 1)
                {
                    var keysLeft1 = new string[] {
                        "left_eye_left_corner",
                        "left_eye_upper_left_quarter",
                        "left_eye_top",
                        "left_eye_upper_right_quarter",
                        "left_eye_right_corner",
                        "left_eye_lower_right_quarter",
                        "left_eye_bottom",
                        "left_eye_lower_left_quarter"
                    };

                    var keysRight1 = new string[] {
                        "right_eye_left_corner",
                        "right_eye_upper_left_quarter",
                        "right_eye_top",
                        "right_eye_upper_right_quarter",
                        "right_eye_right_corner",
                        "right_eye_lower_right_quarter",
                        "right_eye_bottom",
                        "right_eye_lower_left_quarter"
                    };

                    var face = data1.Faces.First();

                    Services.FacePlusPlus.Landmark
                        lepa, lepb, lepc, lepd, lepe, lepf, lepg, leph,
                        repa, repb, repc, repd, repe, repf, repg, reph;

                    lepa = face.Landmark["left_eye_right_corner"];
                    lepb = face.Landmark["left_eye_upper_right_quarter"];
                    lepc = face.Landmark["left_eye_top"];
                    lepd = face.Landmark["left_eye_upper_left_quarter"];
                    lepe = face.Landmark["left_eye_left_corner"];
                    lepf = face.Landmark["left_eye_lower_left_quarter"];
                    lepg = face.Landmark["left_eye_bottom"];
                    leph = face.Landmark["left_eye_lower_right_quarter"];

                    repa = face.Landmark["right_eye_left_corner"];
                    repb = face.Landmark["right_eye_upper_left_quarter"];
                    repc = face.Landmark["right_eye_top"];
                    repd = face.Landmark["right_eye_upper_right_quarter"];
                    repe = face.Landmark["right_eye_right_corner"];
                    repf = face.Landmark["right_eye_lower_right_quarter"];
                    repg = face.Landmark["right_eye_bottom"];
                    reph = face.Landmark["right_eye_lower_left_quarter"];

                    foreach (var key in keysLeft1)
                        if (face.Landmark.ContainsKey(key))
                            pointsLeft.Add(new OpenCvSharp.Point2f(face.Landmark[key].X, face.Landmark[key].Y));

                    foreach (var key in keysRight1)
                        if (face.Landmark.ContainsKey(key))
                            pointsRight.Add(new OpenCvSharp.Point2f(face.Landmark[key].X, face.Landmark[key].Y));

                    gender = face.Attributes.Gender.Value.ToLower();
                    ethnicity = face.Attributes.Ethnicity.Value.ToLower();
                    age = face.Attributes.Age.Value;
                    beautyScore = (gender == "male" ? face.Attributes.Beauty.MaleScore : face.Attributes.Beauty.FemaleScore);
                    acne = face.Attributes.Skinstatus.Acne;
                    darkCircle = face.Attributes.Skinstatus.DarkCircle;
                    health = face.Attributes.Skinstatus.Health;
                    stain = face.Attributes.Skinstatus.Stain;

                    resultsPoints =
                        $"{lepa.X.ToString().Replace(",", ".")},{lepa.Y.ToString().Replace(",", ".")}," +
                        $"{lepb.X.ToString().Replace(",", ".")},{lepb.Y.ToString().Replace(",", ".")}," +
                        $"{lepc.X.ToString().Replace(",", ".")},{lepc.Y.ToString().Replace(",", ".")}," +
                        $"{lepd.X.ToString().Replace(",", ".")},{lepd.Y.ToString().Replace(",", ".")}," +
                        $"{lepe.X.ToString().Replace(",", ".")},{lepe.Y.ToString().Replace(",", ".")}," +
                        $"{lepf.X.ToString().Replace(",", ".")},{lepf.Y.ToString().Replace(",", ".")}," +
                        $"{lepg.X.ToString().Replace(",", ".")},{lepg.Y.ToString().Replace(",", ".")}," +
                        $"{leph.X.ToString().Replace(",", ".")},{leph.Y.ToString().Replace(",", ".")}," +
                        $"{repa.X.ToString().Replace(",", ".")},{repa.Y.ToString().Replace(",", ".")}," +
                        $"{repb.X.ToString().Replace(",", ".")},{repb.Y.ToString().Replace(",", ".")}," +
                        $"{repc.X.ToString().Replace(",", ".")},{repc.Y.ToString().Replace(",", ".")}," +
                        $"{repd.X.ToString().Replace(",", ".")},{repd.Y.ToString().Replace(",", ".")}," +
                        $"{repe.X.ToString().Replace(",", ".")},{repe.Y.ToString().Replace(",", ".")}," +
                        $"{repf.X.ToString().Replace(",", ".")},{repf.Y.ToString().Replace(",", ".")}," +
                        $"{repg.X.ToString().Replace(",", ".")},{repg.Y.ToString().Replace(",", ".")}," +
                        $"{reph.X.ToString().Replace(",", ".")},{reph.Y.ToString().Replace(",", ".")}";
                }
            }
            //else if (rdoKairos.IsChecked.Value)
            //{
            //    var data2 = await new Services.Kairos.Api().GetDataAsync(uri.AbsoluteUri);

            //    var keysLeft2 = new string[] {
            //        "leftEyeCornerLeft",
            //        "leftEyeTopInnerLeft",
            //        "leftEyeTopInnerRight",
            //        "leftEyeCornerRight",
            //        "leftEyeBottomInnerRight",
            //        "leftEyeBottomInnerLeft"
            //    };

            //    var keysRight2 = new string[] {
            //        "rightEyeCornerLeft",
            //        "rightEyeTopInnerLeft",
            //        "rightEyeTopInnerRight",
            //        "rightEyeCornerRight",
            //        "rightEyeBottomInnerRight",
            //        "rightEyeBottomInnerLeft"
            //    };

            //    foreach (var key in keysLeft2)
            //        foreach (var face in data2.Frames[0].People)
            //            foreach (var landmark in face.Landmarks)
            //                if (landmark.ContainsKey(key))
            //                    pointsLeft.Add(new OpenCvSharp.Point2f(landmark[key].X, landmark[key].Y));

            //    foreach (var key in keysRight2)
            //        foreach (var face in data2.Frames[0].People)
            //            foreach (var landmark in face.Landmarks)
            //                if (landmark.ContainsKey(key))
            //                    pointsRight.Add(new OpenCvSharp.Point2f(landmark[key].X, landmark[key].Y));
            //}

            // common
            DrawArea(image, pointsLeft, true);
            DrawArea(image, pointsRight, true);

            var extension = new FileInfo(bitmap.UriSource.LocalPath).Extension;
            var directory = Path.Combine(Path.GetTempPath(), "FShapeX");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var filename = Guid.NewGuid().ToString() + extension;
            var tempFile = Path.Combine(directory, filename);
            image.SaveImage(tempFile);
            imgSourceSingle.Source = new BitmapImage(new Uri(tempFile));

            //var programFiles = Environment.GetFolderPath(Environment.Is64BitProcess ? Environment.SpecialFolder.ProgramFiles : Environment.SpecialFolder.ProgramFilesX86);
            //var points = Path.Combine(programFiles, "FShapeX", "App", "Assets", "points.csv");
            //var points2 = Path.Combine(programFiles, "FShapeX", "App", "Assets", "points2.csv");

            var points = "C:/FShapeX/App/Dataset/points.csv";
            var points2 = "C:/FShapeX/App/Dataset/points2.csv";

            File.Copy(points, points2, true);

            resultsLabels = $"Personal,{filename},{gender},{ethnicity}";

            File.AppendAllLines(points2, new List<string>() { resultsLabels + "," + resultsPoints });

            int k = (rdoK4.IsChecked.Value ? 4 : (rdoK5.IsChecked.Value ? 5 : (rdoK6.IsChecked.Value ? 6 : (rdoK7.IsChecked.Value ? 7 : 0))));
            var graphPath = $"C:/FShapeX/App/Clusters/k{k}";

            try
            {
                new R().Evaluate(k);

                var graphFile = Directory.GetFiles(graphPath).OrderBy(o => new FileInfo(o).CreationTime).LastOrDefault();

                var newFile = Path.Combine(directory, Guid.NewGuid().ToString() + new FileInfo(graphFile).Extension);

                File.Copy(graphFile, newFile, true);

                lblCluster.Text = new FileInfo(graphFile).Name.Replace(".png", "");

                imgGraph.Source = new BitmapImage(new Uri(newFile));
            }
            catch (Exception)
            {
                imgGraph.Source = new BitmapImage();
                lblCluster.Text = "0";
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private async void Multi()
        {
            if (string.IsNullOrEmpty(txtInput.Text))
                return;

            if (string.IsNullOrEmpty(txtOutput.Text))
                txtOutput.Text = txtInput.Text;

            List<FileInfo> files = GetFilesRecursive(txtInput.Text);

            var pipe = new PipeProcess();

            string labels = "dataset,file,gender,ethnicity";

            string points =
                "leax,leay,lebx,leby,lecx,lecy,ledx,ledy,leex,ledy,lefx,lefy,legx,legy,lehx,lehy," +
                "reax,reay,rebx,reby,recx,recy,redx,redy,reex,redy,refx,refy,regx,regy,rehx,rehy";

            string featuresEuclidean =
                "leabe,leace,leade,leaee,leafe,leage,leahe," +
                "lebce,lebde,lebee,lebfe,lebge,lebhe," +
                "lecde,lecee,lecfe,lecge,leche," +
                "ledee,ledfe,ledge,ledhe," +
                "leefe,leege,leehe," +
                "lefge,lefhe," +
                "leghe," +

                "reabe,reace,reade,reaee,reafe,reage,reahe," +
                "rebce,rebde,rebee,rebfe,rebge,rebhe," +
                "recde,recee,recfe,recge,reche," +
                "redee,redfe,redge,redhe," +
                "reefe,reege,reehe," +
                "refge,refhe," +
                "reghe";

            string featuresManhattan =
                "leabm,leacm,leadm,leaem,leafm,leagm,leahm," +
                "lebcm,lebdm,lebem,lebfm,lebgm,lebhm," +
                "lecdm,lecem,lecfm,lecgm,lechm," +
                "ledem,ledfm,ledgm,ledhm," +
                "leefm,leegm,leehm," +
                "lefgm,lefhm," +
                "leghm," +

                "reabm,reacm,readm,reaem,reafm,reagm,reahm," +
                "rebcm,rebdm,rebem,rebfm,rebgm,rebhm," +
                "recdm,recem,recfm,recgm,rechm," +
                "redem,redfm,redgm,redhm," +
                "reefm,reegm,reehm," +
                "refgm,refhm," +
                "reghm";

            string featuresChebyshev =
                "leabc,leacc,leadc,leaec,leafc,leagc,leahc," +
                "lebcc,lebdc,lebec,lebfc,lebgc,lebhc," +
                "lecdc,lecec,lecfc,lecgc,lechc," +
                "ledec,ledfc,ledgc,ledhc," +
                "leefc,leegc,leehc," +
                "lefgc,lefhc," +
                "leghc," +

                "reabc,reacc,readc,reaec,reafc,reagc,reahc," +
                "rebcc,rebdc,rebec,rebfc,rebgc,rebhc," +
                "recdc,recec,recfc,recgc,rechc," +
                "redec,redfc,redgc,redhc," +
                "reefc,reegc,reehc," +
                "refgc,refhc," +
                "reghc";

            string featuresFull =
                featuresEuclidean + "," +
                featuresManhattan + "," +
                featuresChebyshev;

            string
                datasetPoints = "dataset-points.csv",
                datasetEuclidean = "dataset-euclidean.csv",
                datasetManhattan = "dataset-manhattan.csv",
                datasetChebyshev = "dataset-chebyshev.csv",
                datasetFull = "dataset-full.csv";

            File.AppendAllLines(txtOutput.Text + "//" + datasetPoints, new List<string>() { labels + "," + points });
            File.AppendAllLines(txtOutput.Text + "//" + datasetEuclidean, new List<string>() { labels + "," + featuresEuclidean });
            File.AppendAllLines(txtOutput.Text + "//" + datasetManhattan, new List<string>() { labels + "," + featuresManhattan });
            File.AppendAllLines(txtOutput.Text + "//" + datasetChebyshev, new List<string>() { labels + "," + featuresChebyshev });
            File.AppendAllLines(txtOutput.Text + "//" + datasetFull, new List<string>() { labels + "," + featuresFull });

            var i = 1;

            foreach (var file in files)
            {
                lblStatusBar.Text = $"{i}/{files.Count} | {file.FullName}";

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.None;
                bitmap.UriSource = new Uri(file.FullName);
                bitmap.EndInit();
                imgSourceMulti.Source = bitmap;

                if (chkDpi.IsChecked.Value)
                {
                    bitmap = pipe.Resize(file.FullName);
                    imgSourceMulti.Source = bitmap;
                }

                var uri = pipe.GenerateURI(bitmap.UriSource.LocalPath, file.Name, file.Directory.Name);

                var pointsLeft = new List<OpenCvSharp.Point2f>();
                var pointsRight = new List<OpenCvSharp.Point2f>();

                // code 1

                if (rdoFacePlusPlus.IsChecked.Value)
                {
                    //var data = new FacePlusPlus.Model();
                    //try { await new FacePlusPlus.Api().GetDataAsync(uri.AbsoluteUri); }
                    //catch (Exception) { continue; }

                    var data = await new Services.FacePlusPlus.Api().GetDataAsync(uri.AbsoluteUri);

                    if (data?.Faces?.Count == 1)
                    {
                        var face = data.Faces.First();

                        Services.FacePlusPlus.Landmark
                            lepa, lepb, lepc, lepd, lepe, lepf, lepg, leph,
                            repa, repb, repc, repd, repe, repf, repg, reph;

                        lepa = face.Landmark["left_eye_right_corner"];
                        lepb = face.Landmark["left_eye_upper_right_quarter"];
                        lepc = face.Landmark["left_eye_top"];
                        lepd = face.Landmark["left_eye_upper_left_quarter"];
                        lepe = face.Landmark["left_eye_left_corner"];
                        lepf = face.Landmark["left_eye_lower_left_quarter"];
                        lepg = face.Landmark["left_eye_bottom"];
                        leph = face.Landmark["left_eye_lower_right_quarter"];

                        repa = face.Landmark["right_eye_left_corner"];
                        repb = face.Landmark["right_eye_upper_left_quarter"];
                        repc = face.Landmark["right_eye_top"];
                        repd = face.Landmark["right_eye_upper_right_quarter"];
                        repe = face.Landmark["right_eye_right_corner"];
                        repf = face.Landmark["right_eye_lower_right_quarter"];
                        repg = face.Landmark["right_eye_bottom"];
                        reph = face.Landmark["right_eye_lower_left_quarter"];

                        // Left Eye ... Euclidean
                        double
                            leabe, leace, leade, leaee, leafe, leage, leahe,
                            lebce, lebde, lebee, lebfe, lebge, lebhe,
                            lecde, lecee, lecfe, lecge, leche,
                            ledee, ledfe, ledge, ledhe,
                            leefe, leege, leehe,
                            lefge, lefhe,
                            leghe;

                        // A -> ...
                        leabe = pipe.EuclideanDistance(lepa.X, lepb.X, lepa.Y, lepb.Y);
                        leace = pipe.EuclideanDistance(lepa.X, lepc.X, lepa.Y, lepc.Y);
                        leade = pipe.EuclideanDistance(lepa.X, lepd.X, lepa.Y, lepd.Y);
                        leaee = pipe.EuclideanDistance(lepa.X, lepe.X, lepa.Y, lepe.Y);
                        leafe = pipe.EuclideanDistance(lepa.X, lepf.X, lepa.Y, lepf.Y);
                        leage = pipe.EuclideanDistance(lepa.X, lepg.X, lepa.Y, lepg.Y);
                        leahe = pipe.EuclideanDistance(lepa.X, leph.X, lepa.Y, leph.Y);

                        // B -> ...
                        lebce = pipe.EuclideanDistance(lepb.X, lepc.X, lepb.Y, lepc.Y);
                        lebde = pipe.EuclideanDistance(lepb.X, lepd.X, lepb.Y, lepd.Y);
                        lebee = pipe.EuclideanDistance(lepb.X, lepe.X, lepb.Y, lepe.Y);
                        lebfe = pipe.EuclideanDistance(lepb.X, lepf.X, lepb.Y, lepf.Y);
                        lebge = pipe.EuclideanDistance(lepb.X, lepg.X, lepb.Y, lepg.Y);
                        lebhe = pipe.EuclideanDistance(lepb.X, leph.X, lepb.Y, leph.Y);

                        // C -> ...
                        lecde = pipe.EuclideanDistance(lepc.X, lepd.X, lepc.Y, lepd.Y);
                        lecee = pipe.EuclideanDistance(lepc.X, lepe.X, lepc.Y, lepe.Y);
                        lecfe = pipe.EuclideanDistance(lepc.X, lepf.X, lepc.Y, lepf.Y);
                        lecge = pipe.EuclideanDistance(lepc.X, lepg.X, lepc.Y, lepg.Y);
                        leche = pipe.EuclideanDistance(lepc.X, leph.X, lepc.Y, leph.Y);

                        // D -> ...
                        ledee = pipe.EuclideanDistance(lepd.X, lepe.X, lepd.Y, lepe.Y);
                        ledfe = pipe.EuclideanDistance(lepd.X, lepf.X, lepd.Y, lepf.Y);
                        ledge = pipe.EuclideanDistance(lepd.X, lepg.X, lepd.Y, lepg.Y);
                        ledhe = pipe.EuclideanDistance(lepd.X, leph.X, lepd.Y, leph.Y);

                        // E -> ...
                        leefe = pipe.EuclideanDistance(lepe.X, lepf.X, lepe.Y, lepf.Y);
                        leege = pipe.EuclideanDistance(lepe.X, lepg.X, lepe.Y, lepg.Y);
                        leehe = pipe.EuclideanDistance(lepe.X, leph.X, lepe.Y, leph.Y);

                        // F -> ...
                        lefge = pipe.EuclideanDistance(lepf.X, lepg.X, lepf.Y, lepg.Y);
                        lefhe = pipe.EuclideanDistance(lepf.X, leph.X, lepf.Y, leph.Y);

                        // G -> ...
                        leghe = pipe.EuclideanDistance(lepg.X, leph.X, lepg.Y, leph.Y);


                        // Right Eye ... Euclidean
                        double
                            reabe, reace, reade, reaee, reafe, reage, reahe,
                            rebce, rebde, rebee, rebfe, rebge, rebhe,
                            recde, recee, recfe, recge, reche,
                            redee, redfe, redge, redhe,
                            reefe, reege, reehe,
                            refge, refhe,
                            reghe;

                        // A -> ...
                        reabe = pipe.EuclideanDistance(repa.X, repb.X, repa.Y, repb.Y);
                        reace = pipe.EuclideanDistance(repa.X, repc.X, repa.Y, repc.Y);
                        reade = pipe.EuclideanDistance(repa.X, repd.X, repa.Y, repd.Y);
                        reaee = pipe.EuclideanDistance(repa.X, repe.X, repa.Y, repe.Y);
                        reafe = pipe.EuclideanDistance(repa.X, repf.X, repa.Y, repf.Y);
                        reage = pipe.EuclideanDistance(repa.X, repg.X, repa.Y, repg.Y);
                        reahe = pipe.EuclideanDistance(repa.X, reph.X, repa.Y, reph.Y);
                        // B -> ...
                        rebce = pipe.EuclideanDistance(repb.X, repc.X, repb.Y, repc.Y);
                        rebde = pipe.EuclideanDistance(repb.X, repd.X, repb.Y, repd.Y);
                        rebee = pipe.EuclideanDistance(repb.X, repe.X, repb.Y, repe.Y);
                        rebfe = pipe.EuclideanDistance(repb.X, repf.X, repb.Y, repf.Y);
                        rebge = pipe.EuclideanDistance(repb.X, repg.X, repb.Y, repg.Y);
                        rebhe = pipe.EuclideanDistance(repb.X, reph.X, repb.Y, reph.Y);
                        // C -> ...
                        recde = pipe.EuclideanDistance(repc.X, repd.X, repc.Y, repd.Y);
                        recee = pipe.EuclideanDistance(repc.X, repe.X, repc.Y, repe.Y);
                        recfe = pipe.EuclideanDistance(repc.X, repf.X, repc.Y, repf.Y);
                        recge = pipe.EuclideanDistance(repc.X, repg.X, repc.Y, repg.Y);
                        reche = pipe.EuclideanDistance(repc.X, reph.X, repc.Y, reph.Y);
                        // D -> ...
                        redee = pipe.EuclideanDistance(repd.X, repe.X, repd.Y, repe.Y);
                        redfe = pipe.EuclideanDistance(repd.X, repf.X, repd.Y, repf.Y);
                        redge = pipe.EuclideanDistance(repd.X, repg.X, repd.Y, repg.Y);
                        redhe = pipe.EuclideanDistance(repd.X, reph.X, repd.Y, reph.Y);
                        // E -> ...
                        reefe = pipe.EuclideanDistance(repe.X, repf.X, repe.Y, repf.Y);
                        reege = pipe.EuclideanDistance(repe.X, repg.X, repe.Y, repg.Y);
                        reehe = pipe.EuclideanDistance(repe.X, reph.X, repe.Y, reph.Y);
                        // F -> ...
                        refge = pipe.EuclideanDistance(repf.X, repg.X, repf.Y, repg.Y);
                        refhe = pipe.EuclideanDistance(repf.X, reph.X, repf.Y, reph.Y);
                        // G -> ...
                        reghe = pipe.EuclideanDistance(repg.X, reph.X, repg.Y, reph.Y);


                        // Left Eye ... Manhattan
                        double
                            leabm, leacm, leadm, leaem, leafm, leagm, leahm,
                            lebcm, lebdm, lebem, lebfm, lebgm, lebhm,
                            lecdm, lecem, lecfm, lecgm, lechm,
                            ledem, ledfm, ledgm, ledhm,
                            leefm, leegm, leehm,
                            lefgm, lefhm,
                            leghm;

                        // A -> ...
                        leabm = pipe.ManhattanDistance(lepa.X, lepb.X, lepa.Y, lepb.Y);
                        leacm = pipe.ManhattanDistance(lepa.X, lepc.X, lepa.Y, lepc.Y);
                        leadm = pipe.ManhattanDistance(lepa.X, lepd.X, lepa.Y, lepd.Y);
                        leaem = pipe.ManhattanDistance(lepa.X, lepe.X, lepa.Y, lepe.Y);
                        leafm = pipe.ManhattanDistance(lepa.X, lepf.X, lepa.Y, lepf.Y);
                        leagm = pipe.ManhattanDistance(lepa.X, lepg.X, lepa.Y, lepg.Y);
                        leahm = pipe.ManhattanDistance(lepa.X, leph.X, lepa.Y, leph.Y);
                        // B -> ...
                        lebcm = pipe.ManhattanDistance(lepb.X, lepc.X, lepb.Y, lepc.Y);
                        lebdm = pipe.ManhattanDistance(lepb.X, lepd.X, lepb.Y, lepd.Y);
                        lebem = pipe.ManhattanDistance(lepb.X, lepe.X, lepb.Y, lepe.Y);
                        lebfm = pipe.ManhattanDistance(lepb.X, lepf.X, lepb.Y, lepf.Y);
                        lebgm = pipe.ManhattanDistance(lepb.X, lepg.X, lepb.Y, lepg.Y);
                        lebhm = pipe.ManhattanDistance(lepb.X, leph.X, lepb.Y, leph.Y);
                        // C -> ...
                        lecdm = pipe.ManhattanDistance(lepc.X, lepd.X, lepc.Y, lepd.Y);
                        lecem = pipe.ManhattanDistance(lepc.X, lepe.X, lepc.Y, lepe.Y);
                        lecfm = pipe.ManhattanDistance(lepc.X, lepf.X, lepc.Y, lepf.Y);
                        lecgm = pipe.ManhattanDistance(lepc.X, lepg.X, lepc.Y, lepg.Y);
                        lechm = pipe.ManhattanDistance(lepc.X, leph.X, lepc.Y, leph.Y);
                        // D -> ...
                        ledem = pipe.ManhattanDistance(lepd.X, lepe.X, lepd.Y, lepe.Y);
                        ledfm = pipe.ManhattanDistance(lepd.X, lepf.X, lepd.Y, lepf.Y);
                        ledgm = pipe.ManhattanDistance(lepd.X, lepg.X, lepd.Y, lepg.Y);
                        ledhm = pipe.ManhattanDistance(lepd.X, leph.X, lepd.Y, leph.Y);
                        // E -> ...
                        leefm = pipe.ManhattanDistance(lepe.X, lepf.X, lepe.Y, lepf.Y);
                        leegm = pipe.ManhattanDistance(lepe.X, lepg.X, lepe.Y, lepg.Y);
                        leehm = pipe.ManhattanDistance(lepe.X, leph.X, lepe.Y, leph.Y);
                        // F -> ...
                        lefgm = pipe.ManhattanDistance(lepf.X, lepg.X, lepf.Y, lepg.Y);
                        lefhm = pipe.ManhattanDistance(lepf.X, leph.X, lepf.Y, leph.Y);
                        // G -> ...
                        leghm = pipe.ManhattanDistance(lepg.X, leph.X, lepg.Y, leph.Y);


                        // Right Eye ... Manhattan
                        double
                            reabm, reacm, readm, reaem, reafm, reagm, reahm,
                            rebcm, rebdm, rebem, rebfm, rebgm, rebhm,
                            recdm, recem, recfm, recgm, rechm,
                            redem, redfm, redgm, redhm,
                            reefm, reegm, reehm,
                            refgm, refhm,
                            reghm;

                        // A -> ...
                        reabm = pipe.ManhattanDistance(repa.X, repb.X, repa.Y, repb.Y);
                        reacm = pipe.ManhattanDistance(repa.X, repc.X, repa.Y, repc.Y);
                        readm = pipe.ManhattanDistance(repa.X, repd.X, repa.Y, repd.Y);
                        reaem = pipe.ManhattanDistance(repa.X, repe.X, repa.Y, repe.Y);
                        reafm = pipe.ManhattanDistance(repa.X, repf.X, repa.Y, repf.Y);
                        reagm = pipe.ManhattanDistance(repa.X, repg.X, repa.Y, repg.Y);
                        reahm = pipe.ManhattanDistance(repa.X, reph.X, repa.Y, reph.Y);
                        // B -> ...
                        rebcm = pipe.ManhattanDistance(repb.X, repc.X, repb.Y, repc.Y);
                        rebdm = pipe.ManhattanDistance(repb.X, repd.X, repb.Y, repd.Y);
                        rebem = pipe.ManhattanDistance(repb.X, repe.X, repb.Y, repe.Y);
                        rebfm = pipe.ManhattanDistance(repb.X, repf.X, repb.Y, repf.Y);
                        rebgm = pipe.ManhattanDistance(repb.X, repg.X, repb.Y, repg.Y);
                        rebhm = pipe.ManhattanDistance(repb.X, reph.X, repb.Y, reph.Y);
                        // C -> ...
                        recdm = pipe.ManhattanDistance(repc.X, repd.X, repc.Y, repd.Y);
                        recem = pipe.ManhattanDistance(repc.X, repe.X, repc.Y, repe.Y);
                        recfm = pipe.ManhattanDistance(repc.X, repf.X, repc.Y, repf.Y);
                        recgm = pipe.ManhattanDistance(repc.X, repg.X, repc.Y, repg.Y);
                        rechm = pipe.ManhattanDistance(repc.X, reph.X, repc.Y, reph.Y);
                        // D -> ...
                        redem = pipe.ManhattanDistance(repd.X, repe.X, repd.Y, repe.Y);
                        redfm = pipe.ManhattanDistance(repd.X, repf.X, repd.Y, repf.Y);
                        redgm = pipe.ManhattanDistance(repd.X, repg.X, repd.Y, repg.Y);
                        redhm = pipe.ManhattanDistance(repd.X, reph.X, repd.Y, reph.Y);
                        // E -> ...
                        reefm = pipe.ManhattanDistance(repe.X, repf.X, repe.Y, repf.Y);
                        reegm = pipe.ManhattanDistance(repe.X, repg.X, repe.Y, repg.Y);
                        reehm = pipe.ManhattanDistance(repe.X, reph.X, repe.Y, reph.Y);
                        // F -> ...
                        refgm = pipe.ManhattanDistance(repf.X, repg.X, repf.Y, repg.Y);
                        refhm = pipe.ManhattanDistance(repf.X, reph.X, repf.Y, reph.Y);
                        // G -> ...
                        reghm = pipe.ManhattanDistance(repg.X, reph.X, repg.Y, reph.Y);


                        // Left Eye ... Chebyshev
                        double
                            leabc, leacc, leadc, leaec, leafc, leagc, leahc,
                            lebcc, lebdc, lebec, lebfc, lebgc, lebhc,
                            lecdc, lecec, lecfc, lecgc, lechc,
                            ledec, ledfc, ledgc, ledhc,
                            leefc, leegc, leehc,
                            lefgc, lefhc,
                            leghc;

                        // A -> ...
                        leabc = pipe.ChebyshevDistance(lepa.X, lepb.X, lepa.Y, lepb.Y);
                        leacc = pipe.ChebyshevDistance(lepa.X, lepc.X, lepa.Y, lepc.Y);
                        leadc = pipe.ChebyshevDistance(lepa.X, lepd.X, lepa.Y, lepd.Y);
                        leaec = pipe.ChebyshevDistance(lepa.X, lepe.X, lepa.Y, lepe.Y);
                        leafc = pipe.ChebyshevDistance(lepa.X, lepf.X, lepa.Y, lepf.Y);
                        leagc = pipe.ChebyshevDistance(lepa.X, lepg.X, lepa.Y, lepg.Y);
                        leahc = pipe.ChebyshevDistance(lepa.X, leph.X, lepa.Y, leph.Y);
                        // B -> ...
                        lebcc = pipe.ChebyshevDistance(lepb.X, lepc.X, lepb.Y, lepc.Y);
                        lebdc = pipe.ChebyshevDistance(lepb.X, lepd.X, lepb.Y, lepd.Y);
                        lebec = pipe.ChebyshevDistance(lepb.X, lepe.X, lepb.Y, lepe.Y);
                        lebfc = pipe.ChebyshevDistance(lepb.X, lepf.X, lepb.Y, lepf.Y);
                        lebgc = pipe.ChebyshevDistance(lepb.X, lepg.X, lepb.Y, lepg.Y);
                        lebhc = pipe.ChebyshevDistance(lepb.X, leph.X, lepb.Y, leph.Y);
                        // C -> ...
                        lecdc = pipe.ChebyshevDistance(lepc.X, lepd.X, lepc.Y, lepd.Y);
                        lecec = pipe.ChebyshevDistance(lepc.X, lepe.X, lepc.Y, lepe.Y);
                        lecfc = pipe.ChebyshevDistance(lepc.X, lepf.X, lepc.Y, lepf.Y);
                        lecgc = pipe.ChebyshevDistance(lepc.X, lepg.X, lepc.Y, lepg.Y);
                        lechc = pipe.ChebyshevDistance(lepc.X, leph.X, lepc.Y, leph.Y);
                        // D -> ...
                        ledec = pipe.ChebyshevDistance(lepd.X, lepe.X, lepd.Y, lepe.Y);
                        ledfc = pipe.ChebyshevDistance(lepd.X, lepf.X, lepd.Y, lepf.Y);
                        ledgc = pipe.ChebyshevDistance(lepd.X, lepg.X, lepd.Y, lepg.Y);
                        ledhc = pipe.ChebyshevDistance(lepd.X, leph.X, lepd.Y, leph.Y);
                        // E -> ...
                        leefc = pipe.ChebyshevDistance(lepe.X, lepf.X, lepe.Y, lepf.Y);
                        leegc = pipe.ChebyshevDistance(lepe.X, lepg.X, lepe.Y, lepg.Y);
                        leehc = pipe.ChebyshevDistance(lepe.X, leph.X, lepe.Y, leph.Y);
                        // F -> ...
                        lefgc = pipe.ChebyshevDistance(lepf.X, lepg.X, lepf.Y, lepg.Y);
                        lefhc = pipe.ChebyshevDistance(lepf.X, leph.X, lepf.Y, leph.Y);
                        // G -> ...
                        leghc = pipe.ChebyshevDistance(lepg.X, leph.X, lepg.Y, leph.Y);


                        // Right Eye ... Chebyshev
                        double
                            reabc, reacc, readc, reaec, reafc, reagc, reahc,
                            rebcc, rebdc, rebec, rebfc, rebgc, rebhc,
                            recdc, recec, recfc, recgc, rechc,
                            redec, redfc, redgc, redhc,
                            reefc, reegc, reehc,
                            refgc, refhc,
                            reghc;

                        // A -> ...
                        reabc = pipe.ChebyshevDistance(repa.X, repb.X, repa.Y, repb.Y);
                        reacc = pipe.ChebyshevDistance(repa.X, repc.X, repa.Y, repc.Y);
                        readc = pipe.ChebyshevDistance(repa.X, repd.X, repa.Y, repd.Y);
                        reaec = pipe.ChebyshevDistance(repa.X, repe.X, repa.Y, repe.Y);
                        reafc = pipe.ChebyshevDistance(repa.X, repf.X, repa.Y, repf.Y);
                        reagc = pipe.ChebyshevDistance(repa.X, repg.X, repa.Y, repg.Y);
                        reahc = pipe.ChebyshevDistance(repa.X, reph.X, repa.Y, reph.Y);
                        // B -> ...
                        rebcc = pipe.ChebyshevDistance(repb.X, repc.X, repb.Y, repc.Y);
                        rebdc = pipe.ChebyshevDistance(repb.X, repd.X, repb.Y, repd.Y);
                        rebec = pipe.ChebyshevDistance(repb.X, repe.X, repb.Y, repe.Y);
                        rebfc = pipe.ChebyshevDistance(repb.X, repf.X, repb.Y, repf.Y);
                        rebgc = pipe.ChebyshevDistance(repb.X, repg.X, repb.Y, repg.Y);
                        rebhc = pipe.ChebyshevDistance(repb.X, reph.X, repb.Y, reph.Y);
                        // C -> ...
                        recdc = pipe.ChebyshevDistance(repc.X, repd.X, repc.Y, repd.Y);
                        recec = pipe.ChebyshevDistance(repc.X, repe.X, repc.Y, repe.Y);
                        recfc = pipe.ChebyshevDistance(repc.X, repf.X, repc.Y, repf.Y);
                        recgc = pipe.ChebyshevDistance(repc.X, repg.X, repc.Y, repg.Y);
                        rechc = pipe.ChebyshevDistance(repc.X, reph.X, repc.Y, reph.Y);
                        // D -> ...
                        redec = pipe.ChebyshevDistance(repd.X, repe.X, repd.Y, repe.Y);
                        redfc = pipe.ChebyshevDistance(repd.X, repf.X, repd.Y, repf.Y);
                        redgc = pipe.ChebyshevDistance(repd.X, repg.X, repd.Y, repg.Y);
                        redhc = pipe.ChebyshevDistance(repd.X, reph.X, repd.Y, reph.Y);
                        // E -> ...
                        reefc = pipe.ChebyshevDistance(repe.X, repf.X, repe.Y, repf.Y);
                        reegc = pipe.ChebyshevDistance(repe.X, repg.X, repe.Y, repg.Y);
                        reehc = pipe.ChebyshevDistance(repe.X, reph.X, repe.Y, reph.Y);
                        // F -> ...
                        refgc = pipe.ChebyshevDistance(repf.X, repg.X, repf.Y, repg.Y);
                        refhc = pipe.ChebyshevDistance(repf.X, reph.X, repf.Y, reph.Y);
                        // G -> ...
                        reghc = pipe.ChebyshevDistance(repg.X, reph.X, repg.Y, reph.Y);

                        string resultsLabels =
                            $"{file.Directory.Name},{file.Name},{face.Attributes.Gender.Value.ToLower()},{face.Attributes.Ethnicity.Value.ToLower()}";

                        string resultsPoints =
                            $"{lepa.X.ToString().Replace(",", ".")},{lepa.Y.ToString().Replace(",", ".")}," +
                            $"{lepb.X.ToString().Replace(",", ".")},{lepb.Y.ToString().Replace(",", ".")}," +
                            $"{lepc.X.ToString().Replace(",", ".")},{lepc.Y.ToString().Replace(",", ".")}," +
                            $"{lepd.X.ToString().Replace(",", ".")},{lepd.Y.ToString().Replace(",", ".")}," +
                            $"{lepe.X.ToString().Replace(",", ".")},{lepe.Y.ToString().Replace(",", ".")}," +
                            $"{lepf.X.ToString().Replace(",", ".")},{lepf.Y.ToString().Replace(",", ".")}," +
                            $"{lepg.X.ToString().Replace(",", ".")},{lepg.Y.ToString().Replace(",", ".")}," +
                            $"{leph.X.ToString().Replace(",", ".")},{leph.Y.ToString().Replace(",", ".")}," +
                            $"{repa.X.ToString().Replace(",", ".")},{repa.Y.ToString().Replace(",", ".")}," +
                            $"{repb.X.ToString().Replace(",", ".")},{repb.Y.ToString().Replace(",", ".")}," +
                            $"{repc.X.ToString().Replace(",", ".")},{repc.Y.ToString().Replace(",", ".")}," +
                            $"{repd.X.ToString().Replace(",", ".")},{repd.Y.ToString().Replace(",", ".")}," +
                            $"{repe.X.ToString().Replace(",", ".")},{repe.Y.ToString().Replace(",", ".")}," +
                            $"{repf.X.ToString().Replace(",", ".")},{repf.Y.ToString().Replace(",", ".")}," +
                            $"{repg.X.ToString().Replace(",", ".")},{repg.Y.ToString().Replace(",", ".")}," +
                            $"{reph.X.ToString().Replace(",", ".")},{reph.Y.ToString().Replace(",", ".")}";

                        string resultsEuclidean =
                            $"{leabe.ToString().Replace(",", ".")},{leace.ToString().Replace(",", ".")},{leade.ToString().Replace(",", ".")},{leaee.ToString().Replace(",", ".")},{leafe.ToString().Replace(",", ".")},{leage.ToString().Replace(",", ".")},{leahe.ToString().Replace(",", ".")}," +
                            $"{lebce.ToString().Replace(",", ".")},{lebde.ToString().Replace(",", ".")},{lebee.ToString().Replace(",", ".")},{lebfe.ToString().Replace(",", ".")},{lebge.ToString().Replace(",", ".")},{lebhe.ToString().Replace(",", ".")}," +
                            $"{lecde.ToString().Replace(",", ".")},{lecee.ToString().Replace(",", ".")},{lecfe.ToString().Replace(",", ".")},{lecge.ToString().Replace(",", ".")},{leche.ToString().Replace(",", ".")}," +
                            $"{ledee.ToString().Replace(",", ".")},{ledfe.ToString().Replace(",", ".")},{ledge.ToString().Replace(",", ".")},{ledhe.ToString().Replace(",", ".")}," +
                            $"{leefe.ToString().Replace(",", ".")},{leege.ToString().Replace(",", ".")},{leehe.ToString().Replace(",", ".")}," +
                            $"{lefge.ToString().Replace(",", ".")},{lefhe.ToString().Replace(",", ".")}," +
                            $"{leghe.ToString().Replace(",", ".")}," +

                            $"{reabe.ToString().Replace(",", ".")},{reace.ToString().Replace(",", ".")},{reade.ToString().Replace(",", ".")},{reaee.ToString().Replace(",", ".")},{reafe.ToString().Replace(",", ".")},{reage.ToString().Replace(",", ".")},{reahe.ToString().Replace(",", ".")}," +
                            $"{rebce.ToString().Replace(",", ".")},{rebde.ToString().Replace(",", ".")},{rebee.ToString().Replace(",", ".")},{rebfe.ToString().Replace(",", ".")},{rebge.ToString().Replace(",", ".")},{rebhe.ToString().Replace(",", ".")}," +
                            $"{recde.ToString().Replace(",", ".")},{recee.ToString().Replace(",", ".")},{recfe.ToString().Replace(",", ".")},{recge.ToString().Replace(",", ".")},{reche.ToString().Replace(",", ".")}," +
                            $"{redee.ToString().Replace(",", ".")},{redfe.ToString().Replace(",", ".")},{redge.ToString().Replace(",", ".")},{redhe.ToString().Replace(",", ".")}," +
                            $"{reefe.ToString().Replace(",", ".")},{reege.ToString().Replace(",", ".")},{reehe.ToString().Replace(",", ".")}," +
                            $"{refge.ToString().Replace(",", ".")},{refhe.ToString().Replace(",", ".")}," +
                            $"{reghe.ToString().Replace(",", ".")}";

                        string resultsManhattan =
                            $"{leabm.ToString().Replace(",", ".")},{leacm.ToString().Replace(",", ".")},{leadm.ToString().Replace(",", ".")},{leaem.ToString().Replace(",", ".")},{leafm.ToString().Replace(",", ".")},{leagm.ToString().Replace(",", ".")},{leahm.ToString().Replace(",", ".")}," +
                            $"{lebcm.ToString().Replace(",", ".")},{lebdm.ToString().Replace(",", ".")},{lebem.ToString().Replace(",", ".")},{lebfm.ToString().Replace(",", ".")},{lebgm.ToString().Replace(",", ".")},{lebhm.ToString().Replace(",", ".")}," +
                            $"{lecdm.ToString().Replace(",", ".")},{lecem.ToString().Replace(",", ".")},{lecfm.ToString().Replace(",", ".")},{lecgm.ToString().Replace(",", ".")},{lechm.ToString().Replace(",", ".")}," +
                            $"{ledem.ToString().Replace(",", ".")},{ledfm.ToString().Replace(",", ".")},{ledgm.ToString().Replace(",", ".")},{ledhm.ToString().Replace(",", ".")}," +
                            $"{leefm.ToString().Replace(",", ".")},{leegm.ToString().Replace(",", ".")},{leehm.ToString().Replace(",", ".")}," +
                            $"{lefgm.ToString().Replace(",", ".")},{lefhm.ToString().Replace(",", ".")}," +
                            $"{leghm.ToString().Replace(",", ".")}," +

                            $"{reabm.ToString().Replace(",", ".")},{reacm.ToString().Replace(",", ".")},{readm.ToString().Replace(",", ".")},{reaem.ToString().Replace(",", ".")},{reafm.ToString().Replace(",", ".")},{reagm.ToString().Replace(",", ".")},{reahm.ToString().Replace(",", ".")}," +
                            $"{rebcm.ToString().Replace(",", ".")},{rebdm.ToString().Replace(",", ".")},{rebem.ToString().Replace(",", ".")},{rebfm.ToString().Replace(",", ".")},{rebgm.ToString().Replace(",", ".")},{rebhm.ToString().Replace(",", ".")}," +
                            $"{recdm.ToString().Replace(",", ".")},{recem.ToString().Replace(",", ".")},{recfm.ToString().Replace(",", ".")},{recgm.ToString().Replace(",", ".")},{rechm.ToString().Replace(",", ".")}," +
                            $"{redem.ToString().Replace(",", ".")},{redfm.ToString().Replace(",", ".")},{redgm.ToString().Replace(",", ".")},{redhm.ToString().Replace(",", ".")}," +
                            $"{reefm.ToString().Replace(",", ".")},{reegm.ToString().Replace(",", ".")},{reehm.ToString().Replace(",", ".")}," +
                            $"{refgm.ToString().Replace(",", ".")},{refhm.ToString().Replace(",", ".")}," +
                            $"{reghm.ToString().Replace(",", ".")}";

                        string resultsChebyshev =
                            $"{leabc.ToString().Replace(",", ".")},{leacc.ToString().Replace(",", ".")},{leadc.ToString().Replace(",", ".")},{leaec.ToString().Replace(",", ".")},{leafc.ToString().Replace(",", ".")},{leagc.ToString().Replace(",", ".")},{leahc.ToString().Replace(",", ".")}," +
                            $"{lebcc.ToString().Replace(",", ".")},{lebdc.ToString().Replace(",", ".")},{lebec.ToString().Replace(",", ".")},{lebfc.ToString().Replace(",", ".")},{lebgc.ToString().Replace(",", ".")},{lebhc.ToString().Replace(",", ".")}," +
                            $"{lecdc.ToString().Replace(",", ".")},{lecec.ToString().Replace(",", ".")},{lecfc.ToString().Replace(",", ".")},{lecgc.ToString().Replace(",", ".")},{lechc.ToString().Replace(",", ".")}," +
                            $"{ledec.ToString().Replace(",", ".")},{ledfc.ToString().Replace(",", ".")},{ledgc.ToString().Replace(",", ".")},{ledhc.ToString().Replace(",", ".")}," +
                            $"{leefc.ToString().Replace(",", ".")},{leegc.ToString().Replace(",", ".")},{leehc.ToString().Replace(",", ".")}," +
                            $"{lefgc.ToString().Replace(",", ".")},{lefhc.ToString().Replace(",", ".")}," +
                            $"{leghc.ToString().Replace(",", ".")}," +

                            $"{reabc.ToString().Replace(",", ".")},{reacc.ToString().Replace(",", ".")},{readc.ToString().Replace(",", ".")},{reaec.ToString().Replace(",", ".")},{reafc.ToString().Replace(",", ".")},{reagc.ToString().Replace(",", ".")},{reahc.ToString().Replace(",", ".")}," +
                            $"{rebcc.ToString().Replace(",", ".")},{rebdc.ToString().Replace(",", ".")},{rebec.ToString().Replace(",", ".")},{rebfc.ToString().Replace(",", ".")},{rebgc.ToString().Replace(",", ".")},{rebhc.ToString().Replace(",", ".")}," +
                            $"{recdc.ToString().Replace(",", ".")},{recec.ToString().Replace(",", ".")},{recfc.ToString().Replace(",", ".")},{recgc.ToString().Replace(",", ".")},{rechc.ToString().Replace(",", ".")}," +
                            $"{redec.ToString().Replace(",", ".")},{redfc.ToString().Replace(",", ".")},{redgc.ToString().Replace(",", ".")},{redhc.ToString().Replace(",", ".")}," +
                            $"{reefc.ToString().Replace(",", ".")},{reegc.ToString().Replace(",", ".")},{reehc.ToString().Replace(",", ".")}," +
                            $"{refgc.ToString().Replace(",", ".")},{refhc.ToString().Replace(",", ".")}," +
                            $"{reghc.ToString().Replace(",", ".")}";

                        string resultsFull =
                            resultsEuclidean + "," + resultsManhattan + "," + resultsChebyshev;

                        File.AppendAllLines(txtOutput.Text + "//" + datasetPoints, new List<string>() { resultsLabels + "," + resultsPoints });
                        File.AppendAllLines(txtOutput.Text + "//" + datasetEuclidean, new List<string>() { resultsLabels + "," + resultsEuclidean });
                        File.AppendAllLines(txtOutput.Text + "//" + datasetManhattan, new List<string>() { resultsLabels + "," + resultsManhattan });
                        File.AppendAllLines(txtOutput.Text + "//" + datasetChebyshev, new List<string>() { resultsLabels + "," + resultsChebyshev });
                        File.AppendAllLines(txtOutput.Text + "//" + datasetFull, new List<string>() { resultsLabels + "," + resultsFull });
                    }
                }

                i++;
                await Task.Delay(500);
            }
        }



        private void DrawArea(OpenCvSharp.Mat image, List<OpenCvSharp.Point2f> points, bool addMarker = false)
        {
            if (points.Count == 0)
                return;

            if (addMarker)
            {
                //var markerStyle = (OpenCvSharp.MarkerStyle)Enum.Parse(typeof(OpenCvSharp.MarkerStyle), comboBoxMarkerStyle.SelectedItem.ToString());
                //var color = (Color)(comboBoxColorStyle.SelectedItem as PropertyInfo).GetValue(null, null);
                //var colorStyle = OpenCvSharp.Scalar.FromRgb(color.R, color.G, color.B);
                foreach (OpenCvSharp.Point2f item in points)
                    image.DrawMarker((int)item.X, (int)item.Y, OpenCvSharp.Scalar.Green, OpenCvSharp.MarkerStyle.CircleFilled, 2, OpenCvSharp.LineTypes.AntiAlias, 1);
            }
        }

        private List<FileInfo> GetFilesRecursive(string path)
        {
            var dInfo = new DirectoryInfo(path);

            var files = dInfo.GetFiles().Where(w => w.Extension.In(".png", ".jpg")).ToList();

            var directories = dInfo.GetDirectories();
            if (directories.Count() > 0)
                foreach (var directory in directories)
                    files.AddRange(GetFilesRecursive(directory.FullName));

            return files;
        }
    }
}
