var sectorOrder;

var newListTimeout = 3000;
var mainCarousel;
var showenIntervals;
var totalPositions;
var showenSliderItems = 0;

//#region comment on production

//var sectorOrderJson = `[
//  { "SectorGuid": "guidId1", "SectorName": "A", "SectorOrder": "0" },
//  { "SectorGuid": "guidId2", "SectorName": "B", "SectorOrder": "1" },
//  { "SectorGuid": "guidId3", "SectorName": "C", "SectorOrder": "2" },
//  { "SectorGuid": "guidId4", "SectorName": "D", "SectorOrder": "3" },
//  { "SectorGuid": "guidId5", "SectorName": "E", "SectorOrder": "4" },
//  { "SectorGuid": "guidId6", "SectorName": "F", "SectorOrder": "5" },
//  { "SectorGuid": "guidId7", "SectorName": "G", "SectorOrder": "6" },
//  { "SectorGuid": "guidId8", "SectorName": "H", "SectorOrder": "7" }
//]`;

//var jsonExample = `[
//  {
//    "SectorGuid": "guidId2",
//    "SectorName": "B",
//    "InServiceQueueCount" : "12",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-B1",
//        "Ticket": "B1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-B2",
//        "Ticket": "B2",
//        "OperatorDeskNumber": "2"
//      },
//      {
//        "QueueGuid": "id-B3",
//        "Ticket": "B3",
//        "OperatorDeskNumber": "3"
//      },
//      {
//        "QueueGuid": "id-B4",
//        "Ticket": "B4",
//        "OperatorDeskNumber": "4"
//      },
//      {
//        "QueueGuid": "id-B5",
//        "Ticket": "B5",
//        "OperatorDeskNumber": "5"
//      },
//      {
//        "QueueGuid": "id-B6",
//        "Ticket": "B6",
//        "OperatorDeskNumber": "6"
//      }
//      ]
//  },
//  {
//    "SectorGuid": "guidId3",
//    "SectorName": "C",
//    "InServiceQueueCount" : "8",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-C1",
//        "Ticket": "C1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-C2",
//        "Ticket": "C2",
//        "OperatorDeskNumber": "2"
//      },
//      {
//        "QueueGuid": "id-C3",
//        "Ticket": "C3",
//        "OperatorDeskNumber": "3"
//      },
//      {
//        "QueueGuid": "id-C4",
//        "Ticket": "C4",
//        "OperatorDeskNumber": "4"
//      },
//      {
//        "QueueGuid": "id-C5",
//        "Ticket": "C5",
//        "OperatorDeskNumber": "5"
//      },
//      {
//        "QueueGuid": "id-C6",
//        "Ticket": "C6",
//        "OperatorDeskNumber": "6"
//      },
//      {
//        "QueueGuid": "id-C7",
//        "Ticket": "C7",
//        "OperatorDeskNumber": "7"
//      },
//      {
//        "QueueGuid": "id-C8",
//        "Ticket": "C8",
//        "OperatorDeskNumber": "8"
//      },
//      {
//        "QueueGuid": "id-C9",
//        "Ticket": "C9",
//        "OperatorDeskNumber": "9"
//      },
//      {
//        "QueueGuid": "id-C10",
//        "Ticket": "C10",
//        "OperatorDeskNumber": "10"
//      },
//      {
//        "QueueGuid": "id-C11",
//        "Ticket": "C11",
//        "OperatorDeskNumber": "11"
//      },
//      {
//        "QueueGuid": "id-C12",
//        "Ticket": "C12",
//        "OperatorDeskNumber": "12"
//      },
//      {
//        "QueueGuid": "id-C13",
//        "Ticket": "C13",
//        "OperatorDeskNumber": "13"
//      },
//      {
//        "QueueGuid": "id-C14",
//        "Ticket": "C14",
//        "OperatorDeskNumber": "14"
//      },
//      {
//        "QueueGuid": "id-C15",
//        "Ticket": "C15",
//        "OperatorDeskNumber": "15"
//      },
//      {
//        "QueueGuid": "id-C16",
//        "Ticket": "C16",
//        "OperatorDeskNumber": "16"
//      },
//      {
//        "QueueGuid": "id-C17",
//        "Ticket": "C17",
//        "OperatorDeskNumber": "17"
//      },
//      {
//        "QueueGuid": "id-C22",
//        "Ticket": "C22",
//        "OperatorDeskNumber": "22"
//      }
//      ]
//  },
//  {
//    "SectorGuid": "guidId4",
//    "SectorName": "D",
//    "InServiceQueueCount" : "4",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-D1",
//        "Ticket": "D1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-D2",
//        "Ticket": "D2",
//        "OperatorDeskNumber": "2"
//      },
//      {
//        "QueueGuid": "id-D3",
//        "Ticket": "D3",
//        "OperatorDeskNumber": "3"
//      },
//      {
//        "QueueGuid": "id-D4",
//        "Ticket": "D4",
//        "OperatorDeskNumber": "4"
//      },
//      {
//        "QueueGuid": "id-D5",
//        "Ticket": "D5",
//        "OperatorDeskNumber": "5"
//      },
//      {
//        "QueueGuid": "id-D6",
//        "Ticket": "D6",
//        "OperatorDeskNumber": "6"
//      },
//      {
//        "QueueGuid": "id-D7",
//        "Ticket": "D7",
//        "OperatorDeskNumber": "7"
//      },
//      {
//        "QueueGuid": "id-D8",
//        "Ticket": "D8",
//        "OperatorDeskNumber": "8"
//      }        
//      ]
//  },
//  {
//    "SectorGuid": "guidId8",
//    "SectorName": "F",
//    "InServiceQueueCount": "22",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-F1",
//        "Ticket": "F1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-F2",
//        "Ticket": "F2",
//        "OperatorDeskNumber": "2"
//      },
//      {
//        "QueueGuid": "id-F3",
//        "Ticket": "F3",
//        "OperatorDeskNumber": "3"
//      },
//      {
//        "QueueGuid": "id-F4",
//        "Ticket": "F4",
//        "OperatorDeskNumber": "4"
//      },
//      {
//        "QueueGuid": "id-F5",
//        "Ticket": "F5",
//        "OperatorDeskNumber": "5"
//      },
//      {
//        "QueueGuid": "id-F6",
//        "Ticket": "F6",
//        "OperatorDeskNumber": "6"
//      },
//      {
//        "QueueGuid": "id-F7",
//        "Ticket": "F7",
//        "OperatorDeskNumber": "7"
//      },
//      {
//        "QueueGuid": "id-F8",
//        "Ticket": "F8",
//        "OperatorDeskNumber": "8"
//      },
//      {
//        "QueueGuid": "id-F9",
//        "Ticket": "F9",
//        "OperatorDeskNumber": "9"
//      },
//      {
//        "QueueGuid": "id-F10",
//        "Ticket": "F10",
//        "OperatorDeskNumber": "10"
//      },
//      {
//        "QueueGuid": "id-F11",
//        "Ticket": "F11",
//        "OperatorDeskNumber": "11"
//      },
//      {
//        "QueueGuid": "id-F12",
//        "Ticket": "F12",
//        "OperatorDeskNumber": "12"
//      },
//      {
//        "QueueGuid": "id-F13",
//        "Ticket": "F13",
//        "OperatorDeskNumber": "13"
//      }
//    ]
//  },
//  {
//    "SectorGuid": "guidId7",
//    "SectorName": "G",
//    "InServiceQueueCount" : "22",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-G1",
//        "Ticket": "G1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-G2",
//        "Ticket": "G2",
//        "OperatorDeskNumber": "2"
//      },
//      {
//        "QueueGuid": "id-G3",
//        "Ticket": "G3",
//        "OperatorDeskNumber": "3"
//      },
//      {
//        "QueueGuid": "id-G4",
//        "Ticket": "G4",
//        "OperatorDeskNumber": "4"
//      },
//      {
//        "QueueGuid": "id-G5",
//        "Ticket": "G5",
//        "OperatorDeskNumber": "5"
//      },
//      {
//        "QueueGuid": "id-G6",
//        "Ticket": "G6",
//        "OperatorDeskNumber": "6"
//      },
//      {
//        "QueueGuid": "id-G7",
//        "Ticket": "G7",
//        "OperatorDeskNumber": "7"
//      },
//      {
//        "QueueGuid": "id-G8",
//        "Ticket": "G8",
//        "OperatorDeskNumber": "8"
//      },
//      {
//        "QueueGuid": "id-G9",
//        "Ticket": "G9",
//        "OperatorDeskNumber": "9"
//      },
//      {
//        "QueueGuid": "id-G10",
//        "Ticket": "G10",
//        "OperatorDeskNumber": "10"
//      },
//      {
//        "QueueGuid": "id-G11",
//        "Ticket": "G11",
//        "OperatorDeskNumber": "11"
//      },
//      {
//        "QueueGuid": "id-G12",
//        "Ticket": "G12",
//        "OperatorDeskNumber": "12"
//      },
//      {
//        "QueueGuid": "id-G13",
//        "Ticket": "G13",
//        "OperatorDeskNumber": "13"
//      },
//      {
//        "QueueGuid": "id-G14",
//        "Ticket": "G14",
//        "OperatorDeskNumber": "14"
//      },
//      {
//        "QueueGuid": "id-G15",
//        "Ticket": "G15",
//        "OperatorDeskNumber": "15"
//      },
//      {
//        "QueueGuid": "id-G16",
//        "Ticket": "G16",
//        "OperatorDeskNumber": "16"
//      },
//      {
//        "QueueGuid": "id-G17",
//        "Ticket": "G17",
//        "OperatorDeskNumber": "17"
//      },
//      {
//        "QueueGuid": "id-G18",
//        "Ticket": "G18",
//        "OperatorDeskNumber": "18"
//      },
//      {
//        "QueueGuid": "id-G19",
//        "Ticket": "G19",
//        "OperatorDeskNumber": "19"
//      },
//      {
//        "QueueGuid": "id-G20",
//        "Ticket": "G20",
//        "OperatorDeskNumber": "20"
//      },
//      {
//        "QueueGuid": "id-G21",
//        "Ticket": "G21",
//        "OperatorDeskNumber": "21"
//      },
//      {
//        "QueueGuid": "id-G22",
//        "Ticket": "G22",
//        "OperatorDeskNumber": "22"
//      },
//      {
//        "QueueGuid": "id-G23",
//        "Ticket": "G23",
//        "OperatorDeskNumber": "23"
//      },
//      {
//        "QueueGuid": "id-G24",
//        "Ticket": "G24",
//        "OperatorDeskNumber": "24"
//      }
//      ]
//  },
//  {
//    "SectorGuid": "guidId9",
//    "SectorName": "H",
//    "InServiceQueueCount" : "22",
//    "QueuesOnSector": [
//      {
//        "QueueGuid": "id-H1",
//        "Ticket": "H1",
//        "OperatorDeskNumber": "1"
//      },
//      {
//        "QueueGuid": "id-H2",
//        "Ticket": "H2",
//        "OperatorDeskNumber": "2"
//      }
//      ]
//  }
  
