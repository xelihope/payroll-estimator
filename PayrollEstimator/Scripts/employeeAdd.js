angular.module("app")
    .controller("employeeAddCtrl", function ($scope, $http) {
        $scope.formData = {
            Dependents: []
        };

        $scope.cancel = function () {
            $scope.formData = {
                Dependents: []
            };
            $("#addDialog").dialog("close");
        };

        $scope.save = function () {
            $http.put("/api/employee", $scope.formData)
                .then(function (res) {
                    window.location.href = "/";
                }, function (res) {
                    alert(res.data.Message);
                });
        };

        $scope.addDependent = function() {
            $scope.formData.Dependents.push({});
        };

        $("#addDialog").dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            title: "Add Employee",
            height: 700,
            width: 450,
            close: $scope.cancel
        });
    });