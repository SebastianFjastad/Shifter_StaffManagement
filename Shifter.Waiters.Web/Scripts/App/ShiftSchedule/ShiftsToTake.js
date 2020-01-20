/* File Created: June 16, 2013 */

function ShiftsToTake() {
    
    var context = this;

    this.Load = function () {
        context.BindHandlers();
        context.FilterWaiters($("#timeslotToTake"));
    };

    this.UnBindHandlers = function () {
        $("#timeslotToTake").unbind("change");
    };
    
    this.BindHandlers = function () {

        context.UnBindHandlers();

        $("#timeslotToTake").change(function () {
            context.FilterWaiters($(this));
        });
    };

    this.FilterWaiters = function (timeslot) {
        var startTime = timeslot.val();
        $(".waiters").each(function () {
            if ($(this).attr("data-start-time") != startTime) {
                $(this).hide();
            }
            else {
                $(this).show();
                $("#takeFromWaiter").val($(this).val());
            }
        });
    };
}