using System;
using System.Windows;

namespace PlotApp.MVVM.Views.PlotView {
    /// <summary>
    /// Логика взаимодействия для PlotView.xaml
    /// </summary>
    public partial class PlotView : Window {
        public PlotView() {
            try {
                InitializeComponent();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
