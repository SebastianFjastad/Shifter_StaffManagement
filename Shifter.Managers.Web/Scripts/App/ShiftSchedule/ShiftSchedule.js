/* File Created: June 16, 2013 */

function ShiftSchedule() {

    var context = this;

    var startDate = "";
    var endDate = "";
    var copyToDate = "";
    var weekTabSelector = "";
    var selectedStaffTypeIds = [];
    var selectedStaffTypeNames = [];

    this.shiftScheduleChangedTopic = "";

    this.Load = function () {
        context.BindHandlers();
        $(".tabItem").first().click();

        $("#quickEditTimeslotSelect").prop('disabled', true);

        // Reference the auto-generated proxy for the hub.  
        var broadcaster = $.connection.messageHub;

        // Create a function that the hub can call back to display messages.
        broadcaster.client.raiseEvent = function (topic, message) {
            if (topic == context.shiftScheduleChangedTopic) {
                var shift = $("#shift_" + message);
                if (shift.length > 0) {
                    var parameters = { shiftId: message };
                    //dataService.LoadShiftSummary(parameters, context.LoadShiftSummaryComplete, message);
                }
            }
        };

        $.connection.hub.start();
    };

    //#region bind handlers

    this.UnBindHandlers = function () {
        $(".quickEditTimeslotSelect").unbind("change");
        $(".tabItem").unbind("click");
        $("#ContactNumbercopySchedule").unbind("click");
        $("#overrideCopyWarnings").unbind("click");
        $("#copyScheduleConfirm").unbind("click");
    };

    this.BindHandlers = function () {

        context.UnBindHandlers();

        $('#selectedStaffTypeId').multiselect({
            buttonWidth: '138px',
            nonSelectedText: 'All staff',
            allSelectedText: 'All staff',
            onChange: function (option, checked, select) {

                context.AddRemoveSelectedStaffTypes(option, checked);

                if (selectedStaffTypeIds.length > 0) {
                    $(".timslots").hide();
                    $(".timslots").prop('disabled', true);
                    $.each(selectedStaffTypeIds, function (index, staffTypeId) {
                        $(".timslotForType_" + staffTypeId).show();
                        $(".timslotForType_" + staffTypeId).prop('disabled', false);
                    });
                } else {
                    $(".timslots").show();
                    $(".timslots").prop('disabled', false);
                }

                if (selectedStaffTypeIds.length != 1) {
                    $("#quickEditTimeslotSelect").val("0");
                    $("#quickEditTimeslotSelect").prop('disabled', true);
                } else {
                    $("#quickEditTimeslotSelect").prop('disabled', false);
                }

                context.FilterShifts();
            }
        });

        $(".quickEditTimeslotSelect").change(function () {
            if ($(this).val() == 0) {
                $(this).css("font-weight", "Normal;");
            } else {
                $(this).css("font-weight", "Bold");
            }
        });

        $(".tabItem").click(function () {
            var weekNumber = $(this).attr("data-week-number");
            $(".tabbedContent").hide();
            $("#week_" + weekNumber).show();

            //used for filtering
            $(".tabItem").removeClass("selectedTab active");
            $(this).addClass("selectedTab active");

            context.FilterShifts();
        });

        $("#printWeek").click(function() {
            window.print();
        });

        $("#copySchedule").click(function () {
            //hide the selected week from the dropdown
            $(".weekOption").show();
            var selectedStartDate = $(".selectedTab").attr("data-week-from");
            $(".weekOption[data-week-from='" + selectedStartDate + "']").hide();

            var target = context.GetSelectedStaffTypes();
            $("#copyScheduleForStaffType").html(target);

            $("#selectedWeek").find("option:first-child").prop("selected", true);

            var selectedWeekEnd = $(".selectedTab").attr("data-week-to");
            $("#currentWeekForCopy").html(selectedStartDate.slice(0, -5) + " - " + selectedWeekEnd.slice(0, -5));

            $("#scheduleCopyPopup").modal("show");

            $("#copyScheduleConfirm").prop("disabled", true);

            $("#selectedWeek").change(function () {
                $("#copyScheduleConfirm").prop("disabled", !$("#selectedWeek > option:selected").attr("data-week-from"));
            });
        });

        $("#overrideCopyWarnings").click(function () {
            var parameters = { scheduleStartDate: startDate, scheduleEndDate: endDate, copyToStartDate: copyToDate, staffTypeId: selectedStaffTypeIds.toString() };

            dataService.ForceScheduleCopy(parameters, context.ForceComplete);
        });

        $("#copyScheduleConfirm").click(function () {
            startDate = $(".selectedTab").attr("data-week-from");
            endDate = $(".selectedTab").attr("data-week-to");

            var selectedOption = $(".weekOption:selected");
            copyToDate = selectedOption.attr("data-week-from");

            weekTabSelector = selectedOption.attr("data-week-tab-selector");

            var parameters = { scheduleStartDate: startDate, scheduleEndDate: endDate, copyToStartDate: copyToDate, staffTypeIds: selectedStaffTypeIds.toString() };

            dataService.CopySchedule(parameters, context.CopyComplete);
        });
    };

    //#endregion

    //#region methods

    this.GetSelectedStaffTypes = function () {
        if (selectedStaffTypeNames.length > 0) {
            return selectedStaffTypeNames.toString();
        }
        return "All";
    };

    this.AddRemoveSelectedStaffTypes = function (option, checked) {
        var selectedId = $(option).val();
        var selectedName = $(option).attr('data-staff-type-name');

        var indexOfId = $.inArray(selectedId, selectedStaffTypeIds);
        if ((indexOfId == -1) && checked == true) {
            selectedStaffTypeIds.push(selectedId);
            selectedStaffTypeNames.push(selectedName);
        } else {
            selectedStaffTypeIds.splice(indexOfId, 1);
            selectedStaffTypeNames.splice(indexOfId, 1);
        }
    };

    this.FilterShifts = function () {
        $("#selectedTimeslot").val("");

        var weekFrom = $(".selectedTab").attr("data-week-from");
        var weekTo = $(".selectedTab").attr("data-week-to");
        var parameters = { staffTypeIds: selectedStaffTypeIds.toString(), weekFrom: weekFrom, weekTo: weekTo };

        dataService.LoadShifts(parameters, context.LoadShiftsComplete);
    };

    //#endregion

    //#region On Complete events

    this.LoadShiftsComplete = function (data) {
        if (data.indexOf("elementForAjax") > 0) {
            $("#shiftScheduleResults").html(data);
        }
    };

    this.CopyComplete = function (data) {
        if (data.HasErrors) {
            $("#scheduleCopyErrors").html(data.Errors);
        } else if (data.HasWarnings) {
            $("#scheduleCopyPopup").modal("hide");
            $("#scheduleCopyOverridePopup").modal("show");
        } else {
            $("#scheduleCopyPopup").modal("hide");
            $("." + weekTabSelector).click();
        }
    };

    this.ForceComplete = function (data) {
        if (data.HasErrors) {
            $("#forceCopyErrors").html(data.Errors);
        } else {
            $("#scheduleCopyOverridePopup").modal("hide");
            $("." + weekTabSelector).click();
        }
    };

    //#endregion
}