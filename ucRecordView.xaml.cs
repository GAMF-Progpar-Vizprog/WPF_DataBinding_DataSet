using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_DataBinding_DataSet
{
	/// <summary>
	/// Interaction logic for ucRecordView.xaml
	/// </summary>
	public partial class ucRecordView : UserControl
	{
		private dsStudents ds;
		public ucRecordView()
		{
			InitializeComponent();
		}


		private void btPrevious_Click(object sender, RoutedEventArgs e)
		{
			ICollectionView cv = CollectionViewSource.GetDefaultView(ds.dtStudents);
			// If there is a previous item to move to.
			if (cv.CurrentPosition > 0)
				cv.MoveCurrentToPrevious();
			else
				// If not (i.e., the current one is the first), jump to the last one.
				cv.MoveCurrentToLast();
		}


		private void btNext_Click(object sender, RoutedEventArgs e)
		{
			ICollectionView cv = CollectionViewSource.GetDefaultView(ds.dtStudents);
			// If there are still items in the list, move to the next one.
			if (cv.CurrentPosition < ds.dtStudents.Rows.Count - 1)
				cv.MoveCurrentToNext();
			else
				// If not (i.e., the current one is the last), jump to the first one.
				cv.MoveCurrentToFirst();
		}

		private void btLowestAverage_Click(object sender, RoutedEventArgs e)
		{
			// Sorts the records of the view in ascending order based on the average.
			ds.dtStudents.DefaultView.Sort="OverallAverage ASC";
			ICollectionView cv = CollectionViewSource.GetDefaultView(ds.dtStudents);
			// Jump to the first record.
			cv.MoveCurrentToFirst();
		}

		private void btHighestAverage_Click(object sender, RoutedEventArgs e)
		{
			// Sorts the records of the view in ascending order based on the average.
			ds.dtStudents.DefaultView.Sort = "OverallAverage ASC";
			ICollectionView cv = CollectionViewSource.GetDefaultView(ds.dtStudents);
			// Jump to the last record.
			cv.MoveCurrentToLast();
		}

		private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if(!IsVisible) return;
			ds = Application.Current.Properties["ds"] as dsStudents;
			if (ds == null) return;
			grGrid.DataContext = ds.dtStudents;
		}
	}
}
