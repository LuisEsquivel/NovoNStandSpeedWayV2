

var arrayCellsData = ["Id", "Descripcion", "ActivoBit", "FechaAlta"];
var arrayColumnsTable = ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"];


//navegadores modernos
document.addEventListener("DOMContentLoaded", async function (event) {
    var data = await window.list("/CentroDeCostos/List");
    window.Table(arrayColumnsTable, data, arrayCellsData);
});

document.addEventListener(("click"), () => {

    if ($("#Accion").val() == "update") {
        $("#UbicacionIdVar").prop("readonly", true);
    } else {
        $("#UbicacionIdVar").prop("readonly", false);
    }

});


function GetInfoById(id) {
    window.GetById("/CentroDeCostos/GetById", id);
    $("#Accion").val("update");
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}



async function Add() {
    var form = await document.getElementById("form");
    window.add("/CentroDeCostos/Add", form, arrayColumnsTable, true, arrayCellsData)
}



$(document).keyup(function (e) {
    if (e.which == 27) {

        if (window.maximized == false) {
            window.Cerrar();
        }

        if (window.maximized == true) {
            window.MinimizeModal();
            window.AbrirModal();
        }

    }
});

