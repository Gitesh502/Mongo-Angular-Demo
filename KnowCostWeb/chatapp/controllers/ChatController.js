var ChatController = function ($scope, $filter, toaster, ngDialog) {
    $scope.chatHub = $.connection.chatAppHub;// initializes hub
    $scope.name = "Guest";
    $scope.message = "";
    $scope.userConnectionId = "";
    $scope.messages = {};
    $scope.init = function () {
        $.connection.hub.start().done(function () {
            $scope.Connect();
        });
        $.connection.hub.error(function (err) {
            console.log('An error occurred: ' + err);
        });
    }

    $scope.Connect = function () {
        // ngDialog.open({ template: 'openConnectPopup' });
        $scope.chatHub.server.connect($scope.name);
    }

    $scope.connectHub = function () {
        if ($scope.chatHub != null && $scope.chatHub != undefined)
            $scope.chatHub.server.connect($scope.name);
    }


  
    $scope.OnlineUsers = [];
    $scope.OUsers = [];
    $scope.chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        $scope.OnlineUsers = $.parseJSON(allUsers);
        $scope.$apply();
        $scope.userConnectionId = id;
        $scope.name = userName;
    }
    $scope.chatHub.client.onNewUserConnected = function (id, name) {

        $scope.OnlineUsers.push({ ConnectionId: id, UserName: name });
        $scope.$apply();
        $scope.userConnectionId = id;
        $scope.name = name;
    }
    $scope.chatHub.client.onUserDisconnected = function (id, userName) {
    }
    $scope.chatHub.client.messageReceived = function (userName, message) {
    }
    $scope.chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {
    }
    $scope.openInPrivate = function (id) {

    }
}

ChatController.$inject = ['$scope','$filter', 'toaster', 'ngDialog'];