/* File Created: June 16, 2013 */

function ManageWaiters() {

    var context = this;

    this.Load = function () {

        if ($(".timeoutAlert").length > 0) {
            context.AlertTimeout();
        }
    };

    this.AlertTimeout = function() {
        setTimeout(function () { $(".timeoutAlert").slideUp(); }, 4000);
    };
}