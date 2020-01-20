function ShifterUX() {

    //#region General

    //Checks if input is a number
    this.IsNumberKey = function (e) {
        var isValid = true;
        var charCode = (e.which) ? e.which : event.keyCode;
        if (charCode > 31 && (charCode < 46 || charCode > 57)) {
            isValid = false;
        }
        return isValid;
    };

    //Gets current date & time
    this.GetCurrentDateTime = function () {
        var currentdate = new Date();
        var datetime = currentdate.getDate() + "/"
            + (currentdate.getMonth() + 1) + "/"
            + currentdate.getFullYear() + " @ "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        return datetime;
    };

    this.dialog = function (element, buttonText, callback, data) {
        element.dialog({
            resizable: false,
            modal: true,
            buttons: {
                buttonText: function () {
                    callback(data);
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    };
    
    //#endregion

    //Validates all fields marked as required in an element/context
    this.Validate = function (validationContext, errorBox) {
        var isValid = true;

        $(".invalid").removeClass("invalid");

        if (validationContext) {
            
            validationContext.find(".required").each(function () {
                if ($(this).val() == "") {

                    if ($(this).is("select")) {
                        $(this).parent(".selector").addClass("invalid");
                        $(this).parent(".selector").prop("title", $(this).attr("data-validation-name") + " is required");
                    }

                    $(this).addClass("invalid");
                    $(this).prop("title", $(this).attr("data-validation-name") + " is required");

                    isValid = false;

                }
            });
        }

        //Show hide errors optional
        if (errorBox) {
            if (!isValid) {
                errorBox.show();
            } else {
                errorBox.hide();
            }
        }

        this.BindReEvaluateValidity();

        return isValid;
    };

    this.BindReEvaluateValidity = function () {
        $(".invalid").unbind("change");
        
        $(".invalid").keyup(function() {
            if ($(this).val().length > 0) {
                $(this).removeClass("invalid");
            }
        });
    };
}
