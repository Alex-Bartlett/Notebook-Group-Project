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
    /// Interaction logic for ThirdPage.xaml
    /// </summary>
    public partial class ThirdPage : Window
    {
        public ThirdPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SecondPage sp = new SecondPage();
            sp.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FourthPage fp = new FourthPage();
            fp.ShowDialog();
        }
    }
}
