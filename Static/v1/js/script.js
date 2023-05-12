var countdownMinute = '120';
var countdownSecond = '00';
var logoutAfterSecond = '40';
var date = new Date;

$(document).ready(function () {

    $(".leftMenu .menuFooter span.year").text(date.getFullYear());

    //#region Layout
    /*--------------- ERROR page ------------------*/
    if ($("#qError").length) {
        $("body").mousemove(function (event) {
            var eye = $(".eye");
            var x = (eye.offset().left) + (eye.width() / 2);
            var y = (eye.offset().top) + (eye.height() / 2);
            var rad = Math.atan2(event.pageX - x, event.pageY - y);
            var rot = (rad * (180 / Math.PI) * -1) + 180;
            eye.css({
                '-webkit-transform': 'rotate(' + rot + 'deg)',
                '-moz-transform': 'rotate(' + rot + 'deg)',
                '-ms-transform': 'rotate(' + rot + 'deg)',
                'transform': 'rotate(' + rot + 'deg)'
            });
        });
    }
    /*--------------- END ERROR page ------------------*/

    var mainStyle = $("#mainStyle").attr("href");
    if (!localStorage.getItem("qmaticTheme")) {
        localStorage.setItem("qmaticTheme", "dark");
    }
    else {
        var qmaticThemeNow = localStorage.getItem("qmaticTheme")
        if (qmaticThemeNow == 'darkTheme') {
            if (mainStyle.indexOf('/style.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/style.css/g, "styleForDark.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForLight.css/g, "styleForDark.css"));
            }
        }
        else if (qmaticThemeNow == 'lightTheme') {
            if (mainStyle.indexOf('/style.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/style.css/g, "styleForLight.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForDark.css/g, "styleForLight.css"));
            }
        }
        else {
            if (mainStyle.indexOf('/styleForDark.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForDark.css/g, "style.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForLight.css/g, "style.css"));
            }
        }
    }

    if ($(".leftMenu").length) {
        if (!localStorage.getItem("qmaticMenuOpened")) {
            localStorage.setItem("qmaticMenuOpened", "closed");
        }
        else {
            if (localStorage.getItem("qmaticMenuOpened") === "opened") {
                $(".leftMenu").css("transition", "none");
                $(".leftMenu .logo").css("transition", "none");
                $(".leftMenu .menu ul").css("transition", "none");
                $(".leftMenu .footerMenu").css("transition", "none");
                $("body .container-fluid").css("transition", "none");

                $("body").addClass("openMenu");
                setTimeout(function () {
                    $(".leftMenu").css("transition", "all 0.3s");
                    $(".leftMenu .logo").css("transition", "all 0.3s");
                    $(".leftMenu .menu ul").css("transition", "all 0.3s");
                    $(".leftMenu .footerMenu").css("transition", "all 0.3s");
                    $("body .container-fluid").css("transition", "all 0.3s");
                }, 100);
            }
            else if (localStorage.getItem("qmaticMenuOpened") === "closed") {
                $("body").removeClass("openMenu");
            }
        }
    }
    if ($("#dtBox").length) {// common dtp plugin
        $("#dtBox").DateTimePicker();
    }

    setTimeout(() => {
        $("#loader").fadeOut('slow');
    }, 1000);

    $(".createNewBtn a").click(function (e) {
        var formUrl = $(this).parent().attr("data-formUrl");
        e.preventDefault();
        clearRightMenu();
        $.ajax({
            type: "GET",
            url: formUrl,
            success: function (respData) {
                if (respData.status == "true") {
                    $('.rightMenu').html(respData.viewResult);
                    $("body").addClass("openRightMenu");
                    if (!$("main .mTable").length) {
                        setTimeout(function () {
                            $(gridID).resize();
                        }, 300);
                        $(gridID).jqxGrid('clearselection');
                    }
                    $(".selectpicker").selectpicker("refresh");
                    $('#btnDeleteForm').remove();
                }
                else {
                    Swal.fire({ type: 'error', text: respData.message, showConfirmButton: true, confirmButtonText: "Bağla", allowOutsideClick: false });
                    $("body").removeClass("openRightMenu");
                }
            }
        });

        //$("body").addClass("openRightMenu");
        //if (!$("main .mTable").length) {
        //    setTimeout(function () {
        //        $(gridID).resize();
        //    }, 300);
        //}
    });

    //#region leftMenu

    $(".leftMenu .menu .drop>a").click(function () {
        var current = $(this);
        $(current).parents(".drop").toggleClass("open");
        $(current).next(".down").slideToggle('500');
        $(document).on("click", ".leftMenu .menu li a", function () {
            if (!$(this).parents(".drop").length) {
                $(current).parents(".drop").removeClass("open");
                $(current).next(".down").slideUp('fast');
            }
        });
    });
    $(".leftMenu .menu a").click(function () {
        var current = $(this);
        $(current).parents(".menu").find("li.active").removeClass("active");
        $(current).parents("li").addClass("active");
    });


    $(".leftMenu").hover(function () {
        $(this).find(".open .down").slideDown("fast");
    }, function () {
        if (!$("body").hasClass("openMenu"))
            $(this).find(".open .down").slideUp("fast");
    });
    $(".leftMenu .fa-thumbtack").click(function () {
        if ($("body").hasClass("openMenu")) {
            $("body").removeClass("openMenu");
            localStorage.setItem("qmaticMenuOpened", "closed");
        }
        else {
            $("body").addClass("openMenu");
            localStorage.setItem("qmaticMenuOpened", "opened");
        }
        treeResizeForMenu(400);
        if (!$("main .mTable").length) {
            setTimeout(function () {
                $(gridID).resize();
            }, 300);
        }
    });
    $(".leftMenu .menuFooter .footerMenu li a").click(function (e) {
        e.preventDefault();
        var fMenu = $(this).parent().data("footer-menu");
        var trnslt = -(230 * (fMenu - 1));
        $(".leftMenu .menuFooter .footerMenu li a.active").removeClass("active");
        $(this).addClass("active");
        $(".leftMenu .menu").css("transform", "translateX(" + trnslt + "px)");
    });

    var pathElements = window.location.pathname.split("/");
    var currentActiveMainMenu = '/' + pathElements[pathElements.length - 2] + '/' + pathElements[pathElements.length - 1]; /*-for backend-*/
    //var currentActiveMainMenu = pathElements[pathElements.length - 1]; /*-for frontend-*/
    if ($(".leftMenu .menu").length) {
        var currentActiveLink = $(".leftMenu .menu a[href='" + currentActiveMainMenu + "']");
        if ($(currentActiveLink).parents("li.drop").length) {
            $(currentActiveLink).parents(".down").prev().click();
        }
        var currentActiveContainer = $(currentActiveLink).parent("li").addClass("active").parents("ul").data("main-menu");
        $(".leftMenu .menuFooter .footerMenu li[data-footer-menu='" + currentActiveContainer + "'] a").click();
    }
    //#endregion

    //#region footer
    var footerDropUpHeight = $("footer .dropUp").outerHeight();
    $("footer .dropUp").css("top", -footerDropUpHeight);
    $("footer .item .colorDrop a").click(function (e) {
        e.preventDefault();
        var themeClass = $(this).attr("class");
        localStorage.setItem("qmaticTheme", themeClass);
        if (themeClass == 'darkTheme') {

            if (mainStyle.indexOf('/style.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/style.css/g, "styleForDark.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForLight.css/g, "styleForDark.css"));
            }
        }
        else if (themeClass == 'lightTheme') {
            if (mainStyle.indexOf('/style.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/style.css/g, "styleForLight.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForDark.css/g, "styleForLight.css"));
            }
        }
        else {
            if (mainStyle.indexOf('/styleForDark.css') != -1) {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForDark.css/g, "style.css"));
            }
            else {
                $("#mainStyle").attr("href", mainStyle.replace(/styleForLight.css/g, "style.css"));
            }
        }
    });
    $("footer .profile").click(function () {
        current = $("footer");
        $(current).toggleClass("opened");
        $(current).find(".userAbout").toggle('fast');

        $(document).on("click", function (e) {
            if (!$(e.target).closest('footer').length && !$(e.target).closest('#changePasswordModal').length) {
                $(current).removeClass("opened");
                $(current).find(".userAbout").show('fast');
            }
        });
    });
    $("footer .langSelect").click(function () {
        $(this).find(".langList").slideToggle(100);
        $(document).on("click", function (e) {
            if (!$(e.target).closest('footer').length) {
                $("footer .langSelect .langList").slideUp(100);
            }
        });
    });
    $("footer .themeSelect").click(function (e) {
        e.preventDefault();
        $(this).find(".colorDrop").slideToggle(100);
        $(document).on("click", function (e) {
            if (!$(e.target).closest('.themeSelect').length) {
                $("footer .themeSelect .colorDrop").slideUp(100);
            }
        });
    });

    $("#changePasswordModal .showPass").mousedown(function () {
        $(this).prev().attr('type', 'text');
    });
    $("#changePasswordModal .showPass").mouseup(function () {
        $(this).prev().attr('type', 'password');
    });
    //#endregion

    //#region rightMenu

    $(document).on("click", ".rightMenu .closeRightMenu a", function (e) {
        e.preventDefault();
        $("body").removeClass("openRightMenu");
        treeResizeForMenu(400);
        if ($("#qServices").length) {
            removeSelectedService();
        }
        else if ($("#qSectors").length) {
            removeSelectedSector();
        }
        if (!$("main .mTable").length) {
            setTimeout(function () {
                $(gridID).resize();
            }, 300);
        }
    });
    $(document).on("click", ".rightMenu .menuHeading a", function (e) {
        e.preventDefault();
        var heading = $(this).parent().data("heading");
        $(this).parents("ul").find("a.active").removeClass("active");
        $(this).addClass("active");
        $(".rightMenu .menuContent .contentInner[data-content]").removeClass("active");
        $(".rightMenu .menuContent .contentInner[data-content=" + heading + "]").addClass("active");
    });
    $(document).on("click", ".rightMenu .menuContent .contentInner .form-group.photoGroup .squareBtn", function (e) {
        e.preventDefault();
        $(this).prev().click();
    });
    $(document).on("click", ".rightMenu .menuFoot .mBtn-sec", function () {
        $(".rightMenu .closeRightMenu a").click();
    });
    //#endregion

    //#endregion

    //#region mTable click events

    $(".mTable tr").dblclick(function (e) {
        if (!$(e.target).closest('table tr td:first-of-type').length && !$(this).hasClass("hiddenTR")) {
            $(this).parents(".mTable").find(".selectedRow").removeClass("selectedRow");
            $(this).addClass("selectedRow");
            if ($("#qServices").length) {
                activateServiceTree(this);
            }
            else if ($("#qSectors").length) {
                activateSectorTree(this);
            }
        }

    });

    $(".mTable tr").click(function (e) {
        if ($("body").hasClass("openRightMenu") && !$(e.target).closest('table tr td:first-of-type').length && !$(this).hasClass("hiddenTR")) {
            $(this).parents(".mTable").find(".selectedRow").removeClass("selectedRow");
            $(this).addClass("selectedRow");
            if ($("#qServices").length) {
                activateServiceTree(this);
            }
            else if ($("#qSectors").length) {
                activateSectorTree(this);
            }
        }

        //if (!$(this).hasClass("hiddenTR")) {
        //    $(this).parents(".mTable").find(".selectedRow").removeClass("selectedRow");
        //    $(this).addClass("selectedRow");
        //}
    });
    //#endregion

    //#region countdown
    if (Number(countdownMinute) < 10) {
        countdownMinute = "0" + countdownMinute;
    }
    $("footer .countdownMinute").text(countdownMinute);
    if (Number(countdownSecond) < 10) {
        countdownSecond = "0" + countdownSecond;
    }
    $("footer .countdownSecond").text(countdownSecond);

    var countdownInterval = setInterval(() => {
        var currentMinute = Number($("footer .countdownMinute").text());
        var currentSecond = Number($("footer .countdownSecond").text());
        currentSecond--;
        if (currentSecond < 0) {
            if (currentMinute > 0) {
                currentSecond = 59;
                currentMinute--;
            }
            else {
                $("#sessionEndModal").modal("hide");
                Swal.fire(
                    {
                        title: "Sessiya müddəti bitmişdir",
                        text: "Yenidən daxil olun",
                        type: "warning",
                        confirmButtonText: "Daxil ol",
                        allowOutsideClick: false
                    }).then((result) => {
                        location.reload();
                    });
                clearInterval(countdownInterval);
            }
        }
        if (currentSecond < 10) {
            currentSecond = "0" + currentSecond;
        }
        if (currentMinute < 10) {
            currentMinute = "0" + currentMinute;
        }
        if (currentSecond >= 0) {
            $("footer .countdownSecond").text(currentSecond);
            $("#sessionEndModal .modalCountdown .second").text(currentSecond);
        }

        $("footer .countdownMinute").text(currentMinute);
        $("#sessionEndModal .modalCountdown .minute").text(currentMinute);

        if (currentMinute == 0 && currentSecond == logoutAfterSecond) {
            $("#sessionEndModal").modal({ backdrop: 'static', keyboard: false });
        }
    }, 1000);

    $("#btnContinue").click(function () {
        $.ajax({
            type: "GET",
            url: "/home/ping",
            global: false,
            success: function (respData) {
                if (respData.status == "true") {
                    updateCountdown(countdownMinute, countdownSecond);
                }
            }
        });
    });
    //#endregion

    //#region Services
    $(document).on("click", "#qServices .downContent .down a", function (e) {
        e.preventDefault();
        var serviceTreeId = $(this).data("service-id");
        $("#qServices .mTable tr.selectedRow").removeClass("selectedRow");
        $("#qServices .mTable tr[data-service-id='" + serviceTreeId + "']").addClass("selectedRow");
        $("body").addClass("openRightMenu");
        $(this).parents(".downContent").find("a.active").removeClass('active');
        $(this).addClass("active");
    });
    //$(document).on("click", "#qServices .createNewBtn a", function (e) {
    //    e.preventDefault();
    //    $("body").addClass("openRightMenu");
    //});

    $(document).on("click", "#qServices .treeListContent .drop i", function (e) {
        var currDrop = $(this);
        if ($(currDrop).hasClass("fa-angle-right")) {
            $(currDrop).removeClass("fa-angle-right");
            $(currDrop).addClass("fa-angle-down");
        }
        else {
            $(currDrop).addClass("fa-angle-right");
            $(currDrop).removeClass("fa-angle-down");
        }
        $(currDrop).next().next().slideToggle('fast');
    });
    function activateServiceTree(current) {
        $("body").addClass("openRightMenu");
        var serviceId = $(current).data("service-id");
        var compatibleTree = $("#qServices .downContent a[data-service-id='" + serviceId + "']");
        $("#qServices .downContent a.active").removeClass("active");
        $(compatibleTree).parents(".down").slideDown();
        $(compatibleTree).parents(".down").prev().prev("i").removeClass("fa-angle-right").addClass("fa-angle-down");
        $(compatibleTree).addClass("active");
    }
    function removeSelectedService() {
        $("#qServices .mTable tr.selectedRow").removeClass("selectedRow");
        $("#qServices .downContent .down a.active").removeClass("active");
    }
    $("#docsSelect").on('changed.bs.select', function (e, clickedIndex, isSelected) {
        var selectedDocs = $("#docsSelect option");
        var nowClicked = selectedDocs[clickedIndex];
        var selectedValue = $(nowClicked).attr("value");

        if (isSelected) {
            $("#requiredDocsModal table tbody").append(`
                <tr data-value=` + selectedValue + `>
                    <td></td>
                    <td>` + $(nowClicked).text() + `</td>
                    <td>
                        <input type="text" class="form-control" value="" placeholder="Qeydlərinizi daxil edin.">
                    </td>
                    <td>
                        <input type="checkbox" class="switch mSwitch" checked title="Aktiv">
                    </td>
                </tr>
            `);
        }
        else {
            $("#requiredDocsModal table tbody tr[data-value=" + selectedValue + "]").remove();
        }
        var allTr = $("#requiredDocsModal table tbody tr");
        for (let i = 0; i < allTr.length; i++) {
            $(allTr[i]).find("td:first-of-type").text(i + 1);
        }

    });
    $(document).on("click", "#requiredDocsModal .mTable a", function (e) {
        e.preventDefault();
        var rowValue = $(this).parents("tr").data("value");
        $(this).parents("tr").remove();
        $("#docsSelect").find("[value=" + rowValue + "]").prop("selected", false);
        $("#docsSelect").selectpicker("refresh");
    });
    $("#requiredDocsModal").on("change", "input.switch", function () {
        if ($(this).is(":checked")) {
            $(this).attr("title", "Aktiv");
        }
        else {
            $(this).attr("title", "Deaktiv");
        }
    });
    $(".rightMenu").on("click", ".addNewInputGroup a", function () {
        $("#requiredDocsModal").modal({
            backdrop: 'static',
            keyboard: false
        });
        var defaultRowValues = $(".rightMenu .serviceRightTable tbody tr");
        $("#requiredDocsModal table tbody").empty();
        $('#docsSelect').selectpicker('deselectAll');
        for (let i = 0; i < defaultRowValues.length; i++) {
            var currentValue = $(defaultRowValues[i]).data("value");
            var currentText = $(defaultRowValues[i]).find("td:nth-of-type(2)").text();
            var currentRemark = $(defaultRowValues[i]).find("td:nth-of-type(3)").text();
            var currentIsActive = $(defaultRowValues[i]).find("td:nth-of-type(4)").data("is-active");

            $("#docsSelect").find("[value=" + currentValue + "]").prop("selected", true);
            $("#requiredDocsModal table tbody").append(`
                <tr data-value=`+ currentValue + `>
                    <td>`+ (i + 1) + `</td>
                    <td>`+ currentText + `</td>
                    <td>
                        <input type="text" class="form-control" placeholder="Qeydlərinizi daxil edin." value="`+ currentRemark + `">
                    </td>   
                    <td>
                        
                    </td>
                </tr>
       `);
            if (currentIsActive) {
                var tableInput = `<input type="checkbox" class="switch mSwitch" title="Aktiv" checked>`;
            }
            else {
                var tableInput = `<input type="checkbox" class="switch mSwitch" title="Deaktiv">`;
            }
            $("#requiredDocsModal table tbody tr[data-value=" + currentValue + "] td:last-of-type").append(tableInput);
        }
        $("#docsSelect").selectpicker("refresh");
    });
    $(document).on("click", "#requiredDocsModal button.mBtn-pr", function () {
        $("#requiredDocsModal").modal('hide');
        var changedRows = $("#requiredDocsModal .mTable tbody tr");
        $(".rightMenu .serviceRightTable tbody").empty();
        for (let i = 0; i < changedRows.length; i++) {
            var currValue = $(changedRows[i]).data("value");
            var currText = $(changedRows[i]).find("td:nth-of-type(2)").text();
            var currText2 = $(changedRows[i]).find("td:nth-of-type(3) input").val();
            var currentIsActive = $(changedRows[i]).find("td:last-of-type input").is(":checked");
            $(".rightMenu .serviceRightTable tbody").append(`
                     <tr data-value=`+ currValue + `>
                         <td>`+ (i + 1) + `</td>
                         <td>`+ currText + `</td>
                         <td>`+ currText2 + `</td>                         
                     </tr>
            `);
            if (currentIsActive) {
                var tableInput = `<td data-is-active="true" title="Aktiv">aktiv</td>`;
            }
            else {
                var tableInput = `<td data-is-active="false" title="Deaktiv">deaktiv</td>`;
            }
            $(".rightMenu .serviceRightTable tbody tr[data-value=" + currValue + "]").append(tableInput);
        }
    });
    $(document).on("click", "#requiredDocsModal button.mBtn-sec", function () {
        $("#docsSelect").selectpicker("refresh");
    });
    //#endregion

    //#region Sectors
    $(".mGroupHeading a[data-group]").click(function (e) {
        e.preventDefault();
        $(this).parents(".mGroupHeading").find("a.active").removeClass("active");
        $(this).addClass("active");
        var groupNum = $(this).data('group');
        $(".mainContent").addClass("d-none");
        $(".mainContent[data-content='" + groupNum + "']").removeClass("d-none");
        if ($("#qSectors").length && $(e.target).closest("a[data-group='1']").length) {
            sectorTreeReload();
        }
        if ($("#qSectors").length) {
            localStorage.setItem("qmaticSectorGroupHeading", groupNum);
        }
        else if ($("#qServices").length) {
            localStorage.setItem("qmaticServiceGroupHeading", groupNum);
        }
    });
    if (!localStorage.getItem("qmaticSectorGroupHeading")) {
        localStorage.setItem("qmaticSectorGroupHeading", "1");
    }
    else {
        var lastClickedSectorHeading = localStorage.getItem("qmaticSectorGroupHeading");
        $("#qSectors .mGroupHeading a[data-group='" + lastClickedSectorHeading + "']").click();
    }
    if (!localStorage.getItem("qmaticServiceGroupHeading")) {
        localStorage.setItem("qmaticServiceGroupHeading", "1");
    }
    else {
        var lastClickedServiceHeading = localStorage.getItem("qmaticServiceGroupHeading");
        $("#qServices .mGroupHeading a[data-group='" + lastClickedServiceHeading + "']").click();
    }
    //////////////
    var currentShown = 0;
    var moveableCol = $("#qSectors .sectorRow .moveableCol>div");
    var sectorCount = $(moveableCol).length;
    var colWidth = $(moveableCol[0]).outerWidth();
    var cols = 4;
    var maxTr = (moveableCol.length - cols) * colWidth;
    var reloadPos = colWidth * currentShown;
    if ($("#qSectors").length && sectorCount > 4) {
        $("#qSectors .sectorRow .sectorArrows").removeClass("d-none");
    }
    $("#qSectors .sectorRow a.sectorRightArrow").click(function (e) {
        e.preventDefault();
        if (reloadPos < maxTr) {
            currentShown += 1;
            colWidth = $(moveableCol[0]).outerWidth();
            reloadPos = colWidth * currentShown;
            $("#qSectors .sectorRow .moveableCol").css("transform", "translateX(-" + reloadPos + "px)");
        }
    });
    $("#qSectors .sectorRow a.sectorLeftArrow").click(function (e) {
        e.preventDefault();
        if (reloadPos > 0) {
            currentShown -= 1;
            colWidth = $(moveableCol[0]).outerWidth();
            reloadPos = colWidth * currentShown;
            $("#qSectors .sectorRow .moveableCol").css("transform", "translateX(-" + reloadPos + "px)");
        }
    });
    $(window).on("resize", function () {
        if ($("#qSectors").length) {
            sectorTreeResize();
        }
    });
    $("#qSectors .createNewBtn a").click(function (e) {
        e.preventDefault();
        $("body").addClass("openRightMenu");
        treeResizeForMenu(400);
    });
    $("#qSectors .mainContent .sectorBox").dblclick(function (e) {
        e.preventDefault();
        $("body").addClass("openRightMenu");
        treeResizeForMenu(400);
        $("#qSectors .mainContent .sectorBox.active").removeClass("active");
        $(this).addClass("active");
        var dataSectorId = $(this).data("sector-id");
        $("#qSectors .mTable tr.selectedRow").removeClass("selectedRow");
        $("#qSectors .mTable tr[data-sector-id='" + dataSectorId + "']").addClass("selectedRow");
    });

    $("#qSectors .mainContent .sectorBox").click(function (e) {
        if ($("body").hasClass("openRightMenu")) {
            e.preventDefault();
            $("body").addClass("openRightMenu");
            treeResizeForMenu(400);
            $("#qSectors .mainContent .sectorBox.active").removeClass("active");
            $(this).addClass("active");
            var dataSectorId = $(this).data("sector-id");
            $("#qSectors .mTable tr.selectedRow").removeClass("selectedRow");
            $("#qSectors .mTable tr[data-sector-id='" + dataSectorId + "']").addClass("selectedRow");
        }
    });

    $("#qSectors .mainContent .visibleColCount").click(function () {
        var colCount = Number($("#qSectors .mainContent .visibleColCount select").find(":selected").val());
        var stageCol = $("#qSectors .mainContent .stageRow>div");
        var sectorCol = $("#qSectors .mainContent .sectorRow .moveableCol>div");
        $("#qSectors .sectorRow .sectorArrows").addClass("d-none");
        switch (colCount) {
            case 2:
                $(stageCol).removeClass();
                $(stageCol).addClass("col-6");
                $(sectorCol).removeClass();
                $(sectorCol).addClass("col-6");
                cols = 2;
                break;
            case 3:
                $(stageCol).removeClass();
                $(stageCol).addClass("col-8");
                $(sectorCol).removeClass();
                $(sectorCol).addClass("col-4");
                cols = 3;
                break;
            case 4:
                $(stageCol).removeClass();
                $(stageCol).addClass("col-9");
                $(sectorCol).removeClass();
                $(sectorCol).addClass("col-3");
                cols = 4;
                break;
            case 6:
                $(stageCol).removeClass();
                $(stageCol).addClass("col-10");
                $(sectorCol).removeClass();
                $(sectorCol).addClass("col-2");
                cols = 6;
                break;
            case 12:
                $(stageCol).removeClass();
                $(stageCol).addClass("col-11");
                $(sectorCol).removeClass();
                $(sectorCol).addClass("col-1");
                cols = 12;
                break;
        }
        if (sectorCount > cols) {
            $("#qSectors .sectorRow .sectorArrows").removeClass("d-none");
        }
        $("#qSectors .sectorRow .moveableCol").css("transform", "translateX(-" + 0 + "px)");
        currentShown = 0;
        moveableCol = $("#qSectors .sectorRow .moveableCol>div");
        colWidth = $(moveableCol[0]).outerWidth();
        maxTr = (moveableCol.length - cols) * colWidth;
        reloadPos = colWidth * currentShown;
    });
    function sectorTreeResize() {
        moveableCol = $("#qSectors .sectorRow .moveableCol>div");
        colWidth = $(moveableCol[0]).outerWidth();
        reloadPos = colWidth * currentShown;
        maxTr = (moveableCol.length - cols) * colWidth;
        $("#qSectors .sectorRow .moveableCol").css("transition", "none");
        $("#qSectors .sectorRow .moveableCol").css("transform", "translateX(-" + reloadPos + "px)");
        setTimeout(() => {
            $("#qSectors .sectorRow .moveableCol").css("transition", "all 0.5s");
        }, 100);
    }
    function treeResizeForMenu(timer) {
        if ($("#qSectors").length) {
            setTimeout(() => {
                sectorTreeResize();
            }, timer);
        }
    }
    function activateSectorTree(current) {
        $("body").addClass("openRightMenu");
        var sectorId = $(current).data("sector-id");
        var compatibleTree = $("#qSectors .sectorRow .sectorBox[data-sector-id='" + sectorId + "']");
        $("#qSectors .sectorRow .sectorBox.active").removeClass("active");
        $("#qSectors .sectorRow div[data-col-no]").removeClass("activeCol");
        $(compatibleTree).addClass("active");
        $(compatibleTree).parents("div[data-col-no]").addClass("activeCol");
    }
    function sectorTreeReload() {
        treeResizeForMenu(0);
        setTimeout(() => {
            var colNo = $("#qSectors .sectorRow .moveableCol .activeCol").data("col-no");
            var currentColCount = Number($("#qSectors .visibleColCount select").find(":selected").val());
            currentShown = colNo - currentColCount;
            if (currentShown < 0) {
                currentShown = 0;
            }
            reloadPos = colWidth * currentShown;
            $("#qSectors .sectorRow .moveableCol").css("transform", "translateX(-" + reloadPos + "px)");
        }, 100);
    }
    function removeSelectedSector() {
        $("#qSectors .mTable tr.selectedRow").removeClass("selectedRow");
        $("#qSectors .sectorRow .sectorBox.active").removeClass("active");
    }
    //#endregion

    //#region Dashboard

    var pColor = '#0769af';
    if ($("#dashboardChart1").length) {
        var myChart1 = echarts.init(document.getElementById('dashboardChart1'));

        option = {
            color: ['#0769af', '#f4423a', '#fcc102', '#4bb057', '#a848bb', '#00afbf', '#f87143', '#b0964b', '#4b4eb0', '#b04b70', '#848484', '#030363'],
            legend: {
                orient: 'vertical',
                x: 'right',
                y: 'center',
                data: ['1', '2', '3', '4', '5', '6', '7', '8', '9']
            },
            series: [
                {
                    name: 'Aktiv',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false
                        },
                        emphasis: {
                            show: true
                        }
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    },
                    data: [
                        { value: 60, name: '1' },
                        { value: 30, name: '2' },
                        { value: 20, name: '3' },
                        { value: 10, name: '4' },
                        { value: 10, name: '5' },
                        { value: 10, name: '6' },
                        { value: 5, name: '7' },
                        { value: 1, name: '8' },
                        { value: 1, name: '9' }
                    ]
                }
            ]
        };
        myChart1.setOption(option);
    }
    if ($("#dashboardChart2").length) {
        var myChart2 = echarts.init(document.getElementById('dashboardChart2'));
        var option = {
            color: [pColor, '#47b3ff'],
            tooltip: {},
            legend: {
                data: ['Revenue', 'ROAS'],
                right: ['0']
            },
            xAxis: {
                data: ["Birləşmiş su kanal MMC", "Socar", "AZU", "BP", "AzTelekom", "BATS", "Apert", "Azmark", "Turanbank"],
                axisLabel: {
                    rotate: 60
                }
            },
            yAxis: {},
            series: [{
                name: 'Revenue',
                type: 'bar',
                data: [4, 11, 9, 3, 2, 1, 2, 3, 1]
            }, {
                name: 'ROAS',
                type: 'bar',
                data: [11, 9, 3, 2, 5, 2, 6, 1, 4]
            }]
        };
        myChart2.setOption(option);
    }
    if ($("#dashboardChart3").length) {
        var myChart3 = echarts.init(document.getElementById('dashboardChart3'));

        option = {
            color: ['#0769af', '#f4423a', '#fcc102', '#4bb057', '#a848bb', '#00afbf', '#f87143', '#b0964b', '#4b4eb0', '#b04b70', '#848484', '#030363'],
            legend: {
                orient: 'vertical',
                x: 'right',
                y: 'center',
                data: ['1', '2', '3', '4', '5', '6', '7', '8', '9']
            },
            series: [
                {
                    name: 'Aktiv',
                    type: 'pie',
                    radius: ['40%', '70%'],
                    avoidLabelOverlap: false,
                    label: {
                        normal: {
                            show: false
                        },
                        emphasis: {
                            show: true
                        }
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    },
                    data: [
                        { value: 40, name: '1' },
                        { value: 60, name: '2' },
                        { value: 15, name: '3' },
                        { value: 20, name: '4' },
                        { value: 2, name: '5' },
                        { value: 3, name: '6' },
                        { value: 5, name: '7' },
                        { value: 1, name: '9' }
                    ]
                }
            ]
        };
        myChart3.setOption(option);
    }
    if ($("#dashboardChart4").length) {
        var myChart4 = echarts.init(document.getElementById('dashboardChart4'));
        var option = {
            color: [pColor, '#47b3ff'],
            tooltip: {},
            legend: {
                data: ['Revenue', 'ROAS'],
                right: ['0']
            },
            xAxis: {
                data: ["Birləşmiş su kanal MMC", "Socar", "AZU", "BP", "AzTelekom", "BATS", "Apert", "Azmark", "Turanbank"],
                axisLabel: {
                    rotate: 60
                }
            },
            yAxis: {},
            series: [{
                name: 'Revenue',
                type: 'bar',
                data: [11, 9, 3, 2, 5, 2, 6, 1, 4]
            }, {
                name: 'ROAS',
                type: 'bar',
                data: [4, 11, 9, 3, 2, 1, 2, 3, 1]
            }]
        };
        myChart4.setOption(option);
    }
    if ($("#qDashboard").length) {
        $(window).on('resize', function () {
            setTimeout(function () {
                myChart1.resize();
                myChart2.resize();
                myChart3.resize();
                myChart4.resize();
            }, 100);
        });
    }
    //#endregion

});

function clearRightMenu() {
    $(".rightMenu .menuHeading li[data-heading='1'] a").click();
    $(".rightMenu .menuContent input:not([data-field])").not(":input[name='__RequestVerificationToken']").val('');
    $(".rightMenu .menuContent textarea").val('');
    $(".rightMenu .menuContent select option[selected]").removeAttr("selected");
    $(".rightMenu .menuContent select option:first-of-type").attr("selected", "selected");
    $(".rightMenu .menuContent .selectpicker").selectpicker('refresh');
    $(".rightMenu .menuContent .mCheckbox input:checked").click();
}

function copyToClipboard(elem) {
    var targetId = "_hiddenCopyText_";
    var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
    var origSelectionStart, origSelectionEnd;
    if (isInput) {
        target = elem;
        origSelectionStart = elem.selectionStart;
        origSelectionEnd = elem.selectionEnd;
    } else {
        target = document.getElementById(targetId);
        if (!target) {
            var target = document.createElement("textarea");
            target.style.position = "absolute";
            target.style.left = "-9999px";
            target.style.top = "0";
            target.id = targetId;
            document.body.appendChild(target);
        }
        target.textContent = elem.textContent;
    }
    var currentFocus = document.activeElement;
    target.focus();
    target.setSelectionRange(0, target.value.length);

    var succeed;
    try {
        succeed = document.execCommand("copy");
    } catch (e) {
        succeed = false;
    }
    if (currentFocus && typeof currentFocus.focus === "function") {
        currentFocus.focus();
    }

    if (isInput) {
        elem.setSelectionRange(origSelectionStart, origSelectionEnd);
    } else {
        target.textContent = "";
    }
    return succeed;
}

function updateCountdown(min, sec) {
    var minute = Number(min);
    var second = Number(sec);
    if (minute < 10) {
        minute = "0" + minute;
    }
    if (second < 10) {
        second = "0" + second;
    }
    $("footer .countdownSecond").text(second);
    $("#sessionEndModal .modalCountdown .second").text(second);

    $("footer .countdownMinute").text(minute);
    $("#sessionEndModal .modalCountdown .minute").text(minute);
}

function stringIsNullOrWhiteSpace(str) {
    return str === null || str.match(/^ *$/) !== null;
}

function refreshRightMenu() {
    clearRightMenu();
    if (!$("main .mTable").length) {
        $(gridID).jqxGrid('clearselection');
    }
}

function refreshGrid() {
    $(gridID).jqxGrid('updatebounddata', 'filter');
}


