using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

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
			
			AddSessionButton.Click += (o, a) => OnAddSessionButtonClick();
		}
		
		public void MoveToolTipBox(double m0, double m1, double m2, double m3)
		{
			// determine whether the error box is showing
			double eBoxHeight = ErrorBox.Visibility == Visibility.Visible ? ErrorBox.ActualHeight + 30 : 0;
			
			// change the margin
			Thickness marginIA = InputArrow.Margin;
			marginIA.Top = m0 + eBoxHeight;
			InputArrow.Margin = marginIA;
			
			Thickness margin = InfoBox.Margin;
			margin.Top = m1 + eBoxHeight;
			
			InfoBox.Margin = margin;
			TextBoxTitle.Margin = margin;
			
			margin.Top = m2 + eBoxHeight;
			InfoBoxBorder.Margin = margin;
			
			margin.Top = m3 + eBoxHeight;
			TextBoxContent.Margin = margin;
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
			// alter the text
			TextBoxTitle.Text = "Session name";
			TextBoxContent.Text = "A common name used to identify the connection, the name is unique and cannot be changed.";
			
			// change the margin
			MoveToolTipBox(10, -3, -2, 30);
			OnFocus();
		}
		
		public void OnInputBox2Focus()
		{
			// alter the text and style
			TextBoxTitle.Text = "Hostname or IP Address";
			TextBoxContent.Text = "The hostname or IP address to connect to.";
		
			// change the margin
			MoveToolTipBox(60, 45, 46, 88);
			OnFocus();
		}
		
		public void OnInputBox3Focus()
		{
			// alter the text and style
			TextBoxTitle.Text = "SSH Port";
			TextBoxContent.Text = "SSH Port to connect to, usually 22. If the server you are connecting to is yours, it's recommended to change the port to something random for security.";
		
			// change the margin
			MoveToolTipBox(113, 45, 46, 88);
			OnFocus();
		}
		
		public void OnInputBox4Focus()
		{
			// alter the text and style
			TextBoxTitle.Text = "Login username";
			TextBoxContent.Text = "A username to automatically send to the SSH server, this setting is not required.";
			
			// change the margin
			MoveToolTipBox(163, 45, 46, 88);
			OnFocus();
		}
		
		public void OnAddSessionButtonClick()
		{
			// collect the session information
			string SessionName = InputBox1.Text;
			string Hostname = InputBox2.Text;
			string Port = InputBox3.Text;
			string Username = InputBox4.Text;
			
			// check for errors
			// first we check if all required fields are filled in.
			if (SessionName.Length == 0 || Hostname.Length == 0 || Port.Length == 0)
			{
				eTitle.Text = "ERROR";
				eMessage.Text = "Please make sure all required fields are not empty.";
				ErrorBox.Visibility = Visibility.Visible;
				return;
			}
			
			// check session name isn't already used
			
			// check for a valid hostname/ip (format only, no dns checking)
			Match matchIP = Regex.Match(Hostname, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
			Match matchHN = Regex.Match(Hostname, @"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$", RegexOptions.IgnoreCase);
			if (!matchIP.Success && !matchHN.Success)
			{
				eTitle.Text = "ERROR";
				eMessage.Text = "The hostname or IP address you have entered appears to be invalid.";
				ErrorBox.Visibility = Visibility.Visible;
				return;
			}
			
			// check if port number is valid
			int intPort;
			bool validInt = Int32.TryParse(Port, out intPort);
			if (!validInt || intPort < 0 || intPort > 65535)
			{
				eTitle.Text = "ERROR";
				eMessage.Text = "The port you have entered is an invalid port number.";
				ErrorBox.Visibility = Visibility.Visible;
				return;
			}
			
			// no errors, continue, first hide any error boxes
			ErrorBox.Visibility = Visibility.Collapsed;
			
			//XmlNode userNode = xmlDoc.CreateElement("sessions");
		}
	}
}