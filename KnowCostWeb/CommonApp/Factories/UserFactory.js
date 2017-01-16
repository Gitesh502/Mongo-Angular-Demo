var UserFactory = function ($http, $q) {
    var getUserByEmail = function (email) {
        var deferedObj = $q.defer();
        $http({
            url: '/api/User/GetUserByEmail',
            method: 'GET',
            params:{email:email}
        })
        .success(function (response) {
           deferedObj.resolve({ success: true, Id: 1, response: response });
        })
        .error(function (msg) {
            deferedObj.reject(msg);
        })
        return deferedObj.promise;
    };
    var getRegisteredUsers=function()
    {
        var deferedObj = $q.defer();
        $http({
            url: '/api/User/GetRegisteredUsers',
            method: 'GET'
        })
        .success(function (response) {
        if(response.isValid)
            deferedObj.resolve({ success: true, Id: 1, response: response });
        else
            deferedObj.resolve({ success: true, Id: 0, response: response });
        })
        .error(function (msg) {
            deferedObj.reject(msg);
        })
        return deferedObj.promise;
    }
    var publicAPI = {
        GetUserByEmail: getUserByEmail,
        GetRegisteredUsers: getRegisteredUsers
    };
    return publicAPI;
}

UserFactory.$inject=['$http','$q']