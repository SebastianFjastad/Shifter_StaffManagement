
function Settings() {
    
    var context = this;

    this.Load = function () {
        context.BindHandlers();
        var url = document.location.toString();
        if (url.match('#')) {
            $('#settingsTabs a[href=#' + url.split('#')[1] + ']').tab('show');
        } else if ($("#notificationsContainer").length > 0) {
            $('#settingsTabs a:first').tab('show');
        } else {
            $('#settingsTabs a:last').tab('show');
        }

        $('#calculate .input-daterange').datepicker({
            autoclose: true,
            format: "d M yyyy",
            todayHighlight: true
        });
    };

    this.BindHandlers = function () {
        $("#calculateHours").unbind("click");

        $('#selectedStaffTypeId').multiselect({
            buttonWidth: '150px',
            nonSelectedText: 'All staff',
            allSelectedText: 'All staff',
            //checkboxName: "staffTypeIds"
        });

        $("#calculateHours").click(function () {
            if ($("#fromDate").val() && $("#toDate").val()) {

                var ids = [];
                $("#staffTypeContainer").find("input[type=checkbox]:checked").each(function () {
                    ids.push($(this).val());
                });

                $("#selectedStaffTypeIds").val(ids.toString());

                var params = $("#calculateHoursForm").serialize();

                dataService.LoadWaiterHours(params, context.CalculateComplete);
            } else {
                alert("please provide a date range.");
            }
        });
    };

    this.CalculateComplete = function(result) {
        $("#waiterHoursContainer").html(result);
    };
}