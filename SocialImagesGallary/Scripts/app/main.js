﻿//функция document.ready должна быть в теге script в html файле
$(document).ready(function () {
    ko.applyBindings(viewModel);
    var userName = $('#userName').val();
    getAllUsers(userName);
});
//такие штуки тоже должны быть в html файле. Нужно юзать razor для их генерации
var apiUserUrls = {
    common: "/Admin/",
    getAllUsers: "/api/User/",
    postUser: "/Admin/Create",
    deleteUser: "/Admin/Delete"
};
var viewModel = {
    users: ko.observableArray(),
    profileHandler: function(data) {
        sendAjaxRequest("POST", reloadPage, "Profile/LoadUsersProfile", { userNam: data.UserName });
    },
    galleryHandler: function(data) {
        sendAjaxRequest("POST", reloadPage, "Image/LoadUsersGallery", { userNam: data.UserName });
    },
    friendHandler: function (data) {
        sendAjaxRequest("POST", function(data) {}, "Friend/AddFriend", { friendName: data.UserName });
    }
};

function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getAllUsers(userNam) {
    sendAjaxRequest("GET", insertData, apiUserUrls.getAllUsers, {userName:userNam});
}

function insertData(data) {
    viewModel.users.removeAll();
    for (var i = 0; i < data.length; i++) {
        viewModel.users.push(data[i]);
    }
}

function reloadPage(response) {
    window.location=response.url;
}


