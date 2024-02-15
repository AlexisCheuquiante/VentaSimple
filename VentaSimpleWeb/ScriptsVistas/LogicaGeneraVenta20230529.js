var _idContribuyente = [];
var _arrayPrestaciones = [];
var _idLineaProducto = 1;
var _arrayProductosAgregados = [];

$(document).ready(function () {

    ObtenerTiposPago();
    ObtenerPrestaciones();
    $('#cmbPrestaciones').change(function () {
        var idPrestacion = $('#cmbPrestaciones').val();

        $.each(_arrayPrestaciones, function (value, item) {

            if (item.Id == idPrestacion) {

                $('#txtValorPrestacion').val(item.Valor);
                
            }
        });

    });
 
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
                window.open(data, "_blank");
                setTimeout(() => { window.location.href = '/GeneraVenta' }, 2000);
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
function GuardarFactura_V2() {

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
        Pres_Id: $('#cmbPrestaciones').val(),
        PrestacionStr: $('#cmbPrestaciones').dropdown('get text'),
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
                window.open(data, "_blank");
                setTimeout(() => { window.location.href = '/GeneraVenta' }, 2000);
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
function GuardarFacturaRapida() {


    $('#btnGuardarBoletaRapido').addClass('loading');
    $('#btnGuardarBoletaRapido').addClass('disabled');

    var strParams = {
        Cantidad: $('#txtCantidad').val(),
        
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
                window.open(data, "_blank");
                setTimeout(() => { window.location.href = '/GeneraVenta' }, 2000);
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
    //document.getElementById("txtRut").value = "";
    //document.getElementById("txtContribuyente").value = "";
    //document.getElementById("txtDetalle").value = "";
    //document.getElementById("txtValor").value = "";
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
function ObtenerPrestaciones() {

    $.ajax({
        url: window.urlObtenerPrestaciones,
        type: 'POST',
        success: function (data) {
            $('#cmbPrestaciones').dropdown('clear');
            $('#cmbPrestaciones').empty();
            $('#cmbPrestaciones').append('<option value="0">[Seleccione establecimiento]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Descripcion + '</option>';
                    $('#cmbPrestaciones').append(texto);
                    _arrayPrestaciones.push(item);
                }
            );

        },
        error: function () {
            alert('Error al cargar los establecimientos');
        }
    });

}
function AgregarPrestacion() {

    if (ValidaAgregar() === false) {
        //alert('no valido');
        return;
    }

    var DetalleProducto = {
        IdLineaProducto: _idLineaProducto,
        Cantidad: 1,
        Id_Prestacion: $('#cmbPrestaciones').val(),
        DescripcionProducto: $('#cmbPrestaciones').dropdown('get text'),
        Valor: $('#txtValorPrestacion').val(),
    };

    _idLineaProducto++;

    $.ajax({
        url: window.urlAgregarPrestacion,
        type: 'POST',
        data: { entity: DetalleProducto },
        success: function (data) {
            _arrayProductosAgregados.push(DetalleProducto);
            document.getElementById("txtValorPrestacion").value = "";
            $('#cmbPrestaciones').dropdown('clear');
            ObtenerPrestaciones();
            $('#DivMessajeErrorGeneralAgregar').addClass("hidden");
            PintaGrid();
            //if (data === 'exito') {
            //    location.reload();
            //}
            //if (data === 'error') {
            //    $('#divErroLogin').removeClass("hidden");
            //}

        },
        error: function (ex) {
            alert('Error al agregar el producto');
        }
    });

}
function LimpiaEstilosAgregar() {
    //Limpio el estilo Error antes de validar
    $('#divcmbPrestaciones').removeClass("error");
    $('#divtxtValorPrestacion').removeClass("error");
}
function ValidaAgregar() {
    var esValido = true;
    var errores = [];

    LimpiaEstilosAgregar();

    if ($('#cmbPrestaciones').val() < 1) {
        $('#divcmbPrestaciones').addClass("error");
        errores.push('Debe seleccionar al menos una prestación para agregar');
    }
    if ($('#txtValorPrestacion').val() === '') {
        $('#divtxtValorPrestacion').addClass("error");
        errores.push('Debe indicar el valor de la prestación');
    }


    if (errores.length > 0) {
        var mensaje = '';
        $('#DivMessajeErrorGeneralAgregar').removeClass("hidden");

        for (i = 0; i < errores.length; i++) {
            mensaje += '<li>' + errores[i] + '</li>';
        }

        mensaje += '</ul>';
        $('#listMessajeErrorAgregar').empty();

        $('#listMessajeErrorAgregar').prepend(mensaje);

        // showMessage('#divMensajeNuevoCamion', 'danger', mensaje);
        return false;
    }
    else {
        return true;
    }


}
function PintaGrid() {
    var total = 0;
    var tabla = "<table class='ui celled table'>";
    tabla = tabla + "<thead><tr><th></th><th>Prestación</th><th>Valor</th></tr></thead>";
    tabla = tabla + "<tbody>";
    $.each(_arrayProductosAgregados, function (value, item) {
        tabla = tabla + "<tr>";
        tabla = tabla + "<td><button type='button' style='width:50px;' class='ui icon red button' onclick='QuitarPrestacion(" + item.IdLineaProducto + ",0);'><i class='trash icon'></i></button></td><td>" + item.DescripcionProducto + "</td><td>" + item.Valor + "</td>";
        /*<button class='ui icon red button' ><i class='trash alternate icon'></i></button>*/
            
        
        tabla = tabla + "</tr>";
        total = total + parseInt(item.Valor);
    });

    document.getElementById("txtValor").value = total;
    $('#grdDatos').html(tabla);
}
function QuitarPrestacion(id) {

    if (id > 0) {
        $.ajax({
            url: window.urlQuitarPrestacion,
            type: 'POST',
            data: { id: id },
            success: function (data) {
                if (data != null) {

                    _arrayProductosAgregados = data;
                    PintaGrid();
                }
            },
            error: function (data) {
                console.log(data);
                showMessage('body', 'danger', 'Ocurrió un error al eliminar la prestacion.' + data);
            }
        });
    }
}