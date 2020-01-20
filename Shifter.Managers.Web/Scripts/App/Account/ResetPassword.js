
function ResetPassword() {
    
    var context = this;

    this.Load = function () {
        context.BindHandlers();
    };

    this.UnBindHandlers = function () {
        $("#findMe").unbind("click");
    };
    
    this.BindHandlers = function () {

        context.UnBindHandlers();

        $("#findMe").click(function () {
            $("#errorMessage").hide();

            var params = { username: $("#username").val() }
            dataService.FindMe(params, context.FindMeComplete);
        });
    };

    this.FindMeComplete = function (result) {
        if (result.HasErrors)
        {
            $("#errorMessage").html(result.Message);
            $("#errorMessage").show();

            $("#findMeContainer").show();
            $("#confirmEmailAddress").hide();
        }
        else if (result.HasErrors == false)
        {
            $("#managerId").val(result.Data.ManagerId);
            $("#emailAddress").html(result.Data.EmailAddress);

            $("#findMeContainer").hide();
            $("#confirmEmailAddress").show();
        }
    };
}