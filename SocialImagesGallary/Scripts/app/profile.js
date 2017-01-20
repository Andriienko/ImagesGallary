// Ты создаешь глобальные переменные и функции в глобальном скоупе. Переделай в модуль или обьект

var viewModel = {
    friends:ko.observableArray(),
    id: ko.observable(),
    userName: ko.observable(),
    name: ko.observable(),
    adress: ko.observable(),
    email: ko.observable(),
    friendHandler: function (data) {
        sendAjaxRequest("POST", insertFriend, "Friend/AddFriend", { friendName: data.UserName });
    }
};
var profileUrls = {
    getProfile: "/Profile/GetProfile",
    loadAvatar: "/Profile/UploadUserImage"
};
function sendAjaxRequest(httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
function getProfile(userNam) {
    sendAjaxRequest("GET", insertData, profileUrls.getProfile, {userName:userNam});
}
function insertData(data) {
    viewModel.id(data.Id);
    viewModel.userName(data.UserName);
    viewModel.name(data.Name);
    viewModel.email(data.Email);
    viewModel.adress(data.Adress);
}
function insertFriend(friend) {
    //viewModel.friends.push(friend);
}
function getAllFriends(userNam) {
    sendAjaxRequest("POST", insertAllFriends, "Friend/GetFriends", {userName:userNam});
}

function insertAllFriends(data) {
    viewModel.friends.removeAll();
    for (var i = 0; i < data.length; i++) {
        viewModel.friends.push(data[i]);
    }
    var owner = $('#accountOwner').val();
    hideBtnsIfOwnPage(owner);
}

function hideBtnsIfOwnPage(accountOwner) {
    console.log(accountOwner);
    if (accountOwner === viewModel.userName()) {
        $('.hiden').hide();
    }
}

function hideBtnsIfNotOwner() {
    var owner = $('#accountOwner').val();
    var userName = $('#userName').val();
    if (owner !== userName) {
        $('.nav-for-load').hide();
    }
}
