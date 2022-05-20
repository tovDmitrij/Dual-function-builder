using System.Windows.Controls;
using System.Windows.Media;
namespace MLTA_1_2
{
    /// <summary>
    /// Выводит результат (СДНФ, СКНФ, ПНФ)
    /// </summary>
    public partial class Result : Page
    {
        private readonly bool[] truthTable;
        /// <summary>
        /// Выводит результат (СДНФ, СКНФ, ПНФ)
        /// </summary>
        /// <param name="massive">Таблица истинности</param>
        public Result(bool[] massive)
        {
            InitializeComponent();
            truthTable = massive;
            Run();
        }
        private void Run()
        {
            bool dual = true;
            bool[] dualValues = new bool[truthTable.Length];
            for(int i = 0; i < truthTable.Length; i++)
            {
                dualValues[truthTable.Length - 1 - i] = !truthTable[i];
                if (!DoXOR(truthTable[i], truthTable[truthTable.Length - 1 - i]))
                    dual = false;
            }
            switch (dual)
            {
                case true:
                    labelSelfDual.Content = "Ф-ия самодвойственная";
                    break;
                case false:
                    labelSelfDual.Content = "Ф-ия не самодвойственная";
                    break;
            }
            labelSelfDual.FontSize = 16;
            labelSelfDual.Foreground = Brushes.AliceBlue;

            labelFunction.Content = $"" +
                $"СДНФ: {new PDNF(truthTable).Run()}\n" +
                $"СКНФ: {new PCNF(truthTable).Run()}\n" +
                $"ПНФ: {new PNF(truthTable).Run()}";
            labelFunction.FontSize = 16;
            labelFunction.Foreground = Brushes.AliceBlue;

            labelDualFunction.Content = $"" +
                $"СДНФ: {new PDNF(dualValues).Run()}\n" +
                $"СКНФ: {new PCNF(dualValues).Run()}\n" +
                $"ПНФ: {new PNF(dualValues).Run()}";
            labelDualFunction.FontSize = 16;
            labelDualFunction.Foreground = Brushes.AliceBlue;
        }
        /// <summary>
        /// Преобразование <paramref name="a"/> ⊕ <paramref name="b"/> = (¬<paramref name="a"/>⋁¬<paramref name="b"/>)⋀(<paramref name="a"/>⋁<paramref name="b"/>)
        /// </summary>
        /// <returns>Результат преобразования</returns>
        private static bool DoXOR(bool a, bool b) => (!a || !b) && (a || b);
    }
}