var NavBarController = function ($scope, ManageMessagesFactory, moment, commonVariableService)
{
    $scope.fromMessages = {
        fullName: '',
        message: '',
        fromuserid: '',
        datetime: '',
    };
    $scope.lstfromMessages = [];
    $scope.initNavbar = function () {
        $scope.getMessagesByToUserId();
    }
    $scope.getMessagesByToUserId = function () {
        var result = ManageMessagesFactory.getMessagesByFromUserId("");
        result.then(function (result) {
            console.log(result)
            if (result.success) {
                var res = result.response;
                angular.forEach(res, function (value, key) {
                    $scope.fromMessages = {userName:value.fromUser.UserName, fullName: value.fromUser.FullName, message: value.MessageDescription, fromuserid: value.fromUser.Id, datetime: value.MessageOn,isRead:false };
                    commonVariableService.setfromMessagesProperties($scope.fromMessages);
                });
                $scope.lstfromMessages = commonVariableService.getfromMessagesProperties();
               // this.$apply();
            }
            else {
                alert("Error is getting user details.Please refresh the page")
            }
        })
    }
    $scope.openInPrivateBox = function (that)
    {
        commonVariableService.setPrivateChatBoxes({ Uname: that.x.userName, connectionId: that.x.fromuserid, FullName: that.x.fullName });
    }
}
NavBarController.$inject = ['$scope', 'ManageMessagesFactory', 'moment', 'commonVariableService'];