using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PopularDNS
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			this.AppWindow.MoveAndResize(new RectInt32(500, 500, 300, 500));

		}

		private void btnShecan_Click(object sender, RoutedEventArgs e)
		{
		NetworkHelper.SetDNS("178.22.122.100", "185.51.200.2");

		}

		private void btnElectro_Click(object sender, RoutedEventArgs e)
		{

			NetworkHelper.SetDNS("78.157.42.100", "78.157.42.101");

		}

		private void btnBegzar_Click(object sender, RoutedEventArgs e)
		{
			NetworkHelper.SetDNS("185.55.226.26", "185.55.225.25");

		}

		private void btn403_Click(object sender, RoutedEventArgs e)
		{
			NetworkHelper.SetDNS("10.202.10.202", "10.202.10.102");
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			NetworkHelper.UnsetDNS();
		}
    }
}
