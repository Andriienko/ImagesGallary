var SocialUsers = {};
SocialUsers.viewModel = {
    users: ko.observableArray(),
    profileHandler: function(data) {
        SocialUsers.sendAjaxRequest("POST", SocialUsers.reloadPage, Urls.loadUserProfile, { userNam: data.UserName });
    },
    galleryHandler: function(data) {
        SocialUsers.sendAjaxRequest("POST", SocialUsers.reloadPage, Urls.loadUserGallery, { userNam: data.UserName });
    },
    friendHandler: function (data) {
        SocialUsers.sendAjaxRequest("POST", function (data) { }, Urls.addFriend, { friendName: data.UserName });
    }
};

SocialUsers.sendAjaxRequest = function (httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
SocialUsers.getAllUsers = function (userNam,url) {
    this.sendAjaxRequest("GET", this.insertData,url, { userName: userNam });
}

SocialUsers.insertData = function (data) {
    SocialUsers.viewModel.users.removeAll();
    for (var i = 0; i < data.length; i++) {
        SocialUsers.viewModel.users.push(data[i]);
    }
}

SocialUsers.reloadPage = function (response) {
    window.location=response.url;
}


