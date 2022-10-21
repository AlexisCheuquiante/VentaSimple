var _id = 0;

$(document).ready(function () {

    ObtenerEstablecimientos();

});

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
function fnAdministrador(id) {
    var id = $('#idEsAdministrador').val();


    if (id === 'False') {
        $('#divcmbEstablecimiento').show();
    }
}
function GuardarUsuario() {

    if (ValidaGuardar() === false) {
        //alert('no valido');
        return;
    }

    $('#btnGuardarUsuario').addClass('loading');
    $('#btnGuardarUsuario').addClass('disabled');

    var strParams = {
        Id: _id,
        Est_Id: $('#cmbEstablecimiento').val(),
        Administrador: $('#idEsAdministrador').val(),
        Nombre: $('#txtNombreCompleto').val(),
        NombreUsuario: $('#txtUsuario').val(),
        Password: $('#txtContraseña').val(),
    };

    $.ajax({
        url: window.urlInsertarUsuario,
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
function LimpiaEstilos() {
    //Limpio el estilo Error antes de validar
    $('#divtxtNombreCompleto').removeClass("error");
    $('#divcmbEsAdministrador').removeClass("error");
    $('#divtxtUsuario').removeClass("error");
    $('#divtxtContraseña').removeClass("error");
}
function ValidaGuardar() {
    var esValido = true;
    var errores = [];

    LimpiaEstilos();

    if ($('#txtNombreCompleto').val() === '') {
        $('#divtxtNombreCompleto').addClass("error");
        errores.push('Debe indicar el nombre completo');
    }


    if ($('#idEsAdministrador').val() === '') {
        $('#divcmbEsAdministrador').addClass("error");
        errores.push('Debe indicar si es o no administrador');
    }

    if ($('#txtUsuario').val() === '') {
        $('#divtxtUsuario').addClass("error");
        errores.push('Debe indicar el usuario que se asignara');
    }

    if ($('#txtContraseña').val() === '') {
        $('#divtxtContraseña').addClass("error");
        errores.push('Debe indicar la contraseña del usuario');
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
function PreparaEliminarUsuario(id) {
    $('#idUsuarioSeleccionado').val(id);

}
function EliminaUsuario() {
    $('#btnEliminar').addClass('loading');
    $('#btnEliminar').addClass('disabled');

    id = $('#idUsuarioSeleccionado').val();
    $.ajax({
        url: window.urlEliminarUsuario,
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
function ObtenerUsuario(id) {
    $('#idUsuarioSeleccionado').val(id);

    id = $('#idUsuarioSeleccionado').val();
    $.ajax({
        url: window.urlObtenerUsuario,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            
            $('#txtNombreCompleto').val(data.Nombre);

            if (data.Administrador == true) {
                $('#divtxtAdmnistrador').show();
                $('#txtAdmnistrador').val("Si");
            }
            if (data.Administrador == false) {
                $('#divtxtAdmnistrador').show();
                $('#txtAdmnistrador').val("No");
            }
            $("#cmbEstablecimiento").dropdown('set selected', data.Est_Id);
            $('#txtUsuario').val(data.NombreUsuario)
            $('#txtContraseña').val(data.Password);
        },

        error: function () {
            alert('Error al cargar el usuario seleccionado');
        }
    });
    _id = id;
}