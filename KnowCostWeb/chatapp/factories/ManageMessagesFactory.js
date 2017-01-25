function ManageMessages($http, $q) {
    var ManageMessages = {};

    ManageMessages.getMessagesByFromUserId = function (fromUserId) {
        var deferedObj = $q.defer();
        $http.get('/api/UserMessage/GetMessagesByUserId', fromUserId)
        .success(function (response) {
            deferedObj.resolve({ success: true, Id: 1, response: response });
        })
        .error(function () {
            deferedObj.resolve({ success: false, Id: -1 });
        });
        return deferedObj.promise;
    }
    return ManageMessages;
}
ManageMessages.$inject = ['$http', '$q']