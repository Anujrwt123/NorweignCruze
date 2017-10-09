// Function To Delete values


function finalDelete(ID,Name,Url,option)
{
    //debugger;   
    var result = confirm('You are about to delete' + Name + '. If yes click ok.')
    
    if (result)
    {
        var data;
        if (option != undefined)
        {
            data = {ID:ID,Option:option}
        }
        else
        {
            data = { ID: ID}
        }
        $.ajax({            
            url: Url,
            method: "POST",
            data: data,
            success: function (data) {
               //debugger;
                var data = JSON.parse(data);
                if (data.Result == "Done")
                {
                    $("#dataContainer").load(data.Data);
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


function goToPrevious(returnUrl)
{
    ////debugger;
    val=$('#returnUrl').val();
    if(val!=undefined && val!='')
    {
        val = val.replace('~', '');
        window.location.href = val;
    }
    else
    {
        returnUrl = returnUrl.replace('~', '')
        window.location.href = returnUrl;
    }
}





 