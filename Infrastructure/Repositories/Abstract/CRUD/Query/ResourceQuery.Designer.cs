//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using System;

    /// <summary>
    ///   A strongly-typed resource class for retrieving localized strings
    ///   related to database query operations, such as finding or searching entities.
    /// </summary>
    /// <remarks>
    ///   This class was auto-generated by the StronglyTypedResourceBuilder tool
    ///   and is not intended to be manually edited. To modify the resources,
    ///   edit the .ResX file and regenerate this class or rebuild the Visual Studio project.
    /// </remarks>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceQuery
    {
        // The ResourceManager instance responsible for managing and providing access
        // to the localized resources contained within this ResourceQuery class.
        private static global::System.Resources.ResourceManager resourceMan;
        // The CultureInfo instance that enables overriding the current thread's
        // culture for resource lookups. This can be set to a specific culture if needed.
        private static global::System.Globalization.CultureInfo resourceCulture;
        /// <summary>
        ///   Constructor for the ResourceQuery class. It's unused and private,
        ///   as this class only provides static members for resource management.
        /// </summary>
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceQuery()
        {
        }

        /// <summary>
        ///   Retrieves the ResourceManager instance that handles localized resource lookups.
        ///   This instance is cached after the first access.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    // Initialize the ResourceManager with the appropriate resource file.
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Repositories.Abstract.CRUD.Query.ResourceQuery", typeof(ResourceQuery).Assembly);
                    resourceMan = temp;
                }

                return resourceMan;
            }
        }

        /// <summary>
        ///   Provides the capability to override the current thread's CultureInfo
        ///   for all resource lookups using this class. Useful for localization.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }

            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string indicating that an entity was successfully found
        ///   by its identifier.
        /// </summary>
        internal static string SuccessfullyFind
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyFind", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string indicating that a search operation in a specified entity
        ///   completed successfully, with placeholder for dynamic entity name insertion.
        /// </summary>
        internal static string SuccessfullySearchGeneric
        {
            get
            {
                return ResourceManager.GetString("SuccessfullySearchGeneric", resourceCulture);
            }
        }
    }
}