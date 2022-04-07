namespace Yva.UsersCsvImport.Dto.Yva;

/// <summary>
/// All exists error codes for all known errors in users CSV-file.
/// </summary>
public enum UsersFileErrorCode
{
            /// <summary>
        /// Row format is broken.
        /// </summary>
        UnknownError = 1,

        /// <summary>
        /// It was not possible to determine the delimiter in the file.
        /// </summary>
        InvalidDelimiter,
        
        /// <summary>
        /// Required cell in the header or the entire header is missing.
        /// </summary>
        MissingHeader,
        
        /// <summary>
        /// Cell in the row is missing.
        /// </summary>
        MissingField,

        /// <summary>
        /// Required "Email address" cell is empty.
        /// </summary>
        MissingEmailAddress,
        
        /// <summary>
        /// Invalid value in "Email address" cell.
        /// </summary>
        InvalidEmailAddressFormat,
        
        /// <summary>
        /// Invalid value in "Collect passive data" cell. 
        /// </summary>
        InvalidIncludeInReportsFormat,
        
        /// <summary>
        /// Invalid value in "Send surveys" cell.
        /// </summary>
        InvalidSendSurveysFormat,
        
        /// <summary>
        /// Invalid value in "Collect 360-degree feedback" cell. 
        /// </summary>
        InvalidCollect360SurveyFormat,
        
        /// <summary>
        /// Invalid value in "Language" cell.
        /// </summary>
        InvalidLanguageFormat,
        
        /// <summary>
        /// Invalid value in "Time zone" cell.
        /// </summary>
        InvalidTimeZoneFormat,
        
        /// <summary>
        /// Invalid value in "Manager email" cell.
        /// </summary>
        InvalidManagerEmailFormat,
        
        /// <summary>
        /// Invalid value in "Hire date" cell.
        /// </summary>
        InvalidHireDateFormat,
        
        /// <summary>
        /// Invalid value in "Termination date" cell. 
        /// </summary>
        InvalidTerminationDateFormat,
        
        /// <summary>
        /// Invalid value in "Send all baseline questions" cell.
        /// </summary>
        InvalidSendAllBaselineQuestions,
        
        /// <summary>
        /// Invalid value in "Birth date" cell. 
        /// </summary>
        InvalidBirthDateFormat,
        
        /// <summary>
        /// Invalid value in "Gender" cell.
        /// </summary>
        InvalidGenderFormat,
        
        /// <summary>
        /// Invalid value in "Employment type" cell.
        /// </summary>
        InvalidEmploymentTypeFormat,
        
        /// <summary>
        /// Invalid value in "Remote work" cell.
        /// </summary>
        InvalidRemoteWorkFormat,
        
        /// <summary>
        /// Duplicate user has been identified.
        /// </summary>
        UserDuplicate
}