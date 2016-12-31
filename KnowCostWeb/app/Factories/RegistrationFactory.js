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
            CountryID: userDetails.country
        })
        .success(function () {
            if (data == "True") {
                deferredObject.resolve({ success: true });
            } else {
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
