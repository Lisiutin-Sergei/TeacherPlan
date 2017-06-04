using System.Linq;
using TeacherPlan.Utilities.Common;

namespace TeacherPlan.Utilities.ExtensionMethods
{
    /// <summary>
    /// Методы расширения для Enuberable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Находится ли элемент среди списка.
        /// </summary>
        /// <typeparam name="T">Тип элемента.</typeparam>
        /// <param name="element">Сам элемент.</param>
        /// <param name="list">Список, в котором ищем.</param>
        /// <returns>Находится ли элемент среди списка.</returns>
        public static bool In<T>(this T element, params T[] list)
            where T : struct
        {
            Argument.Require(list?.Any() ?? false, "Необходимо указать список элементов.");

            return list.Contains(element);
        }
    }
}
