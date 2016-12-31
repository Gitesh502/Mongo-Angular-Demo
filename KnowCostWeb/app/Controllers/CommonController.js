var CommonController=function($scope,$window)
{
    $scope.RedirectToHome=function()
    {
        $window.location.href = "/Home";
    }
}
CommonController.$inject = ['$scope', '$window']