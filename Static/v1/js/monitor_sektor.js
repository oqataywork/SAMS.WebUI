
    // ------------header--------------
    var currenttime = $("#qScreen9 .time h1").text().split(':');
    var second = Number(currenttime[2]), minute = Number(currenttime[1]), hour = Number(currenttime[0]);

    setInterval(() => {
        second = Number(second);
        minute = Number(minute);
        hour = Number(hour);

        second++;
        if (second > 59) {
            second = 0;
            minute++;
        }
        if (minute > 59) {
            minute = 0;
            hour++;
        }
        if (hour > 23) {
            hour = 0;
        }

        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;
        }
        if (second < 10) {
            second = "0" + second;
        }
        if (second == 0 && minute == 0 && hour == 0) {
            location.reload();
        }
        var result = hour + ":" + minute + ":" + second;
        $("#qScreen9 .time h1").text(result)
    }, 1000);

    // ------------------------------------------


    // ------------------footer------------------------

    // ------------------------------------------



    // ---------------------Main---------------------

    // ----------svg text----
    function svgText() {
        //var txts = $("#qScreen9 .rightArea svg text.cls-4");
        //for (let t = 0; t < txts.length; t++) {
        //    var lng = $(txts[t]).text().length;
        //    if (lng == 1) {
        //        $(txts[t]).attr("transform", "translate(420 107.11)");
        //    }
        //    else if (lng == 2) {
        //        $(txts[t]).attr("transform", "translate(400 107.11)");
        //    }
        //    else if (lng == 3) {
        //        $(txts[t]).attr("transform", "translate(385 107.11)");
        //    }
        //}
    }
    svgText();
    // --------

    // ---------------------Main---------------------
    var sectorName = "A";
    var jsonExample = `[
    {
      "queueGuid": "1a",
      "ticket": "A1",
      "operator": "1",
      "status": "waiting"
    },
    {
      "queueGuid": "2a",
      "ticket": "A2",
      "operator": "2",
      "status": "waiting"
    },
    {
      "queueGuid": "3a",
      "ticket": "A3",
      "operator": "3",
      "status": "service"
    },
    {
      "queueGuid": "4a",
      "ticket": "A4",
      "operator": "4",
      "status": "waiting"
    },
    {
      "queueGuid": "5a",
      "ticket": "A5",
      "operator": "5",
      "status": "waiting"
    },
    {
      "queueGuid": "6a",
      "ticket": "A6",
      "operator": "6",
      "status": "service"
    },
    {
      "queueGuid": "7a",
      "ticket": "A7",
      "operator": "7",
      "status": "waiting"
    },
    {
      "queueGuid": "8a",
      "ticket": "A8",
      "operator": "8",
      "status": "service"
    },
    {
      "queueGuid": "9a",
      "ticket": "A9",
      "operator": "9",
      "status": "service"
    },
    {
      "queueGuid": "10a",
      "ticket": "A10",
      "operator": "10",
      "status": "waiting"
    },
    {
      "queueGuid": "11a",
      "ticket": "A11",
      "operator": "11",
      "status": "waiting"
    },
    {
      "queueGuid": "12a",
      "ticket": "A12",
      "operator": "12",
      "status": "service"
    },
    {
      "queueGuid": "13a",
      "ticket": "A13",
      "operator": "13",
      "status": "service"
    },
    {
      "queueGuid": "14a",
      "ticket": "A14",
      "operator": "14",
      "status": "waiting"
    },
    {
      "queueGuid": "15a",
      "ticket": "A15",
      "operator": "15",
      "status": "service"
    },
    {
      "queueGuid": "16a",
      "ticket": "A16",
      "operator": "16",
      "status": "waiting"
    },
    {
      "queueGuid": "17a",
      "ticket": "A17",
      "operator": "17",
      "status": "service"
    },
    {
      "queueGuid": "18a",
      "ticket": "A18",
      "operator": "18",
      "status": "waiting"
    },
    {
      "queueGuid": "19a",
      "ticket": "A19",
      "operator": "19",
      "status": "waiting"
    },
    {
      "queueGuid": "20a",
      "ticket": "A20",
      "operator": "20",
      "status": "service"
    },
    {
      "queueGuid": "21a",
      "ticket": "A21",
      "operator": "21",
      "status": "service"
    },
    {
      "queueGuid": "22a",
      "ticket": "A22",
      "operator": "22",
      "status": "waiting"
    },
    {
      "queueGuid": "23a",
      "ticket": "A23",
      "operator": "23",
      "status": "waiting"
    },
    
    {
      "queueGuid": "29a",
      "ticket": "A29",
      "operator": "29",
      "status": "service"
    }
    ]`;


    var newListTimeout = 2000;
    var totalPosition = 0;
    var interval;


    function mainMethod(jsonExample, sectorName) {
        clearInterval(interval);
        $("#qScreen9 .mainGrid ul").css("transform", "translateY(0px)");
        totalPosition = 0;
        $("#qScreen9 .mainGrid ul").empty();
        $("#qScreen9 .contentRow .sectorName h3").text('SEKTOR - ' + sectorName);

        var jsonArray = JSON.parse(jsonExample);

        jsonArray.forEach(item => {
            appendLi(item.queueGuid, item.ticket, item.operator, item.status);
        });
        var liHeight = $("#qScreen9 .mainGrid").outerHeight() / 10;
        $("#qScreen9 .mainGrid li").css("height", liHeight + "px");

        interval = setInterval(() => {
            var currentLiHeight = $("#qScreen9 .mainGrid").outerHeight();
            var ulHeight = $("#qScreen9 .mainGrid ul").outerHeight();
            totalPosition += currentLiHeight;
            if (totalPosition >= ulHeight) {
                totalPosition = 0;
            }
            $("#qScreen9 .mainGrid ul").css("transform", "translateY(-" + totalPosition + "px)");
        }, newListTimeout);

        setFooterCount();
        updateRightContent();
        addEmptyLi();
    }


    function setFooterCount() {
        var inserviceCount = $("#qScreen9 .mainGrid li .row.greenBg").length;
        var waitingCount = $("#qScreen9 .mainGrid li .row.redBg").length;
        $("#qScreen9 .slideItem .totalCount .col-6:first-of-type h4").text(inserviceCount);
        $("#qScreen9 .slideItem .totalCount .col-6:last-of-type h4").text(waitingCount);
    }

    function updateRightContent() {
        var allRed = $("#qScreen9 .mainGrid li .row.redBg");
        var obj1 = {};
        var obj2 = {};
        var obj3 = {};

        obj1.ticket = $(allRed[allRed.length - 3]).find(".ticket").text().trim();
        obj1.operator = $(allRed[allRed.length - 3]).find(".operatorNo").text().trim();

        obj2.ticket = $(allRed[allRed.length - 2]).find(".ticket").text().trim();
        obj2.operator = $(allRed[allRed.length - 2]).find(".operatorNo").text().trim();

        obj3.ticket = $(allRed[allRed.length - 1]).find(".ticket").text().trim();
        obj3.operator = $(allRed[allRed.length - 1]).find(".operatorNo").text().trim();

        if (obj1.ticket) {
            $("#qScreen9 #user_1 text.cls-3").text(obj1.ticket);
            $("#qScreen9 #user_1 text.cls-4").text(obj1.operator);
            $("#qScreen9 #user_1").parents("li").removeClass("d-none");
        }
        else {
            $("#qScreen9 #user_1").parents("li").addClass("d-none");
            $("#qScreen9 #user_1 text.cls-3").text();
            $("#qScreen9 #user_1 text.cls-4").text();
        }

        if (obj2.ticket) {
            $("#qScreen9 #user_2 text.cls-3").text(obj2.ticket);
            $("#qScreen9 #user_2 text.cls-4").text(obj2.operator);
            $("#qScreen9 #user_2").parents("li").removeClass("d-none");

        }
        else {
            $("#qScreen9 #user_2").parents("li").addClass("d-none");
            $("#qScreen9 #user_2 text.cls-3").text();
            $("#qScreen9 #user_2 text.cls-4").text();
        }

        if (obj3.ticket) {
            $("#qScreen9 #user_3 text.cls-3").text(obj3.ticket);
            $("#qScreen9 #user_3 text.cls-4").text(obj3.operator);
            $("#qScreen9 #user_3").parents("li").removeClass("d-none");

        }
        else {
            $("#qScreen9 #user_3").parents("li").addClass("d-none");
            $("#qScreen9 #user_3 text.cls-3").text();
            $("#qScreen9 #user_3 text.cls-4").text();
        }
        svgText();
    }

    function appendLi(guid, ticket, operator, status) {
        $("#qScreen9 .mainGrid ul").append(`
        <li data-guid='`+ guid + `'>
            <div class="row text-center no-gutters">
                <div class="col-5">
                    <span class="ticket">
                        `+ ticket + `
                    </span>
                </div>
                <div class="col-2">
                    <span class="arrow">
                        >
                    </span>
                </div>
                <div class="col-5">
                    <span class="operatorNo">
                    `+ operator + `
                    </span>
                </div>
            </div>
        </li>  
  `);
        if (status == "waiting") {
            $("#qScreen9 .mainGrid ul li[data-guid=" + guid + "] .row").addClass("redBg");
        }
        else {
            $("#qScreen9 .mainGrid ul li[data-guid=" + guid + "] .row").addClass("greenBg");
        }
    }

    function addEmptyLi() {
        $("#qScreen9 .mainGrid ul li.emptyLi").remove();
        var liCount = $("#qScreen9 .mainGrid ul li").length;
        var emptyCount = 10 - liCount % 10;

        if (emptyCount != 10 || liCount == 0) {
            for (let l = 0; l < emptyCount; l++) {
                $("#qScreen9 .mainGrid ul").append(`<li class="emptyLi"></li>`);
            }
            var liHeight = $("#qScreen9 .mainGrid").outerHeight() / 10;
            $("#qScreen9 .mainGrid li").css("height", liHeight + "px");
        }


    }

    //mainMethod(jsonExample, sectorName);

    $(window).resize(function () {
        var liHeight = $("#qScreen9 .mainGrid").outerHeight() / 10;
        $("#qScreen9 .mainGrid li").css("height", liHeight + "px");
    });

    // var newData = {
    //     "queueGuid": "30a",
    //     "ticket": "A30",
    //     "operator": "30",
    //     "status": "waiting"
    // };

    // var updated = {
    //     "queueGuid": "3sa",
    //     "ticket": "A124",
    //     "operator": "423",
    //     "status": "waiting"
    // };
    // var removeId = "1a";

    function addData(newData) {
        appendLi(newData.queueGuid, newData.ticket, newData.operator, newData.status);
        setFooterCount();
        updateRightContent();
        addEmptyLi();
    }

    function removeData(removeId) {
        if ($("#qScreen9 .mainGrid ul li[data-guid=" + removeId + "]").length) {
            $("#qScreen9 .mainGrid ul li[data-guid=" + removeId + "]").remove();
            setFooterCount();
            updateRightContent();
            addEmptyLi();
        }
    }

    function updateData(updatedData) {
        if ($("#qScreen9 .mainGrid ul li[data-guid=" + updatedData.queueGuid + "]").length) {
            $("#qScreen9 .mainGrid ul li[data-guid=" + updatedData.queueGuid + "] .ticket").text(updatedData.ticket);
            $("#qScreen9 .mainGrid ul li[data-guid=" + updatedData.queueGuid + "] .operatorNo").text(updatedData.operator);

            if (updatedData.status == "waiting") {
                $("#qScreen9 .mainGrid ul li[data-guid=" + updatedData.queueGuid + "] .row").removeClass("greenBg").addClass("redBg");
            }
            else {
                $("#qScreen9 .mainGrid ul li[data-guid=" + updatedData.queueGuid + "] .row").removeClass("redBg").addClass("greenBg");
            }

        }
        else {
            appendLi(updatedData.queueGuid, updatedData.ticket, updatedData.operator, "service");
        }
        setFooterCount();
        updateRightContent();
        addEmptyLi();
    }

    // $("img").click(function () {
    //   addData(newData);
    // });

$(document).ready(function () {
    $('#qScreen9 .marquee').marquee({
        speed: 100
    });
})