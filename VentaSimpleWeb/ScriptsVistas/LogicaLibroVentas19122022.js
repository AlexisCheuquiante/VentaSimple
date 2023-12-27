$(document).ready(function () {

    ObtenerEstablecimientos();
    ObtenerUsuariosAutorizadores();
    
});

function ObtenerEstablecimientos() {

    $.ajax({
        url: window.urlObtenerEstablecimientos,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroEstablecimiento').dropdown('clear');
            $('#cmbFiltroEstablecimiento').empty();
            $('#cmbFiltroEstablecimiento').append('<option value="-2">[Seleccione establecimiento]</option>');
            $('#cmbFiltroEstablecimiento').append('<option value="-1">Todas</option>');
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
function PreparaNotaCredito(id) {
    $('#Documento').val(id);

}

function CrearNotaCredito() {
    $('#btnEmitirNota').addClass('loading');
    $('#btnEmitirNota').addClass('disabled');
    $('#btnCancelar').addClass('loading');
    $('#btnCancelar').addClass('disabled');

    id = $('#Documento').val();

    var filtro = {
        BoletaId: id,
        UsrId: $('#cmbResponsable').val(),
        Clave_Autorizacion: $('#txtClaveAutorizacion').val(),
    }

    id = id;
    $.ajax({
        url: window.urlCrearNotaCredito,
        type: 'POST',
        data: { filtro: filtro },
        success: function (data) {
            if (data == 'NoAutorizado') {

                $('#divNoAutorizado').removeClass("hidden");
                $('#btnEmitirNota').removeClass('loading');
                $('#btnEmitirNota').removeClass('disabled');
                $('#btnCancelar').removeClass('loading');
                $('#btnCancelar').removeClass('disabled');
            }
            if (data != 'error' && data != 'NoAutorizado') {
                window.open(data, "_blank");
                setTimeout(() => { window.location.href = '/LibroVentas?limpiar=1' }, 1000);
            }
            if (data == 'error') {
                $('#divConsultaEmitir_V2').addClass("hidden");
                $('#divErrorEmitirNuevo').removeClass("hidden");
                $('#btnEmitirNota').removeClass('loading');
                $('#btnEmitirNota').removeClass('disabled');
                $('#btnCancelar').removeClass('loading');
                $('#btnCancelar').removeClass('disabled');
                setTimeout(() => { window.location.href = '/LibroVentas?limpiar=1' }, 1000);
            }
            
        },

        error: function () {
            alert('Error al emitir la nota de crédito');
        }
    });
    /*_id = id;*/

}
function ObtenerUsuariosAutorizadores() {

    $.ajax({
        url: window.urlObtenerUsuariosAutorizadores,
        type: 'POST',
        success: function (data) {
            $('#cmbResponsable').dropdown('clear');
            $('#cmbResponsable').empty();
            $('#cmbResponsable').append('<option value="0">[Seleccione usuario]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Nombre + '</option>';
                    $('#cmbResponsable').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los usuarios');
        }
    });

}
function PreparaAnularBoleta(id) {
    $('#Documento').val(id);

}
function AnularBoleta() {
    $('#btnAnularBE').addClass('loading');
    $('#btnAnularBE').addClass('disabled');
    $('#btnCancelarAnular').addClass('loading');
    $('#btnCancelarAnular').addClass('disabled');

    id = $('#Documento').val();

    var strParams = {
        Id: id,
    }

    id = id;
    $.ajax({
        url: window.urlAnularBoleta,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data != 'error') {
                $('#divExitoAnular').removeClass("hidden");
                setTimeout(() => { window.location.href = '/LibroVentas?limpiar=1' }, 1000);
            }
            if (data == 'error') {
                $('#divExitoAnular').addClass("hidden");
                $('#divErrorAnular').removeClass("hidden");
                $('#btnAnularBE').removeClass('loading');
                $('#btnAnularBE').removeClass('disabled');
                $('#btnCancelarAnular').removeClass('loading');
                $('#btnCancelarAnular').removeClass('disabled');
                setTimeout(() => { window.location.href = '/LibroVentas?limpiar=1' }, 1000);
            }

        },

        error: function () {
            alert('Error al intentar anular la venta');
        }
    });
    /*_id = id;*/

}