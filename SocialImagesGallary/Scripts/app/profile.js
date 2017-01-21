// Ты создаешь глобальные переменные и функции в глобальном скоупе. Переделай в модуль или обьект
var SocialProfile = {};
SocialProfile.viewModel = {
    friends:ko.observableArray(),
    id: ko.observable(),
    userName: ko.observable(),
    name: ko.observable(),
    adress: ko.observable(),
    email: ko.observable(),
    friendHandler: function (data) {
        SocialProfile.sendAjaxRequest("POST", SocialProfile.insertFriend, Urls.addFriend, { friendName: data.UserName });
    }
};

SocialProfile.sendAjaxRequest = function (httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
SocialProfile.getProfile=function (userNam,url) {
    this.sendAjaxRequest("GET", this.insertData, url, {userName:userNam});
}
SocialProfile.insertData=function (data) {
    SocialProfile.viewModel.id(data.Id);
    SocialProfile.viewModel.userName(data.UserName);
    SocialProfile.viewModel.name(data.Name);
    SocialProfile.viewModel.email(data.Email);
    SocialProfile.viewModel.adress(data.Adress);
}
SocialProfile.insertFriend=function (friend) {
    //viewModel.friends.push(friend);
}
SocialProfile.getAllFriends=function (userNam,url) {
    this.sendAjaxRequest("POST", this.insertAllFriends, url, {userName:userNam});
}

SocialProfile.insertAllFriends=function (data) {
    SocialProfile.viewModel.friends.removeAll();
    for (var i = 0; i < data.length; i++) {
        SocialProfile.viewModel.friends.push(data[i]);
    }
    var owner = $('#accountOwner').val();
    SocialProfile.hideBtnsIfOwnPage(owner);
}

SocialProfile.hideBtnsIfOwnPage=function (accountOwner) {
    if (accountOwner === SocialProfile.viewModel.userName()) {
        $('.hiden').hide();
    }
}

SocialProfile.hideBtnsIfNotOwner=function () {
    var owner = $('#accountOwner').val();
    var userName = $('#userName').val();
    if (owner !== userName) {
        $('.nav-for-load').hide();
        $('#header').hide();
    }
}
