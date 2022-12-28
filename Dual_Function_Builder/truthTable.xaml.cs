using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
namespace Dual_Function_Builder
{
    /// <summary>
    /// Построение таблицы истинности
    /// </summary>
    public partial class TruthTable : Page
    {
        private CheckBox[] truthTable;

        private readonly int count;

        public CheckBox[] Table => truthTable;

        /// <summary>
        /// Построение таблицы истинности
        /// </summary>
        /// <param name="count">Кол-во переменных</param>
        public TruthTable(int count)
        {
            this.count = count;
            truthTable = new CheckBox[(int)Math.Pow(2, count)];
            InitializeComponent();
            Run();
        }

        private void Run()
        {
            #region Обновление надписей для заголовков
            string forLetters = "";
            for (int i = 0; i < count; i++)
            {
                forLetters += $"{Enum.GetName(typeof(Letter), i)}";
            }
            letters.Content = forLetters;
            function.Content = $"F({forLetters})";
            #endregion

            #region Заполнение строк
            for (int i = 0; i < (int)Math.Pow(2, count); i++)
            {
                Label letters = new Label();
                letters.Content = string.Concat(Enumerable.Repeat("0", count - Convert.ToString(i, 2).Length)) + Convert.ToString(i, 2);
                letters.FontSize = 16;
                letters.Foreground = Brushes.AliceBlue;

                truthTable[i] = new CheckBox();
                truthTable[i].LayoutTransform = new ScaleTransform
                {
                    ScaleX = 1.63,
                    ScaleY = 1.63
                };

                Letters.Children.Add(letters);
                Function.Children.Add(truthTable[i]);
            }
            #endregion
        }
    }
}