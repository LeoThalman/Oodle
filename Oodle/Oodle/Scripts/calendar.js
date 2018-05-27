$(document).ready(function () {
    Calendar();

    var query = "/Home/GetCalendarData";
    //query the server, which then will query giphy and pass back a json object
    $.ajax({
        type: "GET",
        dataType: "json",
        url: query,
        success: LoadHomework,
        error: failed
    });
	
});
const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

function LoadHomework(data) {
    console.log(data);
    var Items = JSON.parse(data);
    var today = new Date(Date.now());
    var sDate = null;
    var eDate = null;
    for (var i = 0; i < Items.length; i += 1) {
        if (Items[i].IsAQuiz)
        {
            sDate = new Date(Items[i].StartTime);
            eDate = new Date(Items[i].EndTime);
            if (today.getMonth() == sDate.getMonth()) {
                console.log("This month" + sDate.getMonth());
                $("#day" + sDate.getDate()).append("<p>" + Items[i].Name + ": Opens</p>");
            }
            if (today.getMonth() == eDate.getMonth()) {
                console.log("Also this month" + eDate.getDate());
                $("#day" + eDate.getDate()).append("<p>" + Items[i].Name + ": Closes</p>");
            }
        }
        else {
            eDate = new Date(Items[i].EndTime);
            if (today.getMonth() == eDate.getMonth()) {
                console.log("Assignment this month" + eDate.getMonth());
                $("#day" + eDate.getDate()).append("<p>Due: " + Items[i].Name + "</p>");
            }
        }
    }

}

function failed() {
    console.log("Failed");
}

function Calendar() {

    var date = new Date(Date.now());
    var day = date.getDay();
    var month = date.getMonth();
    var year = date.getFullYear();
    var FebNumberOfDays = "";

    if ((year % 100 != 0) && (year % 4 == 0) || (year % 400 == 0)) {
        FebNumberOfDays = 29;
    } else {
        FebNumberOfDays = 28;
    }
    var dayPerMonth = ["31", "" + FebNumberOfDays + "", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31"];

    var maxd = parseInt(dayPerMonth[month]);
    var fday = new Date(date.getFullYear(), date.getMonth(), 1);
    var lday = new Date(date.getFullYear(), date.getMonth(), maxd);
    console.log("First Day: " + fday.getDay());

    var cal = $("<table>", {
        "class": "calendar"
    });
    var monthHeader = $('<thead>\
			      <tr>\
			         <th class="calMonth" colspan="7">'+ months[month] + ' </th>\
			      </tr>\
				</thead>');
    var header = $("<thead>\
			      <tr>\
			         <th>Sunday</th>\
					 <th>Monday</th>\
					 <th>Tuesday</th>\
					 <th>Wednesday</th>\
					 <th>Thursday</th>\
					 <th>Friday</th>\
					 <th>Saturday</th>\
			      </tr>\
				</thead>");
    cal.append(monthHeader);
    cal.append(header);
    var body = $("<tbody>");
    var row = null;
    var cell = null;
    var n = 0;
    var weekStart = 0;
    var weekEnd = 0;
    var weekOffset = fday.getDay();
    console.log(date.getDate());
    for (var w = 0; w < 6; w++) {
        weekStart = ((w * 7) + 1) - weekOffset;
        weekEnd = ((w + 1) * 7) - weekOffset;
        console.log("Week" + w + " First:" + weekStart);
        console.log("Week" + w + " Last:" + weekEnd);
        console.log(weekOffset);
        if (weekStart <= date.getDate() && weekEnd >= date.getDate()) {
            row = $("<tr>", {
                "class": "thisWeek"
            });
        } else {
            row = $("<tr>");
        }
        for (var d = 0; d < 7; d++) {

            n = (w * 7) + (d + 1) - fday.getDay();
            if (n < 1 || n > maxd) {
                cell = $("<td>");
            } else {
                if (n == date.getDate()) {
                    cell = $("<td>", {
                        id:  "day" + n,
                        "class": "today"
                    });
                }
                else {
                    cell = $("<td>", {
                        id: "day" + n
                    });
                }
                cell.append(n);
            }

            row.append(cell);

        }
        body.append(row);
        if (n >= maxd) {
            break;
        }

    }
    cal.append(body);

    $("#calendar").empty();
    $("#calendar").append(cal);
}