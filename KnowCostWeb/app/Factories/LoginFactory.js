var LoginFactory = function ($http, $q) {
    return function (userdetails) {
        var deferedObj = $q.defer();
        var UserModel = {
            email: userdetails.Email,
            password: userdetails.Password,
            rememberMe: userdetails.RememberMe,
            returnUrl: userdetails.returnUrl
        }
        $http.post('/api/Account/Login', UserModel)
        .success(function (response) {
            console.log(deferedObj);
            if (response.SignInStatus == 1) {
                deferedObj.resolve({ success: true,Id:1 });
            }
            else if (response.SignInStatus == 2) {
                deferedObj.resolve({ success: false,Id:2 });
            }
            else if (response.SignInStatus == 3) {
                deferedObj.resolve({ success: false, Id: 3 });
            }
            else if (response.SignInStatus == 0) {
                deferedObj.resolve({ success: false, Id: 0});
            }
            else {
                deferedObj.resolve({ success: false, Id: -1});
            }
        })
         .error(function () {
             deferedObj.resolve({ success: false, Id: -1});
         })
        return deferedObj.promise;
    }
}

LoginFactory.$inject=['$http','$q']