/* File Created: June 16, 2013 */

function ManageTimeslots() {

    var context = this;

    this.Load = function () {
        context.BindHandlers();
        //$('#confirmDelete').appendTo("body");
    };

    this.UnBindHandlers = function () {
        $(".addTimeslot").unbind("click");
    };

    this.BindHandlers = function () {

        context.UnBindHandlers();

        $(".addTimeslot").click(function () {
            var staffTypeId = $(this).attr('data-staff-type-id');
            context.AddTimeslot(staffTypeId);
            $('.addTimeslotContainer').hide();
        });

        context.BindDelete();

        $('.timepickerstart').timepicker({
            showMeridian: false,
            showInputs: false
        });
        $('.timepickerend').timepicker({
            showMeridian: false
        });
    };
    
    this.RemoveTimeslot = function (timeslot) {
        $("#timeslotId").val(timeslot.attr("data-id"));
        $("#timeslotRestaurantId").val(timeslot.attr("data-restaurant-id"));
        $("#staffTypeId").val(timeslot.attr("data-staff-type-id"));

        var data = $("#deleteTimeslotForm").serialize();
        dataService.RemoveTimeslot(data, context.UpdateTimeslots);
    };

    this.AddTimeslot = function (staffTypeId) {
        var container = $(".addContainer_" + staffTypeId);

        var startTime = container.find(".timepickerstart").val();
        var endTime = container.find(".timepickerend").val();

        var data = { startTime: startTime, endTime: endTime, staffTypeId: staffTypeId };
        dataService.AddTimeslot(data, context.UpdateTimeslots);
    };

    this.UpdateTimeslots = function (result) {
        var staffTypeId = result.StaffTypeId;
        $(".staff_" + staffTypeId).html(result.Payload);
        context.BindDelete();
    };

    this.BindDelete = function () {
        //this needs to be changed to work with bootstrap modal
        $(".deleteTimeslot").unbind("click");

        $(".deleteTimeslot").click(function () {
            var timeslot = $(this);
            $("#deleteTimeslotTimes").text(timeslot.data("times"));
            $("#confirmDelete").modal("show");

            $("#deleteTimeslot").unbind("click");
            $("#deleteTimeslot").click(function () {
                context.RemoveTimeslot(timeslot);
                $("#confirmDelete").modal("hide");

                //this is not ideal...
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
            });
        });
    };
}