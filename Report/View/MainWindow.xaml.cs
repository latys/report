using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Report
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<string, Uri> allViews = new Dictionary<string, Uri>(); 
        public MainWindow()
        {
            InitializeComponent();
            allViews.Add("query_page", new Uri("View/QueryVerWasteData.xaml", UriKind.Relative));
            allViews.Add("report_page", new Uri("View/ReportWindow.xaml", UriKind.Relative));
            allViews.Add("page1", new Uri("View/Page1.xaml", UriKind.Relative));
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Uri a = allViews["query_page"];

            mainFrame.NavigationService.Navigate(allViews["query_page"]);
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(allViews["report_page"]);
        }

    }
}
