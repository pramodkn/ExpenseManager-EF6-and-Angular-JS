'use strict';
/**
 * @ngdoc overview
 * @name amoApp
 * @description
 * # amoApp
 *
 * Main module of the application.
 */
angular.module('amoApp', [
    'oc.lazyLoad'
    , 'ui.router'
    , 'ui.bootstrap'
    , 'chart.js'
    , 'angular-loading-bar'
  , ]).config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        debug: false
        , events: true
    , });
    $urlRouterProvider.otherwise('/dashboard');
    $stateProvider.state('dashboard', {
        url: '/dashboard'
        , templateUrl: 'views/dashboard/main.html'
        , resolve: {
            loadMyDirectives: function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: 'amoApp'
                    , files: [
                         , 'scripts/controllers/main.ctrl.js'
                         , 'scripts/controllers/main.fc.js'
                   , 'scripts/directives/header/header.js'
]
                })
            }
        }
    })
  }]);