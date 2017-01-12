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
    $scope.expand = true;
    $scope.PrivateMessages = [];
    $scope.UserInPrivateChat = null;
    $scope.$parent.UserName = document.getElementById("hdnUserName").value;
    $scope.privateBoxMessage = {
        pMessage: ''
    }
    $scope.init = function () {
        //var result = GetUserDetailsFactroy($scope.email, $scope.userId);
        //result.then(function (result) {
        //    console.log(result)
        //    if (result.success) {
        //       // $window.location.href = '/Chat/Home/Index';
        //        localStorage.setItem("loggedUser",angular.toJson(result.response));
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
        $scope.OnlineUsers.length=0;
        //$scope.OnlineUsers = $filter('unique')($.parseJSON(allUsers), 'Email');
        $scope.OnlineUsers = $.parseJSON(allUsers);
        $scope.$apply();
        angular.forEach(messages, function (value, key) {
            console.log(key + ': ' + value);
            $scope.messages.push({ message: value.Message, username: value.UserName, email: value.Email });
        });
        $scope.$apply();
        $scope.userConnectionId = id;
        $scope.name = userName;
        for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
            if ($scope.OnlineUsers[i].UserName == userName) $scope.OnlineUsers.splice(i, 1);
        }
        
        $scope.$apply();
    }
    $scope.chatHub.client.onNewUserConnected = function (id, name, email) {
        for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
            if ($scope.OnlineUsers[i].UserName == email) $scope.OnlineUsers.splice(i, 1);
        }
        $scope.OnlineUsers.push({ ConnectionId: id, UserName: name, Email: email });
        //$scope.OnlineUsers = $filter('unique')($scope.OnlineUsers, 'Email');
        $scope.OnlineUsers =$scope.OnlineUsers;
        $scope.$apply();
    }
    //$scope.chatHub.client.onUserDisconnected = function (id, userName) {
    //    alert('')
    //    for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
    //        if ($scope.OnlineUsers[i].ConnectionId == $scope.userConnectionId) $scope.OnlineUsers.splice(i, 1);
    //    }
    //    $scope.$apply();
    //    $scope.OnlineUsers.push({ ConnectionId: id, UserName: userName, Email: userName });
    //    $scope.$apply();
    //}
    //$scope.chatHub.disconnected = function () {
    //    alert('')
    //    for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
    //        if ($scope.OnlineUsers[i].ConnectionId == $scope.userConnectionId) $scope.OnlineUsers.splice(i, 1);
    //    }
    //    $scope.$apply();
    //    //$scope.OnlineUsers.push({ ConnectionId: id, UserName: userName, Email: userName });
    //    //$scope.$apply();
    //}
    $scope.chatHub.client.messageReceived = function (userName, message, Email) {
        $scope.messages.push({ message: message, username: userName, email: Email });
        $scope.$apply();
    }
    $scope.chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {
        $scope.expand = true;
        $scope.PrivateMessages.push({ to: windowId, from: fromUserName, message: message });
        $scope.$apply();
        if ($scope.$parent.UserName != fromUserName) // otheruser's pm
        {
            if ($scope.UserInPrivateChat == null) {
                $scope.UserInPrivateChat = { name: fromUserName, ConnectionId: windowId }
            }
        }
        $scope.$apply();
        $scope.privateBoxMessage.pMessage = "";
        $scope.$apply();
    }
    $scope.openInPrivate = function (UserName, Email,ConnectionId) {
        $scope.privateChatBoxes.push({ Uname: UserName,connectionId:ConnectionId });
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
        $scope.expand = true;
        $scope.chatHub.server.sendPrivateMessage(toUserConnId, $scope.privateBoxMessage.pMessage);
        $scope.privateBoxMessage.pMessage = "";
    }
   
}

ChatController.$inject = ['$scope', '$filter', 'toaster', 'ngDialog'];