//  ]`;
//var exampleNewData = {
//    SectorGuid: "guidId4",
//    SectorName: "D-2",
//    QueueGuid: "id-D20",
//    Ticket: "D20",
//    OperatorDeskNumber: "20",
//    InServiceQueueCount: "967",
//};
//var exampleUpdatedData = {
//    SectorGuid: "guidId4",
//    QueueGuid: "id-D2",
//    Ticket: "D2-1",
//    OperatorDeskNumber: "2-1",
//};
//var exampleRemovedData = {
//    SectorGuid: "guidId4",
//    QueueGuid: "id-D2",
//    SectorName: "D-2",
//    InServiceQueueCount: "967",
//};
//var exampleNewSector = {
//    SectorGuid: "guidId1",
//    SectorName: "A",
//    InServiceQueueCount: "22",
//    QueuesOnSector: [
//        {
//            QueueGuid: "id-F1",
//            Ticket: "F1",
//            OperatorDeskNumber: "1",
//        },
//        {
//            QueueGuid: "id-F2",
//            Ticket: "F2",
//            OperatorDeskNumber: "2",
//        },
//        {
//            QueueGuid: "id-F3",
//            Ticket: "F3",
//            OperatorDeskNumber: "3",
//        },
//        {
//            QueueGuid: "id-F4",
//            Ticket: "F4",
//            OperatorDeskNumber: "4",
//        },
//        {
//            QueueGuid: "id-F5",
//            Ticket: "F5",
//            OperatorDeskNumber: "5",
//        },
//        {
//            QueueGuid: "id-F6",
//            Ticket: "F6",
//            OperatorDeskNumber: "6",
//        },
//        {
//            QueueGuid: "id-F7",
//            Ticket: "F7",
//            OperatorDeskNumber: "7",
//        },
//        {
//            QueueGuid: "id-F8",
//            Ticket: "F8",
//            OperatorDeskNumber: "8",
//        },
//        {
//            QueueGuid: "id-F9",
//            Ticket: "F9",
//            OperatorDeskNumber: "9",
//        },
//        {
//            QueueGuid: "id-F10",
//            Ticket: "F10",
//            OperatorDeskNumber: "10",
//        },
//        {
//            QueueGuid: "id-F11",
//            Ticket: "F11",
//            OperatorDeskNumber: "11",
//        },
//        {
//            QueueGuid: "id-F12",
//            Ticket: "F12",
//            OperatorDeskNumber: "12",
//        },
//        {
//            QueueGuid: "id-F13",
//            Ticket: "F13",
//            OperatorDeskNumber: "13",
//        },
//    ],
//};
//var exampleRemovedSectorId = "guidId5";
//var exampleUpdatedSector = {
//    SectorGuid: "guidId2",
//    SectorName: "BB",
//    InServiceQueueCount: "23",
//};

