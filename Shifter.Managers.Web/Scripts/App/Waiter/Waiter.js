/* File Created: June 16, 2013 */

function Waiter() {

    var context = this;

    var shifterUx = new ShifterUX();

    this.Load = function () {

        context.BindHandlers();
    };

    this.UnBindHandlers = function () {
        $("#deleteWaiter").unbind("click");
        $("#saveWaiter").unbind("click");
        $("#deleteWaiterConfirm").unbind("click");
    };

    this.BindHandlers = function () {

        context.UnBindHandlers();

        $("#deleteWaiter").click(function () {
            $("#confirmDelete").modal("show");
        });

        $("#saveWaiter").click(function() {
            var isValid = shifterUx.Validate($("#saveWaiterForm"), null);
            if (isValid) {
                $('.changed-input').removeClass("changed-input");
                $("#saveWaiterForm").submit();
            }
        });
        
        $("#deleteWaiterConfirm").click(function () {
            $("#deleteWaiterForm").submit();
        });

        $("#resetPassword").click(function() {
            dataService.ResetWaitersPassword({ id: $("#Staff_Id").val() }, context.PasswordResetComplete);
        });
    };

    $('.timeOffDates').datepicker();

    $("#saveTimeOffConfirm").click(function () {
        $("#saveTimeOffForm").submit();
    });

    $("#deleteTimeOffConfirm").click(function () {
        $("#deleteTimeOffForm").submit();
    });

    $("#addTimeOff").click(function () {
        $("#savePopup").modal("show");
    });

    $(".removeTimeOff").click(function () {
        var id = $(this).attr("data-id");
        var fromDate = $(this).attr("data-from-date");
        var toDate = $(this).attr("data-to-date");

        $("#deleteTimeOffForm").find("#editFrom").html(fromDate);
        $("#deleteTimeOffForm").find("#editTo").html(toDate);
        $("#deleteTimeOffForm").find("#timeOffId").val(id);

        $("#confirmDeletePopup").modal("show");
    });

    this.PasswordResetComplete = function(data) {
        if (data.HasErrors) {
            $("#errorMessages").html("An error occurred while resetting the password.");
            $("#errorBox").slideDown();
            setTimeout(function () { $("#errorBox").slideUp(); }, 4000);
        } else {
            $("#infoMessages").html("The waiters password has been reset and emailed to them.");
            $("#infoBox").slideDown();
            setTimeout(function () { $("#infoBox").slideUp(); }, 4000);
        }
    };
}