(function () {
    'use strict';

    angular
        .module('app')
        .controller('directoriesController', directoriesController);

    directoriesController.$inject = ['$scope', '$http'];

    function directoriesController($scope, $http) {
        $scope.title = 'directoriesController';

        $http.get('api/Values').success(function (data, status, headers, config) {
            $scope.Directories = data;
        }).error(function (data, status, headers, config) { alert("Error" + status) });


        $scope.getDir = function (key) {

            $http.get('api/Values/' + key).success(function (data, status, headers, config) {
                $scope.Directories = data;
            }).error(function (data, status, headers, config) { alert("Error" + status) });
        };

    }
})();
