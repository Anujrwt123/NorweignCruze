// Function To Delete values
function showLoadingDiv() {
    $('#loadingDiv').css('display', 'block');
}

function hideOverLay()
{
    $('#overLay1').css('display', 'none');
}

function showOverLay() {
    $('#overLay1').css('display', 'block');
}
$(document).on('click','#close', function () {
    hideLoadingDiv();
    hideOverLay()
});

function hideLoadingDiv() {
    $('#loadingDiv').css('display', 'none');
}

function finalDelete(id,name,url,option)
{
    //debugger;   
    var result = confirm('You are about to delete' + name + '. If yes click ok.')
    
    if (result)
    {
        var data;
        if (option != undefined)
        {
            url = getUrl(url);
            url += "&option=" + option;
        }
        else
        {
            url = getUrl(url);
        }
        //url = getUrl(url);
        $.ajax({            
            url: url,
            method: "GET",
            data: data,
            success: function (data) {
               //debugger;
                data = JSON.parse(data);
                if (data.Result == "Done")
                {
                    window.location.href = data.Data;
                    //$("#dataContainer").load(data.Data);
                }             

            },
            error: function () {

            }
        });
    }
}


function getBookingHistory(opt,startDate,endDate)
{

    
    var sDate = startDate;
    var eDate = endDate;
    console.log(sDate + "  0------" + eDate);
    $('#loading').show();
    $('#historyContainer').show();
    ////debugger;
    if (sDate != null && eDate != null)
    {
        sdate = formatDate(sDate);
        eDate = formatDate(eDate);
    }

    if (sDate != 'Invalid Date' && eDate != 'Invalid Date')
    {
        $('#data').load('/CabinBookings/GetBooking?From=' + sDate + '&To=' + eDate+'&option='+opt, function () { $('#loading').hide(); });
    }
    

    //$.ajax({
    //    url: Url,
    //    method: "POST",
    //    data: { ID: ID },
    //    success: function (data) {

    //        ////debugger;
    //        var data = JSON.parse(data);
    //        if (data.Result == "Done") {
    //            $("#dataContainer").load(data.Data);
    //        }


    //    },
    //    error: function () {

    //    }
    //});
}





function goToCreate(url) {

    debugger;    
    newurl=getUrl(url);
    window.location.href = newurl;
}


function getUrl(url)
{
    var cururl = window.location.href;
    var comidx = cururl.split('/');
    var returnUrl = "";
    for (i = 3; i < comidx.length; i++) {
        if (comidx[i] != "#") {
            returnUrl += "/" + comidx[i];
        }

    }
    var newurl = "";
    if (url.indexOf('?') > 0) {
        newurl = url + "&returnUrl=" + returnUrl;;
    }
    else {
        newurl = url + "?returnUrl=" + returnUrl;
    }
    return newurl;
}

function goToPrevious(url) {
    debugger;
    //console.log(getURL());
  //  var txt = decodeURI(url);
    //url =  txt;
    //window.location.assign("http://localhost:53676/")
// ReSharper disable once CoercedEqualsUsing
    if (url == "") {
        window.location.href = "/";
    }
    else {
        window.location.href =url;
    }

    //location.href='../'+txt;
}


function AddPerson()
{
    //debugger;
    $.ajax({
        url: '/Categories/SelectCategories',
        success: function (data) {
           //debugger;
            if (data != null) {
                var selectdata = "<option value='-1'>select--</option>";
                for (i = 0; i < data.length; i++) {
                    selectdata += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";

                }

                htmlString = "<div class='Container'>"
                    + "<div class='row text-right'><span id='closebtn' style=' font-size: 19px;border: 1px solid #BFBFBF;margin-right: 3px;color: #A7A7A7;font-weight: bold;cursor:pointer' >&nbsp;X&nbsp;</span></div>"
                + "<div class='row' style='padding: 20px;'>"
                    + "<h3 class='popup-heading'>Add Person to Categories</h3>"
                + "</div>"
                + "<div class='row' style='padding-left:40px'>"
                + "</div>"
                + "<div class='row'>"
                        + "<div class='col-md-8' >"
                            + "<span class='selcategory'>SelectCategory</span>"
							+ " <select id='Mapcategories' style='width:70%'>" + selectdata + "</select>"
                        + "</div>"
                        + "<div class='col-md-3'>"
                            + "<input type='button' id='AddPToC' value='Add Persons'/>"
                        + "</div>"
                + "</div>"
                + "</div>"
                + "<div class='row' id='divpersons' style='padding-left: 40px; padding-top: 20px;'>"
                + "</div>"

                $('#overLay').css('display', 'block');
                $('#Datacontainer').removeAttr('class');
                $('#Datacontainer').html(htmlString);
            }

        },
        error: function () {

        }
    });    
}

