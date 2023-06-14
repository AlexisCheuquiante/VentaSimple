$(document).ready(function () {

    ObtenerModulos();
    ObtenerUsuario();
    ObtenerEstablecimientos();
});

function ObtenerModulos() {

    $.ajax({
        url: window.urlObtenerModulos,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroModulo').dropdown('clear');
            $('#cmbFiltroModulo').empty();
            $('#cmbFiltroModulo').append('<option value="-1">[Seleccione módulo]</option>');
            $.each(data,
                function (value, item) {
                    var texto = '<option value="' + item.Id + '">' + item.Descripcion + '</option>';
                    $('#cmbFiltroModulo').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los módulos');
        }
    });

}
function ObtenerUsuario() {

    $.ajax({
        url: window.urlObtenerUsuario,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroUsuario').dropdown('clear');
            $('#cmbFiltroUsuario').empty();
            $('#cmbFiltroUsuario').append('<option value="-1">[Seleccione usuario]</option>');
            $.each(data,
                function (value, item) {
                    var texto = '<option value="' + item.Id + '">' + item.Nombre + '</option>';
                    $('#cmbFiltroUsuario').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los usuarios');
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

    $('#btnbuscar').addClass("loading");
    $('#btnbuscar').addClass("disabled");

    var entity = {
        Desde: $('#txtFiltroFechaDesde').val(),
        Hasta: $('#txtFiltroFechaHasta').val(),
        Mod_Id: $('#cmbFiltroModulo').val(),
        UsrId: $('#cmbFiltroUsuario').val(),
        EstId: $('#cmbFiltroEstablecimiento').val(),
    }
    $.ajax({
        url: window.urlBusquedaFiltro,
        type: 'POST',
        data: { entity: entity },
        success: function (data) {

            window.location.href = '/Bitacora';

        },
        error: function () {
            showMessage('#divMensajePublicacionViaje', 'danger', 'Ocurrió un error al guardar la información. Por favor intente nuevamente.');
            //hideLoading();
        }
    });

}