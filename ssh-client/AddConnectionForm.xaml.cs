using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ssh_client
{
	/// <summary>
	/// Interaction logic for AddConnectionForm.xaml
	/// </summary>
	public partial class AddConnectionForm : UserControl
	{
		public AddConnectionForm()
		{
			InitializeComponent();
			
			// As usual, bind our events.
			InputBox1.GotFocus += (o, a) => this.OnInputBox1Focus();
			InputBox2.GotFocus += (o, a) => this.OnInputBox2Focus();
			InputBox3.GotFocus += (o, a) => this.OnInputBox3Focus();
			InputBox4.GotFocus += (o, a) => this.OnInputBox4Focus();
		
			InputBox1.LostFocus += (o, a) => OnRemoveFocus();
			InputBox2.LostFocus += (o, a) => OnRemoveFocus();
			InputBox3.LostFocus += (o, a) => OnRemoveFocus();
			InputBox4.LostFocus += (o, a) => OnRemoveFocus();
		}
		
		public void OnRemoveFocus()
		{
			TextBoxTitle.Visibility = Visibility.Hidden;
			TextBoxContent.Visibility = Visibility.Hidden;
			InputArrow.Visibility = Visibility.Hidden;
			InfoBoxBorder.Visibility = Visibility.Hidden;
			InfoBox.Visibility = Visibility.Hidden;
		}
		
		public void OnFocus()
		{
			TextBoxTitle.Visibility = Visibility.Visible;
			TextBoxContent.Visibility = Visibility.Visible;
			InputArrow.Visibility = Visibility.Visible;
			InfoBoxBorder.Visibility = Visibility.Visible;
			InfoBox.Visibility = Visibility.Visible;
		}
		
		public void OnInputBox1Focus()
		{
			InputArrow.Style = FindResource("Input1Arrow") as Style;
			TextBoxTitle.Text = "Session name";
			TextBoxContent.Text = "A common name used to identify the connection, the name is unique and cannot be changed.";
		
			OnFocus();
		}
		
		public void OnInputBox2Focus()
		{
			InputArrow.Style = FindResource("Input1Arrow") as Style;
			TextBoxTitle.Text = "Hostname or IP Address";
			TextBoxContent.Text = "The hostname or IP address to connect to.";
		
			OnFocus();
		}
		
		public void OnInputBox3Focus()
		{
			InputArrow.Style = FindResource("Input1Arrow") as Style;
			TextBoxTitle.Text = "SSH Port";
			TextBoxContent.Text = "SSH Port to connect to, usually 22. If the server you are connecting to is yours, it's recommended to change the port to something random for security.";
		
			OnFocus();
		}
		
		public void OnInputBox4Focus()
		{
			InputArrow.Style = FindResource("Input1Arrow") as Style;
			TextBoxTitle.Text = "Login username";
			TextBoxContent.Text = "A username to automatically send to the SSH server, this setting is not required.";
		
			OnFocus();
		}
	}
}