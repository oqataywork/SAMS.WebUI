$(document).ready(function () {
    

/*----------------------Registration--------------------------*/
    $("#qRegistration").on('click', '.formSection .contentToggle a', function (e) {
        e.preventDefault();
        $(this).parent().prev().slideToggle();
        $(this).children().toggleClass("fa-angle-up").toggleClass("fa-angle-down");
    });


    $("#qRegistration").on('click', '.formSection .tabMenu a', function (e) {
        e.preventDefault();
        var activeTab = $(this).data('active');
        $(this).parents("ul").find("a.active").removeClass("active");
        $(this).addClass("active");
        switch (activeTab) {
            case 1:
                $("#qRegistration .formSection .mainFormRow .mainForm").slideDown();
                $("#qRegistration .formSection .mainFormRow .mainForm .aboutPassRow").css('display', 'flex');
                $("#slcCustomerCitizenCountryGuid").attr('disabled', 'disabled');
                $("#slcCustomerCitizenCountryGuid").selectpicker('refresh');

                $("#hdAllowAnonymous").val('0');

                break;
            case 2:
                $("#qRegistration .formSection .mainFormRow .mainForm").slideDown();
                $("#qRegistration .formSection .mainFormRow .mainForm .aboutPassRow").css('display', 'none');
                $("#slcCustomerCitizenCountryGuid").removeAttr('disabled');
                $("#slcCustomerCitizenCountryGuid").selectpicker('refresh');

                $("#hdAllowAnonymous").val('0');
                break;
            case 3:
                $("#qRegistration .formSection .mainFormRow .mainForm").slideUp();

                $("#hdAllowAnonymous").val('1');
                break;
        }
        console.log($("#txtCustomerCitizenCountryGuid").val());
        console.log($("#slcCustomerCitizenCountryGuid").val());
        //clearRegistrationFormData();
    });

    $("#qRegistration").on('click', '.formSection .mainForm .imageContainer a', function (e) {
        e.preventDefault();
        $(this).prev().click();
    });

    $("#qRegistration").on('click', '.selectServices .treeListContent i.fas', function (e) {
    //$("#qRegistration .selectServices .treeListContent i.fas").click(function () {
        $(this).next().next().slideToggle('fast');
        $(this).toggleClass("fa-angle-right");
        $(this).toggleClass("fa-angle-down");
    });

    $("#qRegistration").on('click', '.downContent .mCheckbox label', function (e) {
    //$("#qRegistration .downContent .mCheckbox label").click(function () {
        if (!$(this).prev().is(':checked')) {
            var labels = $(this).parents(".textArea").next().find("label");
            for (let i = 0; i < labels.length; i++) {
                if (!$(labels[i]).prev().is(":checked")) {
                    $(labels[i]).prev().click();
                }
            }
        }
        else {
            var labels = $(this).parents(".textArea").next().find("label");
            for (let i = 0; i < labels.length; i++) {
                if ($(labels[i]).prev().is(":checked")) {
                    $(labels[i]).prev().click();
                }
            }
        }
        $("#qRegistration .selectServices .selectResults table tbody tr").remove();

        setTimeout(() => {
            fillSelectedTable();
        }, 200);
    });

    function fillSelectedTable() {
        var rowId = 1;
        var checkboxInputs = $("#qRegistration .downContent .mCheckbox>input");
        for (let j = 0; j < checkboxInputs.length; j++) {
            if ($(checkboxInputs[j]).is(":checked")) {
                var serviceGuid = $(checkboxInputs[j]).attr("data-service-guid");
                var serviceNo = $(checkboxInputs[j]).next().find("span:last-of-type b").text();
                var serviceName = $(checkboxInputs[j]).next().find("span:last-of-type strong").text();
                var serviceRequiredDocuments = $(checkboxInputs[j]).attr("data-serviceRequiredDocs");
                if (serviceRequiredDocuments == undefined)
                    serviceRequiredDocuments = " ";

                var serviceSector = $(checkboxInputs[j]).parents(".textArea").find(".sectorArea span").data("sector");
                var waitingServices = $("#qRegistration .regDashboard .sectorSt .regCard[data-sector='" + serviceSector + "'] .col-4:last-of-type span").text();
                $("#qRegistration .selectServices .selectResults table tbody").append("<tr   data-serviceRequiredDocuments='" + serviceRequiredDocuments +
                    "' data-service-id=" + serviceNo + " class='ui-sortable-helper'> <input type='hidden' name='arrSelectedServices' value=" + serviceGuid + " /> <td>" + rowId + "</td><td>" + serviceName + "</td><td>" + serviceSector + "</td></td><td>" + serviceNo + "</td><td>" + waitingServices + "</td><td><a href='#'><i class='fas fa-times'></i></a></td></tr >");
                rowId++;
            }

        }
        refreshWaitingServices();
    }

    function refreshWaitingServices() {
        setInterval(() => {
            var selectedServiceRows = $("#qRegistration .selectServices .selectResults tbody tr");
            for (let k = 0; k < selectedServiceRows.length; k++) {
                var selectedSectorName = $(selectedServiceRows[k]).find("td:nth-of-type(3)").text();
                var waitingSelectedServices = $("#qRegistration .regDashboard .sectorSt .regCard[data-sector='" + selectedSectorName + "'] .col-4:last-of-type span").text();
                console.log(waitingSelectedServices);
                $(selectedServiceRows[k]).find("td:nth-of-type(5)").text(waitingSelectedServices);
            }
        }, 30000);
    }

    $(document).on("click", "#qRegistration .selectServices .sectorArea", function () {
        $(this).prev().find("label").click();
    });

    $(document).on("click", "#qRegistration .selectServices .selectResults table tbody tr td:last-of-type a", function (e) {
        e.preventDefault();
        var selectedRow = $(this).parents("tr").data("service-id");
        $("#qRegistration .downContent .mCheckbox label span:last-of-type b:contains(" + selectedRow + ")").parent().parent().click();
    });

    $(document).on("click", "#qRegistration .selectServices .searchInputsRow a", function (e) {
        e.preventDefault();
        $("#qRegistration .downContent .mCheckbox span.alert-warning").removeClass("alert-warning");
        var nameInput = $("#qRegistration .selectServices .searchInputsRow #searchServicesForName").val().toLowerCase('az');
        if ($.trim(nameInput.replace(/\s+/g, ' ')) != '') {
            var services = $("#qRegistration .downContent .mCheckbox span:last-of-type");
            for (let i = 0; i < services.length; i++) {
                var currentElement = $(services[i]).text().toLowerCase('az');
                if (currentElement.indexOf(nameInput) != -1) {
                    $(services[i]).addClass("alert-warning");
                }
            }
            $("#qRegistration .selectServices .treeListContent i.fas").removeClass("fa-angle-down").addClass("fa-angle-right");
            $("#qRegistration .selectServices .treeListContent .down").slideUp('fast');
            $("#qRegistration .downContent .mCheckbox span.alert-warning").parents(".down").slideDown('fast');
            $("#qRegistration .downContent .mCheckbox span.alert-warning").parents(".down").prev().prev().removeClass("fa-angle-right").addClass("fa-angle-down");
        }
    });

    $("#qRegistration").on('keypress', '.selectServices .searchInputsRow input', function (e) {
    //$("#qRegistration .selectServices .searchInputsRow input").keypress(function (e) {
        if (e.keyCode == 13) {
            $("#qRegistration .selectServices .searchInputsRow a").click();
        }
    });


    $(document).on("mouseup", "#qRegistration .selectResults tbody td", function () {
        refreshTableRowsNo();
    });



    function refreshTableRowsNo() {
        setTimeout(() => {
            var k = 0;
            var sortedLength = $("#qRegistration .selectResults tbody tr:not(.ui-sortable-placeholder)");
            for (let i = 0; i < sortedLength.length; ++i) {
                $(sortedLength[i]).find("td:first-of-type").text(i + 1);
            }
        }, 10);
    }

    $("#qRegistration").on('click',  function (e) {
    //$("#qRegistration").click(function (e) {
        if (!$(e.target).closest('#qRegistration .selectResults tbody').length && $('#qRegistration .selectResults tbody tr.selectedRow').length && !$(e.target).closest('#qRegistration .necessaryDocs').length) {
            $('#qRegistration .selectResults tbody tr.selectedRow').removeClass("selectedRow");
            $("#qRegistration .selectServices .necessaryDocs tbody").empty();
        }
    });

    $("#qRegistration").on('click', '.formSection .aboutPassRow .mCheckbox label', function (e) {
    //$("#qRegistration .formSection .aboutPassRow .mCheckbox label").click(function () {
        $(this).parents(".aboutPassRow").find(".searchingForFIN textarea").focus();
    });

    $("#qRegistration").on('click', '.acceptButton .mBtn-pr', function (e) {
    //$("#qRegistration .acceptButton .mBtn-pr").click(function (e) {
        e.preventDefault();
        checkMultiServiceAllow();
    });

    function checkMultiServiceAllow() {
        if ($("#AllowMultiService").val() == "1") {
            return true;
        }
        else {
            if ($("#qRegistration .selectResults tbody tr").length > 1) {
                Swal.fire({
                    type: 'error',
                    title: 'Yalnız 1 xidmət seçilə bilər',
                    showConfirmButton: false,
                    timer: 1500
                });
                return false;
            }
            else {
                return true;
            }
        }
    }
   
});
