//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Infrastructure.Repositories.Implementation.CRUD.User.Create
{
    using System;

    /// <summary>
    ///   A strongly-typed resource class for looking up localized strings.
    ///   This class provides access to resource strings and manages
    ///   their localization.
    /// </summary>
     // This class is auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file and then rerun ResGen
    // with the /str option or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()] // Prevents the debugger from stepping into this code
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()] // Indicates this is a compiler-generated class
    internal class ResourceUserCreate
    {
        // A private static instance of ResourceManager, lazily instantiated
        private static global::System.Resources.ResourceManager resourceMan;
        // A private static field to hold the current culture for resource lookup
        private static global::System.Globalization.CultureInfo resourceCulture;
        // Suppresses warnings about unused private code
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceUserCreate()
        {
        }

        /// <summary>
        ///   Provides a cached instance of ResourceManager used by this class.
        ///   It manages access to resources for the purpose of localization.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                // Check if the ResourceManager instance is already created
                if (object.ReferenceEquals(resourceMan, null))
                {
                    // Create a new ResourceManager for the specified resources
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Repositories.Implementation.CRUD.User.Create.ResourceUserCreate", typeof(ResourceUserCreate).Assembly);
                    resourceMan = temp;
                }

                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to:
        ///   "The given email is already registered and cannot be used again."
        /// </summary>
        internal static string CreateFailedAlreadyRegisteredEmail
        {
            get
            {
                // Retrieves the associated string from the managed resources
                return ResourceManager.GetString("CreateFailedAlreadyRegisteredEmail", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to:
        ///   "One or more data fields from the User have been submitted with errors: {0}."
        /// </summary>
        internal static string CreateFailedDataSizeCharacter
        {
            get
            {
                // Retrieves the associated string from the managed resources
                return ResourceManager.GetString("CreateFailedDataSizeCharacter", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to:
        ///   "The given email is not in a valid format."
        /// </summary>
        internal static string CreateFailedEmailInvalidFormat
        {
            get
            {
                // Retrieves the associated string from the managed resources
                return ResourceManager.GetString("CreateFailedEmailInvalidFormat", resourceCulture);
            }
        }
    }
}