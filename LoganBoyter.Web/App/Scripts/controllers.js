portfolioApp.controller("IndexController", function ($scope, $http, $filter, LoginService, $location, $timeout) {
    $scope.skills = [];
    $scope.currentPage = 1;
    $scope.searchText = "";
    $scope.reverse = null;
    if (LoginService.checkLoggedIn()) {
        $http.get("api/Account/UserInfo").success(function (data) {
            $scope.userName = data.Email;
            console.log(data.Email);
        }).error(function () { });
    }
    $scope.button = { prev: "disabled", next: "enabled" }
    $scope.getSkills = function () {
        $http.get("api/v1/Skill").success(function (data) {
            $scope.skills = data;
            $scope.$watch("currentPage", function () {
                if ($scope.currentPage === 1) $scope.button.prev = "disabled";
                else $scope.button.prev = "enabled";
                if ($scope.currentPage * 4 < $scope.skills.length) $scope.button.next = "enabled";
                else $scope.button.next = "disabled";
                var begin = (($scope.currentPage - 1) * 4);
                console.log("currentPage changed")
                $scope.pagedSkills = $scope.filteredSkills.slice(begin, begin + 4);
            })
            $scope.filteredSkills = $scope.skills;
            $scope.pagedSkills = $scope.skills.slice(0, 4)
        }).error(function () { console.log("Error on skill get: " + this.response) });
    }
    $scope.getSkills();

    $('#commentModal').on('show.bs.modal', function (e) {
        if ($scope.userName == "Admin") {
            $http.get("api/v1/Comment").success(function (data) { $scope.comments=data }).error(function () { });
        }
    })

    $scope.saveSkill = function () {
        $http.post("api/v1/Skill", $scope.newSkill).success(function (data) {
            $scope.getSkills();
            $scope.newSkill = {};
            console.log(data);
        }).error(function () { console.log("Error on post skill: " + this.response) });
        $("#addSkillModal").modal("hide");
    }

    $scope.deleteSkill = function (skill) {
        $http.delete("api/v1/Skill", { data: skill }).success(function () {
            $scope.getSkills();
            console.log("Skill delete successful");
        }).error(function () { console.log("Error on skill delete: " + this.response) })
    }

    $scope.saveComment = function () {
        if (!$scope.comment.Title || !$scope.comment.Body || !LoginService.checkLoggedIn()) {
            alert("Please be sure to fill out all fields and log in!");
            return;
        }
        $("#commentModal").modal("hide");
        $http.post("api/v1/Comment", $scope.comment).success(function () {
            $scope.comment = {};
            alert("Comment posted successfully!");
        }).error(function () { })
        $scope.comment = {}
    }

    $scope.$watch("searchText", function () {
        $scope.filteredSkills = $filter("filter")($scope.skills, $scope.searchText);
        if ($scope.currentPage !== 1) $scope.currentPage = 1;
        $scope.pagedSkills = $scope.filteredSkills.slice(0, 4);
    })

    $scope.$watch("reverse", function () {
        $scope.filteredSkills = $filter("orderBy")($scope.skills, "Title", $scope.reverse)
        if ($scope.currentPage !== 1) $scope.currentPage = 1;
        $scope.pagedSkills = $scope.filteredSkills.slice(0, 4);
    })

    $scope.registerPage = function () {
        $timeout(function () { $location.path('/Register'); }, 1000);
        $('#loginModal').modal('hide');
    }

    $scope.login = function () {
        $("#loginModal").modal("hide");
        LoginService.processLogin($scope.user.UserName, $scope.user.Password)
            .then(function () {
                $scope.userName = $scope.user.UserName;
                $scope.user = {};
            }, function (status) {
                $scope.token = status;
            });
    }

})


portfolioApp.controller('RegisterController', function ($scope, $http, $location, LoginService, $q) {
    $scope.profile = {};
    // make sure not to let them register unless their email is a valid email, or else it will create their profile but not create a new user in AspNetUsers
    $scope.register = function () {
        var errorMessage = "";
        var checkFields = function () {
            var fieldsCompleted = false;
            if ($scope.profile.Email != undefined && $scope.profile.Password != undefined && $scope.profile.ConfirmPassword != undefined) {
                fieldsCompleted = true;
                if ($scope.profile.Password != $scope.profile.ConfirmPassword) {
                    fieldsCompleted = false;
                    errorMessage = 'Password did not match';
                }
                return fieldsCompleted;
            }
        }
        if (checkFields()) {
            $http.post('api/account/register', $scope.profile).success(function () {
                alert("Account successfully created!");
                if (checkFields()) {
                    LoginService.processLogin($scope.profile.Email, $scope.profile.Password)
                    .then(function () {
                        $location.path('/');
                    }, function (status) {
                        $scope.token = status;
                    });
                } else {
                    alert(errorMessage);
                }
            });
        }
    }
});