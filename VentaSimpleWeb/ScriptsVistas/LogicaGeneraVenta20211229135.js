var _idContribuyente = [];

$(document).ready(function () {

    ObtenerTiposPago();
 
});
function ObtenerContribuyente() {
    var rut = $('#txtRut').val();

    $.ajax({
        url: window.urlObtenerContribuyente,
        type: 'POST',
        data: { rut: rut },
        success: function (data) {

            if (data != "noEncontrado") {
                $('#txtRut').val(data.Rut);
                $('#txtContribuyente').val(data.Razon_Social);
                $('#divtxtContribuyente').addClass('disabled');
            }
            else
            {
                $('#txtRut').val(rut);
                $('#txtContribuyente').val("");
                $('#divtxtContribuyente').removeClass('disabled');
            }
            _idContribuyente = data.Id;
        },
        
        error: function () {

        }
    });
}
function GuardarFactura() {

    if (ValidaGuardar() === false) {
        //alert('no valido');
        return;
    }

    $('#btnGuardarBoleta').addClass('loading');
    $('#btnGuardarBoleta').addClass('disabled');

    var strParams = {
        Contribuyente: $('#txtContribuyente').val(),
        Rut: $('#txtRut').val(),
        ContId: _idContribuyente,
        Cantidad: $('#txtCantidad').val(),
        Glosa: $('#txtDetalle').val(),
        Total: $('#txtValor').val(),
        Tipa_Id: $('#cmbTipoPago').val(),
    };

    $.ajax({
        url: window.urlInsertarFactura,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data != 'error') {
                $('#DivMessajeErrorGeneral').addClass("hidden");
                $('#divExito').removeClass("hidden");
                LimpiarCampos();
                setTimeout(() => { window.open(data, "_blank"); }, 2000);
            }
            if (data === 'error') {
                $('#divError').removeClass("hidden");
                $('#btnGuardarBoleta').removeClass('loading');
                $('#btnGuardarBoleta').removeClass('disabled');
            }
        },
        
        //error: function (ex) {
        //    alert('Error al guardar el producto');
        //}
    });

}

function LimpiarCampos() {
    document.getElementById("txtRut").value = "";
    document.getElementById("txtContribuyente").value = "";
    document.getElementById("txtDetalle").value = "";
    document.getElementById("txtValor").value = "";
    $('#btnGuardarBoleta').removeClass('loading');
    $('#btnGuardarBoleta').removeClass('disabled');
    $('#divExito').addClass("hidden");
    /*window.location.href("/GeneraVenta");*/
}

function LimpiaEstilos() {
    //Limpio el estilo Error antes de validar
    $('#divtxtRut').removeClass("error");
    $('#divtxtContribuyente').removeClass("error");
    $('#divtxtDetalle').removeClass("error");
    $('#divcmbTipoPago').removeClass("error");
    $('#divtxtCantidad').removeClass("error");
    $('#divtxtValor').removeClass("error");
}
function ValidaGuardar() {
    var esValido = true;
    var errores = [];

    LimpiaEstilos();

    if ($('#txtRut').val() === '') {
        $('#divtxtRut').addClass("error");
        errores.push('Debe indicar el rut');
    }

    if ($('#txtContribuyente').val() === '') {
        $('#divtxtContribuyente').addClass("error");
        errores.push('Debe indicar el nombre completo');
    }

    if ($('#txtDetalle').val() === '') {
        $('#divtxtDetalle').addClass("error");
        errores.push('Debe indicar un detalle');
    }

    if ($('#cmbTipoPago').val() < 1) {
        $('#divcmbTipoPago').addClass("error");
        errores.push('Debe seleccionar al menos un tipo de pago');
    }

    if ($('#txtCantidad').val() === '') {
        $('#divtxtCantidad').addClass("error");
        errores.push('Debe indicar una cantidad');
    }

    if ($('#txtValor').val() === '') {
        $('#divtxtValor').addClass("error");
        errores.push('Debe indicar un valor');
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

function ObtenerTiposPago() {

    $.ajax({
        url: window.urlObtenerTiposPago,
        type: 'POST',
        success: function (data) {
            $('#cmbTipoPago').dropdown('clear');
            $('#cmbTipoPago').empty();
            $('#cmbTipoPago').append('<option value="-1">[Seleccione tipo de pago]</option>');
            $.each(data,
                function (value, item) {
                    var texto = '<option value="' + item.Id + '">' + item.Tipo_Pago + '</option>';
                    $('#cmbTipoPago').append(texto);
                    
                }
            );

        },
        error: function () {
            alert('Error al cargar los tipos de pago');
        }
    });

}