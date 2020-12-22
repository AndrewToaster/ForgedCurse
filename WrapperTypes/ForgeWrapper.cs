using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Base class for wrapping around a type with <see cref="ForgeClient"/>
    /// </summary>
    /// <remarks>Due to the nature for this wrapper, wrapped types will most probably have less information then their wrapped type. 
    /// All information is still accessible from the <see cref="WrappedType"/></remarks>
    /// <typeparam name="T">Wrapped type</typeparam>
    public abstract class ForgeWrapper<T> : IDisposable
    {
        /// <summary>
        /// Reference to the wrapped type
        /// </summary>
        public T WrappedType { get; set; }

        /// <summary>
        /// The <see cref="ForgeClient"/> behind this <see cref="ForgeWrapper{T}"/> instance
        /// </summary>
        public ForgeClient Client { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="ForgeWrapper{T}"/>
        /// </summary>
        /// <param name="wrappingBase">The type to wrap around</param>
        /// <param name="client">The client for the <see cref="Client"/></param>
        public ForgeWrapper(T wrappingBase, ForgeClient client)
        {
            WrappedType = wrappingBase;
            Client = client;
        }

        /// <summary>
        /// Disposes the inner <see cref="Client"/>
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// Implicitly converts the <see cref="ForgeWrapper{T}"/> into its wrapped type <typeparamref name="T"/>
        /// </summary>
        /// <param name="wrapper">The wrapper to un-wrap</param>
        public static implicit operator T(ForgeWrapper<T> wrapper)
        {
            return wrapper.WrappedType;
        }

        /// <summary>
        /// Creates a new wrapper <typeparamref name="TWrap"/>
        /// </summary>
        /// <typeparam name="TWrap">The type of the new wrapper</typeparam>
        /// <param name="jsonType">The enclosing JSON type</param>
        /// <param name="client">The <see cref="ForgeClient"/> to be used</param>
        /// <returns>The new wrapper</returns>
        public static TWrap WrapType<TWrap>(T type, ForgeClient client) where TWrap : ForgeWrapper<T>
        {
            return (TWrap)Activator.CreateInstance(typeof(TWrap), type, client);
        }
    }
}
