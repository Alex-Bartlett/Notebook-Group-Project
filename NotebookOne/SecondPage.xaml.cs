﻿using System;
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
using System.Windows.Shapes;

namespace NotebookOne
{
    /// <summary>
    /// Interaction logic for SecondPage.xaml
    /// </summary>
    public partial class SecondPage : Window
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThirdPage tp = new ThirdPage();
            tp.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WelcomeScreen ws = new WelcomeScreen();
            ws.ShowDialog();
        }
    }
}
