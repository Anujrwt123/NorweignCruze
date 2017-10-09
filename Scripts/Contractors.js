var months = ["January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
]

$(document).ready(function () {
    // $('#Per_Diem_On_Land').attr('disabled', true);

    var land=$('#Per_Diem_On_Land').val();
    if(check(land))
    {
        $('#Per_Diem_On_Land').val('35');
    }
    
    var land=$('#Per_Diem_On_Board').val();
    //$('#Per_Diem_On_Board').attr('disabled', true);
    if(check(land))
    {
        $('#Per_Diem_On_Board').val('12.50');
    }
        
    setInterval(init(), 5000);

    var option = getSession("Option");
    if(option==null)
    {
        SaveSessioon("Option", 1);
    }
    //$('#Total_Fee').attr('disabled', true);
    //$('#Total_Per_Diem').attr('disabled', true);
    //$('#Total_Per_Diem_On_Board').attr('disabled', true);
    //$('#Total_Per_Diem_On_Land').attr('disabled', true);    
});



var init = (function () {

    //   alert('hello');


    ///Term
    var Term = $('#Term').val();
    var item;
    var endDate = $('#EndDate').val();
    if (!check(Term)) {
        var year;
        item = Term.split('-');
        endDate = new Date(endDate);
        if (!check(endDate)) {
            year = endDate.getFullYear();
        }

        date1 = item[0].split(',');
        date1_part = date1[0].split(' ');
        var month = months.indexOf(date1_part[1]);
        var startDate = (month + 1) + '/' + date1_part[0] + '/' + year;

        //startDate == new Date(startDate);


        date2 = item[1].split(',');
        date2_Part = date2[0].split(' ');
        var month = months.indexOf(date2_Part[1]);
        var endDate = (month + 1) + '/' + date2_Part[0] + '/' + year;
        //endDate = new Date(endDate);


        $('#Term1').val(startDate);
        $('#Term2').val(endDate);

    }

    //ShipDate

    var Ship = $('#shipDates').val();
    var item;
     var endDate = $('#EndDate').val();
    if (!check(Ship)) {
        var year;
        item = Ship.split('-');
        endDate = new Date(endDate);
        if (!check(endDate)) {
            year = endDate.getFullYear();
        }

        date1 = item[0].split(',');
        date1_part = date1[0].split(' ');
        var month = months.indexOf(date1_part[1]);
        var startDate = (month + 1) + '/' + date1_part[0] + '/' + year;

        // startDate == new Date(startDate);


        date2 = item[1].split(',');
        date2_part = date2[0].split(' ');
        var month = months.indexOf(date2_part[1]);
        var endDate = (month + 1) + '/' + date2_part[0] + '/' + year;
        //endDate = new Date(endDate);

        $('#shipDates1').val(startDate);
        $('#shipDates2').val(endDate);

    }


    ///TampaDates
  //  //debugger;
    var Tampa = $('#TampaDates').val();
    var item;
     var endDate = $('#EndDate').val();
    if (!check(Tampa)) {
        var year;
        item = Tampa.split('-');
        endDate = new Date(endDate);
        if (!check(endDate)) {
            year = endDate.getFullYear();
        }

        date1 = item[0].split(',');
        date1_part = date1[0].split(' ');
        var month = months.indexOf(date1_part[1]);
        var startDate = (month + 1) + '/' + date1_part[0] + '/' + year;

        //startDate == new Date(startDate);


        date2 = item[1].split(',');
        date2_part = date2[0].split(' ');
        var month = months.indexOf(date2_part[1]);
        var endDate = (month + 1) + '/' + date2_part[0] + '/' + year;
        //  endDate = new Date(endDate);

        $('#tampadate1').val(startDate);
        $('#tampadate2').val(endDate);

    }

});



$(document).on('change', '#Days_On_Travel', function () {

    CalculatePerDiemonTravel();
});

$(document).on('change', '#Days_On_Land', function () {
    CalculatePerDiemOnLand();
});

$(document).on('change', '#Per_Diem_On_Land', function () {
    CalculatePerDiemOnLand();
    CalculatePerDiemonTravel();
});


