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
using System.Xml;

namespace ssh_client
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		XmlDocument xmlDoc;
		Dictionary<string, Dictionary<string, string>> sessList = new Dictionary<string, Dictionary<string, string>>();
		
		public MainWindow()
		{
			InitializeComponent();
			xmlDoc = new XmlDocument();
			
			// Bind our events
			MouseLeftButtonDown += OnMouseLeftButtonDown;
			AddConnectionButton.Click += (o, e) => OnAddConnectionButtonClick();
			ScrollTopButton.Click += (o, e) => OnScrollTopButtonClick();
			ScrollDownButton.Click += (o, e) => OnScrollDownButtonClick();
			
			databaseLoad();
			
			AddConnectionForm acf = (AddConnectionForm) LayoutRoot.FindName("AddConnectionForm");
			acf.AssignSessionToForm(sessList["Debian"]);
		}
		
		public void OnAddConnectionButtonClick()
		{
			// hide all controls BUT the AddConnectionForm
			foreach (UserControl c in ContentWindowTabs.Children)
			{
				if (c.Name == "AddConnectionForm")
					AddConnectionForm.Visibility = Visibility.Visible;
				else
					c.Visibility = Visibility.Hidden;
			}
		}
		
		public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
		{
			if (args.ClickCount == 2)
			{
				WindowControls wc = (WindowControls) LayoutRoot.FindName("WindowControls");
				wc.OnMaximizeButtonClick();
			}
			else
			{
				DragMove();
			}
		}
		
		public void OnScrollTopButtonClick()
		{
			ConnectionPanel.ScrollToVerticalOffset(ConnectionPanel.VerticalOffset - 16);
		}
		
		public void OnScrollDownButtonClick()
		{
			ConnectionPanel.ScrollToVerticalOffset(ConnectionPanel.VerticalOffset + 16);
		}
		
		public void databaseLoad()
		{
			// look for an xml document
			try
			{
				xmlDoc.Load("data.xml");
				
				// turns out the document has been loaded so now we
				// can load any settings
				
				// and also load our sessions
				XmlNodeList sessionNodes = xmlDoc.SelectNodes("//data/sessions/session");
				foreach (XmlNode sessionNode in sessionNodes)
            	{
					createNewSession(sessionNode.Attributes["name"].Value,
									 sessionNode.SelectSingleNode("host").InnerText,
									 sessionNode.SelectSingleNode("port").InnerText,
									 sessionNode.SelectSingleNode("lusername").InnerText);
				}
			}
			catch (System.IO.FileNotFoundException)
			{
				// one isn't found, so we'll create it.
				XmlNode rootNode = xmlDoc.CreateElement("data");
				XmlNode settingsNode = xmlDoc.CreateElement("settings");
				XmlNode sessionsNode = xmlDoc.CreateElement("sessions");
				
				xmlDoc.AppendChild(rootNode);
				rootNode.AppendChild(settingsNode);
				rootNode.AppendChild(sessionsNode);
				
				// save the document
				xmlDoc.Save("data.xml");
			}
		}
		
		public void databaseSave()
		{
			// remove all the current sessions
			XmlNode sessionsNode = xmlDoc.SelectSingleNode("//data/sessions");
			sessionsNode.RemoveAll();
			
			// overwrite them all and attempt to save the document
			foreach (var session in sessList)
			{
				// create some variables
				XmlNode sessionNode = xmlDoc.CreateElement("session");
				XmlAttribute nameAttribute = xmlDoc.CreateAttribute("name");
				XmlNode hostNode = xmlDoc.CreateElement("host");
				XmlNode portNode = xmlDoc.CreateElement("port");
				XmlNode lusernameNode = xmlDoc.CreateElement("lusername");
				
				// fill some values in
				nameAttribute.Value = session.Value["name"];
				sessionNode.Attributes.Append(nameAttribute);
				hostNode.InnerText = session.Value["host"];
				portNode.InnerText = session.Value["port"];
				lusernameNode.InnerText = session.Value["lusername"];
				
				// setup the xml structure
				sessionNode.AppendChild(hostNode);
				sessionNode.AppendChild(portNode);
				sessionNode.AppendChild(lusernameNode);
				
				// insert the xml we've created
				sessionsNode.AppendChild(sessionNode);
			}
			
			// save the new xml
			xmlDoc.Save("data.xml");
		}
		
		public void createNewSession(string name, string host, string port, string lusername)
		{
			// load the data into a dataset for easy retrival.
			Dictionary<string, string> newSession = new Dictionary<string, string>();
			newSession["name"] = name;
			newSession["host"] = host;
			newSession["port"] = port;
			newSession["lusername"] = lusername;
			newSession["connected"] = "false";
			sessList[newSession["name"]] = newSession;
			
			// create some xaml objects, buttons etc.
			Button btn = new Button();
			Grid grid = new Grid();
			TextBlock tb1 = new TextBlock();
			TextBlock tb2 = new TextBlock();
			
			// fill the textboxes in etc
			tb1.Text = newSession["name"];
			tb2.Text = newSession["host"];
			tb2.HorizontalAlignment = HorizontalAlignment.Right;
			
			// style the button and some other stuff
			btn.Style = (Style) FindResource("LeftBarConnButtonRed");
			btn.VerticalAlignment = VerticalAlignment.Top;
			
			// add the button to the stack panel
			grid.Children.Add(tb1);
			grid.Children.Add(tb2);
			btn.Content = grid;
			ConnectionStackPanel.Children.Add(btn);	
		}
		
		public void updateSession(Dictionary<string, string> session, string name, string host, string port, string lusername)
		{
			// determine if we can find the session first.
			if (!sessList.ContainsValue(session))
				return;
			
			// load the data into a dataset for easy retrival.
			Dictionary<string, string> newSession = new Dictionary<string, string>();
			newSession["name"] = name;
			newSession["host"] = host;
			newSession["port"] = port;
			newSession["lusername"] = lusername;
			newSession["connected"] = "false";
			
			// alter the session record
			string oldName = session["name"];
			sessList[oldName] = newSession;
			
			databaseSave();
		}
    }
}