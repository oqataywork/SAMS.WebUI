$(document).ready(function () {
  $('.marquee').marquee({
    speed: 100
  });
  var jsonExample = `[
    {
      "id": "1",
      "sector": "A",
      "list": [
        {
          "ticket": "A1",
          "operator": "1"
        },
        {
          "ticket": "A2",
          "operator": "2"
        },
        {
          "ticket": "A3",
          "operator": "3"
        },
        {
          "ticket": "A4",
          "operator": "4"
        },
        {
          "ticket": "A5",
          "operator": "5"
        },
        {
          "ticket": "A6",
          "operator": "6"
        },
        {
          "ticket": "A7",
          "operator": "7"
        },
        {
          "ticket": "A8",
          "operator": "8"
        },
        {
          "ticket": "A9",
          "operator": "9"
        },
        {
          "ticket": "A10",
          "operator": "10"
        },
        {
          "ticket": "A11",
          "operator": "11"
        },
        {
          "ticket": "A12",
          "operator": "12"
        },
        {
          "ticket": "A13",
          "operator": "13"
        },
        {
          "ticket": "A14",
          "operator": "14"
        },
        {
          "ticket": "A15",
          "operator": "15"
        },
        {
          "ticket": "A16",
          "operator": "16"
        },
        {
          "ticket": "A17",
          "operator": "17"
        },
        {
          "ticket": "A18",
          "operator": "18"
        }
        ]
    },
    {
      "id": "2",
      "sector": "B",
      "list": [
        {
          "ticket": "B1",
          "operator": "1"
        },
        {
          "ticket": "B2",
          "operator": "2"
        },
        {
          "ticket": "B3",
          "operator": "3"
        },
        {
          "ticket": "B4",
          "operator": "4"
        },
        {
          "ticket": "B5",
          "operator": "5"
        },
        {
          "ticket": "B6",
          "operator": "6"
        }
        ]
    },
    {
      "id": "3",
      "sector": "C",
      "list": [
        {
          "ticket": "C1",
          "operator": "1"
        },
        {
          "ticket": "C2",
          "operator": "2"
        },
        {
          "ticket": "C3",
          "operator": "3"
        },
        {
          "ticket": "C4",
          "operator": "4"
        },
        {
          "ticket": "C5",
          "operator": "5"
        },
        {
          "ticket": "C6",
          "operator": "6"
        },
        {
          "ticket": "C7",
          "operator": "7"
        },
        {
          "ticket": "C8",
          "operator": "8"
        },
        {
          "ticket": "C9",
          "operator": "9"
        },
        {
          "ticket": "C10",
          "operator": "10"
        },
        {
          "ticket": "C11",
          "operator": "11"
        },
        {
          "ticket": "C12",
          "operator": "12"
        },
        {
          "ticket": "C13",
          "operator": "13"
        },
        {
          "ticket": "C14",
          "operator": "14"
        },
        {
          "ticket": "C15",
          "operator": "15"
        },
        {
          "ticket": "C16",
          "operator": "16"
        }
        ]
    },
    {
      "id": "4",
      "sector": "D",
      "list": [
        {
          "ticket": "D1",
          "operator": "1"
        },
        {
          "ticket": "D2",
          "operator": "2"
        },
        {
          "ticket": "D3",
          "operator": "3"
        },
        {
          "ticket": "D4",
          "operator": "4"
        },
        {
          "ticket": "D5",
          "operator": "5"
        },
        {
          "ticket": "D6",
          "operator": "6"
        },
        {
          "ticket": "D7",
          "operator": "7"
        },
        {
          "ticket": "D8",
          "operator": "8"
        }        
        ]
    },
    {
      "id": "5",
      "sector": "E",
      "list": [
        {
          "ticket": "E1",
          "operator": "1"
        },
        {
          "ticket": "E2",
          "operator": "2"
        },
        {
          "ticket": "E3",
          "operator": "3"
        },
        {
          "ticket": "E4",
          "operator": "4"
        },
        {
          "ticket": "E5",
          "operator": "5"
        },
        {
          "ticket": "E6",
          "operator": "6"
        },
        {
          "ticket": "E7",
          "operator": "7"
        },
        {
          "ticket": "E8",
          "operator": "8"
        },
        {
          "ticket": "E9",
          "operator": "9"
        },
        {
          "ticket": "E10",
          "operator": "10"
        },
        {
          "ticket": "E11",
          "operator": "11"
        },
        {
          "ticket": "E12",
          "operator": "12"
        },
        {
          "ticket": "E13",
          "operator": "13"
        },
        {
          "ticket": "E14",
          "operator": "14"
        },
        {
          "ticket": "E15",
          "operator": "15"
        },
        {
          "ticket": "E16",
          "operator": "16"
        },
        {
          "ticket": "E17",
          "operator": "17"
        },
        {
          "ticket": "E18",
          "operator": "18"
        }
        ]
    }
    ]`;

  var json1 = JSON.parse(jsonExample);

  json1.forEach(item => {
    var sliderElement = `
        <div class="slideItem" id="sliderId`+ item.id + `">
                    <div class="sectorName text-center">
                      <h3>`+ item.sector + `</h3>
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
                      <h4>`+ item.list.length + `</h4>
                    </div>
                  </div>
        `;
    $("#qScreen5 .slider2").append(sliderElement);

    var lists = item.list;

    for (let i = 0; i < lists.length; i++) {
      $("#sliderId" + item.id + " ul").append(`
          <li>
            <div class="row text-center">
              <div class="col-5">
                <span class="ticket">
                  `+ lists[i].ticket + `
                </span>
              </div>
              <div class="col-2">
                <span>
                >
                </span>
              </div>
              <div class="col-5">
                <span class="operatorNo">
                `+ lists[i].operator + `
                </span>
              </div>
            </div>
          </li>
        `)
    }
    setInterval(() => {
      var liHeight = $("#sliderId" + item.id + " ul li:first-of-type").outerHeight();
      var currentScrolling = $("#sliderId" + item.id + " ul").scrollTop();
      var nextScrolling = currentScrolling + liHeight;

      $("#sliderId" + item.id + " ul").scrollTop(nextScrolling);

      currentScrolling = $("#sliderId" + item.id + " ul").scrollTop();
      if (currentScrolling != nextScrolling) {
        $("#sliderId" + item.id + " ul").scrollTop(0);
      }
    }, 1000);

  });

  $('#qScreen5 .slider2').owlCarousel({
    items: 3,
    loop: true,
    dots: false,
    autoplay: true,
    autoplayTimeout: 5000
  });


})