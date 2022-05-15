using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
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
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExamplePage.xaml
    /// </summary>
    public partial class ExamplePage : Page
    {
        private readonly Service _currentExample;
        public ExamplePage(Service selectedExamlpe)
        {
            InitializeComponent();
            _currentExample = selectedExamlpe;
            DataContext = _currentExample;
        }
        private bool _isPlay;
        private void BtnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlay)
            {
                Pause();
                _isPlay = false;
            }
            else
            {
                Play();
                _isPlay = true;
            }
        }
        private void Play()
        {
            _player.Play();
            _timer.Interval = 1000;
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
            IPlay.Visibility = Visibility.Collapsed;
            IPause.Visibility = Visibility.Visible;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var position = _player.Position.Minutes * 60 + _player.Position.Seconds;
                SPosition.Value = position;
            });
        }

        private void Pause()
        {
            _player.Pause();
            IPlay.Visibility = Visibility.Visible;
            IPause.Visibility = Visibility.Collapsed;
        }
        internal void Stop()
        {
            if (_player.CanPause)
                _player.Stop();
            DeleteFile();
        }

        private static void DeleteFile()
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{Data.CurrentDirectory}/orderMusic.mp3";
            File.Delete(path);
        }

        private bool _moveTrack;
        private void SPosition_OnMouseEnter(object sender, MouseEventArgs e)
        {
            _moveTrack = true;
        }
        private void SPosition_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_moveTrack) return;
            var i2 = int.Parse(Math.Truncate(SPosition.Value / 60).ToString()); // Получаем количество минут !
            var i1 = int.Parse(Math.Truncate(SPosition.Value % 60).ToString());

            _player.Position = new TimeSpan(0, 0, i2, i1);
        }
        private void SPosition_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _moveTrack = false;
        }


        private void SVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _player.Volume = SVolume.Value;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.WriteAllBytes($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3", _currentExample.Music);

            var uri = new Uri($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3", UriKind.Absolute);
            _player.Open(uri);

            var info = TagLib.File.Create($"{folder}/{Data.CurrentDirectory}/orderMusic.mp3");
            var time = info.Properties.Duration.Minutes * 60 + info.Properties.Duration.Seconds;
            SPosition.Maximum = time;
            _player.Volume = .5;
        }
        private readonly MediaPlayer _player = new MediaPlayer();
        private readonly Timer _timer = new Timer();


    }
}
