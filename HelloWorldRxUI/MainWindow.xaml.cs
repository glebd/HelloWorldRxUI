using System;
using System.Windows;

namespace HelloWorldRxUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
            (DataContext as AppViewModel).ContinueCommand.Subscribe(_ => MessageBox.Show("This is the end."));
        }
    }
}
