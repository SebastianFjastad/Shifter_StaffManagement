function ManageWall() {

    var context = this;

    var selectedStaffTypeIds = [];
    var includeGroupPosts = false;

    this.Load = function () {
        $('#selectedStaffTypeId').multiselect({
            buttonWidth: '230px',
            onChange: function (option, checked, select) {
                context.addRemoveSelectedStaffTypes(option, checked);
                context.filterWallPosts();
            }
        });

        $("#addPost").click(function () {
            dataService.NewPost(function (result) {
                $("#postEditorContainer").html(result);
                $("#editPostPopup").modal("show");
            });
        });

        $("#savePostConfirm").click(function () {
            $("#wallPostForm").submit();
        });

        $(".cancelEdit").click(function () {
            $("#editPostPopup").modal("hide");
        });

        $("#deletePostConfirm").click(function () {
            $("#deletePostForm").submit();
        });

        context.filterWallPosts();
    };

    this.addRemoveSelectedStaffTypes = function (option, checked) {
        if ($(option).val() == "group") {
            includeGroupPosts = checked;
        } else {
            var selectedId = $(option).val();
            var indexOfId = $.inArray(selectedId, selectedStaffTypeIds);
            if ((indexOfId == -1) && checked == true) {
                selectedStaffTypeIds.push(selectedId);
            } else {
                selectedStaffTypeIds.splice(indexOfId, 1);
            }
        }
    };

    this.loadWallPostsComplete = function (result) {
        $("#postsContainer").html(result);
        context.bindWallPostEvents();
    };

    this.filterWallPosts = function () {
        var parameters = { staffTypeIds: selectedStaffTypeIds.toString(), includeGroupPosts: includeGroupPosts };
        dataService.FilterWallPosts(parameters, context.loadWallPostsComplete);
    };

    this.bindWallPostEvents = function() {

        $(".showComments").unbind("click");
        $(".showHidePost").unbind("click");
        $(".editPost").unbind("click");
        $(".deletePost").unbind("click");

        function showComments(caller) {
            $(caller).closest(".post").find(".commentsContainer").slideDown("fast");;
        };

        function hideComments(caller) {
            $(caller).closest(".post").find(".commentsContainer").slideUp("fast");;
        };

        $(".deletePost").click(function() {
            var postIdToDelete = $(this).attr("data-post-id");
            $("#wallPostId").val(postIdToDelete);
            $("#deletePostPopup").modal("show");
        });

        $(".editPost").click(function() {
            var params = { wallPostId: $(this).attr("data-post-id") };
            dataService.LoadPost(params, function(result) {
                $("#postEditorContainer").html(result);
                $("#editPostPopup").modal("show");
            });
        });

        $(".showComments").click(function() {
            var visible = $(".showComments > .glyphicon").is(".glyphicon-chevron-up");
            if (visible) {
                hideComments(this);
                $(".glyphicon", this).removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
            } else {
                showComments(this);
                $(".glyphicon", this).removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
            }
        });

        $(".showHidePost").click(function() {
            var post = $(this).closest(".post");
            var expanCollapseButton = post.find(".expandCollapseIcon");

            if (expanCollapseButton.hasClass("glyphicon-plus")) {
                post.find(".postBody").slideDown("fast");
                expanCollapseButton.removeClass("glyphicon-plus").addClass("glyphicon-minus");

            } else {
                post.find(".postBody").slideUp("fast");
                hideComments(".showComments", this);
                $(".showComments > .glyphicon").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
                expanCollapseButton.removeClass("glyphicon-minus").addClass("glyphicon-plus");
            };
        });
    };
}