$(document).ready(function () {

    ObtenerEstablecimientos();
    
});

function ObtenerEstablecimientos() {

    $.ajax({
        url: window.urlObtenerEstablecimientos,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroEstablecimiento').dropdown('clear');
            $('#cmbFiltroEstablecimiento').empty();
            $('#cmbFiltroEstablecimiento').append('<option value="-1">[Seleccione establecimiento]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.NombreEstablecimiento + '</option>';
                    $('#cmbFiltroEstablecimiento').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los establecimientos');
        }
    });

}
function BusquedaFiltro() {
    $('#btnBuscarFiltro').addClass("loading");
    $('#btnBuscarFiltro').addClass("disabled");

    $('#dimmer').dimmer('show');

    var entity = {
        Desde: $('#txtFiltroDesde').val(),
        Hasta: $('#txtFiltroHasta').val(),
        EstId: $('#cmbFiltroEstablecimiento').val(),
    }
    $.ajax({
        url: window.urlBusquedaFiltro,
        type: 'POST',
        data: { entity: entity },
        success: function (data) {

            window.location.href = '/LibroVentas';

        },
        error: function () {
            showMessage('#divMensajePublicacionViaje', 'danger', 'Ocurrió un error al guardar la información. Por favor intente nuevamente.');
            //hideLoading();
        }
    });

}

function ObtenerPdf(numeroBoleta) {

    $('#Documento').val(numeroBoleta);

    folioSII = $('#Documento').val();

    $.ajax({
        url: window.urlObtenerPdf,
        type: 'POST',
        data: { folioSII: folioSII },
        success: function (data) {
            if (data != 'error') {
                setTimeout(() => { window.open(data, "_blank"); }, 2000);
            }
            if (data === 'error') {
                $('#divError').removeClass("hidden");
                $('#btnGuardarBoleta').removeClass('loading');
                $('#btnGuardarBoleta').removeClass('disabled');
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar la bodega.' + data);
        }
    });
}

function ObtenerPdfNotaCredito(numero) {

    $('#Documento').val(numero);

    folioSII = $('#Documento').val();

    $.ajax({
        url: window.urlObtenerPdfNotaCredito,
        type: 'POST',
        data: { folioSII: folioSII },
        success: function (data) {
            if (data != 'error') {
                setTimeout(() => { window.open(data, "_blank"); }, 2000);
            }
            if (data === 'error') {
                $('#divError').removeClass("hidden");
                $('#btnGuardarBoleta').removeClass('loading');
                $('#btnGuardarBoleta').removeClass('disabled');
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar la bodega.' + data);
        }
    });
}

function CrearNotaCredito(id) {

    id = id;
    $.ajax({
        url: window.urlCrearNotaCredito,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            if (data != 'error') {
                setTimeout(() => { window.open(data, "_blank"); }, 2000);
            }
        },

        error: function () {
            alert('Error al emitir la nota de crédito');
        }
    });
    /*_id = id;*/

}