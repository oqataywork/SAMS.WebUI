var wavesArr = [];

var themeColor = '';
var wavesurfer2;
switch (localStorage.getItem("qmaticTheme")) {
    case 'lightTheme':
        themeColor = '#7cbed4'
        break;
    case 'darkTheme':
        themeColor = '#272727'
        break;
    default:
        themeColor = '#005a80'
        break;
}
$(document).ready(function () {
    $("#qRecords table .openInnerTable a").click(function (e) {
        e.preventDefault();
        var trParent = $(this).parents("tr");
        if (!$(trParent).hasClass("hasWave") && $(trParent).next().data("record-url")) {
            $(trParent).addClass("hasWave");
            var currTr = $(trParent).next().data("record-url");
            var currIndex = $(trParent).next().data("record-id");
            $(trParent).next().addClass("disableTr");
            createWaves(currTr, currIndex);
        }

        $(trParent).next().toggleClass("d-none");

    });


    $(document).on("click", "#waveform .controlBtn a", function (e) {
        e.preventDefault();
        var recordId = $(this).parents("tr").data("record-id");
        if ($(this).find("i").hasClass("fa-play")) {
            pauseAllRecords();
            wavesArr[recordId].play();
            $(this).find("i").removeClass("fa-play");
            $(this).find("i").addClass("fa-pause");
            wavesArr[recordId].on('audioprocess', function () {
                if (wavesArr[recordId].isPlaying()) {
                    var currentTime = Math.floor(wavesArr[recordId].getCurrentTime());
                    var durationArr = splitDuration(currentTime);
                    var currentContainer = $("#waveform tr[data-record-id='" + recordId + "'] .currentTime");
                    $(currentContainer).find("span.hour").text(durationArr[2]);
                    $(currentContainer).find("span.minute").text(durationArr[1]);
                    $(currentContainer).find("span.second").text(durationArr[0]);
                }
            });
        }
        else {
            wavesArr[recordId].pause();
            $(this).find("i").removeClass("fa-pause");
            $(this).find("i").addClass("fa-play");
        }
    });


    //#region edit record
    // $(document).on("click", "#waveform .operationIcons a.deleteRecord", function (e) {
    //   e.preventDefault();
    //   BootstrapDialog.show({
    //     title: 'Xəbərdarlıq',
    //     message: 'Silmək istədiyinizə əminsinizmi?',
    //     closable: false,
    //     buttons: [{
    //       label: 'Xeyr',
    //       cssClass: 'mBtn mBtn-sec',
    //       action: function (dialogItself) {
    //         dialogItself.close();
    //       }
    //     },
    //     {
    //       label: 'Bəli',
    //       cssClass: 'mBtn mBtn-dng'
    //     }]
    //   });
    // });

    $(document).on("click", "#waveform .operationIcons a.editRecord", function (e) {
        e.preventDefault();
        pauseAllRecords();
        $("#editRecordModal").modal("show");
        $("#editRecordModal .modal-body .mainWave").empty();
        $("#editRecordModal .modal-body .mainWaveMinimap").empty();
        var currentStartTime = $(this).parents("tr").prev().data("start-time");
        var currUrl = $(this).parents("tr").data("record-url");
        var voiceRecordingGuid = $(this).parents("tr").prev("tr").data("voicerecordingguid");
        $(".audioBtns .squareBtn:last-of-type").attr("data-voicerecording-guid", voiceRecordingGuid);
        console.log($(this).parents("tr").prev("tr"));
        console.log(voiceRecordingGuid);
        setTimeout(() => {
            createModalWave(currUrl, currentStartTime);
        }, 500);
    });
    $("#editRecordModal .modal-body .audioBtns a[title]").tooltip({
        trigger: 'hover'
    });
    $("#editRecordModal .modal-footer .mBtn-sec").click(function () {
        wavesurfer2.stop();
        $("#editRecordModal").modal('hide');
        $("#editRecordModal .audioBtns a.disabled").removeClass("disabled");
        $("#editRecordModal .audioBtns a:nth-of-type(2)").addClass("disabled");
    })
    $("#editRecordModal .audioBtns a:first-of-type").click(function (e) {
        e.preventDefault();
        wavesurfer2.play();
        $("#editRecordModal .audioBtns a.disabled").removeClass("disabled");
        $(this).addClass("disabled");
    });
    $("#editRecordModal .audioBtns a:nth-of-type(2)").click(function (e) {
        e.preventDefault();
        wavesurfer2.pause();
        $("#editRecordModal .audioBtns a.disabled").removeClass("disabled");
        $(this).addClass("disabled");
    });
    $("#editRecordModal .audioBtns a:last-of-type").click(function (e) {
        e.preventDefault();
        var startForCut = Math.floor(wavesurfer2.regions.list[1].start);
        var endForCut = Math.ceil(wavesurfer2.regions.list[1].end);

        var voiceRecordingGuid = $(this).attr("data-voicerecording-guid");
        console.log('start -', startForCut);
        console.log('end -', endForCut);

        var fileUrl = "/VoiceRecordings/SplitFile/?voiceRecordingGuid=" + voiceRecordingGuid + "&fromstr=" + startForCut + "&tostr=" + endForCut;
        window.open(fileUrl);
    });
    //#endregion

});

