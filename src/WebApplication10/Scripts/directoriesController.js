(function () {
    'use strict';

    angular
        .module('app')
        .controller('directoriesController', directoriesController);

    directoriesController.$inject = ['$scope', '$http', '$cacheFactory'];

    function directoriesController($scope, $http, $cacheFactory) {
        $scope.title = 'directoriesController';

        //$scope.cache = $cashFactory('filesCash');


        $http.get('api/Values').success(function (data, status, headers, config) {
            $scope.Directories = data;
        }).error(function (data, status, headers, config) { alert("Error" + status) });


        $scope.getDir = function (key) {
          //  if (angular.isUndefined($scope.cache.get(key)))
            $http.get('api/Values/' + key).success(function (data, status, headers, config) {
                //$scope.cache.put(data.CurrentDir, data.Files);
                
                $scope.Directories = data;
            }).error(function (data, status, headers, config) { alert("Error" + status) });
        };

    }
})();
