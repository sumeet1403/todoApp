(function () {
    'use strict';
    //test for grid branch

    /* Controllers */
    angularApp.controller("TodoCtrl", ['$scope', 'ToDoService',
        function ($scope, ToDoService) {
            //$scope.todos = [
            //    { text: 'learn angular', done: true },
            //    { text: 'build an angular app', done: false }];
            var getToDoTasks = function () {
                ToDoService.getTasks()
                    .then(function (response) {
                        $scope.todos = response.data;
                    }
                        , function (response) {
                        });
            }
            getToDoTasks();

            //$scope.addTodo = function () {
            //    $scope.todos.push({ text: $scope.todoText, done: false });
            //    $scope.todoText = '';
            //};

            $scope.addToDoTask = function () {
                $scope.todoTask = { Name: $scope.todoText, IsComplete: false };
                ToDoService.addTask($scope.todoTask)
                    .then(function (response) {
                        $scope.todoText = '';
                        getToDoTasks();
                    }
                        , function (response) {
                        });
            }

            $scope.updateToDoTask = function (todoTask) {
                ToDoService.updateTask(todoTask)
                    .then(function (response) {
                        getToDoTasks();
                    }
                        , function (response) {
                        });
            }

            $scope.remaining = function () {
                var count = 0;
                angular.forEach($scope.todos, function (todo) {
                    count += todo.isComplete ? 0 : 1;
                });
                return count;
            };
        }]);
})();