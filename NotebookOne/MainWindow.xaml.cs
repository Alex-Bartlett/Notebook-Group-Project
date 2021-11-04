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
			LoadFilesIntoGrid(FilesGrid);
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

		/// <summary>
		/// Creates a grid row in the given grid
		/// </summary>
		/// <param name="grid">The grid to use</param>
		/// <param name="name">The name of the row</param>
		private void CreateGridRow(Grid grid, string name)
		{
			RowDefinition row = new RowDefinition();
			row.Height = new GridLength(20, GridUnitType.Auto);
			row.Name = name;
			grid.RowDefinitions.Add(row);
		}

		private void CreateFileBtn(Grid grid, int gridRow, int gridColumn, string text)
		{
			Button btn = new Button();
			btn.Width = 100;
			btn.Height = 20;
			//Set grid positions
			Grid.SetColumn(btn, gridColumn);
			Grid.SetRow(btn, gridRow);
			btn.Name = text;
			btn.Content = text;
			btn.Margin = new Thickness(5, 5, 5, 0);
			//Add button
			grid.Children.Add(btn);
		}

		/// <summary>
		/// Gets all .rtf files in saving directory and returns them as a list
		/// </summary>
		/// <returns>A list of all .rtf files in directory</returns>
		private List<string> GetFilesInDirectory()
		{
			//Get the path of the save folder, and generate if it doesn't exist.
			string folderPath = GetSaveFolder();
			//Retrieve all files from folder
			string[] f = Directory.GetFiles(folderPath);
			List<string> files = f.ToList();
			//Remove non-rtf files
			foreach(string filePath in files)
			{
				if (!filePath.EndsWith(".rtf"))
				{
					files.Remove(filePath);
				}
			}
			return files;
		}

		/// <summary>
		/// Takes all files in the saving directory and loads them into a grid.
		/// </summary>
		/// <param name="grid">The grid to load files into</param>
		private void LoadFilesIntoGrid(Grid grid)
		{
			List<string> files = GetFilesInDirectory();
			for(int index = 0; index < files.Count; index++)
			{
				string filePath = files[index];
				string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
				CreateGridRow(grid, fileName);
				CreateFileBtn(grid, index, 1, fileName);
				Console.WriteLine("Added button for " + fileName + ". I put it in row " + index);
			}
		}
	}
}
