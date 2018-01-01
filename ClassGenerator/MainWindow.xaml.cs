using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassGenerator
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		private FolderBrowserDialog sProjectPathDialog;

		public MainWindow()
		{
			InitializeComponent();

			this.sProjectPathDialog = new FolderBrowserDialog();
			this.sProjectPathDialog.Description = "Specify the path of the project folder.";
			this.sProjectPathDialog.ShowNewFolderButton = true;
			this.sProjectPathDialog.RootFolder = Environment.SpecialFolder.CommonDesktopDirectory;
		}

		private void updateUI()
		{

		}

		private void onLoaded(object sender, RoutedEventArgs e)
		{
			GenerateManager.init();

			var sList = GenerateManager.Instance.getGeneratorList();
			sList.Sort();

			this.TargetTypeComboBox.Items.Clear();

			foreach (var sPair in sList)
				this.TargetTypeComboBox.Items.Add(sPair.Key);

			if (this.TargetTypeComboBox.Items.Count != 0)
				this.TargetTypeComboBox.SelectedIndex = 0;
		}

		private void onClosing(object sender, CancelEventArgs e)
		{
			GenerateManager.fin();
		}

		private void onBrowseButtonClick(object sender, RoutedEventArgs e)
		{

		}

		private void onTargetNameChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void onSpecifyAuthorCheckBoxChanged(object sender, RoutedEventArgs e)
		{
			
		}

		private void onAuthorChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void onWithIncludeGuardCheckBoxChanged(object sender, RoutedEventArgs e)
		{

		}

		private void onIncludeGuardChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void onGenerateButtonClick(object sender, RoutedEventArgs e)
		{

		}
	}
}