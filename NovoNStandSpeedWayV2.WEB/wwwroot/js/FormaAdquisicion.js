
var arrayCellsData = ["Id", "Descripcion", "ActivoBit", "FechaAlta"];
var arrayColumnsTable = ["Centro de Costo", "Descripción", "Activo", "Fecha Alta"];


//navegadores modernos
document.addEventListener("DOMContentLoaded", async function (event) {
    var data = await window.list("/FormaAdquisicion/List");
    window.Table(arrayColumnsTable, data, arrayCellsData);
});




function GetInfoById(id) {
    window.GetById("/FormaAdquisicion/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/FormaAdquisicion/List", ["Id", "Descripción", "Activo", "Fecha Alta"], 0, null);
}



async function Add() {
    var form = document.getElementById("form");
    window.add("/FormaAdquisicion/Add", form, arrayColumnsTable, true, arrayCellsData)
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