﻿
//navegadores modernos
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Usuarios/Listar", ["Id", "Nombre", "Cuenta", "Rol", "Activo", "Fecha Alta"], 0, 0, true, false);
    $.get("/Usuarios/listarRoles", function (data) {
        window.llenarCombo(data, document.getElementById("RolIdInt"), true)
    });
});


function GetInfoById(id) {
    window.GetById("/Usuarios/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Usuarios/List", ["Id", "Nombre", "Cuenta", "Rol", "Activo", "Fecha Alta"], 0, 0, true, true);
}



function Add() {
    var form = document.getElementById("form");
    window.add("/Usuarios/Add", form, ["Id", "Nombre", "Cuenta", "Rol", "Activo", "Fecha Alta"], false)
}


function ChangeInfoStatus(id) {
    window.ChangeStatus("/TipoDeSeguimiento/ChangeStatus", id, ["Id", "Nombre", "Cuenta", "Rol", "Activo", "Fecha Alta"]);
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

