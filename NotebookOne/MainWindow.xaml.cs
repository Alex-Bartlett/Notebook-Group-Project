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
			RefreshFilesGrid();
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
			RefreshFilesGrid();
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
		/// Opens a file dialog, then loads it into the RTB.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenFile(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog openFile1 = new OpenFileDialog();

			openFile1.DefaultExt = "*.rtf";
			openFile1.Filter = "RTF Files|*.rtf";

			if (openFile1.ShowDialog() == true)
			{
				LoadFile(openFile1.FileName);
			}
		}

		/// <summary>
		/// Loads a file into the RTB
		/// </summary>
		/// <param name="path">File path to load</param>
		private void LoadFile(string path)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(path, FileMode.Open, FileAccess.Read);
				TextRange textRange = new TextRange(rtbTextEditor.Document.ContentStart, rtbTextEditor.Document.ContentEnd);
				textRange.Load(file, DataFormats.Rtf);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message + " Did the user try loading the current document?");
			}
			finally
			{
				if (file != null)
				{
					file.Dispose();
				}
			}
		}
		/// <summary>
		/// Creates a grid row in the given grid
		/// </summary>
		/// <param name="grid">The grid to use</param>
		/// <param name="name">The name of the row</param>
		private void CreateGridRow(Grid grid)
		{
			RowDefinition row = new RowDefinition
			{
				Height = new GridLength(20, GridUnitType.Auto)
			};
			grid.RowDefinitions.Add(row);
		}

		private void CreateFileBtn(Grid grid, int gridRow, int gridColumn, string content, string path)
		{
			Button btn = new Button
			{
				Width = 150,
				Height = 25,
				DataContext = path,
				Content = content,
				Margin = new Thickness(5, 5, 5, 0),
				BorderBrush = new SolidColorBrush(Color.FromRgb(116, 116, 116)),
				Background = new SolidColorBrush(Color.FromRgb(116, 116, 116)),
				Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
			};
			btn.Click += LoadBtn_Click;
			//Set grid positions
			Grid.SetColumn(btn, gridColumn);
			Grid.SetRow(btn, gridRow);
			//Add button
			grid.Children.Add(btn);
		}

		/// <summary>
		/// Creates a button in grid that deletes file in given path
		/// </summary>
		/// <param name="grid"></param>
		/// <param name="gridRow"></param>
		/// <param name="gridColumn"></param>
		/// <param name="path">Path of file to delete</param>
		private void CreateDeleteButton(Grid grid, int gridRow, int gridColumn, string path)
		{
			Button btn = new Button
			{
				Width = 25,
				Height = 25,
				DataContext = path,
				BorderBrush = new SolidColorBrush(Color.FromRgb(116, 116, 116)),
				Background = new SolidColorBrush(Color.FromRgb(116, 116, 116)),
				Margin = new Thickness(0, 5, 0, 0)
			};
			btn.Click += DeleteBtn_Click;
			//Create delete image
			Image img = new Image();
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri("pack://application:,,,/NotebookOne;component/Images/btnDelete.png");
			bitmap.EndInit();
			img.Source = bitmap;
			//Add image to button content
			btn.Content = img;
			//Add button to grid
			Grid.SetRow(btn, gridRow);
			Grid.SetColumn(btn, gridColumn);
			grid.Children.Add(btn);
		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			//Cast to button
			Button btn = (Button)sender;
			//Retrieve file path from button data
			string path = btn.DataContext.ToString();
			if (DeleteBtnConfirmation(System.IO.Path.GetFileNameWithoutExtension(path))) {
				File.Delete(path);
				RefreshFilesGrid();
			}
		}

		/// <summary>
		/// Displays a message box asking the user to confirm that they wish to delete the file of supplied name
		/// </summary>
		/// <param name="name">The name of the file to delete (not path)</param>
		/// <returns>True if user selects yes, false if not</returns>
		private bool DeleteBtnConfirmation(string name)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete " + name + "?", "Delete", MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void LoadBtn_Click(object sender, RoutedEventArgs e)
		{
			Button clicked = (Button)sender;
			LoadFile(clicked.DataContext.ToString());
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
			foreach (string filePath in files)
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
			for (int index = 0; index < files.Count; index++)
			{
				string filePath = files[index];
				string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
				CreateGridRow(grid);
				CreateFileBtn(grid, (index + 1), 1, fileName, filePath);
				CreateDeleteButton(grid, (index + 1), 2, filePath);
			}
		}

		private void RefreshFilesGrid()
		{
			Grid grid = FilesGrid;
			//Clear files if they already exist
			int children = grid.Children.Count;
			if (children > 1)
			{
				//Remove all children except first, since this is the label
				grid.Children.RemoveRange(1, grid.Children.Count - 1);
			}
			//Load the files again
			LoadFilesIntoGrid(grid);
		}



	}
}
