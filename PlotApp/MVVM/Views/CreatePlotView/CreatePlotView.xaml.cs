using System;
using System.Windows;

namespace PlotApp.MVVM.Views.CreatePlotView {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreatePlotView : Window {
        public CreatePlotView() {
            try {
                InitializeComponent();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