$(document).on('change', '#Total_Per_Diem_On_Land', function () {
    ////debuger;
    CalculatePerDiemOnLand();
});







$(document).on('change', '#Days_OnBoard', function () {
    CalculatePerdiemOnBoard();   
});

$(document).on('change', '#Per_Diem_On_Board', function () {
    CalculatePerdiemOnBoard();
    CalculatePerDiemOnLand();
});


/// Assign totals of board and land to Total_per_diem


$(document).on('change', '#Total_Per_Diem_On_Board', function () {
    ////debuger;
    CalculatePerdiemOnBoard();
});




//$(document).on('change', '#Per_Diem_On_Land', function () {
//    var dayonland = $('#Days_On_Land').val();
//    var PerDiemOnLand = $('#Per_Diem_On_Land').val();
//    var totalval = dayonland * PerDiemOnLand;
//    $('#Total_Per_Diem_On_Land').val(totalval);
//    var TotalDiemOnBoard = $('Total_Per_Diem_On_Board').val();
//    var totalval1 = parseInt(TotalDiemOnBoard) + parseInt(totalval);
//    $('#Total_Per_Diem').val(totalval1);
//});



//to intiliaze the payement 1 date because it was not initiating at automatically.

//$(document).on('focusin', '#Payment_1_Date', function () {

//    $("#Payment_1_Date").datetimepicker({
//        useCurrent: false
//    }).on('dp.show', function () {
//        return $('#Payment_1_Date').data('DateTimePicker').defaultDate(start);
//    });
//})

//$(document).on('focusout', '#Payment_1_Date', function () {
//    //debugger;
//    var data = $(this).val();
//    if (check(data))
//    {
//        var data = sessionStorage.getItem('payment1Date');
//        $(this).val(data);
//    }

//});


//to intiliaze the payement 1 date because it was not initiating at automatically.

//$(document).on('focusout', '#Payment_2_Date', function () {
//    //debugger;
//    var data = $(this).val();
//    if (check(data)) {
//        var data = sessionStorage.getItem('payment2Date');
//        $(this).val(data);
//    }

//});



//Merging the date of Term and ShipDates..

