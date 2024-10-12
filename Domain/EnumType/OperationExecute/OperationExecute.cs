namespace Domain.EnumType.OperationExecute
{
    /// <summary>
    /// Defines the types of operations that can be executed.
    /// </summary>
    public enum OperationExecute
    {
        /// <summary>
        /// Add a new record.
        /// </summary>
        [EnumMetadata("Add", "Add a new record.")]
        Add,

        /// <summary>
        /// Modify an existing record.
        /// </summary>
        [EnumMetadata("Modified", "Modify an existing record.")]
        Modified,

        /// <summary>
        /// Remove an existing record.
        /// </summary>
        [EnumMetadata("Remove", "Remove an existing record.")]
        Remove,

        /// <summary>
        /// Deactivate an existing record.
        /// </summary>
        [EnumMetadata("Deactivate", "Deactivate an existing record.")]
        Deactivate,

        /// <summary>
        /// Activate a deactivated record.
        /// </summary>
        [EnumMetadata("Activate", "Activate a deactivated record.")]
        Activate,

        /// <summary>
        /// Retrieve a user by their ID.
        /// </summary>
        [EnumMetadata("GetUserById", "Retrieve a user by their ID.")]
        GetUserById,

        /// <summary>
        /// Retrieve all records that match a given filter.
        /// </summary>
        [EnumMetadata("GetAllByFilter", "Retrieve all records that match a given filter.")]
        GetAllByFilter,

        /// <summary>
        /// Retrieve a page of records that match a given filter.
        /// </summary>
        [EnumMetadata("GetPageByFilter", "Retrieve a page of records that match a given filter.")]
        GetPageByFilter,

        /// <summary>
        /// Get the count of records that match a given filter.
        /// </summary>
        [EnumMetadata("GetCountFilter", "Get the count of records that match a given filter.")]
        GetCountFilter,

        /// <summary>
        /// Generate a One-Time Password (OTP).
        /// </summary>
        [EnumMetadata("GenerateOtp", "Generate a One-Time Password (OTP).")]
        GenerateOtp,

        /// <summary>
        /// Login using a One-Time Password (OTP).
        /// </summary>
        [EnumMetadata("LoginOtp", "Login using a One-Time Password (OTP).")]
        LoginOtp,

        /// <summary>
        /// Standard login operation.
        /// </summary>
        [EnumMetadata("Login", "Standard login operation.")]
        Login,

        /// <summary>
        /// General validation operation.
        /// </summary>
        [EnumMetadata("Validate", "General validation operation.")]
        Validate,

        /// <summary>
        /// Validate an email address.
        /// </summary>
        [EnumMetadata("ValidateEmail", "Validate an email address.")]
        ValidateEmail,

        /// <summary>
        /// Validate the provided One-Time Password (OTP).
        /// </summary>
        [EnumMetadata("ValidateOtp", "Validate the provided One-Time Password (OTP).")]
        ValidateOtp,

        /// <summary>
        /// Validate a username.
        /// </summary>
        [EnumMetadata("ValidateUsername", "Validate a username.")]
        ValidateUsername,

        /// <summary>
        /// Set a new password for a user.
        /// </summary>
        [EnumMetadata("SetNewPassword", "Set a new password for a user.")]
        SetNewPassword,

        /// <summary>
        /// Asynchronously send an email.
        /// </summary>
        [EnumMetadata("SendEmailAsync", "Asynchronously send an email.")]
        SendEmailAsync
    }
}
