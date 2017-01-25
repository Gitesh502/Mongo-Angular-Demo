var ChatController = function ($scope, $filter, toaster, ngDialog, webNotification, DesktopNotificationsFactory, UserFactory, $interval) {
    $scope.chatHub = $.connection.chatAppHub;// initializes hub

    $scope.$parent.UserName = document.getElementById("hdnUserName").value;
    $scope.$parent.connectionId = '';
    $scope.$parent.NickName = document.getElementById("hdnUserName").value;

    $scope.manageOnlineUser = {
        ConnectionId: '',
        UserId: document.getElementById("hdnUserId").value,
        Email: document.getElementById("hdnUserName").value,
        FullName: '',
        FirstName: '',
        LastName: '',
        NickName: document.getElementById("hdnUserName").value
    }

    $scope.name = document.getElementById("hdnUserName").value;


    $scope.email = document.getElementById("hdnUserName").value;


    $scope.userId = document.getElementById("hdnUserId").value;




    $scope.groupMsg = "";
    $scope.message = "";
    $scope.messages = [];


    $scope.userConnectionId = "";




    $scope.regUserGridOptions = {};
    $scope.isBoxClicked = false;
    $scope.chatName = "";
    $scope.privateChatBoxes = [];
    $scope.isPrivateChatMinimized = false;
    $scope.slide = false;
    $scope.expand = true;
    $scope.PrivateMessages = [];
    $scope.UserInPrivateChat = null;

    $scope.bgimages = [
        '../Images/backgrounds/1.jpg'
    ];
    $scope.privateChatBoxProperties = {
        showPrivateBox: false,
        isBoxOpened: false
    }
    $scope.privateMessageCount = {
        messageCount: 0,
        isRead: false,
        userEmail: '',
        connectionId: ''
    }
    $scope.loggeduserDetails = {
        firstName: '',
        lastName: '',
        fullName: '',
        nickName: '',
        email: '',
        userId: '',
        userName: ''
    }
    $scope.registeredUsers = {
        firstName: '',
        lastName: '',
        fullName: '',
        nickName: '',
        email: '',
        userId: '',
        userName: '',
    }
    $scope.lstregisteredUsers = [];
    $scope.desktopNotify = {
        title: '',
        body: ''
    }
    $scope.privateBoxMessage = {
        pMessage: ''
    }



    $scope.init = function () {
        var localUser = angular.fromJson(localStorage.getItem('loggedUser'));
        if (localUser === null) {
            $scope.getLoggedUser();
        }
        else {
            $scope.loggeduserDetails.firstName = localUser.UserProfile.FirstName;
            $scope.loggeduserDetails.lastName = localUser.UserProfile.LastName;
            $scope.loggeduserDetails.fullName = localUser.UserProfile.FirstName + " " + localUser.UserProfile.LastName;
            $scope.loggeduserDetails.nickName = localUser.UserProfile.FirstName;
            $scope.loggeduserDetails.email = localUser.UserProfile.Email;
            $scope.loggeduserDetails.userId = localUser.Id;
        }
        $scope.getRegisteredUsers();
        $scope.ConfigureList();
        $scope.beginVertScroll();


        $.connection.hub.start().done(function () {
            $scope.Connect();
        });
        $.connection.hub.error(function (err) {
            console.log('An error occurred: ' + err);
        });
    }
    $scope.getMessagesByToUserId = function () {
        var result = UserFactory.GetUserByEmail($scope.email);
    }
    $scope.getLoggedUser = function () {
        var result = UserFactory.GetUserByEmail($scope.email);
        result.then(function (result) {
            console.log(result)
            if (result.success) {
                $scope.loggeduserDetails.firstName = result.response.UserProfile.FirstName;
                $scope.loggeduserDetails.lastName = result.response.UserProfile.LastName;
                $scope.loggeduserDetails.fullName = result.response.UserProfile.FirstName + " " + result.response.UserProfile.LastName;
                $scope.loggeduserDetails.nickName = result.response.UserProfile.FirstName;
                $scope.loggeduserDetails.email = result.response.UserProfile.Email;
                $scope.loggeduserDetails.userId = result.response.Id;
                localStorage.setItem("loggedUser", angular.toJson(result.response));

            }
            else {
                alert("Error is getting user details.Please refresh the page")
            }
        })
    }
    $scope.getRegisteredUsers = function () {
        var result = UserFactory.GetRegisteredUsers();
        result.then(function (result) {
            if (result.success) {
                var res = result.response;
                angular.forEach(res, function (value, key) {
                    $scope.registeredUsers = {
                        fullName: value.UserProfile.FirstName + " " + value.UserProfile.LastName,
                        email: value.Email,
                        //firstName: value.UserProfile.FirstName,
                        //lastName: value.UserProfile.LastName,
                        //nickName: value.UserProfile.FirstName,
                        //userId: "",
                        //userName: value.Email,
                    }
                    $scope.lstregisteredUsers.push($scope.registeredUsers)
                });

            }
            else {
                alert("Error is getting user details.Please refresh the page")
            }
        })
    }




    //Connect to chathub 

    $scope.Connect = function () {
        $scope.chatHub.server.connect($scope.$parent.NickName);
    }





    $scope.OnlineUsers = [];
    $scope.chatHub.client.onConnected = function (id, onlineUsers, messages) {
        $scope.$parent.connectionId = id;

        $scope.OnlineUsers.length = 0;

        $scope.OnlineUsers = $.parseJSON(onlineUsers);

        $scope.$apply();

        angular.forEach(messages, function (value, key) {
            $scope.messages.push({ message: value.Message, username: value.UserName, email: value.Email, fullName: $scope.loggeduserDetails.fullName});
        });

        $scope.$apply();

        for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
            if ($scope.OnlineUsers[i].UserName === $scope.$parent.UserName) $scope.OnlineUsers.splice(i, 1);
        }

        $scope.$apply();
    }
    $scope.chatHub.client.onNewUserConnected = function (id, onlineUsers, messages) {//id, name, email,firstName,lastName,fullName
        var objResp = $.parseJSON(onlineUsers);
        for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
            if ($scope.OnlineUsers[i].UserName === objResp.UserName) $scope.OnlineUsers.splice(i, 1);
        }


        $scope.OnlineUsers.push(objResp);
        $scope.$apply();

        var title = objResp.FullName + " is online";
        DesktopNotificationsFactory(title, "")
    }




    //$scope.chatHub.client.onUserDisconnected = function (id, userName) {
    //    alert('')
    //    for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
    //        if ($scope.OnlineUsers[i].ConnectionId === $scope.userConnectionId) $scope.OnlineUsers.splice(i, 1);
    //    }
    //    $scope.$apply();
    //    $scope.OnlineUsers.push({ ConnectionId: id, UserName: userName, Email: userName });
    //    $scope.$apply();
    //}
    //$scope.chatHub.disconnected = function () {
    //    alert('')
    //    for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
    //        if ($scope.OnlineUsers[i].ConnectionId === $scope.userConnectionId) $scope.OnlineUsers.splice(i, 1);
    //    }
    //    $scope.$apply();
    //    //$scope.OnlineUsers.push({ ConnectionId: id, UserName: userName, Email: userName });
    //    //$scope.$apply();
    //}
    $scope.sendGroupMsg = function () {
        $scope.chatHub.server.sendMessageToAll($scope.name, $scope.groupMsg, $scope.email);
        $scope.groupMsg = "";
    }
    $scope.chatHub.client.messageReceived = function (userName, message, Email, fullName) {
        $scope.messages.push({ message: message, username: userName, email: Email, fullName: fullName });
        $scope.$apply();
        DesktopNotificationsFactory(Email + " says: ", message)
        $scope.groupMsg = "";
    }
    $scope.chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message,fromFullName) {
        $scope.expand = true;
        $scope.PrivateMessages.push({ to: windowId, from: fromUserName, message: message,fullName:fromFullName });
        $scope.$apply();
        if ($scope.$parent.UserName !== fromUserName) // otheruser's pm
        {
            if ($scope.UserInPrivateChat === null) {
                $scope.UserInPrivateChat = { name: fromUserName, ConnectionId: privateChatBoxes }
            }
        }
        $scope.$apply();
        $scope.privateBoxMessage.pMessage = "";

        //if ($scope.$parent.UserName !== fromUserName)
        DesktopNotificationsFactory(fromUserName + " sent: ", message)
        $scope.privateMessageCount.messageCount = $scope.privateMessageCount.messageCount + 1;
        $scope.privateMessageCount.isRead = false;
        $scope.privateMessageCount.userEmail = fromUserName;
        $scope.privateMessageCount.connectionId = windowId;
        $scope.$apply();
    }

    //check if obejct is alreay is array object (compare two objects)
    $scope.containsObject = function (obj, list) {
        var i;
        for (i = 0; i < list.length; i++) {
            if (angular.equals(list[i], obj)) {
                return true;
            }
        }

        return false;
    };
    $scope.openInPrivate = function (isO) {
        var addToArray = true;
        for (var i = 0; i < $scope.privateChatBoxes.length; i++) {
            if ($scope.privateChatBoxes[i].connectionId === isO.x.ConnectionId) {
                addToArray = false;
            }
        }
        if (addToArray) {
            $scope.privateChatBoxes.push({ Uname: isO.x.UserName, connectionId: isO.x.ConnectionId, FullName: isO.x.FullName });
        }
        $scope.chatName = isO.x.UserName;
        if ($scope.privateMessageCount.userEmail === isO.x.Email) {
            $scope.privateMessageCount.isRead = true;
            $scope.privateMessageCount.messageCount = 0;
        }
    }

    $scope.closePrivateChat = function (Email, ConnectionId) {
        for (i = $scope.privateChatBoxes.length - 1; i >= 0; i--) {
            if ($scope.privateChatBoxes[i].connectionId === ConnectionId) $scope.privateChatBoxes.splice(i, 1);
        }
    }


    $scope.sendPrivateMessage = function (that) {
        $scope.expand = true;
        $scope.chatHub.server.sendPrivateMessage(that.connectionId, $scope.privateBoxMessage.pMessage);
        $scope.privateBoxMessage.pMessage = "";
    }
    $scope.ConfigureList = function () {
        $interval(function () {
            $('ul.container li:nth-child(2n)').css({
                "background": "linear-gradient(to bottom)",
                //"background": "linear-gradient(to bottom, #878ced 0%,#0072c6 49%,#007 51%,#878ced 100%)",
                //"color": "#FFF"
            });
        }, 100, 1);
    }

    $scope.beginVertScroll = function () {

        $interval(
         function () {
             var firstElement = $('ul.container li:first');
             var hgt = firstElement.height() +

                 parseInt(firstElement.css("paddingTop"), 10) + parseInt(firstElement.css("paddingBottom"), 10) +
                 parseInt(firstElement.css("marginTop"), 10) + parseInt(firstElement.css("marginBottom"), 10);

             var cntnt = firstElement.html();

             $("ul.container").append("<li>" + cntnt + "</li>");

             ;

             cntnt = "";
             firstElement.animate({
                 "marginTop": -hgt
             }, 600, function () {

                 $scope.itemToremove = $(this);


                 $('ul.container li').last().css({
                     "background": $(this).css("background"),
                     "color": $(this).css("color")
                 });

                 $(this).remove();

             });
             //alert(hgt);
         },
           5000
         );
    };
}

ChatController.$inject = ['$scope', '$filter', 'toaster', 'ngDialog', 'webNotification', 'DesktopNotificationsFactory', 'UserFactory', '$interval'];