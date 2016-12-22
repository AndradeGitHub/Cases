/****************************************************************************************************************
Geral
****************************************************************************************************************/
function fnConvertToDate(data) {
    var dtConvert = new Date(data.substring(6, 10),
                         data.substring(3, 5),
                         data.substring(0, 2));

    dtConvert.setMonth(dtConvert.getMonth() - 1);

    //var dia = dtConvert.getDate();
    //var mes = ("0" + (dtConvert.getMonth() + 1)).slice(-2);

    var dia = data.substring(0, 2);
    var mes = data.substring(3, 5);

    if ((dia < 1 || dia > 31) ||
        (mes < 1 || mes > 12))
        return "erro";
    else
        return dtConvert;
}

function fnValidaValores() {

    var inputs = document.getElementsByTagName("input");

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "text") {
            if (inputs[i].id.indexOf("vlLimiteMinPatrimonial") != -1 || inputs[i].id.indexOf("vlLimiteMinDiaria") != -1) {

                var objInput = "#" + inputs[i].id.toString();
                $(objInput).css("border", "0");

                var vlLimiteMin = inputs[i].value;
                var vlLimiteMax = inputs[i + 1].value;

                if (vlLimiteMin != "" && vlLimiteMax != "") {
                    if (parseFloat(vlLimiteMin) > parseFloat(vlLimiteMax)) {
                        return inputs[i];
                    }
                }
                else if (vlLimiteMin != "" && vlLimiteMax == "") {
                    if (parseFloat(vlLimiteMin) > 0) {
                        return inputs[i];
                    }
                }
            }
        }
    }

    return "true";
}

function fnEnterAtivo(e, fnSubmit) {
    if (e.keyCode == 13) {
        fnSubmit = replaceAll("|", "'", fnSubmit);
        
        eval(fnSubmit);
    }
}

function replaceAll(find, replace, str) {
    while (str.indexOf(find) > -1) 
    {
        str = str.replace(find, replace);
    }

    return str;
}

/****************************************************************************************************************
Autenticação
****************************************************************************************************************/
$("#btnLogIn").click(function () {
    if ($("#nomeLogin").val() == "") {
        $("#spanMensagem").html("* Informe o Username");
        $("#nomeLogin").css("borderColor", "#e80c4d");
        $("#nomeLogin").focus();
    }
    else if ($("#dsSenha").val() == "") {
        $("#spanMensagem").html("* Informe o Password");
        $("#dsSenha").css("borderColor", "#e80c4d");
        $("#dsSenha").focus();
    }
    else {
        $("#spanMensagem").html("");

        $("#formLogin").submit();
        //fnAjaxPost("/Autenticacao/Login", obj);
    }
});

/****************************************************************************************************************
 Limite Diário e Exceção
****************************************************************************************************************/
function fnValidaLimiteForm(acao, inDiarioExcecao) {    
    var dtIniVigencia = fnConvertToDate($("#dtInicial").val());
    var dtFimVigencia = fnConvertToDate($("#dtFinal").val());         

    if ($("#dtInicial").css("borderColor", "#e80c4d"))
        $("#dtInicial").css("borderColor", "#989a9b");

    if ($("#dtFinal").css("borderColor", "#e80c4d"))
        $("#dtFinal").css("borderColor", "#989a9b");

    if ($("#dtInicial").val() != "" && ($("#dtInicial").val().length < 10 || dtIniVigencia == "erro")) {
        $("#spanMensagem").html("* Informe a Data Inicial corretamente");
        $("#dtInicial").css("borderColor", "#e80c4d");
        $("#dtInicial").focus();
    }
    else if ($("#dtFinal").val() != "" && ($("#dtFinal").val().length < 10 || dtFimVigencia == "erro")) {
        $("#spanMensagem").html("* Informe a Data Final corretamente");
        $("#dtFinal").css("borderColor", "#e80c4d");
        $("#dtFinal").focus();
    }
    else if (($("#dtInicial").val() != "" && $("#dtFinal").val() != "") && (dtIniVigencia > dtFimVigencia)) {
        $("#spanMensagem").html("* A Data Inicial deve ser menor do que a Data Final");
        $("#dtInicial").css("borderColor", "#e80c4d");
        $("#dtInicial").focus();
    }
    else {
        $("#acao").attr("value", acao);

        $("#spanMensagem").html("");
        $("#btnSubmit").attr("value", "Aguarde...");            
        $('#btnSubmit').prop('disabled', true);        

        if (inDiarioExcecao == "diario")
            $("#formLimiteDiario").submit();
        else if (inDiarioExcecao == "excecao")
            $("#formLimiteExcecao").submit();
    }
}