$(document).on('change', '#Term1', function () {
    ////debuger;

    var term1 = $('#Term1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    var start = term1;
    var month = months[term1.getMonth()];
    if (term1 != "Invalid Date") {
        term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();
        start.AddDays(30);

        //var Fstart =start.getMonth()+1 + '/' + start.getDate() + '/' + start.getFullYear();
        var fstart = formatDate(start);
        $('#Payment_1_Date').val(fstart);

      ///  sessionStorage.setItem('payment1Date', fstart);
        

      
            }
    else {
        term1 = "";
    }
  //  term1 = term1.getDate() + ' ' + month;

    var term2 = $('#Term2').val();
    term2 = formatDate(term2);
    term2 = new Date(term2);

    var month = months[term2.getMonth()];
    if (term2 != "Invalid Date") {
        term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
    }
    else {
        term2 = "";
    }
    //term2 = term2.getDate() + ' ' + month;
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    if (check(!term1) && !check(term2))
    {
        var term = term1 + '-' + term2;
        $('#Term').val(term);
        
    }
   
    CalculateTotalFee();
    //var term1 = $('#Term1').val();
    //var term2 = $('#Term2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    //var term = arr + '/' + arr1;
    //$('#Term').val(term);
});

$(document).on('change', '#Term2', function () {
    //debuger;

    var term1 = $('#Term1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    
    var month = months[term1.getMonth()];
    if (term1 != "Invalid Date")
    {
        term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();
    }
    else
    {
        term1 = "";
    }
   

    var term2 = $('#Term2').val();
    var fDate = formatDate(term2);
    term2 = new Date(fDate);
    var end = term2;

    var month = months[term2.getMonth()];
    if (term2 != "Invalid Date")
    {
        term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
        $('#EndDate').val(fDate);
        end.AddDays(30);      
        end = formatDate(end);
        $('#Payment_2_Date').val(end);
        //sessionStorage.setItem('payment2Date',end);
       
    }
    else
    {
        term2 = "";
    }
    
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);

    if (!check(term1) && !check(term2))
    {
        
        var term = term1 + '-' + term2;
        $('#Term').val(term);
       
    }
   

    CalculateTotalFee();
    //var term1 = $('#Term1').val();
    //var term2 = $('#Term2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    //var term = arr + '/' + arr1;
    //$('#Term').val(term);
});

$(document).on('change', '#shipDates2', function () {
    ////debuger;
    var term1 = $('#shipDates1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    var start = term1;
    var month = months[term1.getMonth()];
    term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();

    var term2 = $('#shipDates2').val();
    term2 = formatDate(term2);
    term2 = new Date(term2);
    var end = term2;
    var month = months[term2.getMonth()];
    term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    var term = term1 + '-' + term2;
    $('#shipDates').val(term);

    if (!check(start) && !check(end)) {
        var diff = Date.daysBetween(start, end);
        $("#Days_OnBoard").val(diff);
    }
    CalculatePerdiemOnBoard();
});


//$(document).on('change', '#BaseSalary', function () {

//    CalculateTotalFee();

//});

$(document).on('change', '#shipDates1', function () {
    ////debuger;
    var term1 = $('#shipDates1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    var start = term1;
    var month = months[term1.getMonth()];
    term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();

    var term2 = $('#shipDates2').val();
    term2 = formatDate(term2);
    term2 = new Date(term2);
    var end = term2;
    var month = months[term2.getMonth()];
    term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    
    
    if (!check(start) && !check(end)) {
        var term = term1 + '-' + term2;
        $('#shipDates').val(term);

        var diff = Date.daysBetween(start, end);
        $("#Days_OnBoard").val(diff);
    }
    CalculatePerdiemOnBoard();
});

$(document).on('change', '#Per_Diem_On_Land', function () {
    CalculatePerDiemOnLand();
});


function CalculatePerDiemonTravel()
{
    
    var days = $('#Days_On_Travel').val();
    var PerDiemOnLand = $('#Per_Diem_On_Land').val();

    if (check(days)) {
        days = "";
    }
    if (check(PerDiemOnLand)) {
        PerDiemOnLand = 0;
    }
    var totalval = days * PerDiemOnLand;
    $('#Total_Per_Diem_On_Travel').val(totalval);
    var TotalDiemOnBoard = $('#Total_Per_Diem_On_Board').val();
    var TotalDiemOnLand = $('#Total_Per_Diem_On_Land').val();
    if (check(TotalDiemOnBoard)) {
        TotalDiemOnBoard = "";
    }
    if (check(TotalDiemOnLand)) {
        TotalDiemOnLand = "";
    }
    var totalval1 = parseInt(TotalDiemOnBoard) + parseInt(TotalDiemOnLand) + parseInt(totalval);
    $('#Total_Per_Diem').val(totalval1);
}

function CalculatePerDiemOnLand()
{


    //Calculation for total per Diem on land
    var dayonland = $('#Days_On_Land').val();
    var PerDiemOnLand = $('#Per_Diem_On_Land').val();
    if (check(dayonland))
    {
        dayonland = 0;
    }
    if (check(PerDiemOnLand))
    {
        PerDiemOnLand = 0;
    }

    var totalval = dayonland * PerDiemOnLand;
    $('#Total_Per_Diem_On_Land').val(totalval);
    var TotalDiemOnBoard = $('#Total_Per_Diem_On_Board').val();
    var TotalDiemOnTravel = $('#Total_Per_Diem_On_Travel').val();
    if (check(TotalDiemOnBoard))
    {
        TotalDiemOnBoard = 0;
    }
    if (check(TotalDiemOnTravel)) {
        TotalDiemOnTravel = 0;
    }
    var totalval1 = parseFloat(TotalDiemOnBoard) + parseFloat(TotalDiemOnTravel) + parseFloat(totalval);
    $('#Total_Per_Diem').val(totalval1);


    //Calculation for Total Fee
    //CalculateTotalFee();
}

function CalculatePerdiemOnBoard()
{
    ////debugger;
    var dayonland = $('#Days_OnBoard').val();
    var PerDiemOnLand = $('#Per_Diem_On_Board').val();
    if (check(dayonland))
    {
        dayonland = 0;
    }
    if (check(PerDiemOnLand))
    {
        PerDiemOnLand = 0;
    }
    var totalval = dayonland * PerDiemOnLand;

    $('#Total_Per_Diem_On_Board').val(totalval);

    var Totalonland = $('#Total_Per_Diem_On_Land').val();
    var TotalDiemOnTravel = $('#Total_Per_Diem_On_Travel').val();

    if (check(Totalonland))
    {
        Totalonland =0;
    }
    if (check(TotalDiemOnTravel)) {
        TotalDiemOnTravel = 0;
    }
    var totalval1 = parseFloat(Totalonland) + parseFloat(TotalDiemOnTravel) + parseFloat(totalval);

    $('#Total_Per_Diem').val(totalval1);

   
   // CalculateTotalFee();
}


$(document).on('change', '#BaseSalary', function () {
    var val = $('#BaseSalary').val();
    if (check(val)) {
        return;
    }
    val = val / 7;
    val = parseFloat(val).toFixed(2);
    $('#Day_Rate').val(val);

    CalculateTotalFee();
});

$(document).on('change', '#Day_Rate', function () {
    var val = $('#Day_Rate').val();
    if (check(val)) {
        return;
    }
    val = val * 7;
    val = parseFloat(val).toFixed(2);
    $('#BaseSalary').val(val);
    CalculateTotalFee();
});


$(document).on('change', '#Total_Fee', function () {
    var val = $('#Total_Fee').val();
    if (check(val)) {
        return;
    }
    var startDate = $('#Term1').val();
    var endDate = $('#Term2').val();

    if (check(startDate) && check(endDate)) {
        return;
    }
    startDate = new Date(startDate);
    endDate = new Date(endDate);
    var diff = Date.daysBetween(startDate, endDate);
    var day_rate = (val / diff).toFixed(2);

    $('#Day_Rate').val(day_rate);
    base = day_rate * 7;
    $('#BaseSalary').val(base);
    //CalculateTotalFee();
});

function CalculateTotalFee()
{
  //  //debuger;
    var day_Rate = $('#Day_Rate').val();
    //if (check(baseSalary))
    //{
    //    baseSalary = 0;
    //}
    //var daysonLand = $('#Days_On_Land').val();
    //if (check(daysonLand))
    //{
    //    daysonLand = 0;
    //}
    //var daysonBoard = $('#Days_OnBoard').val();
    //if(check(daysonBoard))
    //{
    //    daysonBoard = 0;
    //}
    //var totalPerDiem = $('#Total_Per_Diem').val();
    //if(check(totalPerDiem))
    //{
    //    totalPerDiem=0;
    //}
    //var totaldays=parseInt(daysonLand)+parseInt(daysonBoard);
    //var totalFee = parseInt(baseSalary) * totaldays;


    if (!check(day_Rate))
    {
        var start = $('#Term1').val();

        var Fstart = formatDate(start);
        var startDate = new Date(Fstart);

        var end = $('#Term2').val();
        var Fend = formatDate(end);
        var endDate = new Date(Fend);

        var count = Date.daysBetween(startDate, endDate);

        if (!check(start) && !check(end)) {
            totalFee = parseFloat(day_Rate) * count;

            var partPayment = parseFloat(totalFee / 2);           
            $('#Total_Fee').val(totalFee);
            divideTotalFeeAmount();

        }
        else {
            //alert(' ');
        }

    }
   
}

function check(elem)
{
    if(elem==undefined || elem=='')
    {
        return true;
    }
    else
    {
        return false;
    }
}



$(document).on('change', '#tampadate1', function () {

    
    var term1 = $('#tampadate1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    var start = term1;
    var month = months[term1.getMonth()];
    term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();

    var term2 = $('#tampadate2').val();
    term2 = formatDate(term2);
    term2 = new Date(term2);
    var end = term2;
    var month = months[term2.getMonth()];
    term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    var term = term1 + '-' + term2;
    $('#TampaDates').val(term);


    if (!check(start) && !check(end)) {
        var diff = Date.daysBetween(start, end);
        $('#Days_On_Land').val(diff);

    }
    CalculatePerDiemOnLand();

})





$(document).on('change', '#tampadate2', function () {

  //  //debuger;
    var term1 = $('#tampadate1').val();
    term1 = formatDate(term1);
    term1 = new Date(term1);
    var start = term1;
    var month = months[term1.getMonth()];
    term1 = term1.getDate() + ' ' + month+', '+term1.getFullYear();

    var term2 = $('#tampadate2').val();
    term2 = formatDate(term2);
    term2 = new Date(term2);
    var end = term2;
    var month = months[term2.getMonth()];
    term2 = term2.getDate() + ' ' + month+', '+term2.getFullYear();
    //var term2 = $('#shipDates2').val();
    //var arr = term1.substring(5, 10);
    //var arr1 = term2.substring(5, 10);
    var term = term1 + '-' + term2;
    $('#TampaDates').val(term);
   

    
    if (!check(start) && !check(end))
    {
        var diff = Date.daysBetween(start, end);
        $('#Days_On_Land').val(diff);
    }
    CalculatePerDiemOnLand();
})




function getContract(id,url,type)
{

    //dialog = ("over-lay").dialog();
    var htmlstring = "<div class='row text-right' style='padding-bottom: 42px;cursor:pointer' id='closebtn' ><span  style=' font-size: 19px;border: 1px solid #BFBFBF;margin-right: 3px;color: #A7A7A7;font-weight: bold;cursor:pointer'>&nbsp;X&nbsp;</span></div><div class='row'><div class='col-md-3'><label>Set Rate as ::</label></div><div class='col-md-5'><select id='sl1' class='fullWidth'><option value='1'>Day Rate</option><option value='2'>Weekly Rate</option><option value='3'>Project Rate</option></select></div></div>";
    htmlstring += "<div class='row'><div class='col-md-3'></div><div class='col-md-5'><input type='button' id='btn1' onclick='gen_Contract(\"" + id + "\",\"" + url + "\",\"" + type + "\")' value='Get Contract' class='btn' /></div></div>";

    $('#overLay2').css('display', 'block');
    
    //$('#conContainer').attr('class', 'setContract');    
    //var val = "";
    //$('#btn1').attr('onclick', val);

    $('#conContainer').html(htmlstring);
    //onclick='gen_Contract('" + id + "','" + url + "')'
}



function gen_Contract(id,url,Type)
{

    var val = $("#sl1").val();
    var url = url + "?ID=" + id + "&val=" + val+"&AgreementType="+Type;

    //$.ajax({
    //    url: url,
    //    success: function (data) {
    //        ////debugger;
    //        alert(data);
    //        console.log(data);
    //        if (data == 1)
    //        {
    //            $('#overLay').css('display', 'none');
    //        }

    //    },
    //    error: function () {

    //    }
    //});
    window.location.href = url;

    $('#overLay').css('display', 'none');
    
}


function divieTotalFee(d)
{

    var totalFee = parseFloat($('#Total_Fee').val());

    if(d==3)
    {
        var partpayment = parseFloat(totalFee / 3);
        $('#Payment_1').val(partpayment);
        $('#Payment_2').val(partpayment);
        $('#Payment_3').val(partpayment);
    }
    else if(d==4)
    {
        var partpayment = parseFloat(totalFee / 4);
        $('#Payment_1').val(partpayment);
        $('#Payment_2').val(partpayment);
        $('#Payment_3').val(partpayment);
        $('#Payment_4').val(partpayment);
    }
    else if(d==5)
    {
        var partpayment = parseFloat(totalFee / 5);
        $('#Payment_1').val(partpayment);
        $('#Payment_2').val(partpayment);
        $('#Payment_3').val(partpayment);
        $('#Payment_4').val(partpayment);
        $('#Payment_5').val(partpayment);
    }
    else if(d==2)
    {
        var partpayment = parseFloat(totalFee / 2);
        $('#Payment_1').val(partpayment);
        $('#Payment_2').val(partpayment);        
    }


}
$(document).on('click', '#addpay', function () {

    if ($('#pay3').is(':hidden')) {
        $('#pay3').show();
        var val = $('#Payment_2_Date').val();
        divieTotalFee(3);
        $('#Payment_3_Date').val(val);
       // $('#Payment_2_Date').val('');
    }
    else if ($('#pay4').is(':hidden')) {
        $('#pay4').show();
        var val = $('#Payment_3_Date').val();
        $('#Payment_4_Date').val(val);
        divieTotalFee(4);
        //$('#Payment_3_Date').val('');
    }
    else if($('#pay5').is(':hidden'))
    {
        $('#pay5').show();
        var val = $('#Payment_4_Date').val();
        $('#Payment_5_Date').val(val);
        divieTotalFee(5);
        //$('#Payment_4_Date').val('');
    }
    
});


$(document).on('click', '#deletepay', function () {

    if (!$('#pay5').is(':hidden')) {
        $('#pay5').hide();
        //var val = $('#Payment_2_Date').val();
        divieTotalFee(4);
        //$('#Payment_3_Date').val(val);
        // $('#Payment_2_Date').val('');
    }
    else if (!$('#pay4').is(':hidden')) {
        $('#pay4').hide();
        //var val = $('#Payment_3_Date').val();
        //$('#Payment_4_Date').val(val);
        divieTotalFee(3);
        //$('#Payment_3_Date').val('');
    }
    else if (!$('#pay3').is(':hidden')) {
        $('#pay3').hide();
        //var val = $('#Payment_4_Date').val();
        //$('#Payment_5_Date').val(val);
        divieTotalFee(2);
        //$('#Payment_4_Date').val('');
    }
});


function divideTotalFeeAmount()
{
    if (!$('#pay5').is(':hidden')) {
       
        divieTotalFee(5);
        //$('#Payment_3_Date').val(val);
        // $('#Payment_2_Date').val('');
    }
    else if (!$('#pay4').is(':hidden')) {
        
        //var val = $('#Payment_3_Date').val();
        //$('#Payment_4_Date').val(val);
        divieTotalFee(4);
        //$('#Payment_3_Date').val('');
    }
    else if (!$('#pay3').is(':hidden')) {
        
        //var val = $('#Payment_4_Date').val();
        //$('#Payment_5_Date').val(val);
        divieTotalFee(3);
        //$('#Payment_4_Date').val('');
    }
    else
    {
        divieTotalFee(2);
    }

}



var SaveSessioon = function (name, data) {
    sessionStorage.setItem(name, data);
}


var getSession = function (name) {

    return sessionStorage.getItem(name);
}

$(document).on('click', '#btnWardrobe', function () {
   // //debugger;
    //$(this).css('background-color', '#EAEAEA');
    //$('#btnProduction').css('background-color', 'white');
    //$('#btnDirectors').css('background-color', 'white');
    getTabData(1);
    
    $('#createnew').attr('href', '/Contractors/Create?option=1');
    
});


$(document).on('click', '#btnProduction', function () {

    //$(this).css('background-color', '#EAEAEA');
    //$('#btnWardrobe').css('background-color', 'white');
    //$('#btnDirectors').css('background-color', 'white');
    getTabData(2);
    $('#createnew').attr('href', '/Contractors/Create?option=2');

});



$(document).on('click', '#btnDirectors', function () {
    //$(this).css('background-color', '#EAEAEA');
    //$('#btnProduction').css('background-color', 'white');
    //$('#btnWardrobe').css('background-color', 'white');
    getTabData(3);
    $('#createnew').attr('href', '/Contractors/Create?option=3');

});

$(document).on('click', '#btndesigners', function () {

    getTabData(4);
    $('#createnew').attr('href', '/Contractors/Create?option=4');
});

$(document).on('click', '#btnNCL', function () {
    debugger;
    getTabData(5);
    $('#createnew').attr('href', '/Contractors/Create?option=5');
});

$(document).on('click', '#btnRegent', function () {

    getTabData(6);
    $('#createnew').attr('href', '/Contractors/Create?option=6');
});

$(document).on('click', '#btnOceana', function () {

    getTabData(7);
    $('#createnew').attr('href', '/Contractors/Create?option=7');
});








function getTabData(data) {
    SaveSessioon('Option', data);
    window.location.href="/Contractors/index/?Type=" + data;
    //$('#dataContainer').load(, function () {
    //    window.Location.reload();
    //});

}

$(document).on('change', '#Hourly_Rate', function () {
    var hrs_per_day = $('#Hours_per_day').val();

    var hrly_rate = $('#Hourly_Rate').val();

    var daily = hrly_rate * hrs_per_day;
    
    var hrs_per_week = $('#Hours_per_week').val();

    var weekly = hrly_rate * hrs_per_week;

    //$('#Hours_per_week').val(weekly);

    $('#Day_Rate').val(daily);

    $('#BaseSalary').val(weekly);
   
    CalculateTotalFee();

});


$(document).on('change', '#Hours_per_day', function () {

    var hrs_per_day = $('#Hours_per_day').val();
    var hrly_rate = $('#Hourly_Rate').val();
    var daily = hrly_rate * hrs_per_day;
    var hrsPerWeek = hrs_per_day * 7;
    $('#Hours_per_week').val(hrsPerWeek);
    var hrs_per_week = hrsPerWeek;
    var weekly = hrly_rate * hrs_per_week;
     //$('#Hours_per_week').val(weekly);
    $('#Day_Rate').val(daily);
    $('#BaseSalary').val(weekly);

    CalculateTotalFee();
});

$(document).on('change', '#Hours_per_week', function () {
    var hrly_rate = $('#Hourly_Rate').val();    
    var hrs_per_week = $('#Hours_per_week').val();
    var perdayhrs = parseInt(hrs_per_week) / 7;
    $('#Day_Rate').val(perdayhrs);
    var weekly = hrly_rate * hrs_per_week;
    $('#BaseSalary').val(weekly);
    CalculateTotalFee();

});







function ContractorDelete(ID,Name,URL)
{
   
    var option = getSession('Option');

    finalDelete(ID, Name, URL,option);

}



$(document).on('change', '#Brands', function () {

    var data = $(this).val();

    $.ajax({
        url: "/Contractors/GetShipsByBrand?Brand=" + data,
        type: "GET",
        success: function (data) {

            var htmlString = "<option value='-1'>select--</option>";
            if (data != null && data.length > 0) {
                for (i = 0; i < data.length; i++) {
                    htmlString += "<option value=\'" + data[i].ID + "\'>" + data[i].Name + "</option>";
                }   

                //console.log(htmlString)
                $('#ship').html(htmlString);
            }
        },
        error: function (data) {

        }
    });
});

$(document).on('change', '#ship', function () {

    debugger;
    var id = $(this).val();

    $.ajax({
        url: "/contractors/GetShowsByShip?ID=" + id,
        type: "GET",
        success: function (data) {
            debugger;
            var htmlString = "";
            if (data != null && data.length > 0) {
                for (i = 0; i < data.length; i++) {
                    htmlString += "<li ><input type='checkbox' value=\'" + data[i].ID + "\'/><span class='small'>" + data[i].Name + "</span></li>";
                }
            }
            $('#ulShows').html(htmlString);
        },
        error: function (err) {
            alert('error:' + err.statusText);
        }
    });
});


function CheckShows()
{
    debugger;
    $('#ulShows>li>input[type="checkbox"]').each(function () {

        var data="";
        if(this.is(':checked'))
        {
            data+=this.attr('value')+',';
        }

    });
    var idx=data.lastIndexOf(',');

    data=data.substr(0,idx);
    //var data = $('#ShowsList').val();
    //var val = $(this).attr('value');
    //if ($(this).is(':checked')) {
    //    data += val + ",";
    //}
    //else {

    //    var d = data.split(',');
    //    for (i = 0; i < d.length; i++) {
    //        if (d[i] == val) {
    //            d.splice(i);
    //        }
    //    }
    //    data = d.join(',');
    //}

    $('#ShowsList').val(data);
}

$(document).on('click', '#ulShows>li>input[type="checkbox"]', function () {

    
});