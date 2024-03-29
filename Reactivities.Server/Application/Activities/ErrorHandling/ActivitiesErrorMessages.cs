﻿namespace Application.Activities.ErrorHandling;

public static class ActivitiesErrorMessages
{
    public const string CreateError = "Unable to create Activity";

    public const string EditError = "Unable to edit Activity";
        
    public const string DeleteError = "Unable to delete Activity";
        
    public const string DoesNotExist = "Activity Id: '{0}' does not exist";

    public const string InvalidCategory = "'Category' is invalid";

    public const string InvalidDate = "'Date' is invalid";

    public const string ActivityTitleLength = "Activity title must be at least 3 letters long.";

    public const string HostCannotBeAddedAsAttendee = "Host cannot be added as attendee";

    public const string UnableToSaveChanges = "Unable to save changes";
}