function fnValidaLimiteOperacaoForm(acao, inDiarioExcecao) {
    var dtIniVigencia = fnConvertToDate($("#dtIniVigencia").val());
    var dtFimVigencia;    

    if ($("#dtIniVigencia").css("borderColor", "#e80c4d"))
        $("#dtIniVigencia").css("borderColor", "#989a9b");

    if (inDiarioExcecao != "excecao") {
        if ($("#dtFimVigencia").css("borderColor", "#e80c4d"))
            $("#dtFimVigencia").css("borderColor", "#989a9b");

        dtFimVigencia = fnConvertToDate($("#dtFimVigencia").val()); 
    }

    if ($("#dtIniVigencia").val() == "" || $("#dtIniVigencia").val().length < 10 || dtIniVigencia == "erro") {
        $("#spanMensagem").html("* Informe a Data Inicial corretamente");
        $("#dtIniVigencia").css("borderColor", "#e80c4d");
        $("#dtIniVigencia").focus();
    }
    else if ((inDiarioExcecao != "excecao") &&
              ($("#dtFimVigencia").val() == "" && $("#dtFimVigencia").val().length < 10 && dtFimVigencia == "erro")) {
        $("#spanMensagem").html("* Informe a Data Final corretamente");
        $("#dtFimVigencia").css("borderColor", "#e80c4d");
        $("#dtFimVigencia").focus();
    }
    else if ((inDiarioExcecao == "diario") && 
             ($("#dtIniVigencia").val() != "" && $("#dtFimVigencia").val() != "") && 
             (dtIniVigencia > dtFimVigencia)) {
        $("#spanMensagem").html("* A Data Inicial deve ser menor do que a Data Final");
        $("#dtIniVigencia").css("borderColor", "#e80c4d");
        $("#dtIniVigencia").focus();
    }
    else {
        $("#acao").attr("value", acao);

        var inputValidado = fnValidaValores();

        if ((acao == "incluir") || (acao == "alterar")) {            
            if (inputValidado == "true") {
                $("#spanMensagem").html("");
                $("#btnSubmit").attr("value", "Aguarde...");            
                $('#btnSubmit').prop('disabled', true);

                if (inDiarioExcecao == "diario")
                    $("#formLimiteDiarioOp").submit();
                else if (inDiarioExcecao == "excecao")
                    $("#formLimiteExcecaoOp").submit();                
            }
            else {
                $("#spanMensagem").html("* O Valor Inicial deve ser menor do que o Valor Final");

                if (acao == "incluir") {
                    var objInput = "#" + inputValidado.id.toString();

                    $(objInput).css("border", "solid 1px;");
                    $(objInput).css("border-color", "#e80c4d");
                    $(objInput).focus();
                }
            }  
        }
        else if (acao == "excluir") {
            $("#spanMensagem").html("");
            $("#btnExcluir").attr("value", "Aguarde...");            
            $('#btnExcluir').prop('disabled', true);            

            if (inDiarioExcecao == "diario")
                $("#formLimiteDiarioOp").submit();
            else if (inDiarioExcecao == "excecao")
                $("#formLimiteExcecaoOp").submit();   
        }                
    }    
}

/****************************************************************************************************************
Limite Mensal
****************************************************************************************************************/
function fnValidaForLimiteMes(acao) {        
    if ($("#dtInicial").css("borderColor", "#e80c4d"))
        $("#dtInicial").css("borderColor", "#989a9b");

    if ($("#dtInicial").val() != "" && $("#dtInicial").val().length < 7) {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtInicial").css("borderColor", "#e80c4d");
        $("#dtInicial").focus();
    }
    else {
        $("#acao").attr("value", acao);

        $("#btnSubmit").attr("value", "Aguarde...");
        $('#btnSubmit').prop('disabled', true);

        $("#spanMensagem").html("");
        $("#formLimiteMensal").submit();
    }
}

