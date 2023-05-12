$(document).ready(function () {
  var exampleJsonForSector = `
  [
      {
        "sectorGuide": "id1",
        "sectorName": "A",
        "data": [
          {
            "operatorGuide": "oId1",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId2",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId3",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id2",
        "sectorName": "B",
        "data": [
          {
            "operatorGuide": "oId11",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId21",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId31",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id3",
        "sectorName": "C",
        "data": [
          {
            "operatorGuide": "oId12",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId22",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId32",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id4",
        "sectorName": "D",
        "data": [
          {
            "operatorGuide": "oId13",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId131",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId231",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId331",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId132",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId232",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId332",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId133",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId233",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId333",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId134",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId234",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId334",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id5",
        "sectorName": "F",
        "data": [
          {
            "operatorGuide": "oId14",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId24",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId34",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id6",
        "sectorName": "G",
        "data": [
          {
            "operatorGuide": "oId110",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId210",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId310",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id7",
        "sectorName": "H",
        "data": [
          {
            "operatorGuide": "oId120",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId220",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId320",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id8",
        "sectorName": "I",
        "data": [
          {
            "operatorGuide": "oId1300",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId2300",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId3300",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13100",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23100",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33100",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13200",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23200",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33200",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13300",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23300",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33300",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13400",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23400",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33400",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id1999",
        "sectorName": "J",
        "data": [
          {
            "operatorGuide": "oId1999",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId2999",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId3999",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id2999",
        "sectorName": "K",
        "data": [
          {
            "operatorGuide": "oId11999",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId21999",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId31999",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id3999",
        "sectorName": "L",
        "data": [
          {
            "operatorGuide": "oId12987",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId22457",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId321346",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id41v3ct",
        "sectorName": "M",
        "data": [
          {
            "operatorGuide": "oId1313464",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23143ct",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId331cvt34",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId131c134vt3",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId2311b3vctx4",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33134ct4",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId132v34ct34tv",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId232t4v3ct34",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId332dfnhA",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13334wgwg",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId233eghhyq3hy",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId333q5rhq3g34g",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId134q34gqw3g",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId234gqagq3gh",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId334a4wegh34g",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id5a4g34ta43eyg",
        "sectorName": "N",
        "data": [
          {
            "operatorGuide": "oId14agh34h34g",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId24aw34gtaq3g",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId34ahg43qh3q",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id634gt176yk",
        "sectorName": "O",
        "data": [
          {
            "operatorGuide": "oId11035w4tjw46jn",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId210w45jhw45j45",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId310ser5tj45j45",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id7adh45",
        "sectorName": "R",
        "data": [
          {
            "operatorGuide": "oId120sth5th",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId220erwuq3hy",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId320wjtqw345hj",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      },
      {
        "sectorGuide": "id8q34yq34",
        "sectorName": "S",
        "data": [
          {
            "operatorGuide": "oId1300q3hyq3hy",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId2300234qgy4h",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId3300aeg34gg",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13100a4gtj",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23100aerh34",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33100hq35h",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13200hq45bg",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23200aW4HYA34G",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33200AE4H34HY",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId13300EARHYEH",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23300JHG",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33300WE6Y",
            "operatorName": "19",
            "queueName": "013"
          },
          {
            "operatorGuide": "oId134004G34G",
            "operatorName": "17",
            "queueName": "011"
          },
          {
            "operatorGuide": "oId23400SDFHG",
            "operatorName": "18",
            "queueName": "012"
          },
          {
            "operatorGuide": "oId33400QWETYH",
            "operatorName": "19",
            "queueName": "013"
          }
        ]
      }
    ]
  `;

  // get json method
  function getNewJson(exampleJsonForSector) {
    $("#loader").fadeIn("fast");
    $(".mainGridArea").empty();
    var currentDataSector = JSON.parse(exampleJsonForSector);
    currentDataSector.forEach((item) => {
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

      $(".mainGridArea").append(customElement);

      for (let i = 0; i < item.data.length; i++) {
        var customLi = `
            <li id="`+ item.data[i].operatorGuide + `">
            <span>`+ item.data[i].operatorName + `</span>
            <span>`+ item.data[i].queueName + `</span>
          </li>
            `;
        $(".mainGridArea #" + item.sectorGuide + " ul").append(customLi);
      }
    });
    $(".mainGridArea>.row:first-of-type").addClass("firstRowShow");
    $("#loader").fadeOut("slow");
  }


  // $(document).click(function () {
  getNewJson(exampleJsonForSector);
  // })

  var bodyHeight = $("body").outerHeight() - 60;
  var startFrom = 0;
  startShowingRow();

  var timeout = 5000;
  var lastElementTimeout = 10000;


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
    var currentTransform = Number(String($(".mainGridArea").css('transform').split(",")[5]).slice(0, -1));
    var firstRow = -($(currentRow).outerHeight() + 15);

    currentTransform += firstRow;
    if (!$(".mainGridArea>.row:last-of-type").hasClass("showNow")) {

      $(".firstRowShow+.row").addClass("firstRowShow");
      $(currentRow).removeClass("firstRowShow");
      $(currentRow).removeClass("showNow");

      $(".mainGridArea").css("transform", "translateY(" + currentTransform + "px)");

      var showenArr = $(".showNow");
      var showenHeight = 0;
      for (let j = 0; j < showenArr.length; j++) {
        showenHeight += ($(showenArr[j]).outerHeight() + 15);
      }

      var nextShow = $(".mainGridArea .row.showNow").last().next();

      while (($(nextShow).outerHeight() + showenHeight) <= bodyHeight) {
        $(nextShow).addClass("showNow");

        showenHeight += $(nextShow).outerHeight();
        nextShow = $(".mainGridArea .row.showNow").last().next();
      }
    }
    else {
      intervalManager(false);
      setTimeout(() => {
        $(".mainGridArea .row:first-of-type").addClass("firstRowShow");
        $(currentRow).removeClass("firstRowShow");

        var rowArrSecond = $(".mainGridArea>.row");
        var totalHeight2 = 0;
        $(".mainGridArea>.row").addClass("showNow");
        for (let i = 0; i < rowArrSecond.length; i++) {
          totalHeight2 += $(rowArrSecond[i]).outerHeight() + 15;
          if (totalHeight2 > bodyHeight) {
            $(rowArrSecond[i]).removeClass("showNow");
          }
        }
        $(".mainGridArea").css("transform", "translateY(0px)");
        intervalManager(true, animateInterval, timeout);
      }, lastElementTimeout);
    }
  }
  function startShowingRow() {
    var rowArrFirst = $(".mainGridArea>.row");
    var totalHeight = 0;
    for (let i = startFrom; i < rowArrFirst.length; i++) {
      totalHeight += $(rowArrFirst[i]).outerHeight() + 15;
      if (totalHeight > bodyHeight) {
        $(rowArrFirst[i]).removeClass("showNow");
      }
    }
  }


  //CHANGE SECTOR ITEM 
  $(document).click(function () {
    var n = Math.floor(Math.random() * 200); changeItem("oId232", n);
    n = Math.floor(Math.random() * 200); changeItem("oId132", n);
    n = Math.floor(Math.random() * 200); changeItem("oId320", n);
    n = Math.floor(Math.random() * 200); changeItem("oId110", n);
  })


  function changeItem(operatorGuide, queueName) {
    var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz.,/!@#$%^&*()_+=-'.split('');

    var currentText = $(".mainGridArea #" + operatorGuide + " span:last-of-type").text().split("");

    for (let q = 0; q < 900; q++) {
      setTimeout(() => {
        for (let k = 0; k < currentText.length; k++) {
          var randChar = Math.floor(Math.random() * chars.length);
          currentText[k] = chars[randChar];
        }
        var resultTxt = currentText.join("");
        $(".mainGridArea #" + operatorGuide + " span:last-of-type").text(resultTxt);
      }, 300);


    }
    setTimeout(() => {
      $(".mainGridArea #" + operatorGuide + " span:last-of-type").text(queueName);
    }, 300);
    // setTimeout(() => {
    // }, 1000);


  }
})