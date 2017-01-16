var ChatController = function ($scope, $filter, toaster, ngDialog, webNotification, DesktopNotificationsFactory, UserFactory, $interval) {
    $scope.chatHub = $.connection.chatAppHub;// initializes hub
    $scope.name = document.getElementById("hdnUserName").value;
    $scope.email = document.getElementById("hdnUserName").value;
    $scope.userId = document.getElementById("hdnUserId").value;
    $scope.message = "";
    $scope.userConnectionId = "";
    $scope.groupMsg = "";
    $scope.messages = [];
    $scope.regUserGridOptions = {};
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
    $scope.bgimages = [
        '../Images/backgrounds/1.jpg'
    ];
    $scope.privateChatBoxProperties = {
        showPrivateBox: false,
        isBoxOpened:false
    }
    $scope.privateMessageCount = {
        messageCount: 0,
        isRead: false,
        userEmail: '',
        connectionId:''
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
    $scope.desktopNotify={
        title: '',
        body:''
    }
    $scope.privateBoxMessage = {
        pMessage: ''
    }
    //$scope.regUserGridOptions = { }
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
            if ($scope.userConnectionId === "")
                $scope.Connect();
        });
        $.connection.hub.error(function (err) {
            console.log('An error occurred: ' + err);
        });
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
    $scope.getRegisteredUsers=function()
    {
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
               // $scope.$apply();
                //$scope.regUserGridOptions.enableSorting = false;
                //$scope.regUserGridOptions.columnDefs = [{ name: 'Name', field: 'fullName' }, { name: 'Send Request', field: 'email' }];
                //$scope.regUserGridOptions.data=$scope.lstregisteredUsers;

                //$scope.regUserGridOptions = {
                //    enableSorting: false,
                //    data: $scope.lstregisteredUsers,
                //    columnDefs: [{ name: 'Name', field: 'fullName' }, { name: 'Send Request', field: 'email' }],
                  
                //};
               // $scope.$apply();
            }
            else {
                alert("Error is getting user details.Please refresh the page")
            }
        })
    }
    $scope.Connect = function () {
        // ngDialog.open({ template: 'openConnectPopup' });

        $scope.chatHub.server.connect($scope.email);
    }

    $scope.connectHub = function () {
        if ($scope.chatHub !== null && $scope.chatHub !== undefined)
            $scope.chatHub.server.connect($scope.name);
    }



    $scope.OnlineUsers = [];
    $scope.OUsers = [];
    $scope.chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        $scope.OnlineUsers.length = 0;
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
            if ($scope.OnlineUsers[i].UserName === userName) $scope.OnlineUsers.splice(i, 1);
        }

        $scope.$apply();
    }
    $scope.chatHub.client.onNewUserConnected = function (id, name, email,firstName,lastName,fullName) {
        for (i = $scope.OnlineUsers.length - 1; i >= 0; i--) {
            if ($scope.OnlineUsers[i].UserName === email) $scope.OnlineUsers.splice(i, 1);
        }
        $scope.OnlineUsers.push({ ConnectionId: id, UserName: name, Email: email,FirstName:firstName,LastName:lastName,FullName:fullName });
        //$scope.OnlineUsers = $filter('unique')($scope.OnlineUsers, 'Email');
        $scope.OnlineUsers = $scope.OnlineUsers;
        $scope.$apply();
        var title = email +" is online";
        DesktopNotificationsFactory(title,"")
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
    $scope.chatHub.client.messageReceived = function (userName, message, Email) {
        $scope.messages.push({ message: message, username: userName, email: Email });
        $scope.$apply();
        DesktopNotificationsFactory(Email + " says: ", message)
        $scope.groupMsg = "";
    }
    $scope.chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {
        $scope.expand = true;
        $scope.PrivateMessages.push({ to: windowId, from: fromUserName, message: message });
        $scope.$apply();
        if ($scope.$parent.UserName !== fromUserName) // otheruser's pm
        {
            if ($scope.UserInPrivateChat === null) {
                $scope.UserInPrivateChat = { name: fromUserName, ConnectionId: windowId }
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
    $scope.openInPrivate = function (UserName, Email, ConnectionId) {
      //  $scope.showPrivateBox = true;
        $scope.privateChatBoxes.push({ Uname: UserName, connectionId: ConnectionId });
        $scope.chatName = UserName;
        $scope.privateChatBoxProperties.showPrivateBox = true;
        $scope.privateChatBoxProperties.isboxopened = true;
        if( $scope.privateMessageCount.userEmail===Email)
        {
            $scope.privateMessageCount.isRead = true;
            $scope.privateMessageCount.messageCount = 0;
        }
    }
    
    $scope.closePrivateChat = function (Email, ConnectionId) {
        for (i = $scope.privateChatBoxes.length - 1; i >= 0; i--) {
            if ($scope.privateChatBoxes[i].connectionId === ConnectionId) $scope.privateChatBoxes.splice(i, 1);
        }
        $scope.privateChatBoxProperties.showPrivateBox = false;
        $scope.privateChatBoxProperties.isboxopened = false;
    }
    

    $scope.sendPrivateMessage = function (toUserConnId, toUserMailID) {
        $scope.expand = true;
        $scope.chatHub.server.sendPrivateMessage(toUserConnId, $scope.privateBoxMessage.pMessage);
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

    $scope.beginVertScroll = function(){
     
        $interval(
         function(){
             var firstElement = $('ul.container li:first');
             var hgt = firstElement.height() +
         
                 parseInt(firstElement.css("paddingTop"), 10) + parseInt(firstElement.css("paddingBottom"), 10)+
                 parseInt(firstElement.css("marginTop"), 10) + parseInt(firstElement.css("marginBottom"), 10);
        
             var cntnt = firstElement.html();
        
             $("ul.container").append("<li>" + cntnt + "</li>");
        
             ;        
        
             cntnt = "";
             firstElement.animate({
                 "marginTop" : -hgt
             }, 600, function(){
          
                 $scope.itemToremove = $(this) ;    
          
          
                 $('ul.container li').last().css({
                     "background" : $(this).css("background"),
                     "color" : $(this).css("color")
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