function fnValidaForLimiteMesOperacao(acao) {    
    if ($("#dtIniVigencia").css("borderColor", "#e80c4d"))
        $("#dtIniVigencia").css("borderColor", "#989a9b");

    if ($("#dtIniVigencia").val() == "" || $("#dtIniVigencia").val().length < 7) {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtIniVigencia").css("borderColor", "#e80c4d");
        $("#dtIniVigencia").focus();
    }
    else 
    {
        $("#acao").attr("value", acao);

        var inputValidado = fnValidaValores();

        if ((acao == "incluir") || (acao == "alterar")) {
            if (inputValidado == "true") {
                $("#btnSubmit").attr("value", "Aguarde...");
                $('#btnSubmit').prop('disabled', true);

                $("#spanMensagem").html("");
                $("#formLimiteMensalOp").submit();                
            }
            else {
                $("#spanMensagem").html("* O Valor Inicial deve ser menor do que o Valor Final");

                if (acao == "incluir") {
                    var objInput = "#" + inputValidado.id.toString();

                    $(objInput).css("border", "solid 1px;");
                    $(objInput).css("border-color", "#e80c4d");
                    $(objInput).focus();
                }
            }
        }
        else if (acao == "excluir") 
        {
            $("#btnExcluir").attr("value", "Aguarde...");
            $('#btnExcluir').prop('disabled', true);

            $("#spanMensagem").html("");
            $("#formLimiteMensalOp").submit();        
        }
    }
}

/****************************************************************************************************************
Enquadramento
****************************************************************************************************************/
function fnValidaEnquadramentoForm(acao, inDiarioMensal) {
    var dtResultado = fnConvertToDate($("#dtResultado").val());  

    if ($("#dtResultado").css("borderColor", "#e80c4d"))
        $("#dtResultado").css("borderColor", "#989a9b");

    if (inDiarioMensal == "diario") {
        if ($("#dtResultado").val() == "" || $("#dtResultado").val().length < 10 || dtResultado == "erro") {
            $("#spanMensagem").html("* Informe a Data corretamente");
            $("#dtResultado").css("borderColor", "#e80c4d");
            $("#dtResultado").focus();
        }
        else {
            $("#acao").attr("value", acao);

            $("#btnPesquisar").attr("value", "Aguarde...");
            $('#btnPesquisar').prop('disabled', true);

            $("#spanMensagem").html("");
            $("#formEnquadramentoDiario").submit();
        }
    }
    else if (inDiarioMensal == "mensal") {
        if ($("#dtResultado").val() == "" || $("#dtResultado").val().length < 7) {
            $("#spanMensagem").html("* Informe a Data corretamente");
            $("#dtResultado").css("borderColor", "#e80c4d");
            $("#dtResultado").focus();
        }
        else {
            $("#acao").attr("value", acao);

            $("#btnPesquisar").attr("value", "Aguarde...");
            $('#btnPesquisar').prop('disabled', true);

            $("#spanMensagem").html("");
            $("#formEnquadramentoMensal").submit();
        }
    }
}

/****************************************************************************************************************
Histórico
****************************************************************************************************************/
function fnValidaHistoricoOperacaoForm(acao) {
    var dataInicial = fnConvertToDate($("#dataInicial").val());
    var dataFinal = fnConvertToDate($("#dataFinal").val()); 

    if ($("#dataInicial").css("borderColor", "#e80c4d"))
        $("#dataInicial").css("borderColor", "#989a9b");

    if ($("#dataInicial").val() == "" || $("#dataInicial").val().length < 10 || dataInicial == "erro") {
        $("#spanMensagem").html("* Informe a Data Inicial corretamente");
        $("#dataInicial").css("borderColor", "#e80c4d");
        $("#dataInicial").focus();
    }
    else if ($("#dataFinal").val() == "" || $("#dataFinal").val().length < 10 || dataFinal == "erro") {
        $("#spanMensagem").html("* Informe a Data Final corretamente");
        $("#dataFinal").css("borderColor", "#e80c4d");
        $("#dataFinal").focus();
    }
    else {
        $("#acao").attr("value", acao);

        $("#btnPesquisar").attr("value", "Aguarde...");
        $('#btnPesquisar').prop('disabled', true);

        $("#spanMensagem").html("");
        $("#formHistoricoOperacao").submit();
    }        
}

