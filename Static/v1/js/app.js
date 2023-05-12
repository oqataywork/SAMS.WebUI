
URL = window.URL || window.webkitURL;

var gumStream; 						//stream from getUserMedia()
var rec; 							//Recorder.js object
var input; 							//MediaStreamAudioSourceNode we'll be recording

// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext //audio context to help us record

var microphone = false;

	/*-----------------------Operators-------------------------*/
	//var mainUserMinute = 0.1;
	var prInterval;
	var timerInterval;
	function progressRefresh() {
		$("#qOperators .operatorBox.progressFinished").removeClass("progressFinished");
		$("#qOperators .operatorBox .progressBar .progress-bar").attr("aria-valuenow", '0');
		$("#qOperators .operatorBox .progressBar .progress-bar").css("width", "0%");
		clearInterval(prInterval);
	}
    function serviceTimeStart(/*dateObj*/) {

        //if (dateObj) {
        //    $("#qOperators .operatorBox .serviceTime .hour").text(dateObj[0].hours);
        //    $("#qOperators .operatorBox .serviceTime .minute").text(dateObj[1].minutes);
        //    $("#qOperators .operatorBox .serviceTime .second").text(dateObj[2].seconds);
        //}

		var nowHour = $("#qOperators .operatorBox .serviceTime .hour");
		var nowMinute = $("#qOperators .operatorBox .serviceTime .minute");
		var nowSecond = $("#qOperators .operatorBox .serviceTime .second");
		timerInterval = setInterval(() => {

			var h = Number($(nowHour).text());
			var m = Number($(nowMinute).text());
			var s = Number($(nowSecond).text());

			s++;
			if (s < 10) {
				s = "0" + s;
			}
			else if (s > 59) {
				s = "00";
				m++;
				if (m > 59) {
                    m = "00";
					h++;
				}
			}
			if (m < 10) {
				m = "0" + m;
			}
			if (h < 10) {
				h = "0" + h;
			}
			if (h >= 24) {
				h = 0;
			}
			$(nowSecond).text(s);
			$(nowMinute).text(m);
			$(nowHour).text(h);
		}, 1000);
	}
	function timerRefresh() {
		$("#qOperators .operatorBox .serviceTime .hour").text("00");
		$("#qOperators .operatorBox .serviceTime .minute").text("00");
		$("#qOperators .operatorBox .serviceTime .second").text("00");
		clearInterval(timerInterval);
	}

    var breakStatus = false;

	// buttons
	var startService = $("#qOperators #startServiceBtn");
	var acceptQueue = $("#qOperators #acceptQueueBtn");
	var addService = $("#qOperators #addServiceBtn");
    var stopService = $("#qOperators #stopServiceBtn");
	var finishService = $("#qOperators #finishServiceBtn");
	var breakSelect = $("#qOperators #breakSelectBtn");
    var citizen = $("#qOperators #citizenBtn");

	function startServiceBtn(event) {
		var btn = startService;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
	}
	function acceptQueueBtn(event) {
		var btn = acceptQueue;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
	}
	function addServiceBtn(event) {
		var btn = addService;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
	}
	function stopServiceBtn(event) {
		var btn = stopService;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
	}
	function finishServiceBtn(event) {
		var btn = finishService;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
	}
	function breakSelectBtn(event) {
		var btn = breakSelect;
		if (!event) {
			$(btn).attr("disabled", "disabled");
		}
		else {
			$(btn).removeAttr("disabled");
		}
}
    function citizenBtn(event)
    {
        var btn = citizen;
        if (!event) {
            $(btn).attr("disabled", "disabled");
        }
        else {
            $(btn).removeAttr("disabled");
        }
    }

	function queueActivation(event) {
		if (event) {
			$("#qOperators .waitingBox").removeClass("deactive");
		}
		else {
			$("#qOperators .waitingBox").addClass("deactive");
		}
	}
	function checkboxActivation(event) {
		if (event) {
			$("#qOperators .checkboxCol").removeClass("deactive");
		}
		else {
			$("#qOperators .checkboxCol").addClass("deactive");
		}
	}
	function checkboxRefresh() {
        $("#qOperators .checkboxCol input#serviceCompleted").click();
	}

	function operatorStatus(statusType, statusName) {
		$(".forOperators .operatorStatus").removeClass("notServed").removeClass("waitForService").removeClass("inBreak").addClass("" + statusType + "");
		$(".forOperators .operatorStatus .text").text(statusName);
	}
	// recording-------
 //   async function startRecording(microphone) {
 //       var constraints = { audio: true, video: false };

	//	navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
	//		microphone= true;
	//		audioContext = new AudioContext();
	//		gumStream = stream;
	//		input = audioContext.createMediaStreamSource(stream);
 //           rec = new Recorder(input, { numChannels: 1 });
 //           rec.record();
			
 //       }).catch(function (err) {
	//		$("#errorModal").modal("show");
 //           microphone = false;
 //           return false;

 //      });
	//}

 //    async function stopRecording() {
	//	rec.stop();
	//	gumStream.getAudioTracks()[0].stop();
 //       rec.exportWAV(createDownloadLink);
	//}
 //    async function createDownloadLink(blob) {
	//	console.log(blob);
 // //    $("#qOperators .mainContent .infoContent").prepend(`
	//	//<li class='d-flex align-items-center mb-3'>
	//	// <audio class='mr-5' controls="" src="`+ URL.createObjectURL(blob) + `"></audio>

	//	//	`+ new Date().toISOString() + `.mp3
	//	//	<a href="`+ URL.createObjectURL(blob) + `" download="` + new Date().toISOString() + `.mp3" class='mBtn mBtn-pr ml-4'>
	//	//		<i class='fas fa-download'></i>
	//	//		Yüklə
	//	//	</a> 
	//	//</li>`);
	//}
	// --------

 //   $().click(function () {
 //       var statusName = 'Xidmətə hazırdır';
 //       var statusType = 'waitForService';
 //       var breakTextDefault = 'Fasilə';
 //       startServiceBtn(false);
 //       stopServiceBtn(true);
 //       acceptQueueBtn(true);
 //       finishServiceBtn(false);
 //       addServiceBtn(true);
 //       breakSelectBtn(false);
 //       queueActivation(true);
 //       checkboxActivation(true);
 //       operatorStatus(statusType, statusName);
 //       $(breakSelect).text(breakTextDefault);
 //       $("#qOperators .operatorBtns .selectDown a.active").removeClass("active");
 //       $(breakSelect).append("<i class='fas fa-angle-down ml-1'></i>");
 //   });
 //   $(stopService).click(function () {startService
 //       var statusName = 'Xidmət göstərilmir';
 //       var statusType = 'notServed';
 //       var breakTextDefault = 'Fasilə';
 //       if (microphone) {
 //           stopRecording();
 //       }
 //       startServiceBtn(true);
 //       stopServiceBtn(false);
 //       acceptQueueBtn(false);
 //       finishServiceBtn(false);
 //       addServiceBtn(false);
 //       breakSelectBtn(true);
 //       queueActivation(true);
 //       checkboxActivation(false);
 //       operatorStatus(statusType, statusName);
 //       $(breakSelect).text(breakTextDefault);
 //       $("#qOperators .operatorBtns .selectDown a.active").removeClass("active");
 //       $(breakSelect).append("<i class='fas fa-angle-down ml-1'></i>");
 //       progressRefresh();
 //       timerRefresh();
 //       checkboxRefresh();
 //   });

 //   $(acceptQueue).click(function () {
 //       startRecording();
 //   });
 //   $(finishService).click(function () {
 //       if (microphone) {
 //           stopRecording();
 //       }
 //       startServiceBtn(false);
 //       stopServiceBtn(true);
 //       acceptQueueBtn(true);
 //       finishServiceBtn(false);
 //       addServiceBtn(true);
 //       breakSelectBtn(false);
 //       queueActivation(true);
 //       timerRefresh();
 //       checkboxRefresh();
 //       progressRefresh();
 //   });

        $(breakSelect).click(function () {
	       	$(this).next().slideToggle('fast');
            $(this).find("i").toggleClass("fa-angle-down").toggleClass("fa-angle-up");
        });
        //$('.breakType').click(function () {
        //    breakStatus = true;
        //});

	/*------------------------------------------------*/
