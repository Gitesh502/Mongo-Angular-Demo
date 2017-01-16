var GetUserDetailsFactroy = function ($http, $q) {
    return function (email,userId) {
        var deferedObj = $q.defer();
        var UserModel = {
            email: email,
            id: userId
        }
        $http.get('/api/User/GetUserByEmail', email)
        .success(function (response) {
            console.log(deferedObj);
            deferedObj.resolve({ success: true, Id: 1,response:response});
        })
         .error(function () {
             deferedObj.resolve({ success: false, Id: -1 });
         })
        return deferedObj.promise;
    }
}
GetUserDetailsFactroy.$inject=['$http','$q']