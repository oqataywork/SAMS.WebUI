
/*----------------------Ruslan Changes 08.01.2020---- Global delete function with modal----------------------*/
// sened yuklenenden sonra 

function deleteAnyItem(e, stringGuidID, linkForDelete) {
    e.preventDefault();

    var id = stringGuidID;
    var link = linkForDelete;

    BootstrapDialog.show({
        title: 'Xəbərdarlıq',
        message: 'Silmək istədiyinizə əminsinizmi?',
        buttons: [{
            label: 'Xeyr',
            cssClass: 'mBtn mBtn-sec',
            action: function (dialogItself) {
                dialogItself.close();
            }
        },
        {
            label: 'Bəli',
            cssClass: 'mBtn mBtn-dng',
            action: function (dialogItself) {


                $.ajax({
                    type: "POST",
                    url: link,
                    data: { "stringGuidID": id },
                    dataType: 'JSON',
                    success: function (respData) {
                        if (respData.status == "true") {
                            Swal.fire({ type: 'success', text: respData.message, showConfirmButton: true, confirmButtonText: "Bağla", allowOutsideClick: false,timer: 1500 });
                            //.then(() => {
                                if (!$("main .mTable").length) {
                                    refreshGrid();
                                    refreshRightMenu();
                                    if ($(".buttonsRow .fa-trash").parent().hasClass("circleBtn")) {
                                        $(".buttonsRow .fa-trash").parent().remove();
                                    }
                                }
                                else {
                                    window.location.reload();
                                }
                            //});
                        }
                        else {
                            Swal.fire({ type: 'error',text: respData.message, showConfirmButton: true, confirmButtonText: "Bağla", allowOutsideClick: false, });
                        }
                    }
                });
                dialogItself.close();
            }
        }]
    });
}


  //$(document).on("click",".rightMenu .menuFoot .buttonsRow .circleBtn",function (e, id ,link) {
 //    e.preventDefault();
 //    BootstrapDialog.show({
 //        title: 'Xəbərdarlıq',
 //        message: 'Silmək istədiyinizə əminsinizmi?',
 //        buttons: [{
 //            label: 'Xeyr',
 //            cssClass: 'mBtn mBtn-sec',
 //            action: function (dialogItself) {
 //                dialogItself.close();
 //            }
 //        },
 //        {
 //            label: 'Bəli',
 //            cssClass: 'mBtn mBtn-dng',
 //            action: function (dialogItself,id,link) {
 //                dialogItself.close();
 //            }
 //        }]
 //    });
 //})

/*--------------------------------------------------------------------------------------------------------*/