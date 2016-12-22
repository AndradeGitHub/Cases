function formatar(src, mask) {
    var tecla = (window.event) ? event.keyCode : e.which;    
    if (tecla < 48 || tecla > 58)
        return false;

    var i = src.value.length;
    var saida = mask.substring(0,1);
    var texto = mask.substring(i)
    
    if (texto.substring(0,1) != saida)  
        src.value += texto.substring(0,1);
}

function somenteNum() {
    var tecla = (window.event) ? event.keyCode : e.which;    

    if (tecla >= 45 && tecla <= 46)
        return true;

    if (tecla < 48 || tecla > 58) 
        return false;
}