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
using System.Windows.Shapes;

namespace Wifier
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


		private void btnSimpleMessageBox_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello, world!");
		}

		private void btnMessageBoxWithTitle_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello, world!", "My App");
		}

		private void btnMessageBoxWithButtons_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("This MessageBox has extra options.\n\nHello, world?", "My App", MessageBoxButton.YesNoCancel);
		}

		private void btnMessageBoxWithResponse_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Would you like to greet the world with a \"Hello, world\"?", "My App", MessageBoxButton.YesNoCancel);
			switch (result)
			{
				case MessageBoxResult.Yes:
					MessageBox.Show("Hello to you too!", "My App");
					break;
				case MessageBoxResult.No:
					MessageBox.Show("Oh well, too bad!", "My App");
					break;
				case MessageBoxResult.Cancel:
					MessageBox.Show("Nevermind then...", "My App");
					break;
			}
		}

		private void btnMessageBoxWithIcon_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello, world!", "My App", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void btnMessageBoxWithDefaultChoice_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello, world?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
		}



	}
}
