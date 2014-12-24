(function () {
    'use strict';

    angular
        .module('app')
        .factory('validationService', validationService);

    validationService.$inject = ['$http', '$q'];

    function validationService($http, $q) {
        var service = {
            getValidations: getValidations
        };

        return service;

        function getValidations(dtoObjectName, jsonObjectName) {
            var httpObject = {
                method: 'GET',
                url: 'http://localhost:52931/api/Validation/GetValidations',
                params: {
                    dtoObjectName: dtoObjectName,
                    jsonObjectName: jsonObjectName
                }
            };

            return $http(httpObject);
        }
    };

})();