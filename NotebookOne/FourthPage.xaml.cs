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
using System.Windows.Shapes;

namespace NotebookOne
{
    /// <summary>
    /// Interaction logic for FourthPage.xaml
    /// </summary>
    public partial class FourthPage : Window
    {
        public FourthPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThirdPage tp = new ThirdPage();
            Close();
            tp.ShowDialog();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FifthPage ftp = new FifthPage();
            Close();
            ftp.ShowDialog();
        }
    }
}
