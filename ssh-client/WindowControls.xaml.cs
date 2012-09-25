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
	/// Interaction logic for WindowControls.xaml
	/// </summary>
	public partial class WindowControls : UserControl
	{
		public bool WindowMaximized = false;
		public double WindowWidth;
		public double WindowHeight;
		public double WindowLeft;
		public double WindowTop;
		
		public WindowControls()
		{
			InitializeComponent();
			
			// bind events, do this in code not XAML. It's annoying to keep
			// track of my event bindings in hundreds of lines of XAML code.
			MinimizeButton.Click += (o, e) => OnMinimizeButtonClick();
			MaximizeButton.Click += (o, e) => OnMaximizeButtonClick();
			CloseButton.Click += (o, e) => OnCloseButtonClick();
        }
		
		public void OnMinimizeButtonClick()
		{
			// Get the window, and change it to Minimized
			Window window = Window.GetWindow(this);
			window.WindowState = WindowState.Minimized;
		}
		
		public void OnMaximizeButtonClick()
		{
			// Get the window, and determine whether we need to maximise it or not.
			Window window = Window.GetWindow(this);
			Button item = (Button) LayoutRoot.FindName("MaximizeButton");
			
			if (!WindowMaximized)
			{
				// save current dimensions
				WindowMaximized = true;
				WindowHeight = window.Height;
				WindowWidth = window.Width;
				WindowLeft = window.Left;
				WindowTop = window.Top;
				
				// new dimensions (maximized)
				window.Height = SystemParameters.WorkArea.Height + 10;
				window.Width = SystemParameters.WorkArea.Width + 10;
				window.Left = -5;
				window.Top = -5;
				window.ResizeMode = ResizeMode.NoResize;
				
				// change the button style
				item.Style = FindResource("WindowControlRestore") as Style;
			}
            else
			{
                // alter windowMaximized variable
				WindowMaximized = false;
				
				// new dimensions (minimized)
				window.Height = WindowHeight;
				window.Width = WindowWidth;
				window.Left = WindowLeft;
				window.Top = WindowTop;
				window.ResizeMode = ResizeMode.CanResizeWithGrip;
				
				// change the button style
				item.Style = FindResource("WindowControlMaximize") as Style;
			}
		}
		
		public void OnCloseButtonClick()
		{
			// shutdown the application properly
			ssh_client.App.Current.Shutdown();
		}
	}
}