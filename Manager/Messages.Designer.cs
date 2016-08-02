﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manager
{
    using System;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Manager.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture
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
        ///   Looks up a localized string similar to &apos;Allocation id invalid.&apos;.
        /// </summary>
        public static string AllocationIdInvalid
        {
            get
            {
                return ResourceManager.GetString("AllocationIdInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Employee already allocated to this project.&apos;.
        /// </summary>
        public static string EmployeeAlreadyOnProject
        {
            get
            {
                return ResourceManager.GetString("EmployeeAlreadyOnProject", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Employee does not have enough unallocated time.&apos;.
        /// </summary>
        public static string EmployeeFreeTimeNotEnough
        {
            get
            {
                return ResourceManager.GetString("EmployeeFreeTimeNotEnough", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Invalid employee id&apos;.
        /// </summary>
        public static string EmployeeIdInvalid
        {
            get
            {
                return ResourceManager.GetString("EmployeeIdInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Error while adding allocation:&apos;.
        /// </summary>
        public static string ErrorWhileAddingAllocation
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department already exist in this office&apos;.
        /// </summary>
        public static string ErrorWhileAddingDepartment
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingDepartment", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department could not be added: Name field is empty.&apos;.
        /// </summary>
        public static string ErrorWhileAddingDepartment_EmptyName
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingDepartment_EmptyName", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department could not be updated: Name too long.&apos;.
        /// </summary>
        public static string ErrorWhileAddingDepartment_NameTooLong
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingDepartment_NameTooLong", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department could not be added: OfficeId invalid.&apos;.
        /// </summary>
        public static string ErrorWhileAddingDepartment_OfficeIdInvalid
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingDepartment_OfficeIdInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position could not be added: Name field is empty.&apos;.
        /// </summary>
        public static string ErrorWhileAddingPosition_EmptyName
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingPosition_EmptyName", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position could not be updated: Name too long.&apos;.
        /// </summary>
        public static string ErrorWhileAddingPosition_NameTooLong
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileAddingPosition_NameTooLong", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Error while deleting allocation.&apos;.
        /// </summary>
        public static string ErrorWhileDeleteingAllocation
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileDeleteingAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Project coud not be deleted.&apos;.
        /// </summary>
        public static string ErrorWhileDeletingProject
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileDeletingProject", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Employee does not exist&apos;.
        /// </summary>
        public static string ErrorWhileReleasingEmployee
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileReleasingEmployee", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Error while updating allocation:&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingAllocation
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department could not be modified: Invalid department id&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingDepartment_InvalidId
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingDepartment_InvalidId", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department could not be updated: OfficeId invalid.&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingDepartment_OfficeIdInvalid
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingDepartment_OfficeIdInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position could not be modified.&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingPosition
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingPosition", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position could not be modified: Name field is empty.&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingPosition_EmptyName
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingPosition_EmptyName", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Error while updating position: Invalid id&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingPosition_InvalidId
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingPosition_InvalidId", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position could not be modified: Name too long.&apos;.
        /// </summary>
        public static string ErrorWhileUpdatingPosition_NameTooLong
        {
            get
            {
                return ResourceManager.GetString("ErrorWhileUpdatingPosition_NameTooLong", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Allocation added successfully.&apos;.
        /// </summary>
        public static string SuccessfullyAddedAllocation
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyAddedAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department was added with success&apos;.
        /// </summary>
        public static string SuccessfullyAddedDepartment
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyAddedDepartment", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position was added with success&apos;.
        /// </summary>
        public static string SuccessfullyAddedPosition
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyAddedPosition", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Successfully deleted allocation.&apos;.
        /// </summary>
        public static string SuccessfullyDeletedAllocation
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyDeletedAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Successfully deleted project.&apos;.
        /// </summary>
        public static string SuccessfullyDeletedProject
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyDeletedProject", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Employee was released with success&apos;.
        /// </summary>
        public static string SuccessfullyReleasedEmployee
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyReleasedEmployee", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Successfully updated allocation&apos;.
        /// </summary>
        public static string SuccessfullyUpdatedAllocation
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyUpdatedAllocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Department was updated with success&apos;.
        /// </summary>
        public static string SuccessfullyUpdatedDepartment
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyUpdatedDepartment", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &apos;Position was updated with success&apos;.
        /// </summary>
        public static string SuccessfullyUpdatedPosition
        {
            get
            {
                return ResourceManager.GetString("SuccessfullyUpdatedPosition", resourceCulture);
            }
        }
    }
}
