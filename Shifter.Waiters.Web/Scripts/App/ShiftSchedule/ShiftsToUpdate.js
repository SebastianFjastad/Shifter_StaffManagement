/* File Created: June 16, 2013 */

function ShiftsToUpdate() {
    
    var context = this;

    this.Load = function () {
        context.BindHandlers();
        context.SetAvailabilityIndicator();
    };

    this.UnBindHandlers = function () {
        $("#shiftToUpdate").unbind("change");
    };
    
    this.BindHandlers = function () {

        context.UnBindHandlers();

        $("#shiftToUpdate").change(function () {
            context.SetAvailabilityIndicator();
        });
    };

    this.SetAvailabilityIndicator = function () {
        //Set hidden field in form
        var makeAvailable = $("#shiftToUpdate option:selected").attr("data-shift-availability");
        $("#makeAvailable").val(makeAvailable);
        
        //Update the button text
        if (makeAvailable == "True") {
            $("#updateShiftAvailability").html("Make available");
        } else {
            $("#updateShiftAvailability").html("Make unavailable");
        }
    };
}