// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.bundle");

// Write your JavaScript code.


// Función para abrir un modal con contenido dinámico
function OpenModal(encabezado, contenido) {
    // Asigna el contenido y el encabezado al modal
    $('#AppModalLabel').text(encabezado);
    $('#AppModalBody').html(contenido);

    // Abre el modal sin permitir cierre por clic fuera o tecla Esc
    $('#AppModal').modal({ backdrop: 'static', keyboard: false });
    $('#AppModal').modal('show');
}

function CloseModal() {

    setInterval(function () {

        // Limpia el contenido del modal
        //$('#AppModalLabel').text('');
        //$('#AppModalBody').html('');
        // Cierra el modal
        $('#AppModal').modal('hide');

    }, 1000);


}

function obtenerDatosFormulario(idForm) {

    const formulario = document.getElementById(idForm);

    if (formulario == undefined) {
        console.log("No se encontro formulario con el id", idForm)
    }

    const datos = {};

    for (let i = 0; i < formulario.elements.length; i++) {
        const elemento = formulario.elements[i];
        if (["INPUT", "SELECT"].includes(elemento.tagName)) {
            datos[elemento.name] = elemento.value;
        }
    }

    return datos;
}

function GenerateCSV(url, idFilterForm) {
    var formData = obtenerDatosFormulario(idFilterForm);
    formData["IsReporteCsv"] = true;

    const contenido = `<div class="text-center">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>`
    OpenModal("Generando archivo csv", contenido);

    $.ajax({
        type: 'GET',
        url: url,
        data: formData,
        beforeSend: function () {

        },
        success: function (response) {

            var blob = new Blob([response], { type: 'application/octet-stream' });
            // Crea un enlace temporal para la descarga del archivo
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = document.title + ".csv";
            link.click();

            // Elimina el enlace temporal
            window.URL.revokeObjectURL(link.href);

        },
        error: function (xhr, status, error) {
            // Acción a realizar en caso de error
            console.error('Error en la petición:', status, error);
            Alert("Se presento un error, vuelve y cargue la pagina");
        },
        complete: function () {

            CloseModal();

        }
    });
}

function GenerateDataTable(urlGetElementos, idTable, idFormFiltro, columnsData, rowCallbackData, orderData = null, lengthMenuData = null) {

    var formData = obtenerDatosFormulario(idFormFiltro);
    var order2 = orderData ?? [[0, 'desc']];
    var lengthMenuData2 = lengthMenuData ?? [10, 25, 50, 100, -1]

    var $tb_rol = $(`table#${idTable}`);
    var table = $tb_rol.DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.5/i18n/es-ES.json',
            "emptyTable": "No hay información",
            "processing": "Procesando..."
        },
        searching: false,
        "lengthMenu": lengthMenuData2,
        scrollY: '500px',
        scrollX: true,
        ajax: {
            url: urlGetElementos,
            type: 'GET',
            processing: true,
            serverSide: true,
            dataSrc: "",
            data: function (d) {

                Object.keys(formData).forEach(propiedad => {
                    d[propiedad] = formData[propiedad];
                });

            },
            error: function (err) {
                // FUNCION ALERT ERROR.
                console.error(err);
                alert("Se presento un error, vuelve y cargue la pagina");
            },
            complete: function (xhr, status) {

            }
        },
        columns: columnsData,
        dom: 'Bfrtip',
        select: true,
        order: order2,
        rowCallback: rowCallbackData,
        "drawCallback": function (settings) {

            $('.dt-buttons').css('float', 'right');

            const option = lengthMenuData2;
            var optionHtml = "";
            const pageLen = table.page.info().length;
            for (var i = 0; i < option.length; i++) {
                var elemen = "";
                if (option[i] == -1) {
                    elemen = `<option selected value="${option[i]}">Todos</option> `;
                } else {
                    elemen = `<option selected value="${option[i]}">${option[i]}</option> `;
                }

                if (option[i] != pageLen) {
                    elemen = elemen.replace("selected", '');
                }

                optionHtml += elemen
            }

            const idSelect = `select-${idTable}`;
            $(".dataTables_info").append(`
                <select style="float: right;" id="${idSelect}">
                    ${optionHtml}
                </select>`);
            $('#' + idSelect).change(function () {

                var length = $(this).val();
                $tb_rol.DataTable().page.len(length).draw();

            });

        },
        buttons: [
            {
                text: 'Filtrar',
                className: 'btn btn-default',
                "attr": {
                    "id": "btn_Search_Dt"
                },
                action: function (e, dt, node, config) {
                    formData = obtenerDatosFormulario(idFormFiltro);
                    dt.ajax.reload(null, false);
                }
            }
        ]
    });

    return table;

}
