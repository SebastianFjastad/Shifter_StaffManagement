/* File Created: June 16, 2013 */

function Index() {

    var context = this;

    //This is used to track which accordion needs to be reloaded after a shift was updated
    this.activeWeekButtonId = "";

    this.shiftScheduleChangedTopic = "";

    this.clientKey = "";

    this.isInDayView = false;

    this.Load = function () {
        context.BindHandlers();
        
        $(".loadShifts").first().click();

        try {

            // Reference the auto-generated proxy for the hub.  
            var broadcaster = $.connection.messageHub;

            // Create a function that the hub can call back to display messages.
            broadcaster.client.raiseEvent = function (topic, message, returnedClientKey) {
                if (topic == context.shiftScheduleChangedTopic && context.clientKey != returnedClientKey) {
                    $("#scheduleChangedNotification").modal("show");
                }
            };

            $.connection.hub.start();
        }
        catch (err) {
            //console.error(err);
        }
    };

    this.UnBindHandlers = function () {
        $(".loadShifts").unbind("click");
        $(".scheduleDay").unbind("click");
        $(".weekTab").unbind("click");
        $(".updateAvailability").unbind("click");
        $(".updateShifts").unbind("click");
        $(".takeShifts").unbind("click");
        $(".takeShiftsFromWaiter").unbind("click");
        $("#makeAvailableConfirm").unbind("click");
        $("#takeShiftConfirm").unbind("click");
    };

    this.BindHandlers = function () {

        context.UnBindHandlers();

        $(".loadShifts").click(function () {
            var isOpen = $(this).closest('.panel-group').find('.panel-collapse').is('.in');
            if (isOpen) {
                $('.glyphicon', this).removeClass('glyphicon-minus-sign').addClass('glyphicon-plus-sign');
            } else {
                $('.glyphicon', this).removeClass('glyphicon-plus-sign').addClass('glyphicon-minus-sign');
            }
            var weekNumber = $(this).attr("data-week-number");
            if ($("#weekContainer_" + weekNumber).html().trim().length <= 0) {
                context.LoadShifts($(this));
            }
        });

        $(".scheduleDay").click(function () {
            context.isInDayView = true;
            var selectedDay = $(this).attr("data-days-date");
            context.LoadShiftForDay(selectedDay);
        });

        //Sets up the modal
        $(".updateAvailability").click(function () {
            context.SetActiveWeekId($(this));

            var shiftId = $(this).attr("data-shift-id");
            var makeAvailable = $(this).attr("data-make-available");

            $("#shiftId").val(shiftId);
            $("#makeAvailable").val(makeAvailable);
            $("#clientKey").val(context.clientKey);

            var confirmMessage = makeAvailable == "True" ? "Are you sure you want to make this shift available?" : "Are you sure you want to make this shift unavailable?";
            $("#makeAvailableConfirmMessage").html(confirmMessage);

            $("#makeAvailableConfirmModal").modal("show");
        });

        $("#makeAvailableConfirm").click(function () {
            var params = $("#updateAvailabilityForm").serialize();
            dataService.UpdateShiftAvailability(params, context.UpdateAvailabilityComplete);
            $("#makeAvailableConfirmModal").modal("hide");
        });

        $(".updateShifts").click(function () {
            context.SetActiveWeekId($(this));

            var shiftDate = $(this).attr("data-shift-date");

            dataService.LoadShiftsToUpdate({ shiftDate: shiftDate }, context.LoadShiftsToUpdateComplete);
        });

        $(".takeShifts").click(function () {
            context.SetActiveWeekId($(this));

            var shiftDate = $(this).attr("data-shift-date");
            dataService.LoadShiftsToTake({ shiftDate: shiftDate }, context.LoadShiftsToTakeComplete);
        });

        $(".takeShiftsFromWaiter").click(function () {
            var shiftDate = $(this).attr("data-shift-date");
            var selectedWaiterId = $(this).attr("data-from-waiter");
            dataService.LoadShiftsToTake({ shiftDate: shiftDate, waiterId: selectedWaiterId }, context.LoadShiftsToTakeComplete);
        });

        $("#takeShiftConfirm").click(function () {
            var params = $("#takeShiftForm").serialize() + '&clientKey=' + context.clientKey;

            dataService.TakeShift(params, context.TakeShiftComplete);
            $("#takeShiftPopup").modal("hide");
        });
    };

    this.SetActiveWeekId = function (selectedShift) {
        context.activeWeekButtonId = "weekButton_" + selectedShift.closest(".weekContainer").attr("data-week-number");
    };

    this.BindUpdateHandler = function () {
        $("#updateShiftAvailability").unbind("click");

        $("#updateShiftAvailability").click(function () {
            var params = $("#shiftToUpdateForm").serialize() + '&clientKey=' + context.clientKey;
            dataService.UpdateShiftAvailability(params, context.UpdateAvailabilityComplete);
            $("#updateShiftPopup").modal("hide");
        });
    };

    this.LoadShifts = function (selected) {
        var fromDate = selected.attr("data-from-date");
        var toDate = selected.attr("data-to-date");
        var weekNumber = selected.attr("data-week-number");
        var parameters = { fromDate: fromDate, toDate: toDate, weekNumber: weekNumber };

        $.ajax({
            type: "GET",
            cache: false,
            url: window.appRoutes.ShiftSchedule.pathToLoadShiftSchedule,
            data: parameters,
            success: function (result) {
                if (result.indexOf("elementForAjax") > 0) {
                    $("#weekContainer_" + weekNumber).html(result);
                    context.BindHandlers();
                } else {
                    location.reload();
                }
            },
            error: function (e) {
                shifterUx.ShowModal("Oops..", "Error loading shifts");
            }
        });
    };

    this.LoadShiftForDay = function (day) {
        var parameters = { date: day };
        dataService.LoadAllShiftsSchedule(parameters, context.LoadShiftScheduleForDayComplete);
    };

    //#region Callbacks

    this.LoadShiftScheduleForDayComplete = function (data) {
        if (data.indexOf("elementForAjax") > 0) {
            $("#shiftScheduleResults").html(data);
            context.BindHandlers();
        } else {
            location.reload();
        }
    };

    this.LoadShiftsToTakeComplete = function (data) {
        if (data.indexOf("elementForAjax") > 0) {
            $("#ShiftToTake").html(data);
            $("#takeShiftPopup").modal("show");
        } else {
            location.reload();
        }
    };

    this.LoadShiftsToUpdateComplete = function (data) {
        if (data.indexOf("elementForAjax") > 0) {
            $("#updateShiftContainer").html(data);
            $("#updateShiftPopup").modal("show");

            context.BindUpdateHandler();
        } else {
            location.reload();
        }
    };

    this.UpdateAvailabilityComplete = function (result) {
        if (!result.HasErrors) {
            context.LoadShifts($("#" + context.activeWeekButtonId));
        } else {
            var message = "";
            $.each(result.Errors, function (index, error) {
                message += error.Text + '\n';
            });

            shifterUx.ShowModal("Oops..", message);
        }
    };

    this.TakeShiftComplete = function (result) {
        if (!result.HasErrors) {
            //Reload the view you were on
            if (context.isInDayView == true) {
                context.LoadShiftForDay($("#currentDateForDayView").val());
            } else {
                context.LoadShifts($("#" + context.activeWeekButtonId));
            }
        } else {

            var message = "";
            $.each(result.Errors, function (index, error) {
                message += error.Text + '\n';
            });

            shifterUx.ShowModal("Oops..", message);
        }
    };

    //#endregion
}