function validarN() {
    let edad = document.getElementById("txtEdad")
    if (isNan(edad.value)) {
        alert('Debe ingresar numeros');
        edad.focus();
        return false;
    }
    return true;
}