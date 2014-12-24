(function () {
    'use strict';

    angular
        .module('app')
        .controller('aatestController', aatestController);

    aatestController.$inject = ['$scope', 'validationData'];

    function aatestController($scope, validationData) {
        activate();

        function activate() {
            $scope.model = {};
            $scope.formconfig = angular.isUndefined(validationData) ? undefined : validationData.data;
        }
    }
})();
