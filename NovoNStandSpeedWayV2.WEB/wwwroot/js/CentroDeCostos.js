

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


function GetTraining(id) {
    window.list("/CentroDeCostos/List", ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"], 0, null);
}



async function Add() {
    var form = await document.getElementById("form");
    var data = await window.add("/CentroDeCostos/Add", form, ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"])
    if (data == "success") {
        window.list("/CentroDeCostos/Listar", ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"], 0, null);
    }
}


//function ChangeInfoStatus(id) {
//    window.ChangeStatus("/CentroDeCostos/ChangeStatus", id, ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"]);
//}


function Modal(url) {
    window.AbrirModal(url);
}


function Cerrar() {

    $('#Modal iframe').attr('src', '');
    $("#Modal").modal("hide");
    window.modal_open = false;

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

