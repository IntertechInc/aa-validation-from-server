(function () {
    'use strict';

    angular.module('app', [
        'ui.router',
        'ui.bootstrap',
        'aa.formExtensions',
        'aa.formExternalConfiguration',
        'aa.notify',
        'aa.select2'
    ])
    .config(['$stateProvider', function ($stateProvider) {
        $stateProvider.state('aatest', {
            url: ''
            , resolve: {
                validationService: 'validationService',
                validationData: function (validationService) {
                    var valData = validationService.getValidations('ValidationTest', 'model');
                    return valData;
                }
            }
            , views: {
                'content@': {
                    templateUrl: 'aatest.html'
                    , controller: 'aatestController'
                }
            }
        });
    }]);
})();