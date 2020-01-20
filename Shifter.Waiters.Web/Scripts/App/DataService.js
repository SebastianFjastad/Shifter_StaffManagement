/* File Created: July 12, 2013 */
function DataService() {

    this.appRoutes = window.appRoutes;

    var dataServiceContext = this;

    //#region General

    $(function () {
        $(document).ajaxStart(function () {
            dataServiceContext.SetBusyState(true);
        });

        $(document).ajaxStop(function () {
            dataServiceContext.SetBusyState(false);
        });
    });

    //sets the busy state of the screen
    this.SetBusyState = function (isBusy) {
        if (isBusy) {
            $("#ajaxLoader").show();
        } else {
            $("#ajaxLoader").hide();
        }
    };

    //#endregion

    //#region Shift Schedule

    this.LoadShiftsToTake = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToLoadShiftsToTake, "Error loading shifts");
    };
    
    this.LoadShiftsToUpdate = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToLoadShiftsToUpdate, "Error loading shifts");
    };
    
    this.LoadShiftSchedule = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToLoadShiftSchedule, "Error loading shift schedule");
    };

    this.LoadAllShiftsSchedule = function (parameters, callback) {
        dataServiceContext.Get(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToLoadAllShiftsSchedule, "Error loading shift schedule");
    };
    
    this.TakeShift = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToTakeShift, "Error taking shift");
    };
    
    this.UpdateShiftAvailability = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.ShiftSchedule.pathToUpdateShiftAvailability, "Error updating shift availability");
    };

    this.FindMe = function (parameters, callback) {
        dataServiceContext.Post(parameters, callback, dataServiceContext.appRoutes.Account.pathToFindMe, "Error finding profile.");
    };
    
    //#endregion

    //#region Ajax

    this.Post = function (parameters, callback, path, errorMessage) {
        $.ajax({
            type: "POST",
            url: path,
            data: parameters,
            success: function (result) {
                callback(result);
            },
            error: function () {
                shifterUx.ShowModal("Oops..", errorMessage);
            }
        });
    };

    this.Get = function (values, callback, path, errorMessage) {
        $.ajax({
            type: "GET",
            url: path,
            data: values,
            success: function (result) {
                callback(result);
            },
            error: function (e) {
                shifterUx.ShowModal("Oops..", errorMessage);
            }
        });
    };
}

//#endregion
