portfolioApp.factory('AuthInterceptor', function ($window) {
    return {
        request: function (config) {
            config.headers = config.headers || {};

            if ($window.sessionStorage.getItem('token')) {
                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.getItem('token');
            }

            return config;
        }
    }
});

portfolioApp.factory('LoginService', function ($q, $http, SessionHandler, $window) {
    var isLoggedIn = false;
    var proccessLogin = function (username, password) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            data: 'username=' + username + '&password=' + password + '&grant_type=password',
            async: false,
            header: { 'Content-Type': 'Application/x-www-form-urlencoded' },
            url: '/Token',
            isArray: false
        }).success(function (data) {
            SessionHandler.setLoggedInToken(data.access_token);
            isLoggedIn = true;
            deferred.resolve();
        }).error(function (data, status) {
            SessionHandler.removeLoggedInToken();
            deferred.reject(status);
        });

        return deferred.promise;
    }

    var checkLoggedIn = function () {
        if ($window.sessionStorage.getItem('token')) isLoggedIn = true;
        else isLoggedIn = false;
        return isLoggedIn;
    }

    var processLogout = function () {
        var deferred = $q.defer();

        $http({
            method: 'POST',
            url: '/api/account/logout'
        }).success(function () {
            SessionHandler.logout();
            isLoggedIn = false;
            deferred.resolve;
        }).error(function (data, status) {
            return deferred.reject(status);
        })

        return deferred.promise;
    }

    return {
        checkLoggedIn: checkLoggedIn,
        processLogin: proccessLogin,
        processLogout: processLogout
    };
});

portfolioApp.factory('SessionHandler', function ($window) {
    var setLoggedInToken = function (token) {
        $window.sessionStorage.setItem('token', token);
    };
    var removeLoggedInToken = function () {
        $window.sessionStorage.removeItem('token');
    };
    var logout = function () {
        removeLoggedInToken();
    };
    return {
        setLoggedInToken: setLoggedInToken,
        removeLoggedInToken: removeLoggedInToken,
        logout: logout
    };
});