$(document).ready(function () {
    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    console.log("Test")

    var date = new Date(Date.now());
    console.log(date);
    var day = date.getDay();
    var month = date.getMonth();
    var year = date.getFullYear();
    var FebNumberOfDays = "";

    if ((year % 100 != 0) && (year % 4 == 0) || (year % 400 == 0)) {
        FebNumberOfDays = 29;
    } else {
        FebNumberOfDays = 28;
    }
    var dayPerMonth = ["31", "" + FebNumberOfDays + "", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31"]

    var fday = new Date(date.getFullYear(), date.getMonth(), 1);
    var lday = new Date(date.getFullYear, date.getMonth(), dayPerMonth[date.getMonth() - 1]);

    var cal = $("<table>", {
        "class": "calendar"
    });

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
    cal.append(header);
    var body = $("<tbody>");
    var row = null;
    var cell = null;
    var n = null;

    for (var w = 0; w < 6; w++) {
        row = $("<tr>");
        for (var d = 0; d < 7; d++) {
            n = w * 7 + (d + 1) - fday;
            if (n < 1 || n > lday) {
                cell = $("<td>");
            } else {
                if (n == date.getDate()) {
                    cell = $("<td>", {
                        id: n,
                        "class": "today"
                    });
                }
                else {
                    cell = ("<td>", {
                        id: n
                    });
                }
                
            }
            row.append(cell);  
        }
        body.append(row);
        if (n >= lday.getDate) {
            break;
        }

    }
    cal.append(body);

    $("#calendar").empty();
    $("#calendar").append(cal);
	
});

const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];


function calendar() {
    var date = new Date(Date.now);
    var day = date.getDay();
    var month = date.getMonth();
    var year = date.getFullYear();
    var FebNumberOfDays = "";

    if ((year % 100 != 0) && (year % 4 == 0) || (year % 400 == 0)) {
        FebNumberOfDays = 29;
    } else {
        FebNumberOfDays = 28;
    }
    var dayPerMonth = ["31", "" + FebNumberOfDays + "", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31"]

    var fday = new Date(date.getFullYear(), date.getMonth(), 1);
    var lday = new Date(date.getFullYear, date.getMonth(), dayPerMonth[date.getMonth() - 1]);

    var cal = $("<table>", {
        "class": "calendar"
    });

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
    cal.append(header);
    var body = $("<tbody>");
    var row = null;
    var cell = null;
    var n = null;

    for (var w = 0; w < 6; w++) {
        row = $("<tr>");
        for (var d = 0; d < 7; d++) {
            n = w * 7 + (d + 1) - fday;
            if (n < 1 || n > lday) {
                cell = $("<td>");
            } else {
                if (n == date.getDate()) {
                    cell = $("<td>", {
                        id: n,
                        "class": "today"
                    }).html(n);
                }
                else {
                    cell = ("<td>", {
                        id: n
                    }).html(n);
                }
                
            }
            row.append(cell);  
        }
        body.append(row);
        if (n >= lday.getDate) {
            break;
        }

    }
    cal.append(body);
    console.log(cal);
    $("#calendar").empty();
    $("#calendar").append(cal);
}