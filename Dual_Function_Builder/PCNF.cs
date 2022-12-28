using System;
namespace Dual_Function_Builder
{
    /// <summary>
    /// Совершенная конъюктивная нормальная форма (СКНФ)
    /// </summary>
    internal class PCNF
    {
        private readonly bool[] values;

        /// <summary>
        /// Совершенная конъюктивная нормальная форма (СКНФ)
        /// </summary>
        /// <param name="values">Таблица истинности</param>
        public PCNF(bool[] values)
        {
            this.values = values;
        }

        /// <summary>
        /// Построить СКНФ
        /// </summary>
        public string Run()
        {
            string result = "";
            int lastIndex = 0;
            bool impossible = true;
            for (int i = 0; i < values.Length; i++)
            {
                if (!values[i])
                {
                    lastIndex = i;
                    impossible = false;
                }
            }
            if (impossible)
                return "Невозможно построить";
            for (int i = 0; i <= lastIndex; i++)
            {
                if (!values[i])
                {
                    bool[] row = CreateRow(i);
                    string current = "";
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j])
                            current += '¬';
                        current += Enum.GetName(typeof(Letter), j);
                        if (j != row.Length - 1)
                            current += '⋁';
                    }
                    result += $"({current})";
                    if (i != lastIndex)
                        result += '⋀';
                    else
                        break;
                }
            }
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
    }
}