var ChatController = function ($scope, $filter, toaster, ngDialog) {
    $scope.chatHub = $.connection.chatAppHub;// initializes hub
    $scope.name = document.getElementById("hdnUserName").value;
    $scope.email = document.getElementById("hdnUserName").value;
    $scope.userId = document.getElementById("hdnUserId").value;
    $scope.message = "";
    $scope.userConnectionId = "";
    $scope.groupMsg = "";
    $scope.messages = [];
    $scope.showPrivateBox = false;
    $scope.isBoxClicked = false;
    $scope.chatName = "";
    $scope.privateChatBoxes = [];
    $scope.privateChatBoxes.length = 0;
    $scope.isPrivateChatMinimized = false;
    $scope.slide = false;
    $scope.privateMessage = "";
    $scope.PrivateMessages = [];
    $scope.UserInPrivateChat = null;
    $scope.$parent.UserName = document.getElementById("hdnUserName").value;
    $scope.init = function () {
        //var result = GetUserDetailsFactroy($scope.email, $scope.userId);
        //result.then(function (result) {
        //    console.log(result)
        //    if (result.success) {
        //        $window.location.href = '/Chat/Home/Index';

        //    }
        //    else {
        //        alert("Error is getting user details.Please refresh the page")
        //    }
        //})
        $.connection.hub.start().done(function () {
            if ($scope.userConnectionId == "")
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
        $scope.OnlineUsers = $filter('unique')($.parseJSON(allUsers), 'Email');
        console.log($scope.OnlineUsers)
        $scope.$apply();
        angular.forEach(messages, function (value, key) {
            console.log(key + ': ' + value);
            $scope.messages.push({ message: value.Message, username: value.UserName, email: value.Email });
        });
        $scope.$apply();
        $scope.userConnectionId = id;
        $scope.name = userName;
    }
    $scope.chatHub.client.onNewUserConnected = function (id, name, email) {
        $scope.OnlineUsers.push({ ConnectionId: id, UserName: name, Email: email });
        $scope.OnlineUsers = $filter('unique')($scope.OnlineUsers, 'Email');
        $scope.$apply();

        $scope.userConnectionId = id;
        $scope.name = name;
    }
    $scope.chatHub.client.onUserDisconnected = function (id, userName) {
    }
    $scope.chatHub.client.messageReceived = function (userName, message, Email) {
        $scope.messages.push({ message: message, username: userName, email: Email });
        $scope.$apply();
    }
    $scope.chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {
        $scope.PrivateMessages.push({ to: windowId, from: fromUserName, message: message });
        if ($scope.$parent.UserName != fromname) // otheruser's pm
        {
            if ($scope.UserInPrivateChat == null) {
                $scope.UserInPrivateChat = { name: fromUserName, ConnectionId: windowId }
            }
        }
    }
    $scope.openInPrivate = function (UserName, Email,ConnectionId) {
        $scope.privateChatBoxes.push({ name: UserName,connectionId:ConnectionId });
       // $scope.showPrivateBox = true;
        $scope.chatName = UserName;
    }

    $scope.sendGroupMsg = function () {
        $scope.chatHub.server.sendMessageToAll($scope.name, $scope.groupMsg, $scope.email);
    }
    $scope.MinimizeChatBox = function (index) {

    }
    $scope.sendPrivateMessage=function(toUserConnId,toUserMailID)
    {
        $scope.chatHub.server.sendPrivateMessage(toUserConnId, $scope.privateMessage);
    }
   
}

ChatController.$inject = ['$scope', '$filter', 'toaster', 'ngDialog'];