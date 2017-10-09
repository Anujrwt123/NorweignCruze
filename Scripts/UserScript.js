

$(document).on('click','#editPhoto',function(){

    var image=$('#ImagePath').val();
    debugger;
    var htmlString = '<div class="Upload-img">' +
        '<div class="row">' +
            '<span id="close" class="glyphicon glyphicon-remove pull-right remove" ></span>' +
        '</div>' +
        '<div class="row">' +
    '<div class="col-md-4 col-md-offset-4">';
    if (image!=null && image!="")
    {
        htmlString += '<img src="' + image + '" class="img-circle fullWidth"/>';
    }
    else
    {
        htmlString += '<img src="/Images/avatar.png" class="img-circle fullWidth"/>';
    }
    
    htmlString+='</div>'+
'</div>'+
'<hr />'+
'<div class="row">'+
    '<div class="col-md-8">'+
        '<input type="file" id="imgFile" class="form-control file-upload"/>'+
    '</div>'+
    '<div class="col-md-4">'+
       '<input type="button" id="btnUpdatePhoto" class="uploadImage" value="Upload"/>'+
    '</div>'+
'</div>'+
'</div>';
    
    $('#Datacontainer1').html(htmlString);
    $('#overLay1').css('display', 'block');
});

function HideOverLay()
{
    $('#overLay1').css('display', 'none');
}
$(document).on('click', '#close', function () {

    HideOverLay()
});

$(document).on('click', '#btnUpdatePhoto', function () {

    debugger;
    var id = $('#ID').val();
    if (id == "")
    {
        id=0
    }
    var file = $('#imgFile').get(0).files;
    var formdata = new FormData();
    if(file.length>0)
    {
        formdata.append('image', file[0]);
    }
    var extarr = ['JPG', 'PNG', 'GIF', 'JPEG', 'BMP'];
    var name= file[0].name;
    var ext = name.substr(name.indexOf('.') + 1, (name.length - name.indexOf('.')));
    var idx = extarr.indexOf(ext.toUpperCase());
    if (idx > 0)
    {

        $.ajax({
            type: "POST",
            url: '/UserHome/UploadImages?id='+id,
            contentType: false,
            processData: false,
            data: formdata,
            success: function (result) {
                if (result != "-1") 
                {
                    $('#profileImage').attr('src', result);
                    sessionStorage.setItem('image', result);
                    $('#ImagePath').val(result);

                }
                else 
                {
                    alert('some error occured try again after sometime.');
                }

                HideOverLay();

            },
            error: function (xhr, status, p3, p4) {
                //var err = "Error " + " " + status + " " + p3 + " " + p4;
                //if (xhr.responseText && xhr.responseText[0] == "{")
                //    err = JSON.parse(xhr.responseText).Message;
                //console.log(err);
                alert(err.statusText);

            }
        });
    }
    else
    {
        alert('file with this extenstion is not allowed. you can uplaod with these extensions.(.jpg,.jpeg,.png,.gif)');
    }
    // file[0].fileName



    

});

$(document).ready(function () {

    var result = sessionStorage.getItem('image');
    if(result!=null)
    {
        $('#ImagePath').val(result);
        $('#profileImage').attr('src', result);
    }

});


$(document).on('change', '#CategoryID', function () {
    debugger;
    var htmlString = '';
    var id = $(this).val();
    if (id == "")
    {
        $('#ulPosition').html(htmlString);
        return;
    }
    $.ajax({
        url: "/UserHome/GetPositions?ID=" + id,
        type: "GET",
        success: function (data) {

            if (data != null) {
                
                
                if (data.length > 0) {
                    for (i = 0; i < data.length; i++) {
                        htmlString += '<li><input type="checkbox" name="' + data[i].ID + '" /><span class="position-list-name">' + data[i].Name + '</span></li>';
                    }
                    $('#ulPosition').html(htmlString);
                }
            }
        },
        error: function () {

        }

    });

});


function Check()
{
    debugger;
    var list = "";
    $('#ulPosition li').each(function (idx, val) {
        debugger;
        if(val.children[0].checked)
        {
            list += val.children[0].getAttribute('name')+",";
        }                    
    });

    var finallist = list.substr(0, list.lastIndexOf(','));

    $('#PositionList').val(finallist);

}


$(document).on('change', '#ViewShips', function () {
    debugger;
    var id = $(this).val();
    var htmlString = "";
    if(id=="")
    {
        $('#ulShows').html(htmlString);
        return;
    }

    $.ajax({
        url: "/UserHome/GetShows?ShipID=" + id,
        type: "GET",
        success: function (data) {

            if(data!=null)
            {
                for(i=0;i<data.length;i++)
                {
                    htmlString += "<li>" + data[i].Name + "</li>";
                }
                $('#ulShows').html(htmlString);
            }
        },
        error: function (err) {

            alert(err.statusText);
        }


    });

});

