$(document).ready(function () {



  //------------------XIDMET OLUNUR------------------//
  var exampleJsonForSector = `
    [
        {
          "ticketNo": "001",
          "operatorNo": "1",
          "sectorName": "A"
        },
        {
          "ticketNo": "002",
          "operatorNo": "2",
          "sectorName": "A"
        },
        {
          "ticketNo": "003",
          "operatorNo": "3",
          "sectorName": "A"
        },
        {
          "ticketNo": "004",
          "operatorNo": "4",
          "sectorName": "B"
        },
        {
          "ticketNo": "005",
          "operatorNo": "5",
          "sectorName": "B"
        },
        {
          "ticketNo": "006",
          "operatorNo": "6",
          "sectorName": "B"
        },
        {
          "ticketNo": "007",
          "operatorNo": "7",
          "sectorName": "A"
        },
        {
          "ticketNo": "008",
          "operatorNo": "8",
          "sectorName": "A"
        },
        {
          "ticketNo": "009",
          "operatorNo": "9",
          "sectorName": "E"
        },
        {
          "ticketNo": "010",
          "operatorNo": "10",
          "sectorName": "E"
        },
        {
          "ticketNo": "0011",
          "operatorNo": "11",
          "sectorName": "E"
        },
        {
          "ticketNo": "0012",
          "operatorNo": "12",
          "sectorName": "E"
        },
        {
          "ticketNo": "0013",
          "operatorNo": "13",
          "sectorName": "E"
        },
        {
          "ticketNo": "0014",
          "operatorNo": "14",
          "sectorName": "A"
        },
        {
          "ticketNo": "0015",
          "operatorNo": "15",
          "sectorName": "A"
        },
        {
          "ticketNo": "0016",
          "operatorNo": "61",
          "sectorName": "A"
        },
        {
          "ticketNo": "0017",
          "operatorNo": "17",
          "sectorName": "A"
        },
        {
          "ticketNo": "0018",
          "operatorNo": "18",
          "sectorName": "A"
        },
        {
          "ticketNo": "0019",
          "operatorNo": "19",
          "sectorName": "A"
        },
        {
          "ticketNo": "0021",
          "operatorNo": "21",
          "sectorName": "G"
        },
        {
          "ticketNo": "0031",
          "operatorNo": "31",
          "sectorName": "G"
        },
        {
          "ticketNo": "0041",
          "operatorNo": "41",
          "sectorName": "G"
        },
        {
          "ticketNo": "0051",
          "operatorNo": "51",
          "sectorName": "G"
        },
        {
          "ticketNo": "0061",
          "operatorNo": "61",
          "sectorName": "A"
        }
      ]
    `;
  function getNewJson(exampleJsonForSector) {
    $("#qScreen4 .sectionContent .slider").empty();
    var currentDataSector = JSON.parse(exampleJsonForSector);
    var dataLength = currentDataSector.length;
    var loopCount = Math.ceil(dataLength / 7);
    var currentCount = 0;
    for (let i = 0; i < loopCount; i++) {
      $("#qScreen4 .sectionContent .slider").append(`
    <div class="sliderContent" data-slide='`+ i + `'">
                              <table class="table mTable">
                                <thead>
                                  <tr>
                                    <th>Bilet №</th>
                                    <th>Sektor</th>
                                    <th>Masa</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  
                                </tbody>
                              </table>
                            </div>
    `);
      for (let j = 0; j < 7; j++) {
        currentCount++;
        if (dataLength >= currentCount) {
          var customTr = `
                                <tr>
                                <td>`+ currentDataSector[j].ticketNo + `</td>
                                <td>`+ currentDataSector[j].sectorName + `</td>
                                <td>`+ currentDataSector[j].operatorNo + `</td>
                                </tr>
                `
          $(".sliderContent[data-slide='" + i + "'] tbody").append(customTr);
        }
        else {
          break;
        }

      }

      currentDataSector.splice(0, 7);
    }
    var slideItem = 2;
    var hasLoop = true;
    if (dataLength < 8) {
      slideItem = 1;
    }
    if (dataLength < 15) {
      hasLoop = false;
    }

    $("#qScreen4 .slider").trigger('destroy.owl.carousel');
    $('#qScreen4 .slider').owlCarousel({
      items: slideItem,
      loop: hasLoop,
      autoplay: true,
      autoplayTimeout: 3000
    });

    var defaultHeight = $(".slider .owl-item:first-of-type").outerHeight();
    $(".slider .owl-item").css("height", "" + defaultHeight + "px");
  }
  getNewJson(exampleJsonForSector);
  // ---------------------------------------------//


  //------------------GOZLEYIR------------------//

  var waitingJson = `
  [
    {
      "sectorGuide": "id1",
      "sectorName": "A",
      "data": [
        {
          "operatorGuide": "oId1",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId2",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId3",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id2",
      "sectorName": "B",
      "data": [
        {
          "operatorGuide": "oId11",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId21",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId31",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id3",
      "sectorName": "C",
      "data": [
        {
          "operatorGuide": "oId12",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId22",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId32",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id4",
      "sectorName": "D",
      "data": [
        {
          "operatorGuide": "oId13",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId131",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId231",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId331",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId132",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId232",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId332",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId133",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId233",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId333",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId134",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId234",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId334",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id5",
      "sectorName": "F",
      "data": [
        {
          "operatorGuide": "oId14",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId24",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId34",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id6",
      "sectorName": "G",
      "data": [
        {
          "operatorGuide": "oId110",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId210",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId310",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id7",
      "sectorName": "H",
      "data": [
        {
          "operatorGuide": "oId120",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId220",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId320",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id8",
      "sectorName": "I",
      "data": [
        {
          "operatorGuide": "oId1300",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId2300",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId3300",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13100",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23100",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33100",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13200",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23200",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33200",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13300",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23300",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33300",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13400",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23400",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33400",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id1999",
      "sectorName": "J",
      "data": [
        {
          "operatorGuide": "oId1999",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId2999",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId3999",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id2999",
      "sectorName": "K",
      "data": [
        {
          "operatorGuide": "oId11999",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId21999",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId31999",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id3999",
      "sectorName": "L",
      "data": [
        {
          "operatorGuide": "oId12987",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId22457",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId321346",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id41v3ct",
      "sectorName": "M",
      "data": [
        {
          "operatorGuide": "oId1313464",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23143ct",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId331cvt34",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId131c134vt3",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId2311b3vctx4",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33134ct4",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId132v34ct34tv",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId232t4v3ct34",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId332dfnhA",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13334wgwg",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId233eghhyq3hy",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId333q5rhq3g34g",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId134q34gqw3g",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId234gqagq3gh",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId334a4wegh34g",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id5a4g34ta43eyg",
      "sectorName": "N",
      "data": [
        {
          "operatorGuide": "oId14agh34h34g",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId24aw34gtaq3g",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId34ahg43qh3q",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id634gt176yk",
      "sectorName": "O",
      "data": [
        {
          "operatorGuide": "oId11035w4tjw46jn",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId210w45jhw45j45",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId310ser5tj45j45",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id7adh45",
      "sectorName": "R",
      "data": [
        {
          "operatorGuide": "oId120sth5th",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId220erwuq3hy",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId320wjtqw345hj",
          "operatorName": "19"
        }
      ]
    },
    {
      "sectorGuide": "id8q34yq34",
      "sectorName": "S",
      "data": [
        {
          "operatorGuide": "oId1300q3hyq3hy",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId2300234qgy4h",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId3300aeg34gg",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13100a4gtj",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23100aerh34",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33100hq35h",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13200hq45bg",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23200aW4HYA34G",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33200AE4H34HY",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId13300EARHYEH",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23300JHG",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33300WE6Y",
          "operatorName": "19"
        },
        {
          "operatorGuide": "oId134004G34G",
          "operatorName": "17"
        },
        {
          "operatorGuide": "oId23400SDFHG",
          "operatorName": "18"
        },
        {
          "operatorGuide": "oId33400QWETYH",
          "operatorName": "19"
        }
      ]
    }
  ]
`;



  // get json method
  function getNewJson2(waitingJson) {
    // $("#loader").fadeIn("fast");
    $(".sectionContent2").empty();
    var currentDataSector2 = JSON.parse(waitingJson);
    currentDataSector2.forEach((item) => {
      var customElement = `
      <div class="row showNow" id="`+ item.sectorGuide + `">
                  <div class="col-2 p-0">
                    <div class="sectorName">
                      <h1>`+ item.sectorName + `</h1>
                    </div>
                  </div>
                  <div class="col-10">
                    <div class="queueArea">
                      <ul>
                      </ul>
                    </div>
                  </div>
                </div>
      `;

      $(".sectionContent2").append(customElement);
      for (let i = 0; i < $(item.data).length; i++) {
        var customLi = `
          <li id="`+ item.data[i].operatorGuide + `">
          <span>`+ item.data[i].operatorName + `</span>
        </li>
          `;
        $(".sectionContent2 #" + item.sectorGuide + " ul").append(customLi);
      }
    });

    $(".sectionContent2>.row:first-of-type").addClass("firstRowShow");
    // $("#loader").fadeOut("slow");
  }
  getNewJson2(waitingJson);

  // data change
  // $(document).click(function () {
  //   getNewJson2(waitingJson);
  //   startShowingRow();
  // })

  var bodyHeight = $(".sectionContent2").outerHeight() - 60;
  var startFrom = 0;
  startShowingRow();

  var timeout = 2000;
  var lastElementTimeout = 3000;


  var intervalID = null;

  function intervalManager(flag, animateInterval, time) {
    if (flag)
      intervalID = setInterval(animateInterval, time);
    else
      clearInterval(intervalID);
  }

  intervalManager(true, animateInterval, timeout);


  function animateInterval() {
    var currentRow = $(".firstRowShow");
    var currentTransform = Number(String($(".sectionContent2").css('transform').split(",")[5]).slice(0, -1));
    var firstRow = -($(currentRow).outerHeight() + 15);

    currentTransform += firstRow;
    if (!$(".sectionContent2>.row:last-of-type").hasClass("showNow")) {

      $(".firstRowShow+.row").addClass("firstRowShow");
      $(currentRow).removeClass("firstRowShow");
      $(currentRow).removeClass("showNow");

      $(".sectionContent2").css("transform", "translateY(" + currentTransform + "px)");

      var showenArr = $(".showNow");
      var showenHeight = 0;
      for (let j = 0; j < showenArr.length; j++) {
        showenHeight += ($(showenArr[j]).outerHeight() + 15);
      }

      var nextShow = $(".sectionContent2 .row.showNow").last().next();

      while (($(nextShow).outerHeight() + showenHeight) <= bodyHeight) {
        $(nextShow).addClass("showNow");

        showenHeight += $(nextShow).outerHeight();
        nextShow = $(".sectionContent2 .row.showNow").last().next();
      }
    }
    else {
      intervalManager(false);
      setTimeout(() => {
        $(".sectionContent2 .row:first-of-type").addClass("firstRowShow");
        $(currentRow).removeClass("firstRowShow");

        var rowArrSecond = $(".sectionContent2>.row");
        var totalHeight2 = 0;
        $(".sectionContent2>.row").addClass("showNow");
        for (let i = 0; i < rowArrSecond.length; i++) {
          totalHeight2 += $(rowArrSecond[i]).outerHeight() + 15;
          if (totalHeight2 > bodyHeight) {
            $(rowArrSecond[i]).removeClass("showNow");
          }
        }
        $(".sectionContent2").css("transition", "all 0s");
        $(".sectionContent2").css("transform", "translateY(0px)");
        setTimeout(() => {
          $(".sectionContent2").css("transition", "all 0.5s");
        }, timeout);
        intervalManager(true, animateInterval, timeout);
      }, lastElementTimeout);
    }
  }
  function startShowingRow() {
    var rowArrFirst = $(".sectionContent2>.row");
    var totalHeight = 0;
    for (let i = startFrom; i < rowArrFirst.length; i++) {
      totalHeight += $(rowArrFirst[i]).outerHeight() + 15;
      if (totalHeight > bodyHeight) {
        $(rowArrFirst[i]).removeClass("showNow");
      }
    }
    $(".sectionContent2").css("transform", "translateY(0px)");

  }

})