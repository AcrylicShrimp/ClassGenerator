
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ClassGenerator
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window, System.Windows.Forms.IWin32Window
	{
		public IntPtr Handle => this.sInteropHelper.Handle;

		private FolderBrowserDialog sProjectPathDialog;
		private WindowInteropHelper sInteropHelper;
		private BlurEffect sBlurEffect;
		private Brush sRedBrush;
		private Brush sBlackBrush;
		private CodeDomProvider sCodeDomProvider;
		private string sProjectPathError;
		private string sTargetNameError;
		private string sAuthorNameError;
		private string sIncludeGuardError;
		private string sNamespaceError;
		private UTF8Encoding sEncoding;

		public MainWindow()
		{
			InitializeComponent();

			this.sProjectPathDialog = new FolderBrowserDialog();
			this.sProjectPathDialog.Description = "Specify the path of the project folder.";
			this.sProjectPathDialog.ShowNewFolderButton = true;
			this.sProjectPathDialog.RootFolder = Environment.SpecialFolder.MyComputer;

			this.sInteropHelper = new WindowInteropHelper(this);

			this.sBlurEffect = new BlurEffect();
			this.sBlurEffect.KernelType = KernelType.Gaussian;
			this.sBlurEffect.Radius = 3d;

			this.sRedBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
			this.sBlackBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

			this.sCodeDomProvider = CodeDomProvider.CreateProvider("Cpp");

			this.sTargetNameError = null;
			this.sAuthorNameError = null;
			this.sIncludeGuardError = null;
			this.sNamespaceError = null;

			this.sEncoding = new UTF8Encoding(false);
		}

		#region Update UI
		private void updateProjectPath()
		{
			this.sProjectPathError = this.ProjectPathTextBlock.Text == "N/A" ? "Please specify the project path." : null;
		}

		private void updateTargetName()
		{
			if (this.TargetNameTextBox.Text.Length == 0)
			{
				this.sTargetNameError = "The target name cannot be empty.";
				return;
			}

			if (!this.sCodeDomProvider.IsValidIdentifier(this.TargetNameTextBox.Text))
			{
				this.sTargetNameError = "The target name is invalid.";
				return;
			}

			this.sTargetNameError = null;
		}

		private void updateAuthorName()
		{
			if (!this.SpecifyAuthorCheckBox.IsChecked.Value)
			{
				this.sAuthorNameError = null;
				return;
			}

			if (this.AuthorTextBox.Text.Length == 0)
			{
				this.sAuthorNameError = "The author name cannot be empty.";
				return;
			}

			if (!this.sCodeDomProvider.IsValidIdentifier(this.AuthorTextBox.Text))
			{
				this.sAuthorNameError = "The author name is invalid.";
				return;
			}

			this.sAuthorNameError = null;
		}

		private void updateIncludeGuard()
		{
			if (!this.WithIncludeGuardCheckBox.IsChecked.Value)
			{
				this.sIncludeGuardError = null;
				return;
			}

			if (this.IncludeGuardTextBox.Text.Length == 0)
			{
				this.sIncludeGuardError = "The include guard cannot be empty.";
				return;
			}

			if (!this.sCodeDomProvider.IsValidIdentifier(this.IncludeGuardTextBox.Text))
			{
				this.sIncludeGuardError = "The include guard is invalid.";
				return;
			}

			this.sIncludeGuardError = null;
		}

		private void updateNamespace()
		{
			if (!this.WithNamespaceCheckBox.IsChecked.Value)
			{
				this.sNamespaceError = null;
				return;
			}

			if (this.NamespaceTextBox.Text.Length == 0)
			{
				this.sNamespaceError = "The namespace cannot be empty.";
				return;
			}

			var sNamespace = this.NamespaceTextBox.Text.Split('.');

			foreach (var sSubNamespace in sNamespace)
			{
				if (sSubNamespace.Length == 0)
				{
					this.sNamespaceError = "The namespace is invalid.";
					return;
				}

				if (!this.sCodeDomProvider.IsValidIdentifier(sSubNamespace))
				{
					this.sNamespaceError = "The namespace is invalid.";
					return;
				}
			}

			this.sNamespaceError = null;
		}

		private void updateGenerateButton()
		{
			if (this.sProjectPathError != null)
			{
				this.StatusTextBlock.Text = this.sProjectPathError;
				this.StatusTextBlock.Foreground = this.sRedBrush;
				this.GenerateButton.IsEnabled = false;
				this.GenerateButton.Effect = this.sBlurEffect;
				return;
			}

			if (this.sTargetNameError != null)
			{
				this.StatusTextBlock.Text = this.sTargetNameError;
				this.StatusTextBlock.Foreground = this.sRedBrush;
				this.GenerateButton.IsEnabled = false;
				this.GenerateButton.Effect = this.sBlurEffect;
				return;
			}

			if (this.sAuthorNameError != null)
			{
				this.StatusTextBlock.Text = this.sAuthorNameError;
				this.StatusTextBlock.Foreground = this.sRedBrush;
				this.GenerateButton.IsEnabled = false;
				this.GenerateButton.Effect = this.sBlurEffect;
				return;
			}

			if (this.sIncludeGuardError != null)
			{
				this.StatusTextBlock.Text = this.sIncludeGuardError;
				this.StatusTextBlock.Foreground = this.sRedBrush;
				this.GenerateButton.IsEnabled = false;
				this.GenerateButton.Effect = this.sBlurEffect;
				return;
			}

			if (this.sNamespaceError != null)
			{
				this.StatusTextBlock.Text = this.sNamespaceError;
				this.StatusTextBlock.Foreground = this.sRedBrush;
				this.GenerateButton.IsEnabled = false;
				this.GenerateButton.Effect = this.sBlurEffect;
				return;
			}

			this.StatusTextBlock.Text = "Ready.";
			this.StatusTextBlock.Foreground = this.sBlackBrush;
			this.GenerateButton.IsEnabled = true;
			this.GenerateButton.Effect = null;
		}
		#endregion

		private static string convertTargetName2IncludeGuard(string sTypeName, string sNamespace, string sTargetName)
		{
			var sIncludeGuardBuilder = new StringBuilder("_");

			sIncludeGuardBuilder.Append(sTypeName.ToUpper());

			if (sNamespace.Length != 0)
				foreach (var sSubNamespace in sNamespace.Split('.'))
				{
					sIncludeGuardBuilder.Append('_');
					sIncludeGuardBuilder.Append(sSubNamespace.ToUpper());
				}

			sIncludeGuardBuilder.Append('_');
			sIncludeGuardBuilder.Append(sTargetName.ToUpper());

			return sIncludeGuardBuilder.ToString();
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

			if (Properties.Settings.Default.ProjectPath.Length == 0)
			{
				this.sProjectPathDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				this.ProjectPathTextBlock.Text = "N/A";
			}
			else
			{
				this.sProjectPathDialog.SelectedPath = Properties.Settings.Default.ProjectPath;
				this.ProjectPathTextBlock.Text = Properties.Settings.Default.ProjectPath;
			}

			this.SpecifyAuthorCheckBox.IsChecked = Properties.Settings.Default.SpecifyAuthor;
			this.AuthorTextBox.Text = Properties.Settings.Default.Author;
			this.WithIncludeGuardCheckBox.IsChecked = Properties.Settings.Default.WithIncludeGuard;
			this.WithNamespaceCheckBox.IsChecked = Properties.Settings.Default.WithNamespace;
			this.NamespaceTextBox.Text = Properties.Settings.Default.Namespace;
			this.UseSpaceForIndentationCheckBox.IsChecked = Properties.Settings.Default.UseSpaceForIndentation;

			if (this.SpecifyAuthorCheckBox.IsChecked.Value)
			{
				this.AuthorTextBox.Effect = null;
				this.AuthorTextBox.IsEnabled = true;
			}
			else
			{
				this.AuthorTextBox.Effect = this.sBlurEffect;
				this.AuthorTextBox.IsEnabled = false;
			}

			if (this.WithIncludeGuardCheckBox.IsChecked.Value)
			{
				this.IncludeGuardTextBox.Effect = null;
				this.IncludeGuardTextBox.IsEnabled = true;
			}
			else
			{
				this.IncludeGuardTextBox.Effect = this.sBlurEffect;
				this.IncludeGuardTextBox.IsEnabled = false;
			}

			if (this.WithNamespaceCheckBox.IsChecked.Value)
			{
				this.NamespaceTextBox.Effect = null;
				this.NamespaceTextBox.IsEnabled = true;
			}
			else
			{
				this.NamespaceTextBox.Effect = this.sBlurEffect;
				this.NamespaceTextBox.IsEnabled = false;
			}

			this.updateProjectPath();
			this.updateTargetName();
			this.updateAuthorName();
			this.updateIncludeGuard();
			this.updateNamespace();
			this.updateGenerateButton();

			if (this.sProjectPathError == null && this.sTargetNameError == null && this.sNamespaceError == null)
				this.IncludeGuardTextBox.Text = MainWindow.convertTargetName2IncludeGuard(this.TargetTypeComboBox.Text, this.NamespaceTextBox.Text, this.TargetNameTextBox.Text);
			else
				this.IncludeGuardTextBox.Text = string.Empty;
		}

		private void onClosing(object sender, CancelEventArgs e)
		{
			GenerateManager.fin();

			if (this.ProjectPathTextBlock.Text != "N/A")
				Properties.Settings.Default.ProjectPath = this.ProjectPathTextBlock.Text;

			Properties.Settings.Default.SpecifyAuthor = this.SpecifyAuthorCheckBox.IsChecked.Value;
			Properties.Settings.Default.Author = this.AuthorTextBox.Text;
			Properties.Settings.Default.WithIncludeGuard = this.WithIncludeGuardCheckBox.IsChecked.Value;
			Properties.Settings.Default.WithNamespace = this.WithNamespaceCheckBox.IsChecked.Value;
			Properties.Settings.Default.Namespace = this.NamespaceTextBox.Text;
			Properties.Settings.Default.UseSpaceForIndentation = this.UseSpaceForIndentationCheckBox.IsChecked.Value;

			Properties.Settings.Default.Save();
		}

		private void onBrowseButtonClick(object sender, RoutedEventArgs e)
		{
			if (this.sProjectPathDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				this.ProjectPathTextBlock.Text = this.sProjectPathDialog.SelectedPath;

			this.updateProjectPath();
		}

		private void onTargetNameChanged(object sender, TextChangedEventArgs e)
		{
			this.updateTargetName();
			this.updateGenerateButton();

			if (this.sProjectPathError == null && this.sTargetNameError == null && this.sNamespaceError == null)
				this.IncludeGuardTextBox.Text = MainWindow.convertTargetName2IncludeGuard(this.TargetTypeComboBox.Text, this.NamespaceTextBox.Text, this.TargetNameTextBox.Text);
			else
				this.IncludeGuardTextBox.Text = string.Empty;
		}

		private void onTargetTypeComboBoxChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.sProjectPathError == null && this.sTargetNameError == null && this.sNamespaceError == null)
				this.IncludeGuardTextBox.Text = MainWindow.convertTargetName2IncludeGuard(this.TargetTypeComboBox.Text, this.NamespaceTextBox.Text, this.TargetNameTextBox.Text);
			else
				this.IncludeGuardTextBox.Text = string.Empty;
		}

		private void onSpecifyAuthorCheckBoxChanged(object sender, RoutedEventArgs e)
		{
			if (this.SpecifyAuthorCheckBox.IsChecked.Value)
			{
				this.AuthorTextBox.Effect = null;
				this.AuthorTextBox.IsEnabled = true;
			}
			else
			{
				this.AuthorTextBox.Effect = this.sBlurEffect;
				this.AuthorTextBox.IsEnabled = false;
			}

			this.updateAuthorName();
			this.updateGenerateButton();
		}

		private void onAuthorChanged(object sender, TextChangedEventArgs e)
		{
			this.updateAuthorName();
			this.updateGenerateButton();
		}

		private void onWithIncludeGuardCheckBoxChanged(object sender, RoutedEventArgs e)
		{
			if (this.WithIncludeGuardCheckBox.IsChecked.Value)
			{
				this.IncludeGuardTextBox.Effect = null;
				this.IncludeGuardTextBox.IsEnabled = true;
			}
			else
			{
				this.IncludeGuardTextBox.Effect = this.sBlurEffect;
				this.IncludeGuardTextBox.IsEnabled = false;
			}

			this.updateIncludeGuard();
			this.updateGenerateButton();
		}

		private void onIncludeGuardChanged(object sender, TextChangedEventArgs e)
		{
			this.updateIncludeGuard();
			this.updateGenerateButton();
		}

		private void onWithNamespaceCheckBoxChanged(object sender, RoutedEventArgs e)
		{
			if (this.WithNamespaceCheckBox.IsChecked.Value)
			{
				this.NamespaceTextBox.Effect = null;
				this.NamespaceTextBox.IsEnabled = true;
			}
			else
			{
				this.NamespaceTextBox.Effect = this.sBlurEffect;
				this.NamespaceTextBox.IsEnabled = false;
			}

			this.updateNamespace();
			this.updateGenerateButton();

			if (this.sProjectPathError == null && this.sTargetNameError == null && this.sNamespaceError == null)
				this.IncludeGuardTextBox.Text = MainWindow.convertTargetName2IncludeGuard(this.TargetTypeComboBox.Text, this.NamespaceTextBox.Text, this.TargetNameTextBox.Text);
			else
				this.IncludeGuardTextBox.Text = string.Empty;
		}

		private void onNamespaceChanged(object sender, TextChangedEventArgs e)
		{
			this.updateNamespace();
			this.updateGenerateButton();

			if (this.sProjectPathError == null && this.sTargetNameError == null && this.sNamespaceError == null)
				this.IncludeGuardTextBox.Text = MainWindow.convertTargetName2IncludeGuard(this.TargetTypeComboBox.Text, this.NamespaceTextBox.Text, this.TargetNameTextBox.Text);
			else
				this.IncludeGuardTextBox.Text = string.Empty;
		}

		private void onGenerateButtonClick(object sender, RoutedEventArgs e)
		{
			var sGenerator = GenerateManager.Instance.getGenerator(this.TargetTypeComboBox.Text);

			if (sGenerator == null)
			{
				System.Windows.Forms.MessageBox.Show(this, "That type is not currently supported.", "Wrong type", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var sDict = sGenerator.generate(
				this.SpecifyAuthorCheckBox.IsChecked.Value ? this.AuthorTextBox.Text : null,
				this.WithIncludeGuardCheckBox.IsChecked.Value ? this.IncludeGuardTextBox.Text : null,
				this.WithNamespaceCheckBox.IsChecked.Value ? this.NamespaceTextBox.Text.Replace(".", "::") : null,
				this.TargetNameTextBox.Text,
				this.UseSpaceForIndentationCheckBox.IsChecked.Value);

			int nSuccess = 0;
			int nFailure = 0;
			int nIgnored = 0;

			foreach (var sPair in sDict)
			{
				var sFile = new FileInfo(Path.Combine(this.ProjectPathTextBlock.Text, string.Format("{0}{1}", this.TargetNameTextBox.Text, sPair.Key)));

				if (sFile.Exists)
					if (System.Windows.Forms.MessageBox.Show(this, string.Format("The file at path below is already exists.\nAre you sure you want to overwrite it?\n\n{0}", sFile.FullName), "File overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
					{
						++nIgnored;
						continue;
					}

				var sDir = sFile.Directory;

				if (!sDir.Exists)
					sDir.Create();

				try
				{
					using (var sWriter = new StreamWriter(sFile.FullName, false, this.sEncoding))
						sWriter.Write(sPair.Value.ToString());

					++nSuccess;
				}
				catch
				{
					++nFailure;
				}
			}

			if (nIgnored == sDict.Count)
			{
				System.Windows.Forms.MessageBox.Show(this, "File generation was canceled.\nNo files were created.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (nFailure == 0)
			{
				if (nIgnored == 0)
				{
					System.Windows.Forms.MessageBox.Show(this, "All files were created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				System.Windows.Forms.MessageBox.Show(this, "Some files were ignored, rest files were created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				if (nSuccess == 0)
				{
					System.Windows.Forms.MessageBox.Show(this, "No files were created.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				System.Windows.Forms.MessageBox.Show(this, "Some files were not created.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}