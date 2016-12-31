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


            } else {
                $scope.userDetails.registrationFailure = true;
            }
        });
    }
}
RegistrationController.$inject = ['$scope', 'RegistrationFactory', '$controller', '$window'];