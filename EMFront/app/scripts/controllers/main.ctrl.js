'use strict';
angular.module('amoApp').controller('ExpenseCtrl', function ($scope, $state, $filter, dataFactory) {
    $scope.format = 'yyyy-MM-dd';
    $scope.openFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedFrom = true;
    };
    $scope.pAvailableFrom = "";
    $scope.selectionOption = [];
    $scope.selectedOption = $scope.selectionOption[-1];
    $scope.pagingInfo = {
        skip: 0
        , take: 5
        , sortBy: 'Id'
        , isAscending: false
        , search: ''
        , totalItems: 0
    };
    $scope.search = function () {
        $scope.pagingInfo.skip = 0;
        loadExpenseGrid();
    };
    $scope.sort = function (sortBy) {
        if (sortBy === $scope.pagingInfo.sortBy) {
            $scope.pagingInfo.isAscending = !$scope.pagingInfo.isAscending;
        }
        else {
            $scope.pagingInfo.sortBy = sortBy;
            $scope.pagingInfo.isAscending = false;
        }
        $scope.pagingInfo.skip = 0;
        loadExpenseGrid();
    };
    $scope.selectPage = function (page) {
        $scope.pagingInfo.skip = ($scope.pagingInfo.skip - 1) * $scope.pagingInfo.take;
        loadExpenseGrid();
    };
    var getExpenseCategories = function () {
        dataFactory.getExpenseCategories().then(function (response) {
            $scope.selectionOption = response.data;
        }, function (error) {
            $scope.selectedOption = $scope.selectionOption[-1];
        });
    }
    getExpenseCategories();
    var loadExpenseGrid = function () {
        dataFactory.getExpensePaged($scope.pagingInfo).then(function (response) {
            $scope.expensesData = response.data.Results;
            $scope.pagingInfo.totalItems = response.data.RowCount;
            $scope.pagingInfo.pageCount = response.data.PageCount;
        }, function (error) {});
    }
    loadExpenseGrid();
    var getExpenseOnCategories = function () {
        dataFactory.getExpenseOnCategories().then(function (response) {
            $scope.categoryNameLabels = response.data.map(function (a) {
                return a.CategoryName;
            });
            $scope.categoryNameTotalAmountLabel = response.data.map(function (a) {
                return a.TotalAmount;
            });
        }, function (error) {
            $scope.status = 'Unable to load customer data: ' + error.message;
        });
    }
    getExpenseOnCategories();
    var getExpenseDayWise = function () {
        dataFactory.getExpense().then(function (response) {
            $scope.labels = response.data.map(function (a) {
                return $filter('date')(a.OnDate, 'EEE, MMM d, y');
            });
            $scope.data = response.data.map(function (a) {
                return a.Amount;
            });
        }, function (error) {});
    }
    getExpenseDayWise();
    $scope.expense = {};
    $scope.saveExpense = function () {
        $scope.expense.AddDate = $filter('date')($scope.expense.AddDate, 'yyyy-MM-dd');
        dataFactory.saveExpense($scope.expense).then(function (response) {
            getExpenseDayWise();
            loadExpenseGrid();
            getExpenseOnCategories();
        }, function (error) {});
    }
    $scope.series = ['Series Expenses'];
    $scope.options = {
        legend: {
            display: false
        }
        , scales: {
            yAxes: [{
                scaleLabel: {
                    display: true
                    , labelString: 'Amount'
                }
            }]
            , xAxes: [{
                scaleLabel: {
                    display: true
                    , labelString: 'Date'
                }
            }]
        }
    };
    $scope.categoryOptions = {
        legend: {
            display: true
            , position: 'right'
        }
        , tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var dataset = data.datasets[tooltipItem.datasetIndex];
                    var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {
                        return previousValue + currentValue;
                    });
                    var currentValue = dataset.data[tooltipItem.index];
                    var precentage = Math.floor(((currentValue / total) * 100) + 0.5);
                    return precentage + "% spend on " + data.labels[tooltipItem.index];
                }
            }
        }
    }
});