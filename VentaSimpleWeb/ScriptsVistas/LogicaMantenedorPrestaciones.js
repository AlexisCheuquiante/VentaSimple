var _id = 0;

$(document).ready(function () {

    ObtenerEstablecimientos();

});

function GuardarPrestacion() {

    if (ValidaGuardar() === false) {
        //alert('no valido');
        return;
    }

    $('#btnGuardaPrestacion').addClass('loading');
    $('#btnGuardaPrestacion').addClass('disabled');

    $('#btnVolverPrestacion').addClass('loading');
    $('#btnVolverPrestacion').addClass('disabled');

    var strParams = {
        Id: _id,
        Est_Id: $('#cmbEstablecimiento').val(),
        Descripcion: $('#txtPrestacion').val(),
        Codigo: $('#txtCodigo').val(),
        Valor: $('#txtValor').val(),
    };

    $.ajax({
        url: window.urlInsertarPrestacion,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data === 'exito') {
                $('#DivMessajeErrorGeneral').addClass("hidden");
                $('#divExito').removeClass("hidden");
                setTimeout(() => { location.reload(); }, 2000);
            }
            //if (data === 'error') {
            //    $('#divErroLogin').removeClass("hidden");
            //}

        },
        error: function (ex) {
            alert('Error al guardar el producto');
        }
    });

}
function ValidaGuardar() {
    var errores = [];

    LimpiaEstilos();

    if ($('#cmbEstablecimiento').val() === '0') {
        $('#divcmbEstablecimiento').addClass('error');
        errores.push('Debe indicar al menos un establecimiento')
    }
    if ($('#txtPrestacion').val() === '') {
        $('#divtxtPrestacion').addClass("error");
        errores.push('Debe indicar la descripción de la prestación');
    }
    if ($('#txtValor').val() === '') {
        $('#divtxtValor').addClass("error");
        errores.push('Debe indicar el valor de la prestación');
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
    $('#divcmbEstablecimiento').removeClass("error");
    $('#divtxtPrestacion').removeClass("error");
    $('#divtxtValor').removeClass("error");
}
function ObtenerEstablecimientos() {

    $.ajax({
        url: window.urlObtenerEstablecimientos,
        type: 'POST',
        success: function (data) {
            $('#cmbEstablecimiento').dropdown('clear');
            $('#cmbEstablecimiento').empty();
            $('#cmbEstablecimiento').append('<option value="0">[Seleccione establecimiento]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.NombreEstablecimiento + '</option>';
                    $('#cmbEstablecimiento').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los establecimientos');
        }
    });

}
function ObtenerPrestacion(id) {
    $('#idPrestacionSeleccionada').val(id);

    id = $('#idPrestacionSeleccionada').val();
    $.ajax({
        url: window.urlObtenerPrestacion,
        type: 'POST',
        data: { id: id },
        success: function (data) {

            $("#cmbEstablecimiento").dropdown('set selected', data.Est_Id);
            $('#txtPrestacion').val(data.Descripcion);
            $('#txtCodigo').val(data.Codigo)
            $('#txtValor').val(data.Valor);
        },

        error: function () {
            alert('Error al cargar la prestación seleccionada');
        }
    });
    _id = id;
}
function PreparaEliminarPrestacion(id) {
    $('#idPrestacionSeleccionada').val(id);

}
function EliminaPrestacion() {

    $('#btnEliminar').addClass('loading');
    $('#btnEliminar').addClass('disabled');
    $('#btnCancelar').addClass('loading');
    $('#btnCancelar').addClass('disabled');

    id = $('#idPrestacionSeleccionada').val();
    $.ajax({
        url: window.urlEliminarPrestacion,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            if (data === 'exito') {
                $('#divConsultaElimina').addClass("hidden");
                $('#divExitoElimina').removeClass("hidden");
                setTimeout(() => { location.reload(); }, 2000);
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar el usuario seleccionado.' + data);
        }
    });
}