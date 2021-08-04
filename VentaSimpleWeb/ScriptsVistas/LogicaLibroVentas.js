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