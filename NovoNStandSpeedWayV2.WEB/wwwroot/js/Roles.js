

var arrayCellsData = ["Id", "Descripcion", "ActivoBit", "FechaAlta"];
var arrayColumnsTable = ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"];


//navegadores modernos
document.addEventListener("DOMContentLoaded", async function (event) {
    var data = await window.list("/Roles/List");
    window.Table(arrayColumnsTable, data, arrayCellsData);
});



function GetInfoById(id) {
    window.GetById("/Roles/GetByID", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Roles/List", ["Id", "Nombre", "Activo", "Fecha Alta"], 0, null);
}



async function Add() {
    var form = document.getElementById("form");
    //await window.add("/Roles/Add", form, ["Id", "Nombre", "Activo", "Fecha Alta"])

    var data = await window.add("/Roles/Add", form, ["Id", "Nombre", "Activo", "Fecha Alta"])
    if (data == "success") {
        window.list("/Roles/Add", form, ["Id", "Nombre", "Activo", "Fecha Alta"], 0, null);
    }
}


function ChangeInfoStatus(id) {
    window.ChangeStatus("/TipoDeSeguimiento/ChangeStatus", id, ["Id", "Nombre", "Activo", "Fecha Alta"]);
}


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