using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WPF_DataBinding_DataSet
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window
  {
    dsStudents ds = new dsStudents();
    public MainWindow()
    {  
      InitializeComponent();
			Populate();
			Application.Current.Properties["ds"] = ds;
		}
		private void miLoad_Click(object sender, RoutedEventArgs e)
		{
			// Create an Open File dialog.
			var ofdOpen = new Microsoft.Win32.OpenFileDialog();
			ofdOpen.DefaultExt = ".xml"; // Default file extension
																	 // File extension filter
			ofdOpen.Filter = "XML Documents (.xml)|*.xml|All Files|*.*";
			ofdOpen.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
			// Show the dialog.
			if (ofdOpen.ShowDialog() == true)
			{
				try
				{
					// Load the XML document.
					ds.ReadXml(ofdOpen.FileName, XmlReadMode.Auto);
				}
				catch (Exception)
				{
					MessageBox.Show("Failed to load the XML document!\r\n" +
													"The database will be populated with three sample records.", "Error",
													MessageBoxButton.OK, MessageBoxImage.Error);
					Populate();
				}
			}
			else
			{
				// Populate the database.
				Populate();
			}
			Application.Current.Properties["ds"] = ds;
		}

		private void Populate()
		{
			// Create three records.
			ds.dtStudents.AdddtStudentsRow("Smarty", "Dwarf", "DERTFB", 3.5, "sd@net.hu");
			ds.dtStudents.AdddtStudentsRow("Grumpy", "Dwarf", "DUFTRP", 4.2, "gd@net.hu");
			ds.dtStudents.AdddtStudentsRow("Papa", "Dwarf", "PAPTFB", 4.9, "pd@net.hu");
		}

		private void miSave_Click(object sender, RoutedEventArgs e)
		{
			// Create a Save File dialog.
			var sfdSave = new Microsoft.Win32.SaveFileDialog();
			sfdSave.DefaultExt = ".xml"; // Default file extension
																	 // File extension filter
			sfdSave.Filter = "XML Documents (.xml)|*.xml|All Files|*.*";
			// Show the dialog.
			if (sfdSave.ShowDialog() == true)
			{
				// Save the XML document.
				ds.WriteXml(sfdSave.FileName, XmlWriteMode.WriteSchema);
			}
		}

		private void miExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown(0);
		}


		private void miRecordByRecord_Click(object sender, RoutedEventArgs e)
		{
			dgGrid.Visibility = Visibility.Collapsed;
			ucRecordView.Visibility = Visibility.Visible;
		}

		private void miAllData_Click(object sender, RoutedEventArgs e)
		{
			ucRecordView.Visibility = Visibility.Collapsed;
			dgGrid.Visibility = Visibility.Visible;
			dgGrid.ItemsSource = ds.dtStudents;
		}

	}
}
