

document.addEventListener("DOMContentLoaded", async function () {
    var data = await window.list("/Producto/List", "");
    LoadProducts(data);
});


function GetInfoById(id) {
    window.GetById("/Producto/GetById", id);
}


async function Add() {
    var form = document.getElementById("form");
    var formData = new FormData(form);
    var data = await window.add("/Producto/Add", formData, "", false);
    LoadProducts(data, true);
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


async function LoadProducts(data, refresh = false) {

    var contenido = "";
    var UrlDelete = '"' + "/Producto/Delete" + '"';
    var itemId;
    var container = document.getElementById("container");


    if (data != null) {

        for (row = 0; row < data.length; row++) {

            var name = data[row]["Name"].toString().replace(" ", "⁁");
            var id = '"' + data[row]["Id"] + '"';
            var description = data[row]["Description"];
            var image = data[row]["Image"];

            itemId = data[row]["Id"].toString() + data[row]["Id"].toString();

            contenido += "<div class='col-lg-4 col-sm-6 card-bordes mt-4' id='" + itemId + "'>";
            contenido += "<figure class='card card-product'>";
            contenido += "<div class='img-wrap'>";
            contenido += "<a href='/Producto/Detalle/" + name + "'>";


            contenido += "<img src='" + image + "' alt='" + name + "' class='img-fluid'>";
            contenido += "</a>";
            contenido += "</div>";

            contenido += "<div class='info-wrap img-fluid'>";

            contenido += "<div class='row d-flex justify-content-center'>";
            contenido += "<a class='pN' href='/Producto/Detalle/" + name + "'>";
            contenido += "<h5 class='product-name mt-3'>" + description + "</h5>";
            contenido += "</a>";
            contenido += "</div>";

            contenido += "<div class='row d-flex justify-content-center' style='cursor: pointer;' onclick='GetInfoById(" + id + ")'>";
            contenido += "<a class='btn btn-sm btn-primary' >";
            contenido += "<span class='text-white'>Editar</span>";
            contenido += "</a>";
            contenido += "</div>";

            contenido += "<div class='row d-flex justify-content-center' style='cursor: pointer;' onclick='DeleteById( " + UrlDelete + " ," + id + ")'>";
            contenido += "<a class='btn btn-sm btn-danger' >";
            contenido += "<span class='text-white'>Eliminar</span>";
            contenido += "</a>";
            contenido += "</div>";

            contenido += "</div>";

            contenido += "</figure>";
            contenido += " </div >";

            contenido += " </div >";
            contenido += " </div >";

            if (refresh == true) {

                if ($("#" + itemId).length > 0) {
                    document.getElementById(itemId).remove();
                }

                container.innerHTML = contenido + container.innerHTML;
            }

        }//enf for


    }// end if data different of null



    if (refresh == false) {
        $("#container").empty();
        container.innerHTML = contenido;
    }

    footer.classList.remove("fixed-bottom");


}

