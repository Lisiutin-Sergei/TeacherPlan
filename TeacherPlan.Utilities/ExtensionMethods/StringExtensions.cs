using System;

namespace TeacherPlan.Utilities.ExtensionMethods
{
    /// <summary>
    /// Методы расширения класса string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Проверить строки на равенство без учета регистра.
        /// </summary>
        /// <param name="originalString">Оригинальная строка.</param>
        /// <param name="compareToString">Строка для сравнения.</param>
        /// <returns>Равны ли строки без учета регистра.</returns>
        public static bool EqualsIgnoreCase(this string originalString, string compareToString)
        {
            if (compareToString == null)
            {
                return false;
            }

            return originalString.Equals(compareToString, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
