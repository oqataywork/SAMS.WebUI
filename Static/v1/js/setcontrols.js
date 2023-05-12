var readonly = @Html.Raw(Json.Encode(Model.ReadOnly));
var visibilty;
if (readonly)
    visibilty = "hidden";
else
    visibilty = "visible";

document.querySelectorAll('.form-control').forEach(el => {
    el.disabled = readonly;
});
document.querySelectorAll('.buttonsRow').forEach(el => {
    el.style.visibility = visibilty;
});