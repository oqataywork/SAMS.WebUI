function imageFileAsURL(evt) {
    var filesSelected = evt.target.files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];

        if (!fileToLoad.type.match('image.*')) {
            return;
        }

        var fileReader = new FileReader();
        fileReader.onload = function (fileLoadedEvent) {

            try
            {
                $("#fileContent").val(fileLoadedEvent.target.result);
                console.log(fileLoadedEvent.target);
                $("#fileContent").change();
                $("#uploadImage").attr("src", fileLoadedEvent.target.result);
                $("#fileType").val(fileToLoad.type);
                console.log(fileToLoad)
            }
            catch (ex) {
                console.log("Exception on file select...", ex);
            }

            
        };
        fileReader.readAsDataURL(fileToLoad);
    }
}

$('#btnUpload').click(function () {
    $('#file').trigger('click');
    document.getElementById("btnSave").style.visibility = "visible";
    document.getElementById("btnUpload").style.visibility = "hidden";
    //$('#btnSave').style.visibility = "visible";
    //$("#btnSave").attr('src', selectedRow.Image);
});
$('#file').on('change', imageFileAsURL);