'use strict';
angular.module('amoApp').factory('dataFactory', ['$http', function ($http) {
    var urlBase = 'http://localhost:56811/api/';
    var dataFactory = {};
    dataFactory.getExpenseOnCategories = function () {
        return $http.get(urlBase + 'expensesOnCategory');
    };
    dataFactory.getExpense = function (id) {
        return $http.get(urlBase + 'expenses');
    };
    dataFactory.getExpensePaged = function (pagingInfo) {
        return $http.get(urlBase + 'expenses/paged', {
            params: pagingInfo
        });
    };
    dataFactory.getExpenseCategories = function (id) {
        return $http.get(urlBase + 'expenseCategories');
    };
    dataFactory.saveExpense = function (d) {
        return $http.post(urlBase + 'expenseSave', d);
    };
    return dataFactory;
}]);