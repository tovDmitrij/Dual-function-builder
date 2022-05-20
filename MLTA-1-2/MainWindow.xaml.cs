using System;
using System.Windows;
using System.Windows.Controls;
namespace MLTA_1_2
{
    public partial class MainWindow : Window
    {
        private TruthTable trTable;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            trTable = new TruthTable(Convert.ToInt32((selectedCount.SelectedItem as ComboBoxItem).Content));
            truthTable.Navigate(trTable);
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            try
            {
                var z = trTable.Table;
                bool[] massive = new bool[z.Length];
                for (int i = 0; i < z.Length; i++)
                {
                    massive[i] = (bool)z[i].IsChecked;
                }
                var r = new Result(massive);
                result.Navigate(r);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Укажите кол-во переменных!");
                return;
            }
        }
    }
}