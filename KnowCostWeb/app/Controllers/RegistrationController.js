var RegistrationController = function ($scope, RegistrationFactory, $controller, $window) {
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
                var notify = {
                    type: 'success',
                    title: 'success',
                    content: 'Registration Successfull!',
                    timeout: 5000 //time in ms
                };
                $scope.$emit('notify', notify);
                $window.location.href = '/Chat/Home/Index';

            } else {
                $scope.userDetails.registrationFailure = true;
                var notify = {
                    type: 'error',
                    title: 'Error',
                    content: 'Error Occured',
                    timeout: 5000 //time in ms
                };
                $scope.$emit('notify', notify);
              
            }
        });
    }
}
RegistrationController.$inject = ['$scope', 'RegistrationFactory', '$controller', '$window'];