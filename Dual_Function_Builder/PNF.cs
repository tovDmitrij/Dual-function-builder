using System;
using System.Linq;
namespace Dual_Function_Builder
{
    /// <summary>
    /// Полиномиальная нормальная форма (ПНФ)
    /// </summary>
    internal class PNF
    {
        private readonly bool[] values;

        private readonly PDNF pdnf;

        /// <summary>
        /// Полиномиальная нормальная форма (ПНФ)
        /// </summary>
        /// <param name="values">Таблица истинности</param>
        public PNF(bool[] values)
        {
            this.values = values;
            pdnf = new PDNF(values);
        }

        /// <summary>
        /// Построить ПНФ
        /// </summary>
        public string Run()
        {
            string result = "";
            string pdnfResult = pdnf.Run();
            if (pdnfResult == "Невозможно построить")
                return "1 ⊕ 1";
            if (values.Count(c => c) == values.Length)
                return "1";

            #region Треугольник Паскаля 

            bool[][] pascalsTriangle = new bool[values.Length][];
            pascalsTriangle[0] = values;
            int lastIndex = -1;
            for (int i = 0; i < values.Length - 1; i++)
            {
                bool[] currentRow = pascalsTriangle[i];
                bool[] nextRow = new bool[currentRow.Length - 1];
                for (int j = 0; j < currentRow.Length - 1; j++)
                    nextRow[j] = DoXOR(currentRow[j], currentRow[j + 1]);
                if (nextRow[0])
                    lastIndex = i + 1;
                pascalsTriangle[i + 1] = nextRow;
            }
            if (pascalsTriangle[0][0])
                result = "1 ⊕";
            for (int i = 1; i < values.Length; i++)
            {
                if (pascalsTriangle[i][0])
                {
                    bool[] row = CreateRow(i);
                    string current = "";
                    int lastJ = -1;
                    for (int j = 0; j < row.Length; j++)
                        if (row[j])
                            lastJ = j;
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j])
                        {
                            current += Enum.GetName(typeof(Letter), j);
                            if (j != lastJ)
                                current += '⋀';
                            else
                                break;
                        }
                    }
                    result += $"({current})";
                    if (i != lastIndex)
                        result += '⊕';
                    else
                        break;
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// Создаёт строку по таблице истинности
        /// </summary>
        /// <param name="num">Номер строки</param>
        private bool[] CreateRow(int num)
        {
            bool[] result = new bool[(int)Math.Log2(values.Length)];
            int i = result.Length - 1;
            while (num != 0)
            {
                result[i] = num % 2 != 0;
                i--;
                num /= 2;
            }
            return result;
        }

        /// <summary>
        /// Преобразование <paramref name="a"/> ⊕ <paramref name="b"/> = (¬<paramref name="a"/>⋁¬<paramref name="b"/>)⋀(<paramref name="a"/>⋁<paramref name="b"/>)
        /// </summary>
        private static bool DoXOR(bool a, bool b) => (!a || !b) && (a || b);
    }
}