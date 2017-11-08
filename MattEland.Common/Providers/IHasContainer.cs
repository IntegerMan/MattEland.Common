// ---------------------------------------------------------
// IHasContainer.cs
// 
// Created on:      09/01/2015 at 11:39 PM
// Last Modified:   09/01/2015 at 11:39 PM
// 
// Last Modified by: Matt Eland 19
// ---------------------------------------------------------

using MattEland.Common.Annotations;

namespace MattEland.Common.Providers
{

    /// <summary>
    ///     Interface for types that have <see cref="IObjectContainer"/> members.
    /// </summary>
    public interface IHasContainer<out T> where T : class, IObjectContainer
    {
        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        [NotNull]
        T Container { get; }
    }
}