//#endregion comment on production end

$(document).ready(function () {
    var currenttime = $("#qScreen6 .time h1").text().split(":");
    var second = Number(currenttime[2]),
        minute = Number(currenttime[1]),
        hour = Number(currenttime[0]);
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
        $("#qScreen6 .time h1").text(result);
    }, 1000);

    // ------------------------------------------

    // ------------------footer------------------------
    $(".marquee").marquee({
        speed: 100
    });
    // ------------------------------------------

    // ---------------------Main---------------------
    if ($("#qScreen6 .mainCol.col-12").length) {
        showenSliderItems = 5;
    } else {
        showenSliderItems = 3;
    }

    //mainMethod(jsonExample);

    $(window).resize(function () {
        var liHeight = $(".mainGrid").outerHeight() / 10;
        $(".mainGrid li").css("height", liHeight + "px");
    });
});

function getSectorOrder(json) {
    sectorOrder = JSON.parse(json);
}


function mainMethod(jsonExample) {
    if (mainCarousel) {
        mainCarousel.trigger("destroy.owl.carousel");
        $("#qScreen6 .slider2").empty();
    }

    // for frontend
    //getSectorOrder(sectorOrderJson);
    //------

    var json1 = JSON.parse(jsonExample);
    json1.forEach((item) => {
        if (item.QueuesOnSector.length || Number(item.InServiceQueueCount > 0)) {
            var guidId = item.SectorGuid;
            var currOrder;
            sectorOrder.filter((obj) => {
                if (obj.SectorGuid == guidId) {
                    currOrder = obj.SectorOrder;
                }
            });

            //#region slideElement
            var sliderElement =
                `
        <div class="slideItem" data-slider-id="` +
                guidId +
                `" data-order="` +
                currOrder +
                `">
          <div class="sectorName text-center">
            <h3>SEKTOR - `+ item.SectorName + `</h3>
          </div>
          <div class="gridHeading">
            <div class="row no-gutters text-center m-2">
              <div class="col-6">
                <h3>Nömrə</h3>
              </div>
              <div class="col-6">
                <h3>Masa</h3>
              </div>
            </div>
          </div>
          <div class="mainGrid">
            <ul>
              
            </ul>
          </div>
          <div class="totalCount">
            <h4>
            Xidmətdə olanlar:
              ` +
                item.InServiceQueueCount +
                `
            </h4>
          </div>
        </div>
        `;
            //#endregion

            $("#qScreen6 .slider2").append(sliderElement);

            item.QueuesOnSector.forEach((element) => {
                appendLi(
                    guidId,
                    element.QueueGuid,
                    element.Ticket,
                    element.OperatorDeskNumber
                );
            });
            addEmptyLi(guidId);
        }
    });

    if (json1.length > showenSliderItems) {
        var carouselLoop = true;
        var sliderItemsCount = showenSliderItems;
    } else {
        var carouselLoop = false;
        var sliderItemsCount = json1.length;
    }

    mainCarousel = $("#qScreen6 .slider2").owlCarousel({
        items: sliderItemsCount,
        // loop: carouselLoop,
        dots: false,
        touchDrag: false,
        mouseDrag: false,
    });

    var liHeight = $(".mainGrid").outerHeight() / 10;
    $(".mainGrid li").css("height", liHeight + "px");

    beginPlaying();
}


