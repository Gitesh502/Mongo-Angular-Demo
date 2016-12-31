var RegistrationFactory=function($http,$q)
{
    return function (userDetails) {
        var defferedObject = $q.defer();

        $http.post("/api/Account/Register", {
            FirstName: userDetails.firstName,
            LastName: userDetails.lastName,
            Email: userDetails.email,
            Password: userDetails.password,
            ConfirmPassword: userDetails.confirmPassword,
            StateID: userDetails.state,
            CountryID: userDetails.country,
            sendEmail: userDetails.sendEmail
        })
        .success(function (response) {
            console.log(deferredObject);
            if (response.SignInStatus == 1) {
                deferredObject.resolve({ success: true });
            }
            else if(response.SignInStatus==2)
            {
                deferredObject.resolve({ success: false });
            }
            else if (response.SignInStatus == 3) {
                deferredObject.resolve({ success: false });
            }
            else if (response.SignInStatus == 0) {
                deferredObject.resolve({ success: false });
            }
            else {
                deferredObject.resolve({ success: false });
            }
        })
        .error(function () {
            deferredObject.resolve({ success: false });
        })
        return deferredObject.promise;

    }
}

RegistrationFactory.$inject = ['$http', '$q'];
