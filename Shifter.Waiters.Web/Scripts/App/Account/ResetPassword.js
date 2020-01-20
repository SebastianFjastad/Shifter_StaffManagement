
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
            $("#errorMessage").parent().hide();

            var params = { username: $("#username").val() }
            dataService.FindMe(params, context.FindMeComplete);
        });

        $("#errorClose").click(function() {
            $("#errorClose").hide();
        });
    };

    this.FindMeComplete = function (result) {
        if (result.HasErrors)
        {
            $("#errorMessage").html(result.Message);
            $("#errorMessage").parent().show();

            $("#findMeContainer").show();
            $("#confirmEmailAddress").hide();
        }
        else if (result.HasErrors == false)
        {
            $("#waiterId").val(result.Data.WaiterId);
            $("#emailAddress").html(result.Data.EmailAddress);

            $("#findMeContainer").hide();
            $("#confirmEmailAddress").show();
        }
    };
}