$(document).ready(function () {
    //$("#qOperators .operatorBtns .selectDown a").click(function (e) {
    //    var statusName = 'Fasilədədir';
    //    var statusType = 'inBreak';
    //    e.preventDefault();
    //    stopServiceBtn(false);
    //    var breakText = $(this).text();
    //    $(this).parents(".selectDown").find("a.active").removeClass("active");
    //    $(this).addClass("active");
    //    $(breakSelect).text(breakText);
    //    $(breakSelect).append("<i class='fas fa-angle-down ml-1'></i>");
    //    $(this).parents(".selectDown").slideUp('fast');
    //    operatorStatus(statusType, statusName);
    //});
    $(document).click(function (e) {
          if (!$(e.target).closest(".selectDown").length && !$(e.target).closest("#breakSelectBtn").length) {
              $("#qOperators .operatorBtns .selectDown").slideUp("fast");
              $(breakSelect).find("i").removeClass("fa-angle-up").addClass("fa-angle-down");
          }
    });



    $("#qOperators .waitingBox .boxBody .queueNo").on("click","a",function (e) {
        e.preventDefault();
        var queueText = $(this).text();
        $("#qOperators .operatorBox .appealNo h1").text(queueText);
        progressRefresh();
        progressStart();
    });


});

/*-------------------------citizen modal-----------------------*/

$("#citizenModal").on("hidden.bs.modal", function (e) {
    $("#citizenModal #citizenCheck1").click();
    if ($("#citizenModal #stopService:checked").length) {
        $("#citizenModal #stopService").click();
    }
})

/*------------------------------------------------*/



	// $("#loadWaves").click(function (e) {
	// 	e.preventDefault();
	// 	createWaves()
	// })
	// function createWaves() {
	// 	console.log("click")
	// 	var records = localStorage.getItem("qmaticAudios");

	// 	// $("#waveform").empty()
	// 	// for (let i = 0; i < audios.length; i++) {
	// 	// 	var wavesurfer = WaveSurfer.create({
	// 	// 		container: '#waveform',
	// 	// 		waveColor: 'gray',
	// 	// 		progressColor: '#005a80'
	// 	// 	});

	// 	// 	var song = audios[i]
	// 	// 	wavesurfer.load(song);
	// 	// 	// wavesurfer.on('ready', function () { wavesurfer.play(); });
	// 	// }
	// 	console.log(records)

	// }