/* File Created: July 12, 2013 */
function DataService() {

    this.appRoutes = window.appRoutes;

    var dataServiceContext = this;

    var enableLoader = true;

    //#region General

    $(function () {
        $(document).ajaxStart(function () {
            if (enableLoader == true) {
                dataServiceContext.SetBusyState(true);
            }
        });

        $(document).ajaxStop(function () {
            dataServiceContext.SetBusyState(false);
        });

        $(document).ajaxError(function(event, xhr, settings, ex) {
            if (dataServiceContext.UserAborted(xhr)) {
                alert("An unexpected error occurred, if it persists please contact the support team.");
            }
        });
    });

    this.ReActivateAjax = function() {
        enableLoader = true;
    };

    //sets the busy state of the screen
    this.SetBusyState = function (isBusy) {
        if (isBusy) {
            $("#ajaxLoader").show();
        } else {
            $("#ajaxLoader").hide();
        }
    };

    this.UserAborted = function (xhr) {
        var headers = xhr.getAllResponseHeaders();
        var userAborted = !headers || headers == '';
        return xhr.statusText = 'error' && userAborted;
    };


    //#endregion

    //#region Wall

    this.NewPost = function (callback) {
        dataServiceContext.Get("", callback, dataServiceContext.appRoutes.Wall.pathToNewPost, "Error adding post.");
    };

    this.LoadPost = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.Wall.pathToLoadPost, "Error loading post.");
    };

    this.FilterWallPosts = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.Wall.pathToLoadPosts, "Error loading posts.");
    };

    //endregion

    //#region Timeslots
    
    this.AddTimeslot = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Timeslot.pathToAddTimeslot, "Error adding timeslot");
    };

    this.RemoveTimeslot = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Timeslot.pathToRemoveTimeslot, "Error removing timeslot");
    };

    //endregion

    //#region Waiter

    this.ResetWaitersPassword = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Waiter.pathToResetWaitersPassword, "Error resetting password");
    };

    //#endregion
    
    //#region Shifts

    this.EditShifts = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.Shift.pathToEditShifts, "Error loading shifts");
    };

    this.LoadShifts = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.Shift.pathToLoadShifts, "Error loading shifts");
    };

    this.LoadShiftSummary = function (parameters, callback, shiftId) {
        $.ajax({
            type: "GET",
            url: dataServiceContext.appRoutes.Shift.pathToLoadShiftSummary,
            data: parameters,
            success: function (result) {
                callback(result, shiftId);
            },
            error: function (e) {
               //Do nothing
            }
        });
    };
    
    this.CopySchedule = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Shift.pathToCopySchedule, "Error copying shifts");
    };

    this.ForceScheduleCopy = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Shift.pathToForceScheduleCopy, "Error copying shifts");
    };
    
    this.SaveShift = function (parameters, callback) {
        enableLoader = false;
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Shift.pathToSaveShift, "Error saving shift");
    };

    this.SaveShiftChanges = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Shift.pathToSaveShiftChanges, "Error saving changes");
    };

    this.DeleteShift = function (parameters, callback) {
        enableLoader = false;
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Shift.pathToDeleteShift, "Error deleting shift");
    };

    this.LoadWaiterHours = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.Settings.pathToLoadWaiterHours, "Error loading hours worked");
    };
    
    //#endregion

    this.FindMe = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Account.pathToFindMe, "Error finding profile.");
    };

    //#region Ajax

    $(document).ajaxComplete(function (event, request, settings) {
        dataServiceContext.ReActivateAjax();
    });

    this.Post = function (parameters, callback, path, errorMessage) {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            success: function(result) {
                callback(result);
            },
            error: function (xhr, status, error) {
                dataServiceContext.HandleError(xhr, errorMessage);
            }
        });
    };

    this.Get = function (values, callback, path, errorMessage) {
        $.ajax({
            type: "GET",
            cache: false,
            url: path,
            data: values,
            success: function (result) {
                callback(result);
            },
            error: function (xhr, status, error) {
                dataServiceContext.HandleError(xhr, errorMessage);
            }
        });
    };

    this.HandleError = function (xhr, errorMessage) {
        if (!dataServiceContext.UserAborted(xhr)) {
            alert(errorMessage);
        }
    };
}

//#endregion
