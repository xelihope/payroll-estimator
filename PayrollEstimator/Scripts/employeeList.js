var app = angular.module("app", []);

app.controller("employeesCtrl", function ($scope, $http) {
    $scope.employees = [];
    $scope.totalCost = {};

    $scope.currencyFormatter = new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD"
    });

    $scope.percentFormatter = new Intl.NumberFormat("en-US", {
        style: 'percent',
        minimumFractionDigits: 0,
        maximumFractionDigits: 0
    }); 

    $scope.openDialog = function () {
        $("#addDialog").dialog("open");
    };

    $scope.getEmployees = function () {
        $(".loadingIcon").show();
        $http.get("/api/employees")
            .then(function(res) {
                $scope.employees = res.data;
                $scope.totalCost = {
                    biWeeklyCost: {
                        pay: $scope.employees.map(x => x.BiWeeklyCost.Pay).reduce((a, b) => a + b),
                        benefitsDeduction: $scope.employees.map(x => x.BiWeeklyCost.BenefitsDeduction).reduce((a, b) => a + b),
                        dependentsBenefitsDeduction: $scope.employees.map(x => x.BiWeeklyCost.DependentsBenefitsDeduction).reduce((a, b) => a + b),
                        netCost: $scope.employees.map(x => x.BiWeeklyCost.NetCost).reduce((a, b) => a + b)
                    },
                    annualCost: {
                        pay: $scope.employees.map(x => x.AnnualCost.Pay).reduce((a, b) => a + b),
                        benefitsDeduction: $scope.employees.map(x => x.AnnualCost.BenefitsDeduction).reduce((a, b) => a + b),
                        dependentsBenefitsDeduction: $scope.employees.map(x => x.AnnualCost.DependentsBenefitsDeduction).reduce((a, b) => a + b),
                        netCost: $scope.employees.map(x => x.AnnualCost.NetCost).reduce((a, b) => a + b)
                    }
                };
                $(".employeeList").show();
                $(".loadingIcon").hide();
            });
    };

    $scope.showDependents = function ($event, employee) {
        employee.showDependents = !employee.showDependents;
        $event.target.innerText = employee.showDependents ? "v" : ">";
    };

    $scope.formatCurrency = function (value) {
        return $scope.currencyFormatter.format(value);
    };

    $scope.formatPercent = function(value) {
        return $scope.percentFormatter.format(value);
    }

    $scope.getEmployees();
});