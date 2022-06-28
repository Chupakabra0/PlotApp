using System;
using System.Windows;

namespace PlotApp.MVVM.Views.FillFunctionView {
    /// <summary>
    /// Логика взаимодействия для FillFunctionView.xaml
    /// </summary>
    public partial class FillFunctionView : Window {
        public FillFunctionView() {
            try {
                InitializeComponent();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