function fnValidaHistoricoProcessamentoForm(acao) {
    var dtProcessada = fnConvertToDate($("#dtProcessada").val());       
    
    if ($("#dtProcessada").css("borderColor", "#e80c4d"))
        $("#dtProcessada").css("borderColor", "#989a9b");

    if ($("#dtProcessada").val() == "" || $("#dtProcessada").val().length < 10 || dtProcessada == "erro") {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtProcessada").css("borderColor", "#e80c4d");
        $("#dtProcessada").focus();
    }
    else {
        $("#acao").attr("value", acao);

        $("#btnPesquisar").attr("value", "Aguarde...");
        $('#btnPesquisar').prop('disabled', true);

        $("#spanMensagem").html("");
        $("#formHistoricoProcessamento").submit();
    }
}

/****************************************************************************************************************
Processamento Manual
****************************************************************************************************************/
function fnValidaProcessamentoManualForm(acao) {
    var dtResultado = fnConvertToDate($("#dtResultado").val());    

    if ($("#dtResultado").css("borderColor", "#e80c4d"))
        $("#dtResultado").css("borderColor", "#989a9b");

    if ($("#dtResultado").val() == "" || $("#dtResultado").val().length < 10 || dtResultado == "erro") {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtResultado").css("borderColor", "#e80c4d");
        $("#dtResultado").focus();
    }
    else 
    {
        var countCheks = 0;
        var chks = null;
        var checks = document.querySelectorAll("input[type=checkbox]");

        for (var i = 0; i < checks.length; i++) {
            if (checks[i].checked) {
                if (checks[i].id != "chkCarteiraTodos" && checks[i].id != "chkCarga") {
                    if (chks == null)
                        chks = checks[i].id;
                    else
                        chks = chks + "," + checks[i].id;

                    countCheks++;
                }
            }
        }

        if (chks == null)
            $("#spanMensagem").html("* Selecione uma carteira");            
        else {
            fnDesabilitaObjetos();

            //Se todas as carteiras forem selecionadas enviar em branco
            if ((checks.length - 1) == countCheks)
                $("#chkProcessar").attr("value", ""); 
            else
                $("#chkProcessar").attr("value", chks);                                     

            $("#divStatusProc").css("display", "block");            

            $("#acao").attr("value", acao); 
                
            $("#trBotao").css("display", "none");  
            $("#tdBotao").css("display", "none");  

            $("#trCarga").css("display", "block");
            $("#trProcessamento").css("display", "none");

            $("#tdStatusCarga").css("font-weight", "bold");
            $("#tdStatusCarga").html("Iniciando");                        

            $("#btnProcessar").attr("value", "Aguarde...");
            $('#btnProcessar').prop('disabled', true);

            $("#trProcessamento").css("display", "block");
            $("#tdStatusProcessamento").html("Aguardando");                           

            $("#spanMensagem").html("");
            $("#formProcessamentoManual").submit();
        }
    }
}

/****************************************************************************************************************
Carga
****************************************************************************************************************/
function fnValidaCargaForm() {
    var dtResultado = fnConvertToDate($("#dtResultado").val());

    if ($("#dtResultado").css("borderColor", "#e80c4d"))
        $("#dtResultado").css("borderColor", "#989a9b");

    if ($("#dtResultado").val() == "" || $("#dtResultado").val().length < 10 || dtResultado == "erro") {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtResultado").css("borderColor", "#e80c4d");
        $("#dtResultado").focus();
    }
    else {
        var countCheks = 0;
        var chks = null;
        var checks = document.querySelectorAll("input[type=checkbox]");

        for (var i = 0; i < checks.length; i++) {
            if (checks[i].checked) {
                if (checks[i].id != "chkCarteiraTodos" && checks[i].id != "chkCarga") {
                    if (chks == null)
                        chks = checks[i].id;
                    else
                        chks = chks + "," + checks[i].id;

                    countCheks++;
                }
            }
        }

        if (chks == null)
            $("#spanMensagem").html("* Selecione uma carteira");
        else {
            fnDesabilitaObjetos();

            //Se todas as carteiras forem selecionadas enviar em branco
            if ((checks.length - 1) == countCheks)
                $("#chkProcessar").attr("value", "");
            else
                $("#chkProcessar").attr("value", chks);

            $("#divStatusProc").css("display", "block");

            $("#acao").attr("value", "carga");

            $("#trBotao").css("display", "none");
            $("#tdBotao").css("display", "none");

            $("#trCarga").css("display", "block");            

            $("#tdStatusCarga").css("font-weight", "bold");
            $("#tdStatusCarga").html("Iniciando");

            $("#btnProcessar").attr("value", "Aguarde...");
            $('#btnProcessar').prop('disabled', true);

            $("#spanMensagem").html("");
            $("#formCargaGeral").submit();
        }
    }
}

