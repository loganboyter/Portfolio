var portfolio = {};
portfolio.wallBook = { title: "WallBook - A wallpaper social media website", desc: "WallBook is a social media website based around wallpapers that I and <a href='http://relos.org' target='_blank'>Christian Soler</a> worked on over the course of 4 days, using ASP.NET Web API, AngularJS, and SQL.", link: "http://wallbook.azurewebsites.net" };
portfolio.staffingOnline = { title: "StaffingOnline - A CRM website", desc: "StaffingOnline is a customer relations management website. This project was worked on by myself and several others on a team, of which I was one of the team leads. We used agile methodologies during the development of StaffingOnline.", link: "http://www.staffingonline.us" };
portfolio.gitHub = { title: "GitHub - Build software better, together", desc: "I also have several short CRUD apps on GitHub, using JavaScript, ASP.NET Web API, ASP.NET MVC, with various other frameworks.", link: "https://github.com/loganboyter?tab=repositories" };


portfolio.showModal = function (project) {
    document.getElementById("title").innerHTML = project.title;
    document.getElementById("desc").innerHTML = project.desc;
    document.getElementById("link").href = project.link;
    $("#projectModal").modal('show');
}

portfolio.hideModal = function () {
    $("#projectModal").modal("hide");
}

$("#menu-close").click(function (e) {
    e.preventDefault();
    $("#sidebar-wrapper").toggleClass("active");
});

// Opens the sidebar menu
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#sidebar-wrapper").toggleClass("active");
});

// Scrolls to the selected menu item on the page
$(function () {
    $('a[href*=#]:not([href=#])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') || location.hostname == this.hostname) {

            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html,body').animate({
                    scrollTop: target.offset().top
                }, 1000);
                return false;
            }
        }
    });
});




/// Starting point
var portfolioApp = angular.module('PortfolioApp', ["ngRoute"]);

/// Route to index
portfolioApp.config(function ($routeProvider, $httpProvider) {
    $routeProvider.when("/", {
        templateUrl: "/App/Views/index.html",
        controller: "IndexController"
    }).when("/Register", {
        templateUrl: "/App/Views/register.html",
        controller:"RegisterController"
    });
    $httpProvider.interceptors.push('AuthInterceptor');
})