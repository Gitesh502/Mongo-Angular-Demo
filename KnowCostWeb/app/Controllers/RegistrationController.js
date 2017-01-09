var RegistrationController = function ($scope, RegistrationFactory, $controller,$window) {
    $controller('CommonController', {$scope:$scope,$window: $window})
    $scope.userDetails = {
        firstName: "",
        lastName: "",
        email: "",
        state: 0,
        country: 0,
        password: "",
        confirmPassword: "",
        sendEmail:true,
        registrationFailure: false
    };

    $scope.register = function () {
        var result = RegistrationFactory($scope.userDetails);
        result.then(function (result) {
            console.log(result)
            if (result.success) {
                toaster.pop('info', "Success", "Registration Successfull");

            } else {
                $scope.userDetails.registrationFailure = true;
                toaster.pop({ type: 'error', title: "Error", body: "Error Occured" });
            }
        });
    }
}
RegistrationController.$inject = ['$scope', 'RegistrationFactory', '$controller', '$window'];