function convertTime(arrClock, arrTime) {
    var endTimeH = Number(arrClock[0]) + Number(arrTime[0]);
    var endTimeM = Number(arrClock[1]) + Number(arrTime[1]);
    var endTimeS = Math.floor(Number(arrClock[2]) + Number(arrTime[2]));
    if (endTimeS > 59) {
        endTimeS -= 60;
        endTimeM++;
    }
    if (endTimeM > 59) {
        endTimeM -= 60;
        endTimeH++;
    }
    if (endTimeH > 23) {
        endTimeH -= 24;
    }

    if (endTimeS < 10) {
        endTimeS = "0" + endTimeS;
    }
    if (endTimeM < 10) {
        endTimeM = "0" + endTimeM;
    }
    if (endTimeH < 10) {
        endTimeH = "0" + endTimeH;
    }

    return [endTimeH, endTimeM, endTimeS];
}
function pauseAllRecords() {
    var currentPlay = $("#waveform i.fa-pause").removeClass("fa-pause").addClass("fa-play").parents("tr").data("record-id");

    if (currentPlay != undefined) {
        wavesArr[currentPlay].pause();
    }
}

function splitDuration(currDuration) {
    var hour = 0;
    var min = 0;
    var sec = currDuration;

    if (sec > 59) {
        min = sec / 60;
        min = Math.floor(min);
        sec = currDuration - (min * 60);
    }
    if (min > 59) {
        hour = min / 60;
        hour = Math.floor(hour);
        min = min - (hour * 60);
    }
    if (hour < 10) {
        hour = "0" + hour;
    }
    if (min < 10) {
        min = "0" + min;
    }
    if (sec < 10) {
        sec = "0" + sec;
    }

    return [sec, min, hour];
}

function createWaves(audio, i) {
    var wavesurfer = WaveSurfer.create({
        container: "#waveform tr[data-record-id='" + i + "'] .recordContainer",
        waveColor: '#ccc',
        progressColor: themeColor,
        height: 60,
        responsive: true,
    });

    var song = audio;
    wavesurfer.load(song);
    wavesArr[i] = wavesurfer;

    wavesurfer.on("ready", function () {
        $("#waveform tr[data-record-id='" + i + "']").removeClass("disableTr");
        var currDuration = Number(wavesurfer.getDuration().toFixed(0));
        var durationArr = splitDuration(currDuration);
        var durationContainer = $("#waveform tr[data-record-id='" + i + "'] .recDuration");
        $(durationContainer).find("span.hour").text(durationArr[2]);
        $(durationContainer).find("span.minute").text(durationArr[1]);
        $(durationContainer).find("span.second").text(durationArr[0]);
    });

    wavesurfer.on('finish', function () {
        $("#waveform tr .controlBtn a i.fa-pause").removeClass("fa-pause").addClass("fa-play");
    });

    wavesurfer.on('error', function (exc) {
        console.log(exc);
        $("#waveform tr[data-record-id='" + i + "']").addClass("disableTr");
    });
}


