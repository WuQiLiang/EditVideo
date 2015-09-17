﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace 简易视频编辑器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 视频加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video_Load(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();//定义计时器
            timer.Interval = TimeSpan.FromMilliseconds(1000);//1秒间隔
            timer.Tick += (ss,ee)=>
                {
                    var ts = MediaElement1.Position;
                    shishiTime.Content=string.Format("{0:00}:{1:00}:{2:00}",ts.Hours,ts.Minutes,ts.Seconds);//格式化当前视频时间
                    timeSlider.Value = ts.TotalMilliseconds;//滑块显示当前视频进度 

                };
                    timer.Start();
        }



        /// <summary>
        /// 视频开始与暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (button.Content.ToString()=="播放")
            {
                
                MediaElement1.Play();
                button.Content = "暂停";
                getVideoName();
            }
            else
            {
                MediaElement1.Pause();
                button.Content = "播放";
                getVideoName();
            }
            
        }

        /// <summary>
        /// 获取视频标题
        /// </summary>
        private void getVideoName()
        {
            string path = this.MediaElement1.Source.LocalPath;
            string currentfileName = path.Substring(path.LastIndexOf('\\') + 1);
            MediaElement1.ToolTip = currentfileName;
        }


        /// <summary>
        /// 视频停止（停止后播放按钮重置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MediaElement1.Stop();
            button.Content = "播放";
        }


        /// <summary>
        /// 打开视频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MediaElement1.Source = new Uri(filepath, UriKind.Relative);
        }

        /// <summary>
        /// 前进5秒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forward_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Position = MediaElement1.Position + TimeSpan.FromSeconds(5);
        }

        /// <summary>
        /// 视频后退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Position = MediaElement1.Position - TimeSpan.FromSeconds(5);
        }

        /// <summary>
        /// 视频开始时获取视频时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {

            var ts = MediaElement1.NaturalDuration.TimeSpan;
            totalTime.Content=string.Format("{0:00}:{1:00}:{2:00}",ts.Hours,ts.Minutes,ts.Seconds);//格式化当前视频时间
            timeSlider.Maximum = ts.TotalMilliseconds;
            
        }

        /// <summary>
        /// 滑块控制视频播放进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var ts = TimeSpan.FromMilliseconds(e.NewValue);
            MediaElement1.Position = ts;
        }



    }
}
