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
			this.InitializeComponent();
        }
		
		protected void MinimiseButtonClick(object sender, RoutedEventArgs e)
		{
			// Get the window, and change it to Minimized
			Window window = Window.GetWindow(this);
			window.WindowState = WindowState.Minimized;
		}
		
		protected void MaximizeButtonClick(object sender, RoutedEventArgs e)
		{
			// Get the window, and determine whether we need to maximise it or not.
			Window window = Window.GetWindow(this);
			if (!this.WindowMaximized)
			{
				// save current dimensions
				this.WindowMaximized = true;
				this.WindowHeight = window.Height;
				this.WindowWidth = window.Width;
				this.WindowLeft = window.Left;
				this.WindowTop = window.Top;
				
				// new dimensions (maximized)
				window.Height = SystemParameters.WorkArea.Height + 10;
				window.Width = SystemParameters.WorkArea.Width + 10;
				window.Left = -5;
				window.Top = -5;
				window.ResizeMode = ResizeMode.NoResize;
				
			}
            else
			{
                // alter windowMaximized variable
				this.WindowMaximized = false;
				
				// new dimensions (minimized)
				window.Height = this.WindowHeight;
				window.Width = this.WindowWidth;
				window.Left = this.WindowLeft;
				window.Top = this.WindowTop;
				window.ResizeMode = ResizeMode.CanResizeWithGrip;
			}
		}
		
		protected void CloseButtonClick(object sender, RoutedEventArgs e)
		{
			// shutdown the application properly
			ssh_client.App.Current.Shutdown();
		}
	}
}