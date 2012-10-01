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
		
		public MainWindow()
		{
			InitializeComponent();
			xmlDoc = new XmlDocument();
			
			// Bind our events
			MouseLeftButtonDown += OnMouseLeftButtonDown;
			AddConnectionButton.Click += (o, e) => AddConnectionForm.Visibility = Visibility.Visible;
			ScrollTopButton.Click += (o, e) => OnScrollTopButtonClick();
			ScrollDownButton.Click += (o, e) => OnScrollDownButtonClick();
			
			// look for an xml document
			try
			{
				xmlDoc.Load("data.xml");
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
			
			
			// create an xml object
			/*XmlDocument xmlDoc = new XmlDocument();
			XmlNode rootNode = xmlDoc.CreateElement("sessions");
            xmlDoc.AppendChild(rootNode);*/
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
    }
}