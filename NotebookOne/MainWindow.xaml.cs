using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NotebookOne
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			GetSaveFolder();
		}

		private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Rich Text Format (*.rtf) |*.rtf|All files (*.*)|*.*";
			if (saveFileDialog.ShowDialog() == true)
			{
				FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
				TextRange textRange = new TextRange(rtbTextEditor.Document.ContentStart, rtbTextEditor.Document.ContentEnd);
				textRange.Save(fileStream, DataFormats.Rtf);
			}
		}

		private string GetSaveFolder()
		{
			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var subFolderPath = System.IO.Path.Combine(path, "Notebook_One");
			//Create folder if does not already exist.
			if (!Directory.Exists(subFolderPath))
			{
				Directory.CreateDirectory(subFolderPath);
			}
			return subFolderPath;
		}
	}
}
