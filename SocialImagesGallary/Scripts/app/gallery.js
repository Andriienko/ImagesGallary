var GalleryObject = { };
GalleryObject.viewModel = {
    messages: ko.observableArray()
};
GalleryObject.sendAjaxRequest=function (httpMethod, calback, url, reqData) {
    $.ajax(url, { type: httpMethod, typeData: "JSON", success: calback, data: reqData });
}
GalleryObject.getImagePartial=function (userNik, index,url) {
    var ob = {
        userName: userNik,
        id:index
    };
    this.sendAjaxRequest("GET", this.insertData,url, ob);
}
GalleryObject.getAllMessages = function (id,url) {
    this.sendAjaxRequest("GET", this.insertMsgs, url, { imgId: id });
}
GalleryObject.insertData=function (data) {
    var target = $('#partial');
    target.empty();
    target.append(data);
    var imgId = $('#imgId').val();
    var count = $('#count').val();
    console.log(count);
    if(count!==0)
        GalleryObject.getAllMessages(imgId,Urls.getAllMessages);
}

GalleryObject.addMessage=function (msgDto,url) {
    this.sendAjaxRequest("POST", function(data) {
        GalleryObject.viewModel.messages.push(data);
    }, url, {newMessage: msgDto });
}
GalleryObject.insertMsgs=function (data) {
    GalleryObject.viewModel.messages.removeAll();
    for (var i = 0; i < data.length; i++) {
        GalleryObject.viewModel.messages.push(data[i]);
    }
}

GalleryObject.isFriends=function (flag) {
    var countVal = $('#count').val();
    var count = parseInt(countVal);
    if (flag === 0) {
        $('.typing').hide();
        $('.coments').hide();
    }
    if (count === 0) {
        $('.typing').hide();
        $('.coments').hide();
        $('.arrows').hide();
    }
}
GalleryObject.hideBtnsIfNotOwner=function () {
    var owner = $('#accountOwner').val();
    var userName = $('#userName').val();
    if (owner !== userName) {
        $('#addPhoto').hide();
    }
}
GalleryObject.addPhoto = function (data, urla) {
    $.ajax({
                url: urla,
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function () {
                    var countVal = $('#count').val();
                    var count = parseInt(countVal);
                    $('#count').val(count + 1);
                }
            });
}
