!function(){"use strict";angular.module("app",[])}(),function(){"use strict";function a(a,b){a.title="directoriesController",b.get("api/Values").success(function(b,c,d,e){a.Directories=b}).error(function(a,b,c,d){alert("Error"+b)}),a.getDir=function(c){b.get("api/Values/"+c).success(function(b,c,d,e){a.Directories=b}).error(function(a,b,c,d){alert("Error"+b)})}}angular.module("app").controller("directoriesController",a),a.$inject=["$scope","$http"]}();