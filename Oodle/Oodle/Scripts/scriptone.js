
var worth2 = document.getElementById("curGrade");
var score2 = document.getElementById("aimGrade");
var grade2 = document.getElementById("finalWorth");
function gradefinals() {


    var curGrade = Number(worth2.value);
    var goalGrade = Number(score2.value);
    var finalWorth = Number(grade2.value);



    var examPer = (goalGrade / 100) * curGrade;
    var gradeAvg = finalWorth + examPer / 2;


    document.getElementById('answer3').innerHTML = gradeAvg;


}




var gDom = document.getElementById("grades");
var aDom = document.getElementById("avg_grades");
var fDom = document.getElementById("final_grade");
var iDom = document.getElementById('yourgrades');

function outputData(g, a, f) {

    gDom.innerHTML = g;
    aDom.innerHTML = a.toFixed(1);
    fDom.innerHTML = f;
}


function calculateGrade(allGrades) {

    var totalGrade = 0;
    var avgGrade = 0;
    var finalGrades = ["A", "B", "C", "D", "F"];
    var finalGrade;

    for (var i = 0; i < allGrades.length; i++) {
        totalGrade += parseInt(allGrades[i]);
        avgGrade = totalGrade / allGrades.length;
    }

    if (avgGrade >= 90) {
        finalGrade = finalGrades[0];
        fDom.className = "green";
    } else if (avgGrade >= 80 && avgGrade < 90) {
        finalGrade = finalGrades[1];
        fDom.className = "teal";
    } else if (avgGrade >= 70 && avgGrade < 80) {
        finalGrade = finalGrades[2];
        fDom.className = "blue";
    } else if (avgGrade >= 60 && avgGrade < 70) {
        finalGrade = finalGrades[3];
        fDom.className = "orange";
    } else {
        finalGrade = finalGrades[4];
        fDom.className = "red";
    }

    outputData(allGrades, avgGrade, finalGrade);
    saveGradeAsCookie(avgGrade, finalGrade);

}

function parseData(input) {

    var grades = input.split(",");
    grades = grades.sort(function (a, b) { return b - a });
    calculateGrade(grades);

}

function submit() {

    if (iDom.value === '') {
        alert("Enter more then one grade so Oodle can Proccess!");
    } else {
        parseData(iDom.value);
    }

}


/* Below inputs data from a "mygrades.txt" file */

var myInputData = new XMLHttpRequest();
myInputData.open("GET", "mygrades.txt");

myInputData.onreadystatechange = function () {
    if (myInputData.readyState == 4) {
        if (myInputData.status == 200) {
            console.log(myInputData.responseText);
            parseData(myInputData.responseText);
        }
    }
};

myInputData.send();



function grade(curGrade, FinalWeight, GoalGrade) {

    //var output = (GoalGrade - (100 - FinalWeight) * curGrade) / FinalWeight;
    var output = GoalGrade + FinalWeight + curGrade;
    document.getElementById("answer").innerHTML = "In order to get the grade you want you need the following on your final: ";
    alert(output);
}





//For our moving boxes on the about page
$('.box').click(function () {

    $(this).animate({
        left: '-50%'
    }, 500, function () {
        $(this).css('left', '150%');
        $(this).appendTo('#container');
    });

    $(this).next().animate({
        left: '50%'
    }, 500);
});




//Toggle FAQ section about page
$(document).ready(function () {
    $(".content-box").hide();
    $(".contorol").click(function () {
        $(this).next(".content-box").slideToggle().siblings(".content-box").slideUp();
        if ($("i").hasClass("fa-plus")) {
            $(this).find("i").toggleClass("fa-minus");

        }
    });
});