$(document).on('change', '#Mapcategories', function () {
   // //debugger;
    var data = $(this).val();

    $.ajax({
        url: '/persons/SelectPersons?ID=' + data,
        success: function (data) {
           //debugger;
            htmlString = "<div class='col-md-2'></div><div class='addpersontable col-md-5'><table id='personsTable' align='center'><tr><th >Name</th><th class='text-center'>Action</th></tr>";

            if (data != null) {
                for (i = 0; i < data.length; i++) {
                    htmlString += "<tr><td class='text-left'>" + data[i].FullName + "</td><td class='text-center'><input type='checkbox' style='width:30%!important' id='mapchk' class='Pchkbox' value='" + data[i].ID + "' " + data[i].Checked + " /> </td></tr>";
                }
                htmlString += "</table></div>";

                $('#divpersons').html(htmlString);
            }

        },
        error: function () {

        }
    });
});

$(document).on('click', '#AddPToC', function () {
    ////debugger;

    showloadDiv();
    var chkboxes = $('.Pchkbox');
    var persons = new Object();
    var personsArr=new Array();
    var CatID = $('#Mapcategories').val();

    for (i = 0; i < chkboxes.length;i++)
    {
        if(chkboxes[i].checked)
        {
            persons = new Object();
            persons.PersonID = chkboxes[i].value;
            persons.ID = 0;
            persons.CategoryID = CatID;
            personsArr.push(persons);
        }
    }
   // var arr=JSON.stringify(personsArr);
    $.ajax({
        url: '/Persons/MapToCategory',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(personsArr),
        dataType: "json",
        success: function (data) {
            ////debugger;
            $('#overLay').css('display', 'none');
            $('#Datacontainer').html("");
            closeloadDiv();
        },
        error: function () {

        }
    });

});




function showloadDiv()
{
    $('#loadingDiv').css('display', 'block');
}

function closeloadDiv() {
    $('#loadingDiv').css('display', 'none');
}



function copyCategory(ID)
{
    //debugger;
    $.ajax({
        url: '/Categories/SelectCategories',
        success: function (data) {
            // //debugger;
            if (data != null) {
                var selectdata = "<option value='-1'>select--</option>";
                for (i = 0; i < data.length; i++) {
                    selectdata += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }

                htmlString = "<div class='row' style='padding: 20px;'><h3 class='popup-heading'>Copy Categories</h3><div class='row' style='padding: 40px;padding-bottom: 10px;color: #5192CB;font-weight: 600;'>SelectCategory</div><div class='row' style='padding-left: 40px;'><div class='col-md-4'><label class='date'>From</label><select id='fromCategory' style=' width: 100%'>" + selectdata + "</select></div><div class='col-md-4'><label class='date'>To</label><select id='toCategory' style=' width: 100%'>" + selectdata + "</select></div><div class='col-md-4'><input type='button' id='copyCategory' value='Copy'/></div></div></div>";
                $('#overLay').css('display', 'block');
                $('#Datacontainer').html(htmlString);
                $('#fromCategory').val(ID);
            }

        },
        error: function () {

        }
    });
}


$(document).on('click', '#copyCategory', function () {

    showloadDiv();
    var from = $('#fromCategory').val();
    var to = $('#toCategory').val();
    

    $.ajax({
        url: 'Categories/CopyCategories',
        type:'POST',
        data:{From:from,To:to},
        success: function (data) {
            $('#overLay').css('display', 'none');
            $('#Datacontainer').html("");
            closeloadDiv();
        },
        error: function (request, status, error) {
            alert(request.responseText);
    }
    });
});




function checkCategory() {
    //debugger;
    var list = $('#categories li');
    var catList = "";
    var count = 0;
    for (i = 0; i < list.length; i++) {
        if ($('#categories li').eq(i).find('input[type="checkbox"]') .is(':checked')) {
            catList += $('#categories li').eq(i).find('input[type="checkbox"]').val() + ',';
            count = 1;
        }
    }
    var lIndex = catList.lastIndexOf(',');
    catList = catList.substr(0, lIndex);
    if (count == 0) {
        alert('please select atleast one category');
        return false;
    }

    var count = 0;
   
    $('#CategoryList').val(catList);

    var password = $('#Password').val();
    var password1 = $('#RePassword').val();
    if (password != password1) {
        alert('password and confirm password does not match');
        return false;
    }
    
    return true;
}


$(document).on("click", "#btncheck", function () {
    //debugger;
    $('#loadingDiv').css('display', 'block');
    var username = $('#UserName').val();

    if (username != undefined) {
        $.ajax({
            url: "/Persons/CheckUserName?username=" + username,
            type: "GET",
            success: function (data) {
                //debugger;
                if (data == "1") {
                    $("#error").css('color', 'red');
                    $("#error").html('User name already exists. Please enter a different user name.');
                }
                else {
                    $("#error").css('color', 'green');
                    $("#error").html('User name available');
                }
                $('#loadingDiv').css('display', 'none');
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }

})

$(document).on("change", ".date", function () {

    //debugger;
    var data=$(this).val();

    var curdate = new Date(data);
    $(".date").datepicker
        (
            "option", "defaultDate", curdate
        );

});



$(document).on('click', '#changepassword', function () {
    $('#loadingDiv').css('display', 'block');
    $('#Datacontainer1').load('/Account/ChangePassword', function () {
        $('#overLay1').css('display', 'block');
        $('#loadingDiv').css('display', 'none');
    });

});





 