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

namespace ClassGenerator
{
	/// <summary>
	/// Interaction logic for ClassEdit.xaml
	/// </summary>
	public partial class ClassEdit : Window
	{
		public ClassEdit()
		{
			InitializeComponent();
		}

        private void onClassnameTextChanged(object sender, TextChangedEventArgs e)
        {
            this._headerfile_textbox.Text = this._classname_textbox.Text + ".h";
            this._sourcefile_textbox.Text = this._classname_textbox.Text + ".cpp";
            this._templatesource_textbox.Text = this._classname_textbox.Text + ".hpp";
        }
    }
}
