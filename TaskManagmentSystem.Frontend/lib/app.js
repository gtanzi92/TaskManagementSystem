(function(angular) {
    'use strict';
    var ngApp = angular.module('taskManagementApp', []);
    

    ngApp.controller('taskController', function ($scope, $http) {
        $scope.description = "";
        $scope.isWork = false;
        $scope.isLeasure = false;
        $scope.isHome = false;

        $scope.tasks = null;

        $scope.search = function search() {
            $http({
                method: 'GET',
                url: 'http://localhost:5281/api/tasks/search3?description=' + $scope.description + '&isWork=' + $scope.isWork + '&isLeasure=' + $scope.isLeasure + '&isHome=' + $scope.isHome
              }).then(function successCallback(response) {
                    //$scope.tasks = JSON.parse(response);
                    console.log(response.data);
                    $scope.tasks = response.data;
                }, function errorCallback(response) {
                    window.alert('Please check the called URL');
                });
        };        
    });

})(window.angular);