function appendLi(guid, id, ticket, operator) {
    $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] ul").append(
        `
      <li data-guid='`+ id + `'>
          <div class="row text-center no-gutters">
              <div class="col-5">
                  <span class="Ticket">
                      ` + ticket +
        `
                  </span>
              </div>
              <div class="col-2">
                  <span class="arrow">
                      >
                  </span>
              </div>
              <div class="col-5">
                  <span class="operatorNo">
                  ` + operator + `
                  </span>
              </div>
          </div>
      </li>  
`
    );
}

function addEmptyLi(guid) {
    $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] ul li.emptyLi").remove();

    var liCount = $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] ul").eq(0).find("li").length;
    var emptyCount = 10 - (liCount % 10);
    if (emptyCount != 10 || liCount == 0) {
        for (let l = 0; l < emptyCount; l++) {
            $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] ul").append(`<li class="emptyLi"></li>`);
        }

        var liHeight = $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] .mainGrid").eq(0).outerHeight() / 10;
        $("#qScreen6 .mainCol .slideItem[data-slider-id=" + guid + "] li").css("height", liHeight + "px");
    }
}

var goBack = 0;

function goNextSlides() {
    scrollShowens("clearIntervals");
    mainCarousel.on("changed.owl.carousel", function (e) {
        if (e.item.count - e.item.index == showenSliderItems) {
            goBack = 1;
        }
    });
    if (goBack == 0) {
        for (let b = 0; b < showenSliderItems; b++) {
            mainCarousel.trigger("next.owl.carousel", [100]);
        }
    } else {
        mainCarousel.trigger("to.owl.carousel", [0, 100]);
        goBack = 0;
    }

    beginPlaying();
}

