using System;
using System.ComponentModel;

namespace Okiroya.Cms.API
{
    /// <summary>
    /// Контракт для fluent api - скрывает методы типа Object, чтобы построить "чистый" интерфейс
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembersForFluentApi
    {
        /// <summary>
        /// Скрытие метода Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object other);

        /// <summary>
        /// Скрытие метода GetHashCode
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Скрытие метода GetType
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Скрытие метода ToString
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}