function createModalWave(currUrl, currentStartTime) {
    var regionEndDefault = 10;
    wavesurfer2 = WaveSurfer.create({
        container: "#editRecordModal .modal-body .mainWave",
        waveColor: '#ccc',
        progressColor: themeColor,
        height: 100,
        width: 120,
        responsive: true,
        hideScrollbar: false,
        plugins: [
            WaveSurfer.regions.create({
                regions: [
                    {
                        id: 1,
                        start: 0,
                        end: regionEndDefault,
                        loop: false,
                        color: 'hsla(400, 100%, 30%, 0.5)'
                    }
                ],
                dragSelection: {
                    slop: 5
                }
            }),
            WaveSurfer.cursor.create({
                showTime: true,
                opacity: 1,
                customShowTimeStyle: {
                    'background-color': '#000',
                    color: '#fff',
                    padding: '2px',
                    'font-size': '10px'
                }
            }),
            WaveSurfer.minimap.create({
                container: '#editRecordModal .modal-body .mainWaveMinimap'
            })
        ]
    });
    wavesurfer2.on("ready", function () {
        var startTimeH = currentStartTime.split(":")[0];
        var startTimeM = currentStartTime.split(":")[1];
        var startTimeS = currentStartTime.split(":")[2];

        $("#editRecordModal .modal-body .mainEditing .startTime input:first-of-type").val(startTimeH);
        $("#editRecordModal .modal-body .mainEditing .startTime input:nth-of-type(2)").val(startTimeM);
        $("#editRecordModal .modal-body .mainEditing .startTime input:last-of-type").val(startTimeS);


        var recordLength = splitDuration(wavesurfer2.getDuration());
        var endTime = convertTime([startTimeH, startTimeM, startTimeS], [recordLength[2], recordLength[1], recordLength[0]]);


        var defaultCropTime = convertTime([startTimeH, startTimeM, startTimeS], [0, Math.floor(regionEndDefault / 60), regionEndDefault % 60]);
        $("#editRecordModal .modal-body .mainEditing .endTime input:first-of-type").val(endTime[0]);
        $("#editRecordModal .modal-body .mainEditing .endTime input:nth-of-type(2)").val(endTime[1]);
        $("#editRecordModal .modal-body .mainEditing .endTime input:last-of-type").val(endTime[2]);

        $("#editRecordModal .modal-body .mainEditing .endInputs input:first-of-type").val(defaultCropTime[0]);
        $("#editRecordModal .modal-body .mainEditing .endInputs input:nth-of-type(2)").val(defaultCropTime[1]);
        $("#editRecordModal .modal-body .mainEditing .endInputs input:last-of-type").val(defaultCropTime[2]);


        $("#editRecordModal .modal-body .mainEditing .startInputs input:first-of-type").val(startTimeH);
        $("#editRecordModal .modal-body .mainEditing .startInputs input:nth-of-type(2)").val(startTimeM);
        $("#editRecordModal .modal-body .mainEditing .startInputs input:last-of-type").val(startTimeS);
    });
    var song = currUrl;
    wavesurfer2.load(song);
    wavesurfer2.regions.list[1].on('update', function () {
        var currRegionStart = wavesurfer2.regions.list[1].start;
        var currRegionEnd = wavesurfer2.regions.list[1].end;
        currRegionStart = Math.floor(currRegionStart);
        currRegionEnd = Math.floor(currRegionEnd);
        var startTimeH = $("#editRecordModal .modal-body .mainEditing .startTime input:first-of-type").val();
        var startTimeM = $("#editRecordModal .modal-body .mainEditing .startTime input:nth-of-type(2)").val();
        var startTimeS = $("#editRecordModal .modal-body .mainEditing .startTime input:last-of-type").val();

        var regArr = splitDuration(currRegionStart);

        var startInput = convertTime([startTimeH, startTimeM, startTimeS], [regArr[2], regArr[1], regArr[0]]);
        $("#editRecordModal .modal-body .mainEditing .startInputs input:first-of-type").val(startInput[0]);
        $("#editRecordModal .modal-body .mainEditing .startInputs input:nth-of-type(2)").val(startInput[1]);
        $("#editRecordModal .modal-body .mainEditing .startInputs input:last-of-type").val(startInput[2]);


        var regArr2 = splitDuration(currRegionEnd);

        var endInput = convertTime([startTimeH, startTimeM, startTimeS], [regArr2[2], regArr2[1], regArr2[0]]);
        $("#editRecordModal .modal-body .mainEditing .endInputs input:first-of-type").val(endInput[0]);
        $("#editRecordModal .modal-body .mainEditing .endInputs input:nth-of-type(2)").val(endInput[1]);
        $("#editRecordModal .modal-body .mainEditing .endInputs input:last-of-type").val(endInput[2]);


    });
    wavesurfer2.on('region-created', function (region) {
        region.remove();
    });
    wavesurfer2.on('audioprocess', function () {
        if (wavesurfer2.isPlaying()) {
            var currentTime = Math.floor(wavesurfer2.getCurrentTime());
            var durationArr = splitDuration(currentTime);
            var currentContainer = $("#editRecordModal .modal-body .mainWaveCurrentTime");
            $(currentContainer).find("span.hour").text(durationArr[2]);
            $(currentContainer).find("span.minute").text(durationArr[1]);
            $(currentContainer).find("span.second").text(durationArr[0]);
        }
    });
    $("#editRecordModal wave").mouseup(function () {
        setTimeout(() => {
            var currentTime = Math.floor(wavesurfer2.getCurrentTime());
            var durationArr = splitDuration(currentTime);
            var currentContainer = $("#editRecordModal .modal-body .mainWaveCurrentTime");
            $(currentContainer).find("span.hour").text(durationArr[2]);
            $(currentContainer).find("span.minute").text(durationArr[1]);
            $(currentContainer).find("span.second").text(durationArr[0]);
        }, 10);
    });
    $("#editRecordModal").modal({
        backdrop: 'static'
    });
    $("#editRecordModal .modal-body .mainEditing .startInputs input").on("input", function (e) {
        var h = Number($("#editRecordModal .modal-body .mainEditing .startInputs input:first-of-type").val());
        var m = Number($("#editRecordModal .modal-body .mainEditing .startInputs input:nth-of-type(2)").val());
        var s = Number($("#editRecordModal .modal-body .mainEditing .startInputs input:last-of-type").val());

        var startTimeH = Number($("#editRecordModal .modal-body .mainEditing .startTime input:first-of-type").val());
        var startTimeM = Number($("#editRecordModal .modal-body .mainEditing .startTime input:nth-of-type(2)").val());
        var startTimeS = Number($("#editRecordModal .modal-body .mainEditing .startTime input:last-of-type").val());

        if (h < startTimeH) {
            h += 24;
        }
        var totalSecond = (h - startTimeH) * 60 * 60 + (m - startTimeM) * 60 + s - startTimeS;

        var waveDuration = wavesurfer2.getDuration();
        if (waveDuration >= totalSecond) {
            wavesurfer2.regions.list[1].update({
                start: totalSecond
            });
        }
    });
    $("#editRecordModal .modal-body .mainEditing .endInputs input").on("input", function () {
        var h = Number($("#editRecordModal .modal-body .mainEditing .endInputs input:first-of-type").val());
        var m = Number($("#editRecordModal .modal-body .mainEditing .endInputs input:nth-of-type(2)").val());
        var s = Number($("#editRecordModal .modal-body .mainEditing .endInputs input:last-of-type").val());

        var startTimeH = Number($("#editRecordModal .modal-body .mainEditing .startTime input:first-of-type").val());
        var startTimeM = Number($("#editRecordModal .modal-body .mainEditing .startTime input:nth-of-type(2)").val());
        var startTimeS = Number($("#editRecordModal .modal-body .mainEditing .startTime input:last-of-type").val());

        if (h < startTimeH) {
            h += 24;
        }
        var totalSecond = (h - startTimeH) * 60 * 60 + (m - startTimeM) * 60 + s - startTimeS;

        var waveDuration = wavesurfer2.getDuration();
        var totalDifference = (m * 60 + h * 60 * 60) - (Number($("#editRecordModal .modal-body .mainEditing .startInputs input:first-of-type").val()) * 60 * 60 + Number($("#editRecordModal .modal-body .mainEditing .startInputs input:last-of-type").val()) * 60);
        if (waveDuration >= totalSecond && totalDifference >= 0) {
            wavesurfer2.regions.list[1].update({
                end: totalSecond
            });
        }
    });
    wavesurfer2.on('finish', function () {
        $("#editRecordModal .audioBtns a:nth-of-type(2)").addClass("disabled");
        $("#editRecordModal .audioBtns a:nth-of-type(1)").removeClass("disabled");
    });

}