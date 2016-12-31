var RegistrationController = function ($scope, RegistrationFactory) {
    $scope.userDetails = {
        firstName: "",
        lastName: "",
        email: "",
        state: 0,
        country: 0,
        password: "",
        confirmPassword: "",
        registrationFailure: false
    };

    $scope.register = function () {
        var result = RegistrationFactory($scope.userDetails);
        result.then(function (result) {
            if (result.success) {
                console.log('S')

            } else {
                $scope.userDetails.registrationFailure = true;
            }
        });
    }
}
RegistrationController.$inject = ['$scope', 'RegistrationFactory'];