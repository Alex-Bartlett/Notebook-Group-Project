using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
		//Auto-save timer
		public Timer autoSaveTimer = new Timer
		{
			Interval = 10000
		};

		public MainWindow()
		{
			InitializeComponent();
			RefreshFilesGrid();
			PopUpBootUp();
			//Start auto-save timer
			autoSaveTimer.Elapsed += AutoSaveTimer_Elapsed;
			autoSaveTimer.Start();
		}		

		private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveAs();
			RefreshFilesGrid();
		}

		private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			string path = GetRTBDataContext();
			//Check data context is applied and that it is a valid path
			if (File.Exists(path))
			{
				FileStream fileStream = new FileStream(path, FileMode.Create);
				TextRange textRange = new TextRange(rtbTextEditor.Document.ContentStart, rtbTextEditor.Document.ContentEnd);
				textRange.Save(fileStream, DataFormats.Rtf);
				fileStream.Dispose();
			}
			else
			{
				SaveAs();
				RefreshFilesGrid();
			}
			
		}

		/// <summary>
		/// Prompts the user to create a new file to save the document to.
		/// </summary>
		private void SaveAs()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Rich Text Format (*.rtf) |*.rtf|All files (*.*)|*.*";
			if (saveFileDialog.ShowDialog() == true)
			{
				FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
				TextRange textRange = new TextRange(rtbTextEditor.Document.ContentStart, rtbTextEditor.Document.ContentEnd);
				textRange.Save(fileStream, DataFormats.Rtf);
				fileStream.Dispose();
				SetRTBDataContext(saveFileDialog.FileName);
			}
		}

		/// <summary>
		/// Sets the data context of the richtextbox to the given string
		/// </summary>
		/// <param name="data">The data to add</param>
		private void SetRTBDataContext(string data)
		{
			rtbTextEditor.DataContext = data;
		}

		/// <summary>
		/// Retrieves the data context of the richtextbox
		/// </summary>
		/// <returns>Path if exists, "NOT_FOUND" if no data context assigned</returns>
		private string GetRTBDataContext()
		{
			return rtbTextEditor.DataContext != null ? rtbTextEditor.DataContext.ToString() : "NOT_FOUND";
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
		/// Checks to see if the save folder has content
		/// </summary>
		/// <returns>True if file count > 0, false otherwise</returns>
		private bool SaveFolderHasContent()
		{
			string path = GetSaveFolder();
			string[] files = Directory.GetFiles(path);
			return files.Length > 0 ? true : false;
		}

		

		/// <summary>
		/// Opens a file dialog, then loads it into the RTB.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenFile(object sender, ExecutedRoutedEventArgs e)
		{
			autoSaveTimer.Stop(); //Stop auto-saving whilst opening
			OpenFileDialog openFile1 = new OpenFileDialog();

			openFile1.DefaultExt = "*.rtf";
			openFile1.Filter = "RTF Files|*.rtf";

			if (openFile1.ShowDialog() == true)
			{
				LoadFile(openFile1.FileName);
			}
			autoSaveTimer.Start(); //Resume auto-saving once opened
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
				SetRTBDataContext(path); //Set current path to data context
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

		private void NewFile(object sender, ExecutedRoutedEventArgs e)
		{
			SaveExecuted(null, null); //Save current document
			rtbTextEditor.Document.Blocks.Clear();
			//Prompt the user to create a new save file for the note
			SaveAs();
			RefreshFilesGrid();
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

		private void rtbTextEditor_SelectionChanged(object sender, RoutedEventArgs e)
		{
			UpdateToggleButtonState();
		}

		private void UpdateToggleButtonState()
		{
			UpdateItemCheckedState(btnBold, TextElement.FontWeightProperty, FontWeights.Bold);
			UpdateItemCheckedState(btnItalic, TextElement.FontStyleProperty, FontStyles.Italic);
			UpdateItemCheckedState(btnUnderline, Inline.TextDecorationsProperty, TextDecorations.Underline);
			//UntoggleAlignmentBtns handles when a button is pressed, but this still handles when a line has different alignment
			UpdateItemCheckedState(btnAlignLeft, Block.TextAlignmentProperty, TextAlignment.Left);
			UpdateItemCheckedState(btnAlignCentre, Block.TextAlignmentProperty, TextAlignment.Center);
			UpdateItemCheckedState(btnAlignRight, Block.TextAlignmentProperty, TextAlignment.Right);
		}

		private void UpdateItemCheckedState(ToggleButton button, DependencyProperty formattingProperty, object expectedValue)
		{
			object currentValue = rtbTextEditor.Selection.GetPropertyValue(formattingProperty);
			button.IsChecked = (currentValue == DependencyProperty.UnsetValue) ? false : currentValue != null && currentValue.Equals(expectedValue);
		}

		private void UntoggleAlignmentBtns(object sender, RoutedEventArgs e)
		{
			//Define all buttons
			List<ToggleButton> btns = new List<ToggleButton>(){btnAlignRight, btnAlignLeft, btnAlignCentre};
			ToggleButton btnPressed = (ToggleButton)sender;
			btns.Remove(btnPressed); //Remove the button that was pressed
			//Set other buttons to unchecked
			foreach (ToggleButton btn in btns)
			{
				btn.IsChecked = false;
			}
			btnPressed.IsChecked = true; //Set pressed button to true
		}

        private void rtbTextEditor_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateSelectedFontFamily();
		}

		private void UpdateSelectedFontFamily()
		{
			FontFamily selectedFont = rtbTextEditor.Selection.GetPropertyValue(TextBlock.FontFamilyProperty) as FontFamily;
			SelectedFontFamily.SelectedValue = selectedFont;
		}

        private void SelectedFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			
			if (!IsInitialized) return;

			object selectedFont = SelectedFontFamily.SelectedValue;

			if (selectedFont != null)
			{
				rtbTextEditor.FontFamily = new FontFamily(selectedFont.ToString());
			}
		
	    }
		private async void PopUpBootUp()
		{
			await Task.Delay(2000);
			if (SaveFolderHasContent() == false)
			{
				WelcomeScreen ws = new WelcomeScreen();
				ws.ShowDialog();

			}
		}

		private void AutoSaveTimer_Elapsed(object sender, ElapsedEventArgs e)
		{ 
			this.Dispatcher.Invoke(() => {
				autoSaveTimer.Stop();
				SaveExecuted(null, null);
				autoSaveTimer.Start();
			});
		}
		
		private void ApplicationCloseButton(object sender, RoutedEventArgs e)
        {
			this.Close();
        }

		private void AboutUs(object sender, RoutedEventArgs e)
        {
			MessageBox.Show("This application has been created by Alex Barlett and Omar Shivji");
        }

		private void Help(object sender, RoutedEventArgs e)
        {
			WelcomeScreen ws = new WelcomeScreen();
			ws.ShowDialog();
        }

	}
}

