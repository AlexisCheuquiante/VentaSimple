var _arrayCuadreCaja = [];
var _total = 0;

$(document).ready(function () {

    ObtenerUsuarios();
    ObtenerEstablecimientos();
});

function ObtenerUsuarios() {

    $.ajax({
        url: window.urlObtenerUsuarios,
        type: 'POST',
        success: function (data) {
            $('#cmbUsuario').dropdown('clear');
            $('#cmbUsuario').empty();
            $('#cmbUsuario').append('<option value="0">[Seleccione usuario]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Nombre + '</option>';
                    $('#cmbUsuario').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los establecimientos');
        }
    });

}
function ObtenerCuadreCaja() {

    if (ValidaCuadre() == false)

        return;

    var filtroCaja = {
        Fecha_Ingreso: $('#txtFecha').val(),
        UsrId: $('#cmbUsuario').val(),
    };

    $.ajax({
        url: window.urlObtenerCuadreCaja,
        type: 'POST',
        data: { filtro: filtroCaja },
        success: function (data) {
            var total = 0;

            var tabla = "<table class='ui celled table'>";
            tabla = tabla + "<thead><tr><th>Tipo de documento</th><th>Tipo de Pago</th><th>Total</th></tr></thead>";
            tabla = tabla + "<tbody>";
            $.each(data, function (value, item) {
                tabla = tabla + "<tr>";
                tabla = tabla + "<td>" + item.Tipo_Documento + "</td><td>" + item.Tipo_Pago + "</td><td>" + item.Total + "</td>";
                tabla = tabla + "</tr>";
                if (item.Tido_Id == 1) {
                    total = total + parseInt(item.Total);
                }
                if (item.Tido_Id == 2) {
                    total = total - parseInt(item.Total);
                }
            });

            tabla = tabla + "<tr>";
            tabla = tabla + "<td></td><td><strong> Total final</strong></td><td>" + total + "</td>";
            tabla = tabla + "</tr>";

            _total = total;


            tabla = tabla + "</tbody>";

            tabla = tabla + "</table>";

            $('#grdDatos').html(tabla);
            $('#divInfo').removeClass("hidden");
        },
        error: function (ex) {
            alert('Error al preparar cuadre de cajas');
        }
    });

}
function ValidaCuadre() {
    var esValido = true;
    var errores = [];

    LimpiaEstilosCuadre();

    if ($('#txtFecha').val() === '') {
        $('#divtxtFecha').addClass("error");
        errores.push('Debe indicar la fecha del cierre de caja');
    }
    if ($('#cmbUsuario').val() === '0') {
        $('#divcmbUsuario').addClass('error');
        errores.push('Debe indicar el usuario del cierre de caja')
    }

    if (errores.length > 0) {
        var mensaje = '';
        $('#DivMessajeErrorCuadre').removeClass("hidden");

        for (i = 0; i < errores.length; i++) {
            mensaje += '<li>' + errores[i] + '</li>';
        }

        mensaje += '</ul>';
        $('#listMessajeErrorCuadre').empty();

        $('#listMessajeErrorCuadre').prepend(mensaje);

        // showMessage('#divMensajeNuevoCamion', 'danger', mensaje);
        return false;
    }
    else {
        return true;
    }


}
function LimpiaEstilosCuadre() {
    //Limpio el estilo Error antes de validar
    $('#divtxtFecha').removeClass("error");
    $('#divcmbUsuario').removeClass("error");
    $('#listMessajeErrorCuadre').empty();
    $('#DivMessajeErrorCuadre').addClass("hidden");
}
function CerrarCaja() {

    if (ValidaGuardar() == false)

        return;

    $('#btnCerrarCaja').addClass('loading');
    $('#btnCerrarCaja').addClass('disabled');
    $('#btnVolverCerrarCaja').addClass('loading');
    $('#btnVolverCerrarCaja').addClass('disabled');

    var strParams = {
        Id_Cajero: $('#cmbUsuario').val(),
        Fecha_Cierre: $('#txtFecha').val(),
        Total: _total,
        Observacion: $('#txtObservacion').val(),
    };

    $.ajax({
        url: window.urlInsertarCierreCaja,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data != 'error' && data != 'existente') {
                $('#divExitoCerrarCaja').removeClass("hidden");
                window.open(data, "_blank");
                setTimeout(() => { window.location.href = '/CierreCaja?limpiar=1' }, 2000);
            }
            if (data === 'error') {
                $('#divErrorCerrarCaja').removeClass("hidden");
                $('#btnCerrarCaja').removeClass('loading');
                $('#btnCerrarCaja').removeClass('disabled');
                $('#btnVolverCerrarCaja').removeClass('loading');
                $('#btnVolverCerrarCaja').removeClass('disabled');
            }
            if (data === 'existente') {
                $('#divExistenteCerrarCaja').removeClass("hidden");
                $('#btnVolverCerrarCaja').removeClass('loading');
                $('#btnVolverCerrarCaja').removeClass('disabled');
            }
        },
        error: function (ex) {
            alert('Error al cerrar caja');
        }
    });

}
function ValidaGuardar() {
    var esValido = true;
    var errores = [];

    LimpiaEstilos();

    if ($('#txtFecha').val() === '') {
        $('#divtxtFecha').addClass("error");
        errores.push('Debe indicar la fecha del cierre de caja');
    }
    if ($('#cmbUsuario').val() === '0') {
        $('#divcmbUsuario').addClass('error');
        errores.push('Debe indicar el usuario del cierre de caja')
    }
    if (_total <= 0) {
        errores.push('El total del cierre de caja no puede ser cero')
    }

    if (errores.length > 0) {
        var mensaje = '';
        $('#DivMessajeErrorGeneral').removeClass("hidden");

        for (i = 0; i < errores.length; i++) {
            mensaje += '<li>' + errores[i] + '</li>';
        }

        mensaje += '</ul>';
        $('#listMessajeError').empty();

        $('#listMessajeError').prepend(mensaje);

        // showMessage('#divMensajeNuevoCamion', 'danger', mensaje);
        return false;
    }
    else {
        return true;
    }


}
function LimpiaEstilos() {
    //Limpio el estilo Error antes de validar
    $('#divtxtFecha').removeClass("error");
    $('#divcmbUsuario').removeClass("error");
    $('#listMessajeError').empty();
    $('#DivMessajeErrorGeneral').addClass("hidden");
}
function BusquedaFiltro() {
    $('#btnBuscarFiltro').addClass("loading");
    $('#btnBuscarFiltro').addClass("disabled");

    $('#dimmer').dimmer('show');

    var entity = {
        Desde: $('#txtFiltroFechaDesde').val(),
        Hasta: $('#txtFiltroFechaHasta').val(),
        EstId: $('#cmbFiltroEstablecimiento').val(),
    }
    $.ajax({
        url: window.urlBusquedaFiltro,
        type: 'POST',
        data: { entity: entity },
        success: function (data) {

            window.location.href = '/CierreCaja';

        },
        error: function () {
            showMessage('#divMensajePublicacionViaje', 'danger', 'Ocurrió un error al guardar la información. Por favor intente nuevamente.');
            //hideLoading();
        }
    });

}
function ObtenerEstablecimientos() {

    $.ajax({
        url: window.urlObtenerEstablecimientos,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroEstablecimiento').dropdown('clear');
            $('#cmbFiltroEstablecimiento').empty();
            $('#cmbFiltroEstablecimiento').append('<option value="0">[Seleccione establecimiento]</option>');
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