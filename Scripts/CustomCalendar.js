
        var months=["January",
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
        var shortDesc = ['jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec'];

        var StringDays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

        var color = ['red','grey','azure','green','orange'];

      //  var dataList = new Object();

        function getData(data,year)
        {
            debugger;
            //if (check(data))
            //{
            //    alert('select any ship from list');
            //    return;
            //}
            var startDate=new Date();
            sessionStorage.setItem("View", 0);
            //hide yearly view and display monthly view.
            $('.cal-div').show();
            $('#Cal').hide();
            $('#exp_data').hide();


          //  alert('gotcha');
         //   var d = typeof (data);
           // alert(data);
            if (data == undefined)
            {
                return;
            }
            //var yourval = JSON.stringify(data);
            //var parsed = $.parseJSON(data);;
          // var arr = ;
            //console.log(yourval);
            //alert(yourval[0]);
            //alert(yourval[0].date);


            //var arr = new Array();
            dataList =data;

            sessionStorage.setItem('dataList',JSON.stringify(data));
            //$.each(yourval, function (i, obj) {
                
            //    //date.push(obj.Date)
            //    //console.log(arr[i].Date +"    "+arr[i].Color);
            //});


            //$.each(arr, function (i, obj) {
            //    // arr.push(obj.Date)
            //    
            //    console.log(new Date(value));
            //});

            //console.log(arr);
            //for (i = 0; i < yourval.length; i++)
            //{
            //    console.log(yourval.data[i].date);
            //    console.log(yourval.data[i].date);
            //}
            //alert(parsed.length);
            //var arr = new Array(data);
           // console.log(arr);
            //   alert(arr[0].Date);
            if (data != undefined && dataList != '') {
                if (dataList.CruiseViewModel.length > 0)
                {
                    startDate = convertJSONDate(dataList.CruiseViewModel[0].Date);
                }
                else
                {
                    if (year != null) {
                        startDate = new Date("01/01/" + year);
                    }
                }
                    
                
            }
            else
            {
             
                //if (year!= null)
                //{
                //    startDate = new Date("01/01/" + year);
                //}
                
            }
           
           // startDate = new Date(parseInt(dataList[0].Date.replace("/Date(", "").replace(")/", ""), 10));
           // console.log(dataList[0].Date);
            createCalendar(startDate);
        }

        function ConvertJsonDateString(jsonDate) {
            var shortDate = null;
            if (jsonDate) {
                var regex = /-?\d+/;
                var matches = regex.exec(jsonDate);
                var dt = new Date(parseInt(matches[0]));
                var month = dt.getMonth() + 1;
                var monthString = month > 9 ? month : '0' + month;
                var day = dt.getDate();
                var dayString = day > 9 ? day : '0' + day;
                var year = dt.getFullYear();
                shortDate = monthString + '-' + dayString + '-' + year;
            }
            return shortDate;
        }

        function convertJSONDate(val) {

            //console.log(val);
            //alert(val)
            var dateStr = ConvertJsonDateString(val);
            var newDate = new Date(dateStr);
            return newDate;
        }


        function createCalendar(startDate)
        {
           // alert(startDate);
            var CalendarDiv = $('#CalendarDiv');
            if (CalendarDiv != undefined) {
                var htmlString = '<div id="Calendar">'
                +'<table id="calT">'
                + '<tr class="month-name">'
                + '<td style="width:10%;border:none;cursor:pointer"><div id="prev" style="cursor:pointer"> <span class="glyphicon glyphicon-circle-arrow-left"></span></div></td>'
                +'<td style="border:none"><div id="disp" style="width:100%"></div></td>'
                + '<td  style="width:10%;border:none;"><div id="next" style="cursor:pointer" ><span class="glyphicon glyphicon-circle-arrow-right"></span></div></td>'
                +'</tr>'
                +'</table>'
                + '<div id="dateportion" style="width:100%;height:480px">'
                +'</div>'
                +'</div>';
                CalendarDiv.html(htmlString);

              

                //console.log(dataList);

                ////debugger;
                if (startDate == undefined || startDate == '')
                {
                    startDate = new Date();
                }
                var month = startDate.getMonth();
                var year = startDate.getFullYear();
                DisplayDates(month,year);
              
            }
        }



        function DisplayDates(month,year)
        {
            ////debugger;
            var end = 0;
            curmonth=month + 1; 
            var findDate = new Date(curmonth+ "/1/"+ year + "");
           
            if (month == 0) {
                end = 31;
            }
            else if ((month) == 1) {
                var yr = parseInt(year);
                //alert('d: '+yr);
                if ((yr % 4 == 0 && yr % 100 != 0) || yr % 400 == 0) {
                    end = 29;
                }
                else {
                    end = 28;
                }

            }
            else if ((month) <= 6) {
                if ((month + 1) % 2 != 0) {
                    end = 30;
                }
                else {
                    end = 31;
                }
            }
            else if ((month) >= 6 && (month) < 12) {
                if ((month) % 2 != 0) {
                    end = 31;
                }
                else {
                    end = 30;
                }
            }

            var displayDate = months[month] + ',' + year;

           // var htmlstring = '<table class="data"><tr><th>sun</th><th>mon</th><th>tue</th><th>wed</th><th>thu</th><th>fri</th><th>sat</th></tr>';
                var start = 1
                var count=1;
                var c;
                var htmlstring = '<table class="data" id="cal"><tr><th style="border-left: 1px solid #DFD5D5;">Sunday</th><th>Monday</th><th>Tueday</th><th>Wednesday</th><th>Thursday</th><th>Friday</th><th style="border-right: 1px solid #DFD5D5;">Saturday</th></tr><tr>'
            while (start <= 6) {
                c = 0;
                if (count > end) {
                    break;
                }
                while (c < 7) {
                    if (count > 31) {
                        break;
                    }
                    if (start == 1) {
                        if (c < findDate.getDay()) {
                            htmlstring += '<td></td>'
                        }
                        else {
                            htmlstring += getHtmlString(count,month);
                            count++;
                        }
                        ////console.log("in column in start");
                    }
                    else {
                        ////console.log("in column in normal");
                        htmlstring += getHtmlString(count,month);
                        count++;
                    }


                    c = c + 1;
                    //   //console.log(c);
                }
                htmlstring += "</tr>";
                start = start + 1;

                // //console.log(htmlstring);
            }

            $("#dateportion").html(htmlstring);
              $("#disp").html(displayDate);

        }


        function getHtmlString(count,month)
        {
            //debugger;
            var day;
            var ar = 0;
            dataList =JSON.parse(sessionStorage.getItem('dataList'));

            //dataList
            if (dataList != null)
            {
                for (i = 0; i < dataList.CruiseViewModel.length; i++) {                    
                    var date = convertJSONDate(dataList.CruiseViewModel[i].Date);
                    day = date.getDate();
                    curmonth = date.getMonth();
                    if (day == count && curmonth == month) {
                        debugger;
                        var task = dataList.CruiseViewModel[i].TaskName;
                        var taskname=[];
                        if (task.indexOf(';') > -1)
                        {
                            taskname=task.split(';');
                        }
                        else
                        {
                            taskname[0] = task;
                        }
                        htmlstring = '<td style="background:' + dataList.CruiseViewModel[i].Color + '">' + count + '</br><span>' + taskname[0] + '</span>';
                        if (taskname.length > 1)
                        {
                            htmlstring += '</br><span>' + taskname[1] + '</span>';
                        }
                        ar = 1;
                        for (j = 0; j < dataList.Subtasks.length; j++) {
                            var date1 = convertJSONDate(dataList.Subtasks[j].Date);
                            day1 = date1.getDate();
                            curmonth1 = date1.getMonth();

                            //day1 == count && curmonth1 == month
                            if (day1 == count && curmonth1 == month) {
                                htmlstring += '<div style="background:' + dataList.Subtasks[j].SubColor + '" >' + dataList.Subtasks[j].Person + '</div>';
                                //ar = 1;
                              //  break;
                            }

                        }
                        htmlstring += "</td>";
                    //break;
                }
                    

                    
                   

                }
                if (ar == 0) {
                    htmlstring = '<td>' + count + '</td>';
                }
            }
            else
            {
                htmlstring = '<td>' + count + '</td>';
            }
            
            return htmlstring;
        }

       

        $(document).on('click', '#next', function () {
            //debugger;
            var datepart = $('#disp').html();
            var val = datepart.split(',');
            var month = months.indexOf(val[0]);
            var year = val[1];
            if (month == 11)
            {
                month = -1;
                year = parseInt(year) + 1;
            }
            DisplayDates(month+1, year);

        })
      


        $(document).on('click', '#prev', function () {

            ////debugger;
            var datepart = $('#disp').html();
            var val = datepart.split(',');
            var month = months.indexOf(val[0]);
            var year = val[1];
            if (month == 0) {
                month =12;
                year = parseInt(year) - 1;
            }
            DisplayDates(month-1, year);

        })


        function getMonthlyView()
        {
            var cruiseID = $('#Cruise').val();
            var year = $('#cmbYear').val();
            if(cruiseID=="" || cruiseID==undefined)
            {
                alert('cruise not selected');
                return;
            }

            if(year=="" || year==undefined)
            {
                alert('year not selected');
                return;
            }

            GetSchedule(cruiseID,year);
        }



//taking out number of dyas in year
        function getDays(year)
        {
            if (year == 0) {
                curDate = new Date();
                year = curDate.getFullYear();
                //year = curyear;
            }
            
            var leapyear = ((year % 4 == 0 && year% 100 != 0) || year % 400 == 0);
            var count = 365;
            if (leapyear) {
                count = 366;
            }
            return count;
        }

//making start date for showing in year
        function makeDate(year)
        {
            var curDate;
            var curyear;
            var datestring;
            var startDate;
            var tempstartDate;
            if (year == 0) {
                curDate = new Date();
                curyear = curDate.getFullYear();
                datestring = "01/01/" + curyear;
                startDate = new Date(datestring);
                tempstartDate = startDate;
                year = curyear;
            }
            else {
                curyear = year;
                datestring = "01/01/" + curyear;
                startDate = new Date(datestring);
                tempstartDate = startDate;
            }
            return tempstartDate;

        }

//making header of Table with dates.
        function makeYearlyViewTable(year)
        {

            var tempstartDate = makeDate(year);
            var count = getDays(year);
            var htmlString = "";
            htmlString += "<div class='row'><table id='yearlytable'><thead><tr><th  style=' width:100px;font-size:12px;line-height:14px;height:120px;padding-top:0'> <div style='widht:100px'></div></th>";
           

            //for header
            for (i = 1; i <=count; i++) {
                var datestr;
                if (i == 0) {
                    datestr = "Ships";
                    htmlString += "<th  style='width:100px;font-size:12px;line-height:14px;;border:1px solid;height:150px ;padding-top:0 ' ><span>" + datestr + "</span></th>";
                }
                else {
                    datestr = convertDateToString(tempstartDate)
                    htmlString += "<th  style='width: 36px;transform : rotate(-90deg);width:100px;font-size:12px;line-height:14px;;border:1px solid;height:150px ;padding-top:0 ' ><span>" + datestr + "</span></th>";
                }

                tempstartDate.AddDays(1);
            }
            htmlString += "</thead><tbody></table>";
            $('.cal-div').hide();
            $('#Cal').show();
            $("#exp_data").show();
            $('.htmldata').val(htmlString);

            $('#Cal').html(htmlString);
            $('#Cal').css('height', '455px');
            $('#Cal').css('width', '97%');
            
        }

// shoing schedules and subschedules in yearly view
        function getfullYearView(data,year)
        {
            //debugger;
            var datestring = makeDate(year).toDateString();
            var count = getDays(year);

            if (data.YearlyCruiseViewModel == null)
            {
                alert('no data availble for the ship.');
                return;
            }
            var htmlString="";
            //End header
            var cruise = data.YearlyCruiseViewModel[0].CruiseID;
            count += 1;
            //First Row 
            for (j = 0; j < data.YearlyCruiseViewModel.length; j++)//for rRow
            {
               // for (ship = 0; ship < data.YearlyCruiseViewModel[j].length; ship++)
                //{
                   tempstartDate = new Date(datestring);
                    // var cruise = data[j].CruiseID;
                //tempstartDate = new Date(datestring);
                    debugger;
                    htmlString += "<tr><td style='border:1px solid;font-weight:bold;text-align:center;padding-left: 20px;padding-right: 20px;background:skyblue;' width='200px'>" + data.YearlyCruiseViewModel[j][0].CruiseName + "</td>";
                    for (k = 1; k < count; k++) //for column
                    {
                        for (sb = 0; sb < data.YearlyCruiseViewModel[j].length; sb++) {
                            //////debugger;
                            c = 0;
                            var Sdate = convertJSONDate(data.YearlyCruiseViewModel[j][sb].Date);
                            if ((Sdate - tempstartDate) == 0) {
                                ////debugger;
                                htmlString += "<td  width='100px' style='word-wrap:break-word;border:1px solid;text-align:center; background:" + data.YearlyCruiseViewModel[j][sb].Color + ";'>" + data.YearlyCruiseViewModel[j][sb].TaskName + "</td>";
                                c = 1;
                                break;
                            }
                        }
                        if (c != 1) {
                            htmlString += "<td width='100px'  style='word-wrap:break-word;border:1px solid;text-align:center' ></td>";
                        }


                        tempstartDate.AddDays(1);
                    }

                    htmlString += "</tr>";                    
                //End First Row

                //Notes 
                    if (data.Notes!=null && data.Notes.length>0)
                    {
                        
                        tempstartDate = new Date(datestring);
                        for (temp = 0; temp < data.Notes.length; temp++)
                        {
                            if(data.YearlyCruiseViewModel[j][0].CruiseID == data.Notes[temp][0].CruiseID)
                            {
                                htmlString += "<tr><td style='border:1px solid;font-weight:bold;text-align:center;padding-left: 20px;padding-right: 20px;background:skyblue;' width='200px'>Notes</td>";
                                for (k = 1; k < count; k++) //for column
                                {
                                    for (sb = 0; sb < data.Notes[temp].length; sb++) {
                                        //////debugger;
                                        c = 0;
                                        var Sdate = convertJSONDate(data.Notes[temp][sb].Date);
                                        if ((Sdate - tempstartDate) == 0) {
                                            ////debugger;
                                            htmlString += "<td  width='100px' style='word-wrap:break-word;border:1px solid;text-align:center;'>" + data.Notes[temp][sb].Notes + "</td>";
                                            c = 1;
                                            break;
                                        }
                                    }
                                    if (c != 1) {
                                        htmlString += "<td width='100px'  style='word-wrap:break-word;border:1px solid;text-align:center' ></td>";
                                    }


                                    tempstartDate.AddDays(1);
                                }

                                htmlString += "</tr>";
                                break;
                            }
                        }

                       
                    }
                    

                //End Notes

                    var category,tempCategory;

                    debugger;
                    for (sub = 0; sub < data.Subtasks.length;sub++)
                    {
                        if (data.Subtasks[sub].CruiseID == data.YearlyCruiseViewModel[j][0].CruiseID)
                        {
                            
                            // var cruise = data[j].CruiseID;
                            tempstartDate = new Date(datestring);
                           
                            category = data.Subtasks[sub].CategoryName;
                            //if (sub == 0)
                            //{
                            //    htmlString += "<tr><td style=';border:1px solid;font-weight:bold;text-align:center;padding-left: 20px;padding-right: 20px;background:orange;' width='200px'>" + data.YearlySubtasks[sub][sb1].CategoryName + "</td>";
                            //}

                            if (category != tempCategory)
                            {
                                //tempstartDate = new Date(datestring);
                                htmlString += "<tr><td style=';border:1px solid;font-weight:bold;text-align:center;padding-left: 20px;padding-right: 20px;background:orange;' width='200px'>" + data.Subtasks[sub].CategoryName + "</td>";
                               
                                for (k = 1; k < count; k++) //for column
                                {
                                    debugger;
                                    htmlString += "<td  width='100px' style='word-wrap:break-word;border:1px solid;text-align:center;'>"; //+ data.Subtasks[sb].Person + "</td>";
                                    var subHtmlString= "";
                                    for (sb = 0; sb < data.Subtasks.length; sb++) {
                                        //////debugger;
                                        c = 0;
                                        var Sdate = convertJSONDate(data.Subtasks[sb].Date);
                                        if ((Sdate - tempstartDate) == 0 && category == data.Subtasks[sb].CategoryName) {
                                            debugger;
                                            subHtmlString += "<div style='font-size:12px;background:" + data.Subtasks[sb].SubColor + ";'>" + data.Subtasks[sb].Person + "</div>";
                                            c = 1;

                                            //break;

                                        }

                                    }
                                    //if (c == 1) {
                                    //    ;
                                    //    //htmlString += "<td width='100px'  style='word-wrap:break-word;border:1px solid;text-align:center'></td>";
                                    //}
                                    
                                    htmlString += subHtmlString + "</td>";
                                    tempstartDate.AddDays(1);
                                }
                                tempCategory = category;
                                htmlString += "</tr>";
                               // console.log(htmlString);
                            }
                            
                        }
                    }
                       
                                       
            }
            //console.log(htmlString);
            //htmlString += "</tbody></table></div>";
            $('#yearlytable').append(htmlString);
            // $("#cmbYear option[value='" + curyear + "']").prop('selected', true);
            
        }


        function showData(year)
        {
            //debugger;

            //$('#divShip').css('display', 'none');
            year=$('#cmbYear').val();

            if(year==undefined || year=="")
            {
                year=0;
            }
            sessionStorage.setItem("View", 1);
            $.ajax({

                url: '/Cruises/getYearlySchedule',
                data:{Year:year},
                success: function (data) {
                    //debugger;
                    // yearlyView(data,year);
                    makeYearlyViewTable(year);
                    getfullYearView(data, year);                    
                },
                error: function () {

                }                
            });
        }


        $(document).on('change', '#cmbYear', function () {
            debugger;
            var year = $(this).val();
            var cruise = $('#Cruise').val();
            if (cruise == "" || cruise == undefined) {
                alert('ship not selected');
                return;
            }
            if (year == "" || year == undefined) {
                return;
                //alert('not a valie ');
            }
            var view = sessionStorage.getItem("View");
            if (view == 1)
            {
                showData(year);
            }
            else
            {
                GetSchedule(cruise, year);
            }
            
            

        });

        function convertDateToString(date) {

            var day = StringDays[date.getDay()];
            var date2 = date.getDate();
            var year = date.getFullYear();
            var month = months[date.getMonth()];

            var customDate = day + ',' + month + ' ' + date2 + ',' + year;

            return customDate;
        }