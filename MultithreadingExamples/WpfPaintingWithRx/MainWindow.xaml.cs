using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfPaintingWithRx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Label label;

        public MainWindow()
        {
            InitializeComponent();

            label = new Label();
            this.canvas.Children.Add(label);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var mouseMovePointsStream =
                Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                          .Select(singleEvent => singleEvent.EventArgs.GetPosition(this.canvas));

            var mouseLeftDownStream = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseLeftButtonDown");
            var mouseLeftUpStream = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseLeftButtonUp");
            var mouseLeftButtonStream = Observable.Merge(mouseLeftDownStream, mouseLeftUpStream).Select(singleEvent => singleEvent.EventArgs.ButtonState);

            var mouseRightDownStream = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseRightButtonDown");
            var mouseRightUpStream = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseRightButtonUp");
            var mouseRightButtonStream = Observable.Merge(mouseRightDownStream, mouseRightUpStream).Select(singleEvent => singleEvent.EventArgs.ButtonState);

            mouseMovePointsStream.Subscribe(onNext: singlePoint =>
            {
                Canvas.SetLeft(label, singlePoint.X);
                Canvas.SetTop(label, singlePoint.Y - 15);
                label.Content = String.Format("X:{0}, Y:{1}", singlePoint.X, singlePoint.Y);
            });

            Observable
                .CombineLatest(mouseMovePointsStream, mouseLeftButtonStream, (point, buttonState) => new { point, buttonState })
                .Where(element => element.buttonState == MouseButtonState.Pressed)
                .Subscribe(onNext: (element) =>
                {
                    PaintEllipse(element.point);
                });

            var mouseMovePointsZippedStream = Observable.Zip(mouseMovePointsStream, mouseMovePointsStream.Skip(1), (previous, current) =>
            {
                return new
                {
                    StartPoint = new Point { X = previous.X, Y = previous.Y },
                    EndPoint = new Point { X = current.X, Y = current.Y }
                };
            });

            Observable
                .CombineLatest(mouseMovePointsZippedStream, mouseRightButtonStream, (points, buttonState) => new { points, buttonState })
                .Where(element => element.buttonState == MouseButtonState.Pressed)
                .Subscribe(onNext: (singleEvent) =>
                {
                    PaintLine(singleEvent.points.StartPoint, singleEvent.points.EndPoint);
                });
        }

        private void PaintEllipse(Point point)
        {
            var ellipse = new Ellipse();
            var blackBrush = new SolidColorBrush() { Color = Color.FromArgb(255, 0, 255, 0) };

            ellipse.Fill = blackBrush;
            ellipse.Height = 2;
            ellipse.Width = 2;

            canvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);
        }

        private void PaintLine(Point startPoint, Point endPoint)
        {
            var line = new Line()
            {
                Stroke = Brushes.Red,
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                StrokeThickness = 2
            };

            canvas.Children.Add(line);
        }
    }
}