function fnCheckAll(obj) {    
    var checks = document.querySelectorAll("input[type=checkbox]");
    for (var i = 0; i < checks.length; i++) {
        if (obj.checked) 
            checks[i].checked = true;        
        else 
            checks[i].checked = false;                    
    }
}

/****************************************************************************************************************
Resultado Processamento
****************************************************************************************************************/
function fnValidaProcessamentoResultadoForm(acao) {
    var dtResultadoPesq = fnConvertToDate($("#dtResultadoPesq").val());   

    if ($("#dtResultadoPesq").css("borderColor", "#e80c4d"))
        $("#dtResultadoPesq").css("borderColor", "#989a9b");

    if ($("#dtResultadoPesq").val() == "" || $("#dtResultadoPesq").val().length < 10 || dtResultadoPesq == "erro") {
        $("#spanMensagem").html("* Informe a Data corretamente");
        $("#dtResultadoPesq").css("borderColor", "#e80c4d");
        $("#dtResultadoPesq").focus();
    }
    else {        
        $("#acao").attr("value", acao);

        $("#btnPesquisar").attr("value", "Aguarde...");
        $('#btnPesquisar').prop('disabled', true);

        $("#spanMensagem").html("");
        $("#formProcessamentoResultado").submit();
    }
}

/****************************************************************************************************************/
function ajaxRequest() {
    var activexmodes = ["Msxml2.XMLHTTP", "Microsoft.XMLHTTP"]; //activeX versions to check for in IE
    if (window.ActiveXObject) { //Test for support for ActiveXObject in IE first (as XMLHttpRequest in IE7 is broken)
        for (var i = 0; i < activexmodes.length; i++) {
            try {
                return new ActiveXObject(activexmodes[i]);
            }
            catch (e) {
                //suppress error
            }
        }
    }
    else if (window.XMLHttpRequest) // if Mozilla, Safari etc
        return new XMLHttpRequest();
    else
        return false;
}

function fnAjaxPost(url, obj) {
    var mypostrequest = new ajaxRequest();

    mypostrequest.onreadystatechange = function () {
        if (mypostrequest.readyState == 4) {
            if (mypostrequest.status == 200 || window.location.href.indexOf("http") == -1) {
                //alert(mypostrequest.responseText);                

                //obj.form.submit();
                mypostrequest.send();
            }
            else {
                alert("An error has occured making the request");
            }
        }
    }

    /*var userId = encodeURIComponent(document.getElementById("userId").value);
    var password = encodeURIComponent(document.getElementById("password").value);
    var parameters = "userId=" + userId + "&password=" + password;*/
    
    mypostrequest.open("POST", url, true);
    //mypostrequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    mypostrequest.setRequestHeader("Content-type", "application / json");

    //mypostrequest.send(parameters);
    mypostrequest.send();
}

function fnAjaxGet() {
    var mygetrequest = new ajaxRequest();

    mygetrequest.onreadystatechange = function () {
        if (mygetrequest.readyState == 4) {
            if (mygetrequest.status == 200 || window.location.href.indexOf("http") == -1) {
                alert(mypostrequest.responseText);
            }
            else {
                alert("An error has occured making the request");
            }
        }
    }
    //var userId = encodeURIComponent(document.getElementById("userId").value);
    //var password = encodeURIComponent(document.getElementById("password").value);
    //mygetrequest.open("GET", "/Enquadramento/EnquadramentoDiario?" + userId + "&password=" + password, true);

    mygetrequest.open("GET", "/Historico/HistoricoProcessamento", true);
    
    mygetrequest.send(null);
}