function ManageMessagesFactory($http, $q) {
    var ManageMessages = {};

    ManageMessages.getMessagesByFromUserId = function (toUserId) {
        var deferedObj = $q.defer();
        $http.get('/api/UserMessage/GetMessagesByUserId?toUserId=texzt')
        .success(function (response) {
            console.log(response)
            deferedObj.resolve({ success: true, Id: 1, response: response });
        })
        .error(function () {
            deferedObj.resolve({ success: false, Id: -1 });
        });
        return deferedObj.promise;
    }
    return ManageMessages;
}
ManageMessagesFactory.$inject = ['$http', '$q']