function scrollShowens(guidArr) {
    if (guidArr == "clearIntervals") {
        for (let i = 0; i < showenSliderItems; i++) {
            clearInterval(showenIntervals[i]);
        }
        $("#qScreen6 .slideItem .mainGrid ul").css("transform", "translateY(0px)");
    }
    else {
        showenIntervals = [];
        totalPositions = [];
        var longerSlide = getMaximum();
        var currentLiHeight = [];
        var ulHeight = [];

        for (let j = 0; j < showenSliderItems; j++) {
            totalPositions.push(0);
            currentLiHeight.push(0);
            ulHeight.push(0);
        }

        for (let i = 0; i < guidArr.length; i++) {
            showenIntervals[i] = setInterval(() => {
                currentLiHeight[i] = $("#qScreen6 .owl-item.active .slideItem[data-slider-id=" + guidArr[i] + "] .mainGrid").outerHeight();
                ulHeight[i] = $("#qScreen6 .owl-item.active .slideItem[data-slider-id=" + guidArr[i] + "] .mainGrid ul").outerHeight();
                totalPositions[i] += currentLiHeight[i];

                if (totalPositions[i] >= ulHeight[i]) {
                    totalPositions[i] = 0;
                    if (i == longerSlide) {
                        goNextSlides();
                    }
                }

                $("#qScreen6 .owl-item.active .slideItem[data-slider-id=" + guidArr[i] + "] .mainGrid ul").css("transform", "translateY(-" + totalPositions[i] + "px)");
            }, newListTimeout);
        }
    }
}

function beginPlaying() {
    var activeSliders = $("#qScreen6 .owl-item.active .slideItem");
    var sliderIdArr = [];
    for (let i = 0; i < activeSliders.length; i++) {
        sliderIdArr.push($(activeSliders[i]).data("slider-id"));
    }
    scrollShowens(sliderIdArr);
}

function getMaximum() {
    var currentMaximum = 0;
    var activeSlidersArr = $("#qScreen6 .owl-item.active .slideItem ul");
    var result = 0;

    for (let i = 0; i < activeSlidersArr.length; i++) {
        if ($(activeSlidersArr[i]).outerHeight() > currentMaximum) {
            currentMaximum = $(activeSlidersArr[i]).outerHeight();
            result = i;
        }
    }
    return result;
}

function findBeforeItem(currOrder) {
    var showenItems = $("#qScreen6 .mainCol .slideItem");
    var showenItemsOrder = [];
    for (let i = 0; i < showenItems.length; i++) {
        showenItemsOrder.push($(showenItems[i]).data("order"));
    }
    showenItemsOrder.push(Number(currOrder));
    showenItemsOrder.sort();
    var currPalace = showenItemsOrder.indexOf(Number(currOrder));
    if (currOrder != 0) {
        return showenItemsOrder[currPalace - 1];
    } else {
        return -1;
    }
}

//----------- SECTOR CHANGES-------------

function addSector(newSector) {
    var activeEl = $("#qScreen6 .owl-item.active").eq(0).find(".slideItem").data("slider-id");
    var guidId = newSector.SectorGuid;

    var currOrder;
    sectorOrder.filter((obj) => {
        if (obj.SectorGuid == guidId) {
            currOrder = obj.SectorOrder;
        }
    });
    if ($("#qScreen6 .mainCol .slideItem[data-slider-id=" + guidId + "]").length) {
        return false;
    }
    scrollShowens("clearIntervals");
    mainCarousel.trigger("destroy.owl.carousel");
    //#region sliderElement
    var sliderElement =
        `
            <div class="slideItem" data-slider-id="` +
        guidId +
        `" data-order="` +
        currOrder +
        `">
                <div class="sectorName text-center">
                <h3>SEKTOR - `+ newSector.SectorName + `</h3>
                </div>
                <div class="gridHeading">
                <div class="row no-gutters text-center m-2">
                    <div class="col-6">
                    <h3>Nömrə</h3>
                    </div>
                    <div class="col-6">
                    <h3>Masa</h3>
                    </div>
                </div>
                </div>
                <div class="mainGrid">
                <ul>
              
                </ul>
                </div>
                <div class="totalCount">
                <h4>
                    ` +
        newSector.InServiceQueueCount +
        `
                </h4>
                </div>
            </div>
            `;
    //#endregion


    /*
    var itemPalace = findBeforeItem(currOrder);
    if (itemPalace != -1) {
        $("#qScreen6 .slider2 .slideItem[data-order=" + itemPalace + "]").after(sliderElement);
    } else {
        $("#qScreen6 .slider2").prepend(sliderElement); //here added first time
    }
    */
    var itemPalace = findBeforeItem(currOrder);
    if (itemPalace != -1) {
        //$("#qScreen6 .slider2 .slideItem[data-order=" + itemPalace + "]").after(sliderElement);
        $("#qScreen6 .slider2 .slideItem").last().after(sliderElement);
    } else {
        $("#qScreen6 .slider2").prepend(sliderElement); //here added first time
    }
    






    if (newSector.QueuesOnSector) {
        newSector.QueuesOnSector.forEach((element) => {
            appendLi(
                guidId,
                element.QueueGuid,
                element.Ticket,
                element.OperatorDeskNumber
            );
        });
    } else if (newSector.QueueGuid && newSector.Ticket && newSector.OperatorDeskNumber) {
        addData(newSector);
    }

    addEmptyLi(guidId);

    var sliderCnt = $("#qScreen6 .slider2 .slideItem").length;
    if (sliderCnt > showenSliderItems) {
        var carouselLoop = true;
        var sliderItemsCount = showenSliderItems;
    } else {
        var carouselLoop = false;
        var sliderItemsCount = sliderCnt;
    }

    mainCarousel = $("#qScreen6 .slider2").owlCarousel({
        items: sliderItemsCount,
        // loop: carouselLoop,
        dots: false,
        touchDrag: false,
        mouseDrag: false
    });

    var liHeight = $(".mainGrid").outerHeight() / 10;
    $(".mainGrid li").css("height", liHeight + "px");

    var owlItems = $("#qScreen6 .owl-item:not(.cloned)");

    for (let j = 0; j < owlItems.length; j++) {
        if ($(owlItems[j]).find(".slideItem").data("slider-id") == activeEl) {
            break;
        }
        else {
            mainCarousel.trigger("next.owl.carousel", [0]);
        }
    }

    beginPlaying();
}

function removeSector(removedSectorId) {
    var activeEl = $("#qScreen6 .owl-item.active").eq(0).find(".slideItem").data("slider-id");
    if (activeEl == removedSectorId)
        activeEl = $("#qScreen6 .owl-item.active+.owl-item").eq(0).find(".slideItem").data("slider-id");

    if (!$("#qScreen6 .mainCol .slideItem[data-slider-id=" + removedSectorId + "]").length)
        return false;

    scrollShowens("clearIntervals");
    mainCarousel.trigger("destroy.owl.carousel");
    $("#qScreen6 .mainCol .slideItem[data-slider-id=" + removedSectorId + "]").remove();
    var sliderCnt = $("#qScreen6 .slider2 .slideItem").length;
    if (sliderCnt > showenSliderItems) {
        var carouselLoop = true;
        var sliderItemsCount = showenSliderItems;
    }
    else {
        var carouselLoop = false;
        var sliderItemsCount = sliderCnt;
    }
    mainCarousel = $("#qScreen6 .slider2").owlCarousel({
        items: sliderItemsCount,
        // loop: carouselLoop,
        dots: false,
        touchDrag: false,
        mouseDrag: false,
    });

    var owlItems = $("#qScreen6 .owl-item:not(.cloned)");

    for (let j = 0; j < owlItems.length; j++) {
        if ($(owlItems[j]).find(".slideItem").data("slider-id") == activeEl) {
            break;
        } else {
            mainCarousel.trigger("next.owl.carousel", [0]);
        }
    }

    beginPlaying();
}

function updateSector(updatedSector) {
    if (
        !$(
            "#qScreen6 .mainCol .slideItem[data-slider-id=" +
            updatedSector.SectorGuid +
            "]"
        ).length
    ) {
        return false;
    }

    $("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedSector.SectorGuid + "] .sectorName h3").
        text('SEKTOR - ' + updatedSector.SectorName);
    $("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedSector.SectorGuid + "] .totalCount h4").text('Xidmətdə olanlar: ' + updatedSector.InServiceQueueCount);
    var sectorLiCount = $("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedSector.SectorGuid + "] ul li:not(.emptyLi)").length;
    if (Number(updatedSector.InServiceQueueCount) == 0 && sectorLiCount == 0) {
        removeSector(updatedSector.SectorGuid);
    }

}

//----------------------------------------

//-----------DATA CHANGES-----------------

// function updateData(updatedData) {
//   if (!$("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedData.SectorGuid + "] li[data-guid=" + updatedData.QueueGuid + "]").length) {
//     return false;
//   }
//   $("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedData.SectorGuid + "] li[data-guid=" + updatedData.QueueGuid + "] .ticket").text(updatedData.Ticket);
//   $("#qScreen6 .mainCol .slideItem[data-slider-id=" + updatedData.SectorGuid + "] li[data-guid=" + updatedData.QueueGuid + "] .operatorNo").text(updatedData.OperatorDeskNumber);
// }
//----------------------------------------

function addData(newData) {
    if (!$("#qScreen6 .mainCol .slideItem[data-slider-id=" + newData.SectorGuid + "]").length) {
        if ($("#qScreen6 .mainCol .slideItem").length) {
            addSector(newData);
        } else {
            var guidId = newData.SectorGuid;
            var currOrder;
            sectorOrder.filter((obj) => {
                if (obj.SectorGuid == guidId) {
                    currOrder = obj.SectorOrder;
                }
            });
            var sliderElement =
                `
                  <div class="slideItem" data-slider-id="` +
                guidId +
                `" data-order="` +
                currOrder +
                `">
                    <div class="sectorName text-center">
                      <h3>SEKTOR - `+ newData.SectorName + `</h3>
                    </div>
                    <div class="gridHeading">
                      <div class="row no-gutters text-center m-2">
                        <div class="col-6">
                          <h3>Nömrə</h3>
                        </div>
                        <div class="col-6">
                          <h3>Masa</h3>
                        </div>
                      </div>
                    </div>
                    <div class="mainGrid">
                      <ul>
                
                      </ul>
                    </div>
                    <div class="totalCount">
                      <h4> 
                        ` +
                newData.InServiceQueueCount +
                `
                      </h4>
                    </div>
                  </div>
                  `;
            $("#qScreen6 .slider2").append(sliderElement);

            appendLi(
                guidId,
                newData.QueueGuid,
                newData.Ticket,
                newData.OperatorDeskNumber
            );
            addEmptyLi(guidId);
        }
    } else {
        appendLi(
            newData.SectorGuid,
            newData.QueueGuid,
            newData.Ticket,
            newData.OperatorDeskNumber
        );
        addEmptyLi(newData.SectorGuid);
        updateSector(newData);
    }
}

function removeData(removedData) {
    if (
        $(
            "#qScreen6 .mainCol .slideItem[data-slider-id=" +
            removedData.SectorGuid +
            "] li[data-guid=" +
            removedData.QueueGuid +
            "]"
        ).length
    ) {
        $(
            "#qScreen6 .mainCol .slideItem[data-slider-id=" +
            removedData.SectorGuid +
            "] li[data-guid=" +
            removedData.QueueGuid +
            "]"
        ).remove();
        addEmptyLi(removedData.SectorGuid);
    }
    updateSector